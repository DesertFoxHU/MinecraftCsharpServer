using System;
using System.Buffers.Binary;
using System.Text;

namespace CsharpServer.Network
{
    public partial class Packet
    {
        public void WriteInt(int value)
        {
            Span<byte> span = stackalloc byte[4];
            BinaryPrimitives.WriteInt32BigEndian(span, value);
            buffer.AddRange(span.ToArray());
        }

        public void WriteLong(long value)
        {
            Span<byte> span = stackalloc byte[8];
            BinaryPrimitives.WriteInt64BigEndian(span, value);
            buffer.AddRange(span.ToArray());
        }

        public void WriteString(string value, int maxLength = short.MaxValue)
        {
            System.Diagnostics.Debug.Assert(value.Length <= maxLength);

            WriteVarInt(Encoding.UTF8.GetBytes(value).Length);
            buffer.AddRange(Encoding.UTF8.GetBytes(value));
        }

        public void WriteVarInt(int value)
        {
            var unsigned = (uint)value;

            do
            {
                var temp = (byte)(unsigned & 127);
                unsigned >>= 7;

                if (unsigned != 0)
                    temp |= 128;

                buffer.Add(temp);
            }
            while (unsigned != 0);
        }

        public void WriteUUID(Guid value)
        {
            if (value == Guid.Empty)
            {
                WriteLong(0L);
                WriteLong(0L);
            }
            else
            {
                var uuid = System.Numerics.BigInteger.Parse(value.ToString().Replace("-", ""), System.Globalization.NumberStyles.HexNumber);
                Write(uuid.ToByteArray());
            }
        }

        /// <summary>Adds a byte to the packet.</summary>
        /// <param name="_value">The byte to add.</param>
        public void Write(byte _value)
        {
            buffer.Add(_value);
        }

        /// <summary>Adds an array of bytes to the packet.</summary>
        /// <param name="_value">The byte array to add.</param>
        public void Write(byte[] _value)
        {
            buffer.AddRange(_value);
        }

        /// <summary>Adds a short to the packet.</summary>
        /// <param name="_value">The short to add.</param>
        public void Write(short _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        /// <summary>Adds a float to the packet.</summary>
        /// <param name="_value">The float to add.</param>
        public void Write(float _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        /// <summary>Adds a bool to the packet.</summary>
        /// <param name="_value">The bool to add.</param>
        public void Write(bool _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }
    }
}
