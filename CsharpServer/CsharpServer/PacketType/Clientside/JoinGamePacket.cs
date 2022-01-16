using CsharpServer.Game;
using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class JoinGamePacket : ClientPacket
    {
        public static int PacketID = 0x26;

        public override int ID { get; set; } = JoinGamePacket.PacketID;

        public Player Player { get; private set; }
        public bool IsHardcore { get; private set; }
        public byte Gamemode { get; private set; }
        public byte PreviousGamemode { get; private set; }
        public int WorldCount { get; private set; }
        public Identifier[] DimensionNames { get; private set; }
        public byte DimensionCodec { get; private set; } //Replace with NBT Tag
        public byte Dimension { get; private set; } //Replace with NBT Tag
        public Identifier DimensionName { get; private set; }
        public long HashedSeed { get; private set; }
        public int MaxPlayers { get; private set; } = 0; //Ignored by the client
        public int ViewDistance { get; private set; } = 16;
        public int SimulationDistance { get; private set; }
        public bool ReducedDebugInfo { get; private set; } = false;
        public bool EnableRespawnScreen { get; private set; } = true;
        public bool IsDebug { get; private set; } = false;
        public bool IsFlat { get; private set; } = false;

        public JoinGamePacket(Player player, bool isHardcore, byte gamemode, byte previousGamemode, int worldCount, Identifier[] dimensionNames, byte dimensionCodec, byte dimension, Identifier dimensionName, long hashedSeed, int maxPlayers, int viewDistance, int simulationDistance, bool reducedDebugInfo, bool enableRespawnScreen, bool isDebug, bool isFlat)
        {
            Player = player;
            IsHardcore = isHardcore;
            Gamemode = gamemode;
            PreviousGamemode = previousGamemode;
            WorldCount = worldCount;
            DimensionNames = dimensionNames;
            DimensionCodec = dimensionCodec;
            Dimension = dimension;
            DimensionName = dimensionName;
            HashedSeed = hashedSeed;
            MaxPlayers = maxPlayers;
            ViewDistance = viewDistance;
            SimulationDistance = simulationDistance;
            ReducedDebugInfo = reducedDebugInfo;
            EnableRespawnScreen = enableRespawnScreen;
            IsDebug = isDebug;
            IsFlat = isFlat;
        }

        public override Packet WrapPacket()
        {
            Packet packet = new Packet(ID);
            packet.WriteInt(Player.EntityID);
            packet.Write(IsHardcore);
            packet.Write(Gamemode);
            packet.Write(PreviousGamemode);
            packet.Write(WorldCount);
            foreach(Identifier identifier in DimensionNames)
            {
                packet.WriteString(identifier.ID);
            }
            //TODO: NBT Dimension Codec
            //TODO: NBT Dimension
            packet.WriteString(DimensionName.ID);
            packet.WriteLong(HashedSeed);
            packet.WriteVarInt(MaxPlayers);
            packet.WriteVarInt(ViewDistance);
            packet.WriteVarInt(SimulationDistance);
            packet.Write(ReducedDebugInfo);
            packet.Write(EnableRespawnScreen);
            packet.Write(IsDebug);
            packet.Write(IsFlat);

            return packet;
        }
    }
}
