using my8.Api.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Business
{
    public abstract class BaseBusiness
    {
        public readonly CurrentProcess _process;

        public BaseBusiness(CurrentProcess process)
        {
            _process = process;
        }
        public void AddError(string errorMessage, params object[] traceKeys)
        {
            _process.AddError(errorMessage, traceKeys);
        }
        public bool CheckIsNotLogin()
        {
            if (string.IsNullOrWhiteSpace(_process.PersonId))
            {
                AddError(ErrorRsx.error_login_expected);
                return true;
            }
            return false;
        }
    }
}
