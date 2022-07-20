using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

using taskminister.musik.Entity;
using taskminister.musik.Interface;


namespace taskminister.musik.Service
{
    public class Global
    {
        public static int barStatus = 0;
        public static long barKB = 0;
        public static long totalKB = 0;
        public static string finalizada = string.Empty;
        public static Task taskInfo = null;
    }

    public class ServMusik : IServMusik
    {
        public IRepoMusik repoMusik { get; set; }
        public ServMusik(IRepoMusik _repoMusik) {
            this.repoMusik = _repoMusik;
        }

        public List<Information> ListOfSongs()
        {
            var tmp = new List<Information>();
            tmp = repoMusik.ListOfSongs();
            return tmp;
        }


        #region Upload Musik

        public void UploadMusikTask(HttpPostedFileBase file, string name, string artist)
        {
            if (file != null)
            {
                //var task = Task.Factory.StartNew(() =>
                Global.taskInfo = Task.Factory.StartNew(() =>
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
                                    //Debug.WriteLine("Global.barKB: " + Global.barKB);
                                    Global.barStatus = (int)((float)totalReadBytes / (float)totalBytes * 100.0);
                                    Debug.WriteLine("Global.barStatus: " + Global.barStatus);
                                    Thread.Sleep(100);
                                }
                            }
                        }
                        //var queryPlayInsert = RepoSQL.PlaylistInsert();
                        //Global.checkInsert = SQLPlaylistInsert(queryPlayInsert, xname, xartist, songUrl, coverUrl);
                    }
                });
            }
        }

        public bool TaskInformation()
        {
            if (Global.taskInfo != null) {
                return Global.taskInfo.IsCompleted;
            }
            return false;
        }
        public int TaskProgresso()
        {
            return Global.barStatus;
        }
        public long TaskSize()
        {
            return Global.barKB;
        }
        private string GetPathAndFilename(string filename)
        {
            string path = HostingEnvironment.ApplicationPhysicalPath + "\\Files\\Songs\\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path + filename;
        }

        #endregion

    }
}
