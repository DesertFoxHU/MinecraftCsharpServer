using CsharpServer.Network;
using CsharpServer.PacketType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace CsharpServer.Network
{
    public static partial class Server
    {
        private static void UDPRecieveCallback(IAsyncResult result)
        {
            try
            {
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpListener.EndReceive(result, ref clientEndPoint);
                udpListener.BeginReceive(UDPRecieveCallback, null);

                if(data.Length < 4)
                {
                    return;
                }

                using (Packet packet = new Packet(data))
                {
                    int clientID = packet.ReadInt();

                    if(clientID == 0)
                    {
                        return;
                    }

                    if(clients[clientID].udp.endPoint == null)
                    {
                        clients[clientID].udp.Connect(clientEndPoint);
                        return;
                    }

                    if(clients[clientID].udp.endPoint.ToString() == clientEndPoint.ToString())
                    {
                        clients[clientID].udp.HandleData(packet);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void TCPConnectionCallback(IAsyncResult result)
        {
            TcpClient client = tcpListener.EndAcceptTcpClient(result);
            tcpListener.BeginAcceptTcpClient(TCPConnectionCallback, null);
            Debug.Send($"Incoming connection from {client.Client.RemoteEndPoint}...", Debug.Mode.DEBUG);

            for (int i = 1; i <= maxPlayers; i++)
            {
                if(clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(client);
                    return;
                }
            }

            Debug.Send($"Somebody tried to connect while the server is full", Debug.Mode.WARN);
        }

        public static void HandlePacket(int packetID, int clientID, ServerPacket packet)
        {
            try
            {
                packetHandlers[packetID](clientID, packet);
            } catch(Exception ex)
            {
                Debug.Send("Not registered packet? " + ex.StackTrace, Debug.Mode.WARN);
            }
        }

        public static void SendUDPData(IPEndPoint clientEndPoint, Packet packet)
        {
            try
            {
                if (clientEndPoint != null)
                {
                    udpListener.BeginSend(packet.ToArray(), packet.Length(), clientEndPoint, null, null);
                }
            }
            catch (Exception ex)
            {
                Debug.Send($"Error sending data to {clientEndPoint} via UDP: {ex}", Debug.Mode.ERROR);
            }
        }
    }
}
