using UnityEngine;
using System.Collections;

namespace Flashunity.Cells
{
    public class B5 : Block
    {
        public B5(Pos pos, ChunkCell parent, ushort type, ushort textureIndex, bool opacity) : base(pos, parent, type, textureIndex, opacity)
        {
        }

        /*
        public B5(Pos pos, ChunkCell parent, ushort type, ushort textureIndex, Light light) : base(pos, parent, type, textureIndex, light)
        {
        }
        */

        /*
        public override BlockFace GetBlockFace(FaceDirection faceDirection)
        {
            return new BlockFace(faceDirection, textureIndex, 0.3f);
        }

        public override bool hideNeighbourFace(Block block)
        {
            return false;
        }
        */


        /*
        public override void AddFaces(Vector3[] vertices, Vector3[] normals, int[] triangles, Vector2[] uv, ref int indexVertices, ref int indexTriangles, ref int indexUv)
        {
            
            if (back != null && (back as Block).hideNeighbourFace(this) &&
                front != null && (front as Block).hideNeighbourFace(this) &&
                top != null && (top as Block).hideNeighbourFace(this) &&
                bottom != null && (bottom as Block).hideNeighbourFace(this) &&
                left != null && (left as Block).hideNeighbourFace(this) &&
                right != null && !(right as Block).hideNeighbourFace(this))
            {
                return;
            }

            AddFace(GetBlockFace(FaceDirection.back), vertices, normals, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv, ref facesEmission.back);

            AddFace(GetBlockFace(FaceDirection.front), vertices, normals, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv, ref facesEmission.front);

            AddFace(GetBlockFace(FaceDirection.top), vertices, normals, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv, ref facesEmission.top);

            AddFace(GetBlockFace(FaceDirection.bottom), vertices, normals, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv, ref facesEmission.bottom);

            AddFace(GetBlockFace(FaceDirection.left), vertices, normals, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv, ref facesEmission.left);

            AddFace(GetBlockFace(FaceDirection.right), vertices, normals, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv, ref facesEmission.right);

        }
        */


    }
}
