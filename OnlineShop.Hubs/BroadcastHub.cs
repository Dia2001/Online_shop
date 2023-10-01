
using System.Collections.Concurrent;
using System.Drawing;
using Microsoft.AspNetCore.SignalR;

namespace OnlineShop.Hubs
{
    public class BroadcastHubBase : Hub<IHubClient>
    {
        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            string connectionId = Context.ConnectionId;
            var userIdentifier = Context.UserIdentifier;

            // TODO: what is this id?
            //var id = httpContext.Request.Query["id"];
            var userIdString = httpContext.Request.Query["userId"];
            if (!string.IsNullOrWhiteSpace(userIdString))
            {
                var userId = Convert.ToInt32(userIdString);
                SignalRConnectionManager.AddConnection(userId, connectionId);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var userIdString = httpContext.Request.Query["userId"];
            string connectionId = Context.ConnectionId;

            if (!string.IsNullOrWhiteSpace(userIdString))
            {
                var userId = Convert.ToInt32(userIdString);
                SignalRConnectionManager.RemoveConnection(userId, connectionId);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }

    public static class SignalRConnectionManager
    {
        private static ConcurrentDictionary<int, List<string>> Connections = new ConcurrentDictionary<int, List<string>>();

        public static void AddConnection(int userId, string connectionId)
        {
            SignalRConnectionManager.Connections.AddOrUpdate(userId, new List<string> { connectionId },
                    (key, existingValue) =>
                    {
                        if (!existingValue.Any(v => v == connectionId))
                        {
                            existingValue.Add(connectionId);
                        }

                        return existingValue;
                    });
        }

        public static void RemoveConnection(int userId, string connectionId)
        {
            SignalRConnectionManager.Connections.AddOrUpdate(userId, new List<string>(),
                    (key, existingValue) =>
                    {
                        if (existingValue.Any(v => v == connectionId))
                        {
                            existingValue.Remove(connectionId);
                        }

                        return existingValue;
                    });

            //if (SignalRConnectionManager.Connections[userId].Count == 0)
            //{
            //    var garbage = new List<string>();
            //    SignalRConnectionManager.Connections.TryRemove(userId, out garbage);
            //}
        }

        public static async void SendSystemNotification(List<int> userIds, IHubContext<BroadcastHubBase, IHubClient> _hubContext)
        {
            var connections = SignalRConnectionManager.Connections.Where(ob => userIds.Contains(ob.Key)).Select(ob => ob.Value).ToList();
            var connectionIds = new List<string>();
            foreach (var connection in connections)
            {
                connectionIds.AddRange(connection);
            }

            await _hubContext.Clients.Clients(connectionIds).SendSystemNotification();
        }

        public static async void SendSystemNotification(int currentUserId, IHubContext<BroadcastHubBase, IHubClient> _hubContext)
        {
            var connectionIds = new List<string>();
            var canGetValue = SignalRConnectionManager.Connections.TryGetValue(currentUserId, out connectionIds);

            if (canGetValue && connectionIds != null && connectionIds.Count != 0)
            {
                await _hubContext.Clients.Clients(connectionIds).SendSystemNotification();
            }
        }
    }
}
