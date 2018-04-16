using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.SmartCenter
{
    public interface ISmartCenter
    {
        Task<List<PostAllType>> Gets(string personId,int skip);
    }
}
