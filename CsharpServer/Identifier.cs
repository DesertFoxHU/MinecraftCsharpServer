using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpServer
{
    public class Identifier
    {
        private const string AllowedCharSet = "01​​234​5​6​78​9abcdefghijklmnopqrstuvwxyz-_";

        public string ID { get; set; } = null;

        public Identifier(string ID)
        {
            ID = ID.ToLower();
            if (!ID.Contains(":"))
            {
                ID = "minecraft:" + ID;
            }
            foreach(char c in ID.ToCharArray())
            {
                if (!AllowedCharSet.Contains(c))
                {
                    throw new Exception($"Illegal character in {ID}, character {c} is not allowed!");
                }
            }
            this.ID = ID;
        }
    }
}
