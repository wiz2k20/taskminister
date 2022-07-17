using Newtonsoft.Json;

using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;

using taskminister.security.Controllers;
using taskminister.musik.Interface;
using taskminister.musik.Service;

using MySql.Data.MySqlClient;
using taskminister.musik.Entity;
using System.Collections.Generic;
using taskminister.musik.Repository;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading.Tasks;
using System.Diagnostics;

namespace taskminister.Controllers
{
    //[Route("api/FileAPI/UploadFiles")]
    
    //public class Global
    //{
    //    //public static int conta = 10;
    //    public static int barStatus = 0;
    //    public static long barKB = 0;
    //    public static bool checkInsert = false;
    //    public static long totalKB = 0;
    //}

    public class MusikController : OneAboveAll
    {
        public IServMusik servMusik;

        public MusikController(IServMusik _servMusik) {
            servMusik = _servMusik;
        }

        public ActionResult MasterIndex() {
            return View();
        }

        //[HttpGet]
        public ActionResult GetPlaylist()
        {
            var data = servMusik.SQLPlaylist();
            var ret = JsonConvert.SerializeObject(data);
            return Json(data);
        }

        public string GetPlaylistInfo()
        {
            var result = servMusik.SQLPlaylist();
            var retorno = JsonConvert.SerializeObject(result);
            return retorno;
        }

        public List<Information> SQLPlaylist()
        {
            using (MySqlConnection c = new MySqlConnection(ConfigurationManager.ConnectionStrings["keeper"].ConnectionString))
            {
                c.Open();
                var d = c.CreateCommand();
                d.CommandText = RepoSQL.MusikSelectAsc();

                var reader = d.ExecuteReader();
                var dbresult = new List<Information>();
                while (reader.Read())
                {
                    dbresult.Add(new Information()
                    {
                        Control = Convert.ToInt32(reader["dbcontrol"]),
                        Name = reader["dbname"].ToString(),
                        Artist = reader["dbartist"].ToString(),
                        Url = reader["dburl"].ToString(),
                        Cover = reader["dbcover"].ToString(),
                    });
                }
                c.Close();
                return dbresult;
            }
        }

        //[HttpPost]
        //public ActionResult UploadFile(HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            string _FileName = Path.GetFileName(file.FileName);
        //            string _path = Path.Combine(Server.MapPath("~/Files/Songs"), _FileName);
        //            file.SaveAs(_path);
        //        }
        //        ViewBag.Message = "File Uploaded Successfully!!";
        //        return View();
        //    }
        //    catch
        //    {
        //        ViewBag.Message = "File upload failed!!";
        //        return View();
        //    }
        //}

        
        [HttpPost]
        public void UploadMusik(HttpPostedFileBase file, string name, string artist)
        {
            Debug.WriteLine("ENTROU NO MÉTODO __ " + DateTime.Now);

            servMusik.UploadMusikTask(file, name, artist);
        }

        //[HttpGet]
        //public ActionResult UploadMusik()
        //{
        //    return View();
        //}

        //public ActionResult PlaylistRemove(int id)
        //{
        //    var queryRemove = RepoSQL.PlaylistRemove();
        //    var result = SQLPlaylistRemove(queryRemove, id.ToString());

        //    return new ObjectResult(new { ajax = result });
        //}

        //[HttpPost]
        //public ActionResult Progress()
        //{
        //    // return this.Content(Startup.Progress.ToString());
        //    return new ObjectResult(new { status = Startup.barStatus.ToString(), kbytes = Startup.barKB.ToString() });
        //}

    }
}