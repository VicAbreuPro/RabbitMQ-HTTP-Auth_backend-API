using System.Data;
using MySql.Data;

namespace API_ES.Data
{
    public class DataAccess{

        public static DataTable GetUsers(){

            DataTable dt = new();
            var sql_select = "SELECT * FROM auth.users";
            return dt;
        }
    }
}