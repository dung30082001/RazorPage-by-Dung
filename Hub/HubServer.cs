using Microsoft.AspNetCore.SignalR;
namespace MyRazorPage.Hubs
{
    public class HubServer : Hub
    {
        public void HasNewData()
        {
            Clients.All.SendAsync("ReloadProduct");
        }
    }
}
