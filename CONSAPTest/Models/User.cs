using CONSAPTest.Helper;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CONSAPTest.Models
{
    public class User
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string phoneNumber { get; set; }

        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId RoleId { get; set; }
    }
}
