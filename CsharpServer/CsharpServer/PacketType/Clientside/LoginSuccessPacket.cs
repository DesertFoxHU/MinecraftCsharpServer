using System;
using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class LoginSuccessPacket : ClientPacket
    {
        public static int PacketID = 0x02;

        public override int ID { get; set; } = LoginSuccessPacket.PacketID;

        public Guid UUID { get; private set; }
        public string Username { get; private set; }

        public LoginSuccessPacket(Guid UUID, string username)
        {
            this.UUID = UUID;
            Username = username;
        }

        public override Packet WrapPacket()
        {
            Packet packet = new Packet(ID);
            packet.WriteUUID(UUID);
            packet.WriteString(Username);
            return packet;
        }
    }
}
