using CsharpServer.Game;
using CsharpServer.PacketType;
using System;
using System.Net;
using System.Net.Sockets;

namespace CsharpServer.Network
{
    public partial class NetworkClient
    {
        public class UDP
        {
            public IPEndPoint endPoint;

            private int id;

            public UDP(int id)
            {
                this.id = id;
            }

            public void Connect(IPEndPoint endPoint)
            {
                this.endPoint = endPoint;
                Debug.Send($"Somebody has connected with UDP connection!");
            }

            public void SendData(Packet packet)
            {
                Server.SendUDPData(endPoint, packet);
            }

            public void HandleData(Packet packetData)
            {
                int packetLength = packetData.ReadVarInt();
                byte[] packetBytes = packetData.ReadBytes(packetLength);

                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet())
                    {
                        int packetId = packet.ReadVarInt();
                        Debug.Send($"Read packet from UDP: {packetId.ToHexId()}", Debug.Mode.DEBUG);
                    }
                });
            }

            public void Disconnect()
            {
                endPoint = null;
            }
        }
    }
}
