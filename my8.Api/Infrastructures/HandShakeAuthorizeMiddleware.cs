using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Infrastructures
{
    public class HandShakeAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public HandShakeAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;

            if (!path.HasValue)
            {
                httpContext.Response.StatusCode = 401;
                return;
            }

            if (path.StartsWithSegments("/handshake"))
            {

            }

            await _next(httpContext);
        }
    }
}
