using CsharpServer.PacketType;

namespace CsharpServer
{
    public class ServerHandle
    {
        public static void HandshakeRecieve(int clientID, AbstractPacket packet)
        {
            Handshake hPacket = (Handshake)packet;
            Debug.Send($"Protocol Version: {hPacket.ProtocolVersion}", Debug.Mode.DEBUG);
            Debug.Send($"Server address: {hPacket.ServerAddress}", Debug.Mode.DEBUG);
            Debug.Send($"Server port: {hPacket.Port}", Debug.Mode.DEBUG);
            Debug.Send($"Status: {hPacket.Status}", Debug.Mode.DEBUG);
            Debug.Send("----------------------------------", Debug.Mode.DEBUG);

            if(hPacket.Status == 1)
            {
                ServerSend.SendJsonResponse(clientID);
            }
        }
    }
}
