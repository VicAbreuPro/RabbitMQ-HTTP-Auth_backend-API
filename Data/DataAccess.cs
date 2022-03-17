using System.Data;
using MySql.Data.MySqlClient;
using API_ES.Model;

namespace API_ES.Data
{
    public class DataAccess{
        
        // Verify user in database with username and password values
        public static int GetUser(string? name, string? password){

            // Auxiliary Variable
            int resp = 0;

            // Sql statement to verify user with select count, if return any value that is not null it means that user with input parameters exist
            var sql_select = "SELECT COUNT(user_id) FROM auth.users WHERE user_name = @nameAux AND user_password = @passwordAux";

            // Verify if the content of input variables is null
            if(name != null && password != null){
                try{

                    // Create new connection instance to MySql Database
                    using(var connection = new MySqlConnection(Conn.strConn))
                    {
                        // Create a new MySql command with auxiliary string that contain SQL statement
                        MySqlCommand sql_select_aux = new MySqlCommand(sql_select, connection);

                        // Change values in SQL command with input parameters
                        sql_select_aux.Parameters.AddWithValue("@nameAux", name);
                        sql_select_aux.Parameters.AddWithValue("@passwordAux", password);

                        // Open connection
                        connection.Open();

                        // Assign the value of first row and first collum, if any value diferent than null is returned, the validation is true
                        resp = Convert.ToInt32(sql_select_aux.ExecuteScalar());

                        // Test verification
                        Console.WriteLine("User Verification -> Username: " + name + " | Password: " + password + " | Resp: " + resp);
                    }
                }catch(Exception){
                    // Return empty Data Table
                    return resp;
                }
            }
            // Return query value
            return resp;
        }

        // Verify the request virtual host access
        public static int GetVhost(string? vhost){

            if( vhost != null && vhost == "Legion") return 1;
            return 0;
        }

        // Verify user resources in requested virtual host
        public static int GetResource(string? username, string? resource, string? permission){

            // Auxiliary Variable
            int resp = 0;

            // Sql statement to verify user resources in requested virtual host and respective permissions
            var sql_inner_join = "SELECT usersresources.resource_id, usersresources.user_id, usersresources.permission_id FROM auth.usersresources INNER JOIN auth.resource ON usersresources.resource_id = resource.resource_id AND resource = @r_Aux INNER JOIN auth.users ON usersresources.user_id = users.user_id AND user_name = @userAux INNER JOIN auth.permissions ON usersresources.permission_id = permissions.permission_id AND permission = @p_Aux";
            
            try{
                // Create new connection instance to MySql Database
                using(var connection = new MySqlConnection(Conn.strConn))
                {
                    // Create a new MySql command with auxiliary string that contain SQL statement
                    MySqlCommand sql_main_command = new MySqlCommand(sql_inner_join, connection);

                    // Change values in SQL command with input parameters
                    sql_main_command.Parameters.AddWithValue("@userAux", username);
                    sql_main_command.Parameters.AddWithValue("@r_Aux", resource);
                    sql_main_command.Parameters.AddWithValue("p_Aux", permission);

                    // Open connection
                    connection.Open();

                    // Assign the value of first row and first collum, if any value diferent than null is returned, the validation is true
                    resp = Convert.ToInt32(sql_main_command.ExecuteScalar());

                    // Test verification
                    Console.WriteLine( "Resource verification -> Username: "+ username + " | Resource: " + resource + " | Permission: " + permission + " | Resp: " + resp);
                }
            }catch(Exception){
                // Return empty Data Table
                return resp;
            }
            // Return query value
            return resp;
        }
        
        // Verify Requested topic access and routing_key to queue
        public static int GetTopic(string? username, string? name, string? permission, string? routing_key){
            
            // Auxiliary Variable
            int resp = 1;

            // Sql statement to verify if user have access to requested topic to request queue with routing_key input and which permission granted
            var slq_inner_join ="SELECT userstopics.user_id, userstopics.topic_id, userstopics.permission_id FROM auth.userstopics INNER JOIN auth.users ON userstopics.user_id = users.user_id AND user_name = @userAux INNER JOIN auth.topic ON userstopics.topic_id = topic.topic_id AND topic_name = @topicAux AND routing_key = @keyAux INNER JOIN auth.permissions ON userstopics.permission_id = permissions.permission_id AND permission = @p_Aux";

            // Verify if the content of input variables is null
            if(name != null && username != null && permission != null && routing_key != null){
                try{
                    // Create new connection instance to MySql Database
                    using(var connection = new MySqlConnection(Conn.strConn))
                    {   
                        // Create a new MySql command with auxiliary string that contain SQL statement
                        MySqlCommand sql_main_command = new MySqlCommand(slq_inner_join, connection);

                        // Change values in SQL command with input parameters
                        sql_main_command.Parameters.AddWithValue("@userAux", username);
                        sql_main_command.Parameters.AddWithValue("@topicAux", name);
                        sql_main_command.Parameters.AddWithValue("@keyAux", routing_key);
                        sql_main_command.Parameters.AddWithValue("p_Aux", permission);

                        // Open connection
                        connection.Open();
                        
                        // Assign the value of first row and first collum, if any value diferent than null is returned, the validation is true
                        resp = Convert.ToInt32(sql_main_command.ExecuteScalar());

                        // Test verification
                        Console.WriteLine( "Topic Verification -> Username: "+ username + " | Topic Name: " + name + "  | Routing Key: " + routing_key + " | Permission: " + permission);
                    }
                }catch(Exception){
                    // Return empty value
                    return resp;
                }
            }
            // Return query value
            return resp;
        }
    }
}