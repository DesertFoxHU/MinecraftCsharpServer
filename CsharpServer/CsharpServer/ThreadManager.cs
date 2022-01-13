using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpServer
{
    public class ThreadManager
    {
        private static readonly List<Action> executeOnMainThread = new List<Action>();
        private static readonly List<Action> executeCopiedMainThread = new List<Action>();
        private static bool actionExecuteOnMainThread = true;

        public static void StartUpdate()
        {
            while (true)
            {
                if (actionExecuteOnMainThread)
                {
                    executeCopiedMainThread.Clear();
                    lock (executeOnMainThread)
                    {
                        executeCopiedMainThread.AddRange(executeOnMainThread);
                        executeOnMainThread.Clear();
                        actionExecuteOnMainThread = false;
                    }

                    for (int i = 0; i < executeCopiedMainThread.Count; i++)
                    {
                        executeCopiedMainThread[i]();
                    }
                }
                Thread.Sleep(200);
            }
        }

        public static void ExecuteOnMainThread(Action action)
        {
            if (action == null)
            {
                Debug.Send($"No action to execute on main thread!", Debug.Mode.ERROR);
                return;
            }

            lock (executeOnMainThread)
            {
                executeOnMainThread.Add(action);
                actionExecuteOnMainThread = true;
            }
        }

    }
}
