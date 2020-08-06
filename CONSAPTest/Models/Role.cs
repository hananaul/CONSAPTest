using CONSAPTest.Helper;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CONSAPTest.Models
{
    public class Role
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        public string roleName { get; set; }
    }
}
