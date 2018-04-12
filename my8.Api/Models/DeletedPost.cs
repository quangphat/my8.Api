using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace my8.Api.Models
{
    public class DeletedPost:StatusPost
    {
        public DeletedPost() { }
        public int Applies { get; set; } //Số lượng apply
        public decimal MinSalary { get; set; } //Mức lương tối thiểu.
        public decimal MaxSalary { get; set; }
        public string Title { get; set; }
        public string EmailToReceiveApply { get; set; }//Gửi thông báo đến email khi có người apply
        public string FacebookToReceiveApply { get; set; }
    }
}

