using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CONSAPTest.Models.MySql
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string phoneNumber { get; set; }

        public int RoleId { get; set; }
    }
}
