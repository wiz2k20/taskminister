using System.Collections.Generic;
using System.Diagnostics;

using taskminister.musik.Entity;
using taskminister.musik.Interface;

namespace taskminister.musik.Service
{
    public class ServMusik : IServMusik
    {
        public IRepoMusik repoMusik { get; set; }
        public ServMusik(IRepoMusik _repoMusik) {
            this.repoMusik = _repoMusik;
        }

        public List<Information> SQLPlaylist()
        {
            var tmp = new List<Information>();

            tmp = repoMusik.SQLPlaylist();

            return tmp;
        }

    }
}
