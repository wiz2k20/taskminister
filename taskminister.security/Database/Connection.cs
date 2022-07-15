using System.Configuration;
using MySql.Data.MySqlClient;

namespace taskminister.security.Database
{
    public class Connection : IConnection
    {
        public MySqlConnection keeper()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["keeper"].ConnectionString);
        }
    }
}
