using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace Flashunity.Cells
{
    public class FaceEmission
    {
        public Color32 color;
        public byte distance;
        public FaceDirection faceDirection;
        public int indexVerticesBegin;

        public Color32[] res = new Color32[4];

        public FaceEmission(Color32 color, byte distance, FaceDirection faceDirection, int indexVerticesBegin)
        {
            this.color = color;
            this.distance = distance;
            this.faceDirection = faceDirection;
            this.indexVerticesBegin = indexVerticesBegin;
        }
    }
}