using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Infrastructures
{
    public static class Utils
    {
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
    }
}
