
namespace taskminister.musik.Repository
{
    public class RepoSQL
    {
        public static string MusikSelectDesc()
        {
            return @"SELECT * FROM musik
                     ORDER BY dbcontrol DESC";
        }

        public static string MusikSelectAsc()
        {
            return @"SELECT * FROM taskmusik
                     ORDER BY dbcontrol ASC";
        }

        public static string MusikInsert()
        {
            return @"INSERT INTO musik (dbname, dbartist, dburl, dbcover)
                    VALUES (@songname, @songartist, @songurl, @songcover);";
        }

        public static string MusikDelete()
        {
            return @"DELETE FROM musik WHERE dbcontrol = @id;";
        }
    }
}
