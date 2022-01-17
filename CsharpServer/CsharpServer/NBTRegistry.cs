using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpServer
{
    public class NBTRegistry
    {
        //public static NbtCompound dimensionCodec;
        //public static NbtCompound dimension;

        public static void Initaliaze()
        {
            /*CompoundTag tag;
            NbtFile reader = new NbtFile();
            reader.LoadFromFile(@"C:\Users\meste\Desktop\CsharpServer\CsharpServer\CsharpServer\bin\Debug\Registry\dimension_codec.nbt");

            dimensionCodec = reader.RootTag;

            dimension = new NbtCompound("minecraft:worldgen/biome");*/
            Debug.Send("Registered 2 NBT compound!");
        }
    }
}
