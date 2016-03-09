using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace Flashunity.Cells
{
    public class Emissions
    {
        // min color byte is 8 max is 32

        public Color32 top;
        public Color32 side;
        public Color32 bottom;

        public byte distance;

        public Emissions(byte distance, Color32 top, Color32 side, Color32 bottom)
        {
            this.distance = distance;

            this.top = top;
            this.side = side;
            this.bottom = bottom;
        }

        public Emissions(byte distance, Color32 color)
        {
            this.distance = distance;

            this.top = color;
            this.side = color;
            this.bottom = color;
        }

        public Color32 GetColorByFaceDirection(FaceDirection faceDirection)
        {
            switch (faceDirection)
            {
                case FaceDirection.top:
                    return top;
                case FaceDirection.back:
                    return side;
                case FaceDirection.left:
                    return side;
                case FaceDirection.right:
                    return side;
                case FaceDirection.front:
                    return side;
                case FaceDirection.bottom:
                    return bottom;
            }

            return new Color32();
        }
    }
}
