using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.ISmartCenter
{
    public interface IFeedLikeSmart
    {
        Task<bool> Like(bool like);
    }
}
