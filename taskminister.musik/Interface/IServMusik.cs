using System.Collections.Generic;

using taskminister.musik.Entity;

namespace taskminister.musik.Interface
{
    public interface IServMusik
    {
        List<Information> SQLPlaylist();
        
    }
}
