using SharpNBT;
using SharpNBT.SNBT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace CsharpServer
{
    public class NBTRegistry
    {
        public static CompoundTag dimensionCodec = null;
        public static CompoundTag dimension;

        public static void Initaliaze()
        {
            //dimensionCodec = ParseFromString(@"Registry/dimension_codec.snbt");
            Debug.Send("Registered 0 NBT compound!");
        }

        private static CompoundTag ParseFromString(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string snbt = sr.ReadToEnd();
                return StringNbt.Parse(snbt);
            }
        }
    }
}
