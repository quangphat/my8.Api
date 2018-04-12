using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class JobPost : StatusPost
    {
        public int Applies { get; set; } //Số lượng apply
        public decimal MinSalary { get; set; } //Mức lương tối thiểu.
        public decimal MaxSalary { get; set; }
        public string Title { get; set; }
        public string EmailToReceiveApply { get; set; }//Gửi thông báo đến email khi có người apply
        public string FacebookToReceiveApply { get; set; }
    }
}
