using Microsoft.AspNetCore.Http;
using my8.Api.IBusiness;
using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace my8.Api.Infrastructures
{
    public class ClientAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public ClientAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        private readonly static List<string> _listPathByPass = new List<string>()
        {
            "/favicon.ico",
            "/lib/",
            "/handshake"
        };
        public async Task Invoke(HttpContext httpContext, IClientAuthorizeBusiness bizAuthorize)
        {
            var path = httpContext.Request.Path;

            if (!path.HasValue)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            if (!_listPathByPass.Any(a => path.Value.StartsWith(a)))
            {
                var headers = ClientAuthorizeModel.Create(
                    httpContext.Request.Headers["X-my8-Key"].FirstOrDefault(),
                    httpContext.Request.Headers["X-my8-Signature"].FirstOrDefault());
                var x = httpContext.Request.Headers["X-my8-userId"].FirstOrDefault();
                if (!headers.IsValid)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }

                var client = await _getClient(headers, bizAuthorize);
                if (client == null)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
                var originalData = _getOriginalDataToHash(httpContext);

                var checksum = string.Empty;

                var segments = path.Value.Split('/');
                checksum = Utils.HmacSha256ToBase64(client.ApiKey + originalData, client.SecretKey);

                if (checksum != headers.Signature)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }

            await _next(httpContext);
        }

        private async Task<ClientAuthorize> _getClient(ClientAuthorizeModel headers, IClientAuthorizeBusiness bizAuthorize)
        {
            var client = await bizAuthorize.Get(headers.ClientKey);
            return client;
        }
        private class ClientAuthorizeModel
        {
            public static ClientAuthorizeModel Create(string dataKey, string dataSignature)
            {
                return new ClientAuthorizeModel()
                {
                    ClientKey = dataKey,
                    Signature = dataSignature,
                    IsValid = !string.IsNullOrEmpty(dataKey) && !string.IsNullOrEmpty(dataSignature)
                };
            }

            public string ClientKey { get; set; }
            public string Signature { get; set; }
            public bool IsValid { get; set; }
        }
        private string _getOriginalDataToHash(HttpContext httpContext)
        {
            var originalData = string.Empty;

            if (httpContext.Request.Method == HttpMethod.Get.Method)
            {
                var list = new List<string>();

                if (httpContext.Request.Query.Count > 0)
                    foreach (var q in httpContext.Request.Query)
                        list.Add(WebUtility.UrlEncode(q.Value));

                if (list.Count > 0)
                    originalData = string.Join(string.Empty, list);
            }
            else
                using (var reader = new StreamReader(httpContext.Request.Body))
                {
                    var body = string.Empty;
                    try
                    {
                        body = reader.ReadToEnd();
                    }
                   catch(Exception e)
                    {

                    }

                    if (body == null)
                        body = string.Empty;
                    else
                        originalData = body;

                    httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                }

            return originalData;
        }
    }
}
