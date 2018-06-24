using my8.Api.my8Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class ShortFeed
    {
        public string PersonId { get; set; }
        public Author PostBy { get; set; }
        public ActionAsType PostingAs { get; set; }
    }
}
