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
    public class Global
    {
        //public static int conta = 10;
        public static int barStatus = 0;
        public static long barKB = 0;
        public static bool checkInsert = false;
        public static long totalKB = 0;
    }

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

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Files/Songs"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

        
        [HttpPost]
        public void UploadMusik(HttpPostedFileBase file, string xName, string xArtist)
        {
            Debug.WriteLine("ENTROU NO MÉTODO");

            Global.barStatus = 0;
            Global.barKB = 0;
            Global.checkInsert = false;
            Global.totalKB = 0;

            if (file != null)
            {
                var urlBeginSong = "../../songs/";
                var urlBeginCover = "../../covers/";
                var onlyExt = Path.GetExtension(file.FileName);

                if (onlyExt == ".mp3")
                {
                    var noExt = Path.GetFileNameWithoutExtension(file.FileName);
                    noExt = Regex.Replace(noExt, @"[^\w]", "_");
                    var fileFullName = noExt + onlyExt;

                    var songUrl = urlBeginSong + fileFullName;
                    var coverUrl = urlBeginCover + "cover.jpg";

                    long totalBytes = file.ContentLength;
                    Global.totalKB = totalBytes / 1024;

                    byte[] buffer = new byte[16 * 1024];
                    using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(fileFullName)))
                    {
                        using (Stream input = file.InputStream)
                        {
                            long totalReadBytes = 0;
                            int readBytes;

                            while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                output.WriteAsync(buffer, 0, readBytes);
                                totalReadBytes += readBytes;
                                Global.barKB = totalReadBytes / 1024;
                                Debug.WriteLine("Global.barKB: " + Global.barKB);
                                Global.barStatus = (int)((float)totalReadBytes / (float)totalBytes * 100.0);
                                Debug.WriteLine("Global.barStatus: " + Global.barStatus);
                                Task.Delay(1000);
                            }
                        }
                    }
                    //var queryPlayInsert = RepoSQL.PlaylistInsert();
                    //Global.checkInsert = SQLPlaylistInsert(queryPlayInsert, xname, xartist, songUrl, coverUrl);

                } //end if
            } //end if

            //return Json(new { dbInsert = Global.checkInsert, infoSize = Global.totalKB });
        }

        private string GetPathAndFilename(string filename)
        {
            // D:\_ARQUIVOS_MEGA\___DEVELOPER\_taskminister\taskminister\taskminister\Content
            string path = HostingEnvironment.ApplicationPhysicalPath + "\\Files\\Songs\\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path + filename;
        }

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