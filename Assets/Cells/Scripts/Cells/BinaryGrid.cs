using System.Collections;
using System.Net.Sockets;
using UnityEngine;

namespace Flashunity.Cells
{
    public class BinaryGrid
    {
        static ushort[] bitSet =
            {
                1,
                2,
                4,
                8,
                16,
                32,
                64,
                128,
                256,
                512,
                1024,
                2048,
                4096,
                8192,
                16384,
                32768,
            };

        static ushort[] bitReset =
            {
                65535 - 1,
                65535 - 2,
                65535 - 4,
                65535 - 8,
                65535 - 16,
                65535 - 32,
                65535 - 64,
                65535 - 128,
                65535 - 256,
                65535 - 512,
                65535 - 1024,
                65535 - 2048,
                65535 - 4096,
                65535 - 8192,
                65535 - 16384,
                65535 - 32768,
            };

        ushort[] grid;

        byte width;
        byte height;

        public BinaryGrid(byte width, byte height)
        {
            this.width = width;
            this.height = height;
            grid = new ushort[width * height];
        }

        public void Clear()
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid [i] = 0;
            }        
        }

        public void Set(byte x, byte y, byte z)
        {
//            Debug.Log("Set, x:" + x + ", y:" + y + ", z:" + z);
            ushort i = (ushort)(y * width + z);
            grid [i] = (ushort)(grid [i] | bitSet [x]);
            //          Debug.Log("gridxy?: " + grid [y] [z]);
        }

        public void Reset(byte x, byte y, byte z)
        {
            ushort i = (ushort)(y * width + z);

            grid [i] = (ushort)(grid [i] & bitReset [x]);
        }

        public bool IsSet(byte x, byte y, byte z)
        {
            ushort i = (ushort)(y * width + z);

            return (grid [i] & bitSet [x]) != 0;
        }

        public bool IsAnyXSet(Pos pos)
        {
            ushort i = (ushort)(pos.y * width + pos.z);

            return (grid [i]) != 0;
        }

        /*
        public bool IsAnyYSet(Pos pos)
        {
            ushort i = (ushort)(pos.y * width + pos.z);

            return (grid [i]) != 0;
        }
        */

        public bool IsSet(Pos pos)
        {
            ushort i = (ushort)(pos.y * width + pos.z);

            return (grid [i] & bitSet [pos.x]) != 0;
        }

    }
}
