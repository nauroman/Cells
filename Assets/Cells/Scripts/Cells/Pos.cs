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

        public readonly ushort indexBack;
        public readonly ushort indexFront;
        public readonly ushort indexTop;
        public readonly ushort indexBottom;
        public readonly ushort indexLeft;
        public readonly ushort indexRight;

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

            if (edge)
            {
                indexBack = (z == WIDTH - 1) ? GetIndex(x, y, 0) : ((ushort)(index + WIDTH_X_HEIGHT));
                indexFront = (z == 0) ? GetIndex(x, y, WIDTH - 1) : ((ushort)(index - WIDTH_X_HEIGHT));
                indexTop = (y == HEIGHT - 1) ? GetIndex(x, 0, z) : (ushort)(index + WIDTH);
                indexBottom = (y == 0) ? GetIndex(x, HEIGHT - 1, z) : (ushort)(index - WIDTH);
                indexLeft = (x == 0) ? GetIndex(WIDTH - 1, y, z) : (ushort)(index - 1);
                indexRight = (x == WIDTH - 1) ? GetIndex(0, y, z) : (ushort)(index + 1);
            } else
            {
                indexBack = (ushort)(index + WIDTH_X_HEIGHT);
                indexFront = (ushort)(index - WIDTH_X_HEIGHT);
                indexTop = (ushort)(index + WIDTH);
                indexBottom = (ushort)(index - WIDTH);
                indexLeft = (ushort)(index - 1);
                indexRight = (ushort)(index + 1);
            }
        }

        public static ushort GetIndex(byte x, byte y, byte z)
        {
            return (ushort)(x + y * WIDTH + z * WIDTH_X_HEIGHT);
        }

        /*
        public bool IsOutside(Vector3 v)
        {
            return v.x < 0 || v.y < 0 || v.z < 0 || v.x == WIDTH || v.y == HEIGHT || v.z == WIDTH;
        }
        */

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