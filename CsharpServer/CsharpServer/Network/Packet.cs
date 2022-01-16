using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers.Binary;

namespace CsharpServer.Network
{
    public partial class Packet : IDisposable
    {
        private List<byte> buffer;
        private byte[] readableBuffer;
        private int readPos;

        public Packet()
        {
            buffer = new List<byte>();
            readPos = 0;
        }

        public Packet(int packetID)
        {
            buffer = new List<byte>();
            readPos = 0;

            WriteVarInt(packetID);
        }

        public Packet(byte[] _data)
        {
            buffer = new List<byte>();
            readPos = 0;

            SetBytes(_data);
        }
    }
}
