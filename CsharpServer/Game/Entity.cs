using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpServer.Game
{
    public class Entity
    {
        public int EntityID { get; private set; }

        public void Init()
        {
            EntityID = World.entities.Count;
            World.entities.Add(this);
        }
    }
}
