using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Infrastructures
{
    public class Errors
    {
        private static readonly Dictionary<string, string> List = new Dictionary<string, string>();
        const string ERR_SYSTEM_ERROR_CAN_NOT_BE_DETECTED = "ERR_SYSTEM_ERROR_CAN_NOT_BE_DETECTED";
        public static string Get(string errorCode)
        {
            return List.ContainsKey(errorCode) ? List[errorCode] : List[ERR_SYSTEM_ERROR_CAN_NOT_BE_DETECTED];
        }
        public static void Init()
        {
        }
    }
}
