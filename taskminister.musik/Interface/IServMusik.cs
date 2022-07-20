using System.Collections.Generic;
using System.Web;

using taskminister.musik.Entity;

namespace taskminister.musik.Interface
{
    public interface IServMusik
    {
        List<Information> ListOfSongs();


        #region Upload
        void UploadMusikTask(HttpPostedFileBase file, string name, string artist);
        int TaskProgresso();
        bool TaskInformation();
        long TaskSize();
        #endregion

    }
}
