﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string WorkAs { get; set; }
        public string Company { get; set; }
        public double Rate { get; set; }//Đánh giá
        public string Avatar { get; set; }

        public string PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Nullable<int> EmailPrivacy { get; set; }
        public string Password { get; set; }
        public DateTime? Birthday { get; set; }
        public Nullable<int> BirthdayPrivacy { get; set; }
        public Nullable<int> WorkAsPrivacy { get; set; }
        public string BornIn { get; set; }
        public Nullable<int> BornInWorkAsPrivacy { get; set; }
        public string Hometown { get; set; }
        public Nullable<int> HometownWorkAsPrivacy { get; set; }
        public string LiveAt { get; set; }
        public Nullable<int> LiveAtWorkAsPrivacy { get; set; }
        public string University { get; set; }
        public Nullable<int> UniversityWorkAsPrivacy { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> PhoneWorkAsPrivacy { get; set; }
        public bool Gender { get; set; }
        public Nullable<int> GenderWorkAsPrivacy { get; set; }
        public string WorkEmail { get; set; }
        public Nullable<int> WorkEmailWorkAsPrivacy { get; set; }
        public string About { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}