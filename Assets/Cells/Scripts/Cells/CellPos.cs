using UnityEngine;
using System.Collections;

namespace Flashunity.Cells
{
    public class CellPos : Pos
    {
        public readonly ushort indexBack;
        public readonly ushort indexFront;
        public readonly ushort indexTop;
        public readonly ushort indexBottom;
        public readonly ushort indexLeft;
        public readonly ushort indexRight;

        public CellPos(Vector3 position) : this((byte)position.x, (byte)position.y, (byte)position.z)
        {
        }

        public CellPos(byte x, byte y, byte z) : base(x, y, z)
        {
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
    }
}