﻿using System.Threading;

namespace CsharpServer
{
    class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            Debug.IsDebug = true;
#endif

            Thread thread = new Thread(ThreadManager.StartUpdate)
            {
                IsBackground = true
            };
            thread.Start();

            Server.Start(10, 25565);

            Thread keepalive = new Thread(KeepAlive.Update)
            {
                IsBackground = true
            };
            keepalive.Start();

            var mre = new ManualResetEvent(false);
            mre.WaitOne();
        }
    }
}
