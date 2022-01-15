using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpServer
{
    public class KeepAlive
    {

        public static void Update()
        {
            while (true)
            {
                foreach(NetworkClient player in Server.GetAliveClients())
                {
                    if(player.state == PlayState.PLAYING)
                    {
                        ServerSend.SendKeepAlive(player.id);
                    }
                }
                Thread.Sleep(10000); //10 sec
            }
        }

    }
}
