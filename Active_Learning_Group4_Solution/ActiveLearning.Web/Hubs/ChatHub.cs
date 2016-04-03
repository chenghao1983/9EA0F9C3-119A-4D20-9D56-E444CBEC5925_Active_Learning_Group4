using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace ActiveLearning.Web.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            var userId = Context.QueryString["uid"];
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);

        }



        public override Task OnConnected()
        {
            //string lame = System.Web.HttpContext.Current.Session["Username"].ToString();
            string name = Context.User.Identity.Name;

            string connectionId = Context.ConnectionId;

            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }


    }
}