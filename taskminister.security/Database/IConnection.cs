using MySql.Data.MySqlClient;

namespace taskminister.security.Database
{
    public interface IConnection
    {
        MySqlConnection keeper();
    }
}
