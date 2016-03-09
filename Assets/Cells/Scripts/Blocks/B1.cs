using UnityEngine;
using System.Collections;

namespace Flashunity.Cells
{
    public class B1 : Block
    {
        public B1(Pos pos, ChunkCell parent, ushort type, ushort textureIndex, bool opacity) : base(pos, parent, type, textureIndex, opacity)
        {
        }
        /*
        public B1(Pos pos, ChunkCell parent, ushort type, ushort textureIndex, Light light) : base(pos, parent, type, textureIndex, light)
        {
        }
        */
    }
}
