using UnityEngine;
using System.Collections;

namespace Flashunity.Cells
{
    public class B0 : Block
    {
        public B0(Pos pos, ChunkCell parent, ushort type, ushort textureIndex, bool opacity) : base(pos, parent, type, textureIndex, opacity)
        {
        }

        /*
        public B0(Pos pos, ChunkCell parent, ushort type, ushort textureIndex, Light light) : base(pos, parent, type, textureIndex, light)
        {
        }
        */

    }
}
