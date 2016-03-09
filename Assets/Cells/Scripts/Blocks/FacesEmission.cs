using UnityEngine;
using System.Collections;

namespace Flashunity.Cells
{
    public class FacesEmission
    {
        public FaceEmission back;
        public FaceEmission front;
        public FaceEmission top;
        public FaceEmission bottom;
        public FaceEmission left;
        public FaceEmission right;

        public void Clear()
        {
            back = null;
            front = null;
            top = null;
            bottom = null;
            left = null;
            right = null;
        }

        /*
        public FaceEmission GetByDirection(FaceDirection faceDirection)
        {
            
        }
        */
    }
}
