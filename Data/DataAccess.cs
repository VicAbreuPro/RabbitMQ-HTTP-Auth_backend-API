using System.Data;
using MySql.Data.MySqlClient;
using API_ES.Model;

namespace API_ES.Data
{
    public class DataAccess{

        public static int GetUser(string? name, string? password){

            int resp = 0;
            var sql_select = "SELECT COUNT(user_id) FROM auth.users WHERE user_name = @nameAux AND user_password = @passwordAux";

            if(name != null && password != null){
                try{
                    using(var connection = new MySqlConnection(Conn.strConn))
                    {
                        MySqlCommand sql_select_aux = new MySqlCommand(sql_select, connection);

                        sql_select_aux.Parameters.AddWithValue("@nameAux", name);
                        sql_select_aux.Parameters.AddWithValue("@passwordAux", password);

                        connection.Open();

                        resp = Convert.ToInt32(sql_select_aux.ExecuteScalar());
                    }
                }catch(Exception){
                    // Retunr empty Data Table
                    return resp;
                }
            }
            return resp;
        }

        public static int GetVhost(string? vhost){

            if( vhost != null && vhost == "Legion") return 1;
            return 0;
        }

        
        public static int GetResource(string? username, string? resource, string? permission){

            int resp = 0;
            var sql_inner_join = "SELECT usersresources.resource_id, usersresources.user_id, usersresources.permission_id FROM auth.usersresources INNER JOIN auth.resource ON usersresources.resource_id = resource.resource_id AND resource = @r_Aux INNER JOIN auth.users ON usersresources.user_id = users.user_id AND user_name = @userAux INNER JOIN auth.permissions ON usersresources.permission_id = permissions.permission_id AND permission = @p_Aux";
            
            Console.WriteLine( "Resource: "+ username);
            try{
                using(var connection = new MySqlConnection(Conn.strConn))
                {
                    MySqlCommand sql_main_command = new MySqlCommand(sql_inner_join, connection);

                    sql_main_command.Parameters.AddWithValue("@userAux", username);
                    sql_main_command.Parameters.AddWithValue("@r_Aux", resource);
                    sql_main_command.Parameters.AddWithValue("p_Aux", permission);

                    connection.Open();

                    resp = Convert.ToInt32(sql_main_command.ExecuteNonQuery());
                }
            }catch(Exception){
                Console.WriteLine("testeEx");
                // Retunr empty Data Table
                return resp;
            }
            Console.WriteLine("Resp: " + resp);
            return resp;
        }

        public static int GetTopic(string? username, string? name, string? permission, string? routing_key){
            
            DataTable dt = new();
            int resp = 1;

            var slq_inner_join ="SELECT userstopics.user_id, userstopics.topic_id, userstopics.permission_id FROM auth.userstopics INNER JOIN auth.users ON userstopics.user_id = users.user_id AND user_name = @userAux INNER JOIN auth.topic ON userstopics.topic_id = topic.topic_id AND topic_name = @topicAux AND routing_key = @keyAux INNER JOIN auth.permissions ON userstopics.permission_id = permissions.permission_id AND permission = @p_Aux";

            if(name != null && username != null && permission != null && routing_key != null){
                try{
                    using(var connection = new MySqlConnection(Conn.strConn))
                    {   
                        MySqlCommand sql_main_command = new MySqlCommand(slq_inner_join, connection);

                        sql_main_command.Parameters.AddWithValue("@userAux", username);
                        sql_main_command.Parameters.AddWithValue("@topicAux", name);
                        sql_main_command.Parameters.AddWithValue("@keyAux", routing_key);
                        sql_main_command.Parameters.AddWithValue("p_Aux", permission);

                        connection.Open();
                        
                        resp = Convert.ToInt32(sql_main_command.ExecuteScalar());
                    }
                }catch(Exception){
                    // Retunr empty value
                    return resp;
                }
            }
            //Console.WriteLine(res);
            return resp;
        }
    }
}