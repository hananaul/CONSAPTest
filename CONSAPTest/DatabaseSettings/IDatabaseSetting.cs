using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CONSAPTest.DatabaseSettings
{
    public interface IDatabaseSetting
    {
        string UsersCollectionName { get; set; }
        string RolesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
