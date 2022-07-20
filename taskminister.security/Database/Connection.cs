using MySql.Data.MySqlClient;
using System.Configuration;

namespace taskminister.security.Database
{
    public class Connection : IConnection
    {
        public MySqlConnection keeper()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["keeper"].ConnectionString);
        }

        public string keeperDependency()
        {
            return ConfigurationManager.ConnectionStrings["keeper"].ConnectionString;
        }

    }
}
