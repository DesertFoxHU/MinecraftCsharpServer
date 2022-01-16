using CsharpServer.Network;
using System;
using System.Threading;

namespace CsharpServer
{
    public class KeepAlive
    {

        public static void Update()
        {
            while (true)
            {
                foreach(NetworkClient client in Server.GetAliveClients())
                {
                    if(client.lastSentAlive != 0)
                    {
                        long elapsedTime = DateTime.Now.Ticks - client.lastSentAlive;
                        if (new TimeSpan(elapsedTime).TotalSeconds >= 30d)
                        {
                            client.Disconnect("{username} has kicked out! Reason: Not responded within 30 seconds");
                            continue;
                        }
                    }

                    if(client.state == PlayState.PLAYING)
                    {
                        ServerSend.SendKeepAlive(client.id);
                    }
                }
                Thread.Sleep(10000); //10 sec
            }
        }

    }
}
