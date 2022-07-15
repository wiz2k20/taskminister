using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace taskminister.Hubs
{
    public class Global
    {
        public static int conta = 10;
    }

    [HubName("musikHub")]
    public class HubMusik : Hub
    {
        [HubMethodName("musikUpload")]
        public void UploadTask()
        {
            taskagoraOne();
        }

        public void taskagoraOne()
        {
            var task = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Global.conta = i;
                    Clients.All.progresso(Global.conta);
                    Thread.Sleep(1000);
                }
                Clients.All.finalizada();
            });
        }

        //[HubMethodName("retornastatus")]
        //public void retornaTarefa()
        //{
        //    taskagoraOne();
        //}

    }
}