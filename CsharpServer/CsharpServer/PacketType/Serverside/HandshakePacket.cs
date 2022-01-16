using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class HandshakePacket : ServerPacket
    {
        public static int PacketID = 0x00;

        public override int ID { get; set; } = HandshakePacket.PacketID;

        public int ProtocolVersion { get; private set; }

        public string ServerAddress { get; private set; }

        public ushort Port { get; private set; }

        public int Status { get; private set; } = 0;

        public HandshakePacket(Packet packet)
        {
            ProtocolVersion = packet.ReadVarInt();
            ServerAddress = packet.ReadString();
            Port = packet.ReadUnsignedShort();
            Status = packet.ReadVarInt();
        }

        public static HandshakePacket ParsePacket(Packet packet)
        {
            return new HandshakePacket(packet);
        }
    }
}
