using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace LaCeibaNetv4
{
    public class LiveHelpHub : Hub
    {
        public void SendMessage(string username, string message)
        {
            Clients.All.showMessage(username, message);
        }
    }
}