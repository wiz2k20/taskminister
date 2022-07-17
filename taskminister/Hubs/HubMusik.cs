using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using taskminister.musik.Interface;

namespace taskminister.Hubs
{
    public class HubMusik : Hub
    {
        public IServMusik servMusik;
        public HubMusik(IServMusik _servMusik) {
            servMusik = _servMusik;
        }

        public void MusikUpload()
        {
            Clients.All.progressbarbegin();

            while (servMusik.TaskInformation() == false) {
                //Clients.All.progresso(servMusik.TaskProgresso());
                //Clients.All.size(servMusik.TaskSize());
                Clients.All.progressbarupdate(servMusik.TaskProgresso(), servMusik.TaskSize());
            }
            //Clients.All.finalizada();
            Clients.All.progressbarend();
        }

    }
    //Debug.WriteLine("isCompleted " + servMusik.TaskInformation());
}