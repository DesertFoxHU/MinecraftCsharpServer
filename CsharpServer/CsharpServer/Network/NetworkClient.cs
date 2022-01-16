using CsharpServer.Game;

namespace CsharpServer.Network
{
    public enum PlayState
    {
        EMPTY,
        PING,
        LOGIN,
        PLAYING
    }

    public partial class NetworkClient
    {
        public static int dataBufferSize = 4096;

        public int id;
        public TCP tcp;
        public UDP udp;

        public PlayState state = PlayState.EMPTY;
        public long lastSentAlive = 0L;
        public Player player = null;

        public NetworkClient(int _clientId)
        {
            id = _clientId;
            tcp = new TCP(id);
            udp = new UDP(id);
        }

        public void Disconnect(string message = "{ip} has disconnected")
        {
            message = message.Replace("{ip}", "" + tcp.socket.Client.RemoteEndPoint);
            if(player != null) message = message.Replace("{username}", player.Username);

            Debug.Send(message);
            tcp.Disconnect();
            udp.Disconnect();

            Server.clients[id].state = PlayState.EMPTY;
            lastSentAlive = 0L;

            World.entities.Remove(player);
            player = null;
        }
    }
}
