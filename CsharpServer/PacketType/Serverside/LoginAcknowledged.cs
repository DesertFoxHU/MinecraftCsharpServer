using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class LoginAcknowledged : ServerPacket
    {
        public static int PacketID = 0x03;

        public override int ID { get; set; } = PacketID;


        public LoginAcknowledged(Packet packet)
        {
        }

        public static LoginAcknowledged ParsePacket(Packet packet)
        {
            return new LoginAcknowledged(packet);
        }
    }
}
