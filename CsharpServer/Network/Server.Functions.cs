using System.Collections.Generic;

namespace CsharpServer.Network
{
    public static partial class Server
    {
        public static List<NetworkClient> GetAliveClients()
        {
            List<NetworkClient> list = new List<NetworkClient>();
            for (int i = 1; i <= maxPlayers; i++)
            {
                if(clients[i].tcp.socket != null)
                {
                    list.Add(clients[i]);
                }
            }
            return list;
        }
    }
}
