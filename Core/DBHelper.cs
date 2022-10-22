using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storeManagement.Core
{
    internal class DBHelper
    {
        private string _dbSource;
        private string _dbName;

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=DB_testing;Integrated Security = True;");

        
    }
}
