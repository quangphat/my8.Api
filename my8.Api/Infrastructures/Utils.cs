using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace my8.Api.Infrastructures
{
    public static class Utils
    {
        public const int LIMIT_ROW_COMMENT = 10;
        public const int LIMIT_FEED = 20;
        public static string ArrStrToMongoSearch(string[] searchs)
        {
            string temp = searchs.Aggregate("", (a, b) => a + $"/{b}/i,");
            if (string.IsNullOrWhiteSpace(temp)) return string.Empty;
            return temp.Remove(temp.LastIndexOf(','));
        }
        public static string ArrStrIdToMongoDbId(string[] Ids)
        {
            string temp = Ids.Aggregate("", (a, b) => a + $"ObjectId('{b}'),");
            if (string.IsNullOrWhiteSpace(temp)) return string.Empty;
            return temp.Remove(temp.LastIndexOf(','));
        }
        public static string[] IntersectOrUnion(List<HashSet<string>> lstHashSet)
        {
            HashSet<string> hashset = new HashSet<string>();
            foreach (HashSet<string> h in lstHashSet)
            {
                if (hashset.Count == 0)
                {
                    hashset.UnionWith(h);
                }
                if (h.Count == 0)
                {
                    hashset.UnionWith(h);
                }
                else
                {
                    hashset.IntersectWith(h);
                }
            }
            return hashset.ToArray();
        }
        public static string FormatCode(this string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return string.Empty;
            string temp = code.TrimStart().Trim().TrimEnd().ToLower();
            return temp;
        }
        public static string HmacSha256ToBase64(string originalData, string secretKey)
        {
            var hashed = string.Empty;

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
                hashed = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalData)));

            return hashed;
        }
        public static long GetUnixTime()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
