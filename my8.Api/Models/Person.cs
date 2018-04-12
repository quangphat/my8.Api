using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class Person
    {
        public Person() { }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        [JsonProperty(PropertyName = "Id")]
        public string PersonId { get; set; }
        public string DisplayName { get; set; }
        public string WorkAs { get; set; }
        public string Company { get; set; }
        public double Rate { get; set; }//Đánh giá
        public string Avatar { get; set; }
        public string Url { get; set; }
        [BsonIgnore]
        public string Firstname { get; set; }
        [BsonIgnore]
        public string Lastname { get; set; }
        [BsonIgnore]
        public string Email { get; set; }
        [BsonIgnore]
        public Nullable<int> EmailPrivacy { get; set; }
        [BsonIgnore]
        public string Password { get; set; }
        [BsonIgnore]
        public DateTime? Birthday { get; set; }
        [BsonIgnore]
        public Nullable<int> BirthdayPrivacy { get; set; }
        [BsonIgnore]
        public Nullable<int> WorkAsPrivacy { get; set; }
        [BsonIgnore]
        public string BornIn { get; set; }
        [BsonIgnore]
        public Nullable<int> BornInWorkAsPrivacy { get; set; }
        [BsonIgnore]
        public string Hometown { get; set; }
        [BsonIgnore]
        public Nullable<int> HometownWorkAsPrivacy { get; set; }
        [BsonIgnore]
        public string LiveAt { get; set; }
        [BsonIgnore]
        public Nullable<int> LiveAtWorkAsPrivacy { get; set; }
        [BsonIgnore]
        public string University { get; set; }
        [BsonIgnore]
        public Nullable<int> UniversityWorkAsPrivacy { get; set; }
        [BsonIgnore]
        public string PhoneNumber { get; set; }
        [BsonIgnore]
        public Nullable<int> PhoneWorkAsPrivacy { get; set; }
        [BsonIgnore]
        public bool Gender { get; set; }
        [BsonIgnore]
        public Nullable<int> GenderWorkAsPrivacy { get; set; }
        [BsonIgnore]
        public string WorkEmail { get; set; }
        [BsonIgnore]
        public Nullable<int> WorkEmailWorkAsPrivacy { get; set; }
        [BsonIgnore]
        public string About { get; set; }
        [BsonIgnore]
        public DateTime? CreatedTime { get; set; }
        [BsonIgnore]
        public DateTime? ModifiedTime { get; set; }
    }
}
