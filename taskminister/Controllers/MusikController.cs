using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using taskminister.musik.Interface;
using taskminister.security.Controllers;


namespace taskminister.Controllers
{
    public class MusikController : OneAboveAll
    {
        public IServMusik servMusik;
        public MusikController(IServMusik _servMusik)
        {
            servMusik = _servMusik;
        }

        public ActionResult MasterIndex() {
            return View();
        }

        [HttpPost]
        public void UploadMusik(HttpPostedFileBase file, string name, string artist)
        {
            servMusik.UploadMusikTask(file, name, artist);
        }

        //public string ListOfSongs()
        //{
        //    var result = servMusik.ListOfSongs();
        //    var retorno = JsonConvert.SerializeObject(result);
        //    return retorno;
        //}

        //[HttpGet]
        //public ActionResult GetPlaylist()
        //{
        //    var data = servMusik.SQLPlaylist();
        //    var ret = JsonConvert.SerializeObject(data);
        //    return Json(data);
        //}

        //public ActionResult PlaylistRemove(int id)
        //{
        //    var queryRemove = RepoSQL.PlaylistRemove();
        //    var result = SQLPlaylistRemove(queryRemove, id.ToString());

        //    return new ObjectResult(new { ajax = result });
        //}

    }
}