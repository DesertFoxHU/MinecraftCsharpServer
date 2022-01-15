using CsharpServer.PacketType;
using Newtonsoft.Json;

namespace CsharpServer
{
    public class ServerSend
    {
        #region Sending data via packets
        /// <summary>Sends a packet to a client via TCP.</summary>
        /// <param name="_toClient">The client to send the packet the packet to.</param>
        /// <param name="_packet">The packet to send to the client.</param>
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        /// <summary>Sends a packet to a client via UDP.</summary>
        /// <param name="_toClient">The client to send the packet the packet to.</param>
        /// <param name="_packet">The packet to send to the client.</param>
        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        /// <summary>Sends a packet to all clients via TCP.</summary>
        /// <param name="_packet">The packet to send.</param>
        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.maxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via TCP.</summary>
        /// <param name="_exceptClient">The client to NOT send the data to.</param>
        /// <param name="_packet">The packet to send.</param>
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.maxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        /// <summary>Sends a packet to all clients via UDP.</summary>
        /// <param name="_packet">The packet to send.</param>
        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.maxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via UDP.</summary>
        /// <param name="_exceptClient">The client to NOT send the data to.</param>
        /// <param name="_packet">The packet to send.</param>
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.maxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }
        #endregion

        public static void SendJsonResponse(int clientID)
        {
            PingPayload pingLoad = new PingPayload()
            {
                Version = new Pingload.VersionPayload() { Protocol = 757, Name = "1.18.1" },
                Players = new Pingload.PlayersPayload() { Max = Server.maxPlayers, Online = 0 },
                Motd = "Server description",
                Icon = "data:image/png;base64,<data>"
            };

            Debug.Send("Sending Json response", Debug.Mode.DEBUG);
            using (Packet packet = new PingJsonResponsePacket(pingLoad).WrapPacket())
            {
                SendTCPData(clientID, packet);
            }
        }

        public static void SendPong(int clientID, long clientValue)
        {
            using(Packet packet = new PongPacket(clientValue).WrapPacket())
            {
                SendTCPData(clientID, packet);
            }
        }

        public static void SendLoginSuccess(int clientID)
        {
            using(Packet packet = new LoginSuccessPacket(System.Guid.Empty, Server.clients[clientID].username).WrapPacket())
            {
                SendTCPData(clientID, packet);
            }
            Debug.Send($"{Server.clients[clientID].username} has succesfully logined!");
        }
    }
}
