using my8.Api.Models;
using my8.Api.my8Enum;
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
        public static long GetUnixTime(DateTime inputDate)
        {
            return ((DateTimeOffset)inputDate).ToUnixTimeSeconds();
        }
        public static string GenerateNotifyCodeCount(string feedId,int feedType,int notifyType,int actionAuthorType)
        {
            return $"{feedId}_{feedType}_{notifyType}_{actionAuthorType}";
        }
        public static string GenerateNotifyCodeCount(Comment comment)
        {
            return GenerateNotifyCodeCount(comment.FeedId, (int)comment.FeedType, (int)NotifyType.Comment,comment.Commentator.AuthorTypeId);
        }
        public static string GenerateNotifyCodeCount(FeedLike feedlike)
        {
            return GenerateNotifyCodeCount(feedlike.FeedId, (int)feedlike.FeedType, (int)NotifyType.Like,feedlike.Author.AuthorTypeId);
        }
        public static string GenerateNotifyCodeExist(string feedId,int feedType, string notifyAuthorId, int notifyAuthorType)
        {
            return $"{feedId}_{feedType}_{notifyAuthorId}_{notifyAuthorType}";
        }
        public static string GenerateNotifyCodeExist(Notification notify)
        {
            return GenerateNotifyCodeExist(notify.FeedId, (int)notify.FeedType, notify.AuthorId, (int)notify.AuthorType);
        }
    }
}
