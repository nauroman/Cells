using UnityEngine;
using System.Collections;
using Flashunity.Cells;

namespace Flashunity.Cells
{
    public class FaceLight
    {
        public Vector3 position;
        public Color32 color32;

        public FaceLight(Vector3 position, Color32 color32)
        {
            this.position = position;
            this.color32 = color32;
        }
    }
}