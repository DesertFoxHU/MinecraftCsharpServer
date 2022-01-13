using System.Threading;

namespace CsharpServer
{
    class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            Debug.IsDebug = true;
#endif

            Thread thread = new Thread(ThreadManager.StartUpdate);
            thread.IsBackground = true;
            thread.Start();

            Server.Start(10, 25565);

            var mre = new ManualResetEvent(false);
            mre.WaitOne();
        }
    }
}
