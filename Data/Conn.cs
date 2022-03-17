
namespace API_ES.Data
{
    public class Conn
    {
        // Variáveis Auxiliares para utilização dos dados de acesso ao servidor
        private const string servidor = "";
        private const string port = "";
        private const string schemaAuth = "";
        private const string usuario = "";
        private const string senha = "";

        static public string strConn = $"server={servidor};port={port};User Id={usuario};database={schemaAuth};password={senha}";
    }
}