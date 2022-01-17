using CsharpServer.PacketType;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace CsharpServer.Network
{
    public delegate void PacketHandler(int fromClient, ServerPacket packet);

    public static partial class Server
    {
        public static int maxPlayers;
        public static int port;
        private static TcpListener tcpListener;
        private static UdpClient udpListener;

        public static Dictionary<int, NetworkClient> clients = new Dictionary<int, NetworkClient>();

        public static Dictionary<int, PacketHandler> packetHandlers;

        public static void Start(int maxPlayers, int port)
        {
            Server.maxPlayers = maxPlayers;
            Server.port = port;

            Initaliaze();

            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(TCPConnectionCallback, null);

            udpListener = new UdpClient(port);
            udpListener.BeginReceive(UDPRecieveCallback, null);

            Debug.Send($"Starting server on {port}...");
        }

        private static void Initaliaze()
        {
            for (int i = 1; i <= maxPlayers; i++)
            {
                clients.Add(i, new NetworkClient(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { HandshakePacket.PacketID, ServerHandle.HandshakeRecieve },
                { PingPacket.PacketID, ServerHandle.PingRecieve },
                { KeepAliveServerPacket.PacketID, ServerHandle.KeepAliveRecieve }
            };
        }

        private static void Stop()
        {
            tcpListener.Stop();
            udpListener.Close();
        }
    }
}
