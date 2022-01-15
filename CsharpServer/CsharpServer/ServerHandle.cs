using CsharpServer.PacketType;

namespace CsharpServer
{
    public class ServerHandle
    {
        public static void HandshakeRecieve(int clientID, ServerPacket packet)
        {
            HandshakePacket hPacket = (HandshakePacket)packet;
            Debug.Send($"Protocol Version: {hPacket.ProtocolVersion}", Debug.Mode.DEBUG);
            Debug.Send($"Server address: {hPacket.ServerAddress}", Debug.Mode.DEBUG);
            Debug.Send($"Server port: {hPacket.Port}", Debug.Mode.DEBUG);
            Debug.Send($"Status: {hPacket.Status}", Debug.Mode.DEBUG);
            Debug.Send("----------------------------------", Debug.Mode.DEBUG);

            if(hPacket.Status == 1)
            {
                Server.clients[clientID].state = PlayState.PING;
                ServerSend.SendJsonResponse(clientID);
            }
            else if(hPacket.Status == 2)
            {
                Server.clients[clientID].state = PlayState.LOGIN;
                Debug.Send($"{Server.clients[clientID].tcp.socket.Client.RemoteEndPoint} has trying to login!");
            }
        }

        public static void PingRecieve(int clientID, ServerPacket packet)
        {
            PingPacket pingPacket = (PingPacket)packet;
            Debug.Send($"Recieved long: {pingPacket.Payload}", Debug.Mode.DEBUG);
            ServerSend.SendPong(clientID, pingPacket.Payload);
        }

        public static void LoginStartRecieve(int clientID, ServerPacket packet)
        {
            LoginStartPacket loginPacket = (LoginStartPacket)packet;
            Debug.Send($"Recieved login attempt from {loginPacket.Username}");
            Server.clients[clientID].username = loginPacket.Username;
            ServerSend.SendLoginSuccess(clientID);
            Server.clients[clientID].state = PlayState.PLAYING;
        }

        public static void KeepAliveRecieve(int clientID, ServerPacket packet)
        {
            KeepAliveServerPacket keepPacket = (KeepAliveServerPacket)packet;

            Debug.Send($"Recieved keep alive long from client: {keepPacket.Value}", Debug.Mode.DEBUG);
            NetworkClient client = Server.clients[clientID];
            if (client.lastSentAlive == 0) return;

            /*if(client.lastSentAlive != keepPacket.Value)
            {
                Server.clients[clientID].Disconnect("{username} has kicked out of server!");
            }*/
        }
    }
}
