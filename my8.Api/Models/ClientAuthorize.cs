using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class ClientAuthorize
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string ApiKey { get; set; }
		public string SecretKey { get; set; }
		public bool Active { get; set; }

    }
}

