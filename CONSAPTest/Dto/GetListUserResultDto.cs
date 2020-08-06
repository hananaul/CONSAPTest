using CONSAPTest.Helper;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CONSAPTest.Dto
{
    public class GetListUserResultDto
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId UserId { get; set; }

        public string userName { get; set; }

        public string email { get; set; }

        public string phoneNumber { get; set; }

        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId RoleId { get; set; }

        public string roleName { get; set; }
    }
}
