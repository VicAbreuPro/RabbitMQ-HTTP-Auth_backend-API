
namespace API_ES.Data
{
    public class Conn
    {
        // Variáveis Auxiliares para utilização dos dados de acesso ao servidor
        private const string servidor = "myvnadb.mysql.database.azure.com";
        private const string port = "3306";
        private const string schemaAuth = "auth";
        private const string usuario = "jynx";
        private const string senha = "Hummerh14x4rally";

        static public string strConn = $"server={servidor};port={port};User Id={usuario};database={schemaAuth};password={senha}";
    }
}