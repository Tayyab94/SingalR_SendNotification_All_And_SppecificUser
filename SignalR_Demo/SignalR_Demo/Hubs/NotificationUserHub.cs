using Microsoft.AspNetCore.SignalR;
using SignalR_Demo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Demo.Hubs
{
    public class NotificationUserHub : Hub
    {

        private IUserConnectionManager _userConnectionManager;




        public NotificationUserHub(IUserConnectionManager userConnectionManager)
        {
            this._userConnectionManager = userConnectionManager;
        }

        public string GetConnectionId()
        {
            var httpConext = this.Context.GetHttpContext();
            var userId = httpConext.Request.Query["userId"];

            _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);

            return Context.ConnectionId;
        }

        //public async override  Task OnConnectedAsync()
        //{
        //    GetConnectionId();
        //}
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            // get Connection Id

            var connectionId = Context.ConnectionId;

            _userConnectionManager.RemoveUserConnection(connectionId);

            var value = await Task.FromResult(0);
        }
    }
}
