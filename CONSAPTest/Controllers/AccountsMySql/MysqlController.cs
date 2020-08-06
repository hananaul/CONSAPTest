using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CONSAPTest.DatabaseSettings;
using CONSAPTest.Models.MySql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CONSAPTest.Controllers.AccountsMySql
{
    [Route("api/[controller]")]
    [ApiController]
    public class MysqlController : ControllerBase
    {
        private MySqlDatabase _mySqlDatabase { get; set; }

        public MysqlController(MySqlDatabase mySqlDatabase)
        {
            _mySqlDatabase = mySqlDatabase;
        }


        [HttpGet("GetTest")]
        public User GetTest()
        {
            using (MySqlConnection conn = _mySqlDatabase.GetConnection())
            {
                List<User> listUser = new List<User>();

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listUser.Add(new User()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            userName = reader["userName"].ToString(),
                            email = reader["email"].ToString(),
                            phoneNumber = reader["phoneNumber"].ToString(),
                            RoleId = Convert.ToInt32(reader["RoleId"])

                        });
                    }
                }

                return listUser.FirstOrDefault();
            }
        }
    }
}