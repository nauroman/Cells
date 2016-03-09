using UnityEngine;
using System.Collections;

namespace Flashunity.Cells
{
    public class Pos
    {
        public readonly byte x;
        public readonly byte y;
        public readonly byte z;

        public readonly ushort index;

        // no more than 16 !!!!
        public const ushort WIDTH = 16;
        public const ushort HEIGHT = 10;
        public const ushort WIDTH_X_HEIGHT = WIDTH * HEIGHT;

        public bool edge;

        public Pos(Vector3 position) : this((byte)position.x, (byte)position.y, (byte)position.z)
        {
        }

        public Pos(byte x, byte y, byte z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            index = (ushort)(x + y * WIDTH + z * WIDTH_X_HEIGHT);

            edge = x == 0 || y == 0 || z == 0 || x == WIDTH - 1 || y == HEIGHT - 1 || z == WIDTH - 1;
        }

        public static ushort GetIndex(byte x, byte y, byte z)
        {
            return (ushort)(x + y * WIDTH + z * WIDTH_X_HEIGHT);
        }

        public Vector3 GetVector3()
        {
            return new Vector3(x, y, z);
        }

        public override string ToString()
        {
            return "x: " + x.ToString() + ", y: " + y.ToString() + " , z: " + z.ToString();
        }
    
    }
}