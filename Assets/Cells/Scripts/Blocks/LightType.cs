using UnityEngine;
using System.Collections;

namespace Flashunity.Cells
{
    public class LightType : Light
    {
        public readonly BinaryGrid binaryGrid;

        public LightType(byte type, Color32 color32, byte distance) : base(type, color32, distance)
        {
            binaryGrid = new BinaryGrid((byte)Pos.WIDTH, (byte)Pos.HEIGHT);
        }
    }
}