using my8.Api.Infrastructures;
namespace my8.Api.SmartCenter
{
    public class BaseSmart
    {
        public readonly CurrentProcess _process;

        public BaseSmart(CurrentProcess process)
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
