using CsharpServer.PacketType;
using System;
using System.Net;
using System.Net.Sockets;

namespace CsharpServer
{
    public class NetworkClient
    {
        public static int dataBufferSize = 4096;

        public int id;
        public TCP tcp;
        public UDP udp;

        public bool isAttemptingToLogin = false;

        public NetworkClient(int _clientId)
        {
            id = _clientId;
            tcp = new TCP(id);
            udp = new UDP(id);
        }

        public class TCP
        {
            public TcpClient socket;

            private readonly int id;
            private NetworkStream stream;
            private Packet recievedData;
            private byte[] recieveBuffer;

            public TCP(int id)
            {
                this.id = id;
            }

            public void Connect(TcpClient socket)
            {
                this.socket = socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                recievedData = new Packet();
                recieveBuffer = new byte[dataBufferSize];

                stream.BeginRead(recieveBuffer, 0, dataBufferSize, RecieveCallback, null);
                Debug.Send($"{socket.Client.RemoteEndPoint} has connected with TCP connection!", Debug.Mode.INFO);
            }

            public void SendData(Packet packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null); // Send data to appropriate client
                    }
                }
                catch (Exception _ex)
                {
                    Debug.Send($"Error sending data to player {id} via TCP: {_ex}");
                }
            }

            private void RecieveCallback(IAsyncResult result)
            {
                Debug.Send($"----------<Packet Read>----------", Debug.Mode.DEBUG);
                try
                {
                    int byteLength = stream.EndRead(result);
                    if(byteLength <= 0)
                    {
                        Debug.Send("Failed to read package probably disconnected!", Debug.Mode.DEBUG);
                        Server.clients[id].Disconnect();
                        return;
                    }

                    byte[] data = new byte[byteLength];
                    Array.Copy(recieveBuffer, data, byteLength);

                    recievedData.Reset(HandleData(data));
                    stream.BeginRead(recieveBuffer, 0, dataBufferSize, RecieveCallback, null);
                }
                catch(Exception ex)
                {
                    Debug.Send($"Error while connecting: {ex.Message}", Debug.Mode.ERROR);
                    Server.clients[id].Disconnect();
                }
            }

            private bool HandleData(byte[] data)
            {
                int packetLength = 0;

                recievedData.SetBytes(data);

                if(recievedData.UnreadLength() >= 1)
                {
                    packetLength = recievedData.ReadVarInt();
                    Debug.Send($"Length is {packetLength}", Debug.Mode.DEBUG);
                    if (packetLength <= 0)
                    {
                        return true;
                    }
                }

                while(packetLength > 0 && packetLength <= recievedData.UnreadLength())
                {
                    byte[] packetBytes = recievedData.ReadBytes(packetLength);
                    ThreadManager.ExecuteOnMainThread(() => 
                    {
                        using (Packet packet = new Packet(packetBytes))
                        {
                            Debug.Send($"Attempt to read packet's ID", Debug.Mode.DEBUG);
                            int packetId = packet.ReadVarInt();
                            Debug.Send($"Read packetid: {packetId.ToHexId()}", Debug.Mode.DEBUG);

                            //Exception for login attempt
                            if (Server.clients[id].isAttemptingToLogin && packetId == LoginStartPacket.PacketID)
                            {
                                ServerPacket loginPacket = LoginStartPacket.ParsePacket(packet);
                                ServerHandle.LoginStartRecieve(id, loginPacket);
                            }
                            else
                            {
                                //Normal packet handling

                                ServerPacket abstractPacket = ServerPacket.ParsePacket(packetId, packet);

                                if (abstractPacket != null)
                                    Server.packetHandlers[packetId](id, abstractPacket);
                            }
                        }
                    });

                    packetLength = 0;
                    if(recievedData.UnreadLength() >= 4)
                    {
                        packetLength = recievedData.ReadVarInt();
                        if(packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                if(packetLength <= 1)
                {
                    return true;
                }

                return false;
            }

            public void Disconnect()
            {
                socket.Close();
                stream = null;
                recievedData = null;
                recieveBuffer = null;
                socket = null;
            }
        }

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
                    using(Packet packet = new Packet())
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

        private void Disconnect()
        {
            Debug.Send($"{tcp.socket.Client.RemoteEndPoint} has disconnected!");
            tcp.Disconnect();
            udp.Disconnect();
            isAttemptingToLogin = false;
        }
    }
}
