using System.Collections.Generic;
using System.Web;

using taskminister.musik.Entity;

namespace taskminister.musik.Interface
{
    public interface IServMusik
    {
        List<Information> SQLPlaylist();
        void UploadMusikTask(HttpPostedFileBase file, string name, string artist);
        int TaskProgresso();
        bool TaskInformation();
        long TaskSize();

    }
}
