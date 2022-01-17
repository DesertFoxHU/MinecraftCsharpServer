using System.Collections.Generic;

namespace CsharpServer.Game
{
    public class World
    {
        public static List<Entity> entities = new List<Entity>();

        public string WorldName { get; private set; }

        public World(string worldName)
        {
            WorldName = worldName;
        }
    }
}
