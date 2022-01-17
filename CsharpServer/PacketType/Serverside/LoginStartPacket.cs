using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class LoginStartPacket : ServerPacket
    {
        public static int PacketID = 0x00;

        public override int ID { get; set; } = LoginStartPacket.PacketID;

        public string Username { get; private set; }

        public LoginStartPacket(Packet packet)
        {
            Username = packet.ReadString();
        }

        public static LoginStartPacket ParsePacket(Packet packet)
        {
            return new LoginStartPacket(packet);
        }
    }
}
