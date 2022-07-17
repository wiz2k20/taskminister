using Microsoft.AspNet.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using taskminister.musik.Interface;

namespace taskminister.Hubs
{
    public class LivingTribunal : Hub
    {
        public IServMusik servMusik;

        public LivingTribunal(IServMusik _servMusik) : base()
        {
            this.servMusik = _servMusik;
        }



        //public void Hello()
        //{
        //    Clients.All.hello();
        //}
    }
}