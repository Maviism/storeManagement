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
        //Set Data Source to DB Server name
        //Set Initial Catalog to DB name
        public DBHelper()
        {
            Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StoreManagement;Integrated Security = True;");
        }
        public SqlConnection Connection { get; set; }
    }
}
