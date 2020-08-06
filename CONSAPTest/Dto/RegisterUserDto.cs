using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CONSAPTest.Dto
{
    public class RegisterUserDto
    {
        public string userName { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string phoneNumber { get; set; }

        public string RoleName { get; set; }
    }
}
