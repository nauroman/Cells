using UnityEngine;
using System.Collections;
using Flashunity.AtlasUVs;
using System;

namespace Flashunity.Cells
{
    public class BlockFace
    {
        readonly public Vector3[] vertices;
        public Color32[] colors32;
        readonly public Vector3[] normals;
        readonly public int[] triangles;
        readonly public Vector2[] uv;

        readonly public FaceDirection faceDirection;

        public BlockFace(FaceDirection faceDirection, int textureIndex, FaceLight[] faceLights, FaceDirection[] shadedEdges)
        //public BlockFace(FaceDirection faceDirection, int textureIndex)
        {
            this.faceDirection = faceDirection;

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

            ApplyDefaultColors();

            if (faceLights != null && faceLights.Length > 0)
                ApplyFaceLights(faceDirection, faceLights);
        }

        public void ApplyDefaultColors()
        {
            colors32 = new Color32[]
            {
                new Color32(8, 8, 8, 255),
                new Color32(8, 8, 8, 255),
                new Color32(8, 8, 8, 255),
                new Color32(8, 8, 8, 255)
            };
        }

        public void ApplyFaceLights(FaceDirection faceDirection, FaceLight[] faceLights)
        {
            for (int i = 0; i < faceLights.Length; i++)
                ApplyFaceLight(faceDirection, faceLights [i]);
        }

        public void ApplyFaceLight(FaceDirection faceDirection, FaceLight faceLight)
        {
            bool highQuality = false;

            if (highQuality)
            {
                for (int i = 0; i < vertices.Length; i++)
                    ApplyVertexColor(i, faceLight);
            } else
            {
                var vv = new Vector3(-0.5f, -0.5f, -0.5f) + faceLight.position;

                float x = vv.x;
                float y = vv.y;
                float z = vv.z;

                if (x < 0)
                    x = -x;

                if (y < 0)
                    y = -y;

                if (z < 0)
                    z = -z;

                var d = (x + y + z) / 3;

                if (d < 9)
                {
                    float dv = d * 11;

                    var lightColor = faceLight.color32;

                    float r = dv < lightColor.r ? lightColor.r - dv : 0;
                    float g = dv < lightColor.g ? lightColor.g - dv : 0;
                    float b = dv < lightColor.b ? lightColor.b - dv : 0;

                    if (r > 0 || g > 0 || b > 0)
                    {
                        var vertexColor = colors32 [0];

                        if (r > 0)
                            vertexColor.r += Convert.ToByte(r);
                        if (g > 0)
                            vertexColor.g += Convert.ToByte(g);
                        if (b > 0)
                            vertexColor.b += Convert.ToByte(b);

                        colors32 [0] = vertexColor;
                        colors32 [1] = vertexColor;
                        colors32 [2] = vertexColor;
                        colors32 [3] = vertexColor;
                    }
                }

            }
        }

        //        void ApplyVertexColor(int index, Color color, Vector3 position)
        void ApplyVertexColor(int index, FaceLight faceLight)
        {
            /*
            var vv = vertices [index] - faceLight.position;

            float x = vv.x;
            float y = vv.y;
            float z = vv.z;


            if (x < 0)
                x = -x;

            if (y < 0)
                y = -y;
            
            if (z < 0)
                z = -z;
            */

            var d = Vector3.Distance(vertices [index], faceLight.position);

            if (d < 9)
            {
                float dv = d * 3;

                var lightColor = faceLight.color32;

                float r = dv < lightColor.r ? lightColor.r - dv : 0;
                float g = dv < lightColor.g ? lightColor.g - dv : 0;
                float b = dv < lightColor.b ? lightColor.b - dv : 0;

                if (r > 0 || g > 0 || b > 0)
                {
                    var vertexColor = colors32 [index];

                    if (r > 0)
                        vertexColor.r += Convert.ToByte(r);
                    if (g > 0)
                        vertexColor.g += Convert.ToByte(g);
                    if (b > 0)
                        vertexColor.b += Convert.ToByte(b);

                    colors32 [index] = vertexColor;
                }
            }
        }





        public BlockFace(FaceDirection faceDirection, int textureIndex, float s)
        {
            this.faceDirection = faceDirection;

            triangles = new int[]{ 0, 1, 2, 0, 2, 3 };

            byte c = 32;

            colors32 = new Color32[]
            {
                new Color32(c, c, c, 255),
                new Color32(c, c, c, 255),
                new Color32(c, c, c, 255),
                new Color32(c, c, c, 255)
            };

            switch (faceDirection)
            {
                case FaceDirection.back:

                    vertices = new Vector3[]
                    {
                        new Vector3(1.0f - s, s, 1.0f - s),
                        new Vector3(1.0f - s, 1.0f - s, 1.0f - s),
                        new Vector3(s, 1.0f - s, 1.0f - s),
                        new Vector3(s, s, 1.0f - s)
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
                        new Vector3(s, s, s),
                        new Vector3(s, 1.0f - s, s),
                        new Vector3(1.0f - s, 1.0f - s, s),
                        new Vector3(1.0f - s, s, s)
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
                        new Vector3(s, 1.0f - s, s),
                        new Vector3(s, 1.0f - s, 1.0f - s),
                        new Vector3(1.0f - s, 1.0f - s, 1.0f - s),
                        new Vector3(1.0f - s, 1.0f - s, s)
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
                        new Vector3(1.0f - s, s, s),
                        new Vector3(1.0f - s, s, 1.0f - s),
                        new Vector3(s, s, 1.0f - s),
                        new Vector3(s, s, s)
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
                        new Vector3(s, s, 1.0f - s),
                        new Vector3(s, 1.0f - s, 1.0f - s),
                        new Vector3(s, 1.0f - s, s),
                        new Vector3(s, s, s)
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
                        new Vector3(1.0f - s, s, s),
                        new Vector3(1.0f - s, 1.0f - s, s),
                        new Vector3(1.0f - s, 1.0f - s, 1.0f - s),
                        new Vector3(1.0f - s, s, 1.0f - s)
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
