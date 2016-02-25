using UnityEngine;
using System.Collections;
using Flashunity.AtlasUVs;

namespace Flashunity.Cells
{
    public class BlockFace
    {
        readonly public Vector3[] vertices;
        readonly public Vector3[] normals;
        readonly public int[] triangles;
        readonly public Vector2[] uv;

        public BlockFace(FaceDirection faceDirection, int textureIndex)
        {

            triangles = new int[]{ 0, 1, 2, 0, 2, 3 };

            switch (faceDirection)
            {
                case FaceDirection.back:

                    vertices = new Vector3[]
                    {
                        new Vector3(1.0f, 0.0f, 1.0f),
                        new Vector3(1.0f, 1.0f, 1.0f),
                        new Vector3(0.0f, 1.0f, 1.0f),
                        new Vector3(0.0f, 0.0f, 1.0f)
                    };

                    normals = new Vector3[]
                    {
                        Vector3.forward,
                        Vector3.forward,
                        Vector3.forward,
                        Vector3.forward
                    };

                    uv = CubesUVs.cubesUVs [textureIndex].side;



                    break;

                case FaceDirection.front:

                    vertices = new Vector3[]
                    {
                        new Vector3(0.0f, 0.0f, 0.0f),
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector3(1.0f, 1.0f, 0.0f),
                        new Vector3(1.0f, 0.0f, 0.0f)

                    };

                    normals = new Vector3[]
                    {
                        Vector3.back,
                        Vector3.back,
                        Vector3.back,
                        Vector3.back
                    };

                      
                    uv = CubesUVs.cubesUVs [textureIndex].side;

                    break;

                case FaceDirection.top:

                    vertices = new Vector3[]
                    {
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector3(0.0f, 1.0f, 1.0f),
                        new Vector3(1.0f, 1.0f, 1.0f),
                        new Vector3(1.0f, 1.0f, 0.0f)

                    };

                    normals = new Vector3[]
                    {
                        Vector3.up,
                        Vector3.up,
                        Vector3.up,
                        Vector3.up
                    };
                        
                    uv = CubesUVs.cubesUVs [textureIndex].top;

                    break;

                case FaceDirection.bottom:

                    vertices = new Vector3[]
                    {
                        new Vector3(1.0f, 0.0f, 0.0f),
                        new Vector3(1.0f, 0.0f, 1.0f),
                        new Vector3(0.0f, 0.0f, 1.0f),
                        new Vector3(0.0f, 0.0f, 0.0f)

                    };

                    normals = new Vector3[]
                    {
                        Vector3.down,
                        Vector3.down,
                        Vector3.down,
                        Vector3.down
                    };

                    uv = CubesUVs.cubesUVs [textureIndex].bottom;

                    break;


                case FaceDirection.left:

                    vertices = new Vector3[]
                    {
                        new Vector3(0.0f, 0.0f, 1.0f),
                        new Vector3(0.0f, 1.0f, 1.0f),
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector3(0.0f, 0.0f, 0.0f)

                    };

                    normals = new Vector3[]
                    {
                        Vector3.left,
                        Vector3.left,
                        Vector3.left,
                        Vector3.left
                    };

                    uv = CubesUVs.cubesUVs [textureIndex].side;

                    break;

                case FaceDirection.right:

                    vertices = new Vector3[]
                    {
                        new Vector3(1.0f, 0.0f, 0.0f),
                        new Vector3(1.0f, 1.0f, 0.0f),
                        new Vector3(1.0f, 1.0f, 1.0f),
                        new Vector3(1.0f, 0.0f, 1.0f)
                    };

                    normals = new Vector3[]
                    {
                        Vector3.right,
                        Vector3.right,
                        Vector3.right,
                        Vector3.right
                    };

                    uv = CubesUVs.cubesUVs [textureIndex].side;

                    break;
            }
        }
    }

    public enum FaceDirection : byte
    {
        back,
        front,
        top,
        bottom,
        left,
        right
    }
}
