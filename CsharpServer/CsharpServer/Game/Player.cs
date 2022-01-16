using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpServer.Game
{
    public class Player : Entity
    {
        public string Username { get; private set; }

        public Player(string username)
        {
            Username = username;
            base.Init();
        }
    }
}
