using UnityEngine;
using System.Collections;

namespace Flashunity.Cells
{

    public class Light
    {
        public readonly byte type;
        public readonly Color32 color32;
        public readonly byte distance;

        public Light(byte type, Color32 color32, byte distance)
        {
            this.type = type;
            this.color32 = color32;
            this.distance = distance;
        }
    }
}