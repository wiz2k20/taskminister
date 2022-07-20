using System.Collections.Generic;

using taskminister.musik.Entity;


namespace taskminister.musik.Interface
{
    public interface IRepoMusik
    {
        List<Information> ListOfSongs();
    }
}
