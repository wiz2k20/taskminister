using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

using Newtonsoft.Json;

using taskminister.musik.Interface;

namespace taskminister.Hubs
{
    public class HubMusik : Hub
    {
        #region di
        public IServMusik servMusik;
        public HubMusik(IServMusik _servMusik) {
            servMusik = _servMusik;
        }
        #endregion

        public void MusikUpload()
        {
            Clients.All.progressbarbegin();
            while (servMusik.TaskInformation() == false)
            {
                Clients.All.progressbarupdate(servMusik.TaskProgresso(), servMusik.TaskSize());
            }
            Clients.All.progressbarend();
        }

        public void ListOfSongs()
        {
            var result = servMusik.ListOfSongs();
            var retorno = JsonConvert.SerializeObject(result);

            Clients.All.showlistofsongs(retorno);
        }


        //public static void Show()
        //{
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<HubMusik>();
        //    context.Clients.All.displayCustomer();
        //}

    }
    //Debug.WriteLine("isCompleted " + servMusik.TaskInformation());
}