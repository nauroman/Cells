using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Flashunity.Logs;
using System;

namespace Flashunity.Cells
{
    public class Block : Cell
    {
        public readonly ushort textureIndex;

        public ushort type;

        protected int facesCount;

        public readonly Transparent transparent;

        Transform transform;
        //       public object MyProperty { get; }

        public Block(Pos pos, Cell parent, ushort type, ushort textureIndex, Transparent transparent) : base(pos, parent, null)
        {
            this.type = type;
            this.textureIndex = textureIndex;
            this.transparent = transparent;

            //Log.Add(Log.Props(this));

            //      Log.Add(Log.Props(this));

            //this.children = null;
        }

        /*
        public bool transparent
        {
            get
            {
                return false;
            }
        }
        */

        public BlockFace GetBlockFace(FaceDirection faceDirection)
        {
            return new BlockFace(faceDirection, textureIndex);
        }

        public bool hideNeighbourFace(Block block)
        {
            return transparent == Transparent.Opaque || (transparent == Transparent.TransparentAndFusionToFusion && block.transparent == Transparent.TransparentAndFusionToFusion) || (transparent == Transparent.TransparentAndFusionToSameType && type == block.type);
        }

        public new void AddToNeighbors()
        {
            if (back != null && back.front == null)
            {
                back.front = this;
                if (hideNeighbourFace(back as Block))
                    (back as Block).DecreaseFacesCount();
            }

            if (front != null && front.back == null)
            {
                front.back = this;
                if (hideNeighbourFace(front as Block))
                    (front as Block).DecreaseFacesCount();
            }

            if (top != null && top.bottom == null)
            {
                top.bottom = this;
                if (hideNeighbourFace(top as Block))
                    (top as Block).DecreaseFacesCount();
            }

            if (bottom != null && bottom.top == null)
            {
                bottom.top = this;
                if (hideNeighbourFace(bottom as Block))
                    (bottom  as Block).DecreaseFacesCount();
            }
            
            if (left != null && left.right == null)
            {
                left.right = this;
                if (hideNeighbourFace(left as Block))
                    (left as Block).DecreaseFacesCount();
            }

            if (right != null && right.left == null)
            {
                right.left = this;
                if (hideNeighbourFace(right as Block))
                    (right as Block).DecreaseFacesCount();
            }
        }


        public new void RemoveFromNeighbors()
        {
            if (back != null && back.front != null)
            {
                back.front = null;
                if (hideNeighbourFace(back as Block))
                    (back as Block).IncreaseFacesCount();
            }

            if (front != null && front.back != null)
            {
                front.back = null;
                if (hideNeighbourFace(front as Block))
                    (front as Block).IncreaseFacesCount();
            }

            if (top != null && top.bottom != null)
            {
                top.bottom = null;
                if (hideNeighbourFace(top as Block))
                    (top as Block).IncreaseFacesCount();
            }

            if (bottom != null && bottom.top != null)
            {
                bottom.top = null;
                if (hideNeighbourFace(bottom as Block))
                    (bottom as Block).IncreaseFacesCount();
            }

            if (left != null && left.right != null)
            {
                left.right = null;
                if (hideNeighbourFace(left as Block))
                    (left as Block).IncreaseFacesCount();
            }

            if (right != null && right.left != null)
            {
                right.left = null;
                if (hideNeighbourFace(right as Block))
                    (right as Block).IncreaseFacesCount();
            }
        }

        public int FacesCount
        {
            get
            {
                return facesCount;
            }
        }

        public void IncreaseFacesCount()
        {
            facesCount++;
        }

        public void DecreaseFacesCount()
        {
            facesCount--;
        }

            
        //        public void AddFaces(Vector3[] vertices, int[] triangles, Vector2[] uv)
        public void AddFaces(Vector3[] vertices, int[] triangles, Vector2[] uv, ref int indexVertices, ref int indexTriangles, ref int indexUv)
        {
            if (back == null || !(back as Block).hideNeighbourFace(this))
                AddFace(GetBlockFace(FaceDirection.back), vertices, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv);
//                AddFace(GetBlockFace(FaceDirection.back), vertices, triangles, uv);

            if (front == null || !(front as Block).hideNeighbourFace(this))
                AddFace(GetBlockFace(FaceDirection.front), vertices, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv);
//            AddFace(GetBlockFace(FaceDirection.front), vertices, triangles, uv);

            if (top == null || !(top as Block).hideNeighbourFace(this))
                AddFace(GetBlockFace(FaceDirection.top), vertices, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv);
//            AddFace(GetBlockFace(FaceDirection.top), vertices, triangles, uv);

            if (bottom == null || !(bottom as Block).hideNeighbourFace(this))
                AddFace(GetBlockFace(FaceDirection.bottom), vertices, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv);
//            AddFace(GetBlockFace(FaceDirection.bottom), vertices, triangles, uv);

            if (left == null || !(left as Block).hideNeighbourFace(this))
                AddFace(GetBlockFace(FaceDirection.left), vertices, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv);
//            AddFace(GetBlockFace(FaceDirection.left), vertices, triangles, uv);

            if (right == null || !(right as Block).hideNeighbourFace(this))
                AddFace(GetBlockFace(FaceDirection.right), vertices, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv);
//            AddFace(GetBlockFace(FaceDirection.right), vertices, triangles, uv);
        }

        //        void AddFace(BlockFace blockFace, Vector3[] vertices, int[] triangles, Vector2[] uv)
        void AddFace(BlockFace blockFace, Vector3[] vertices, int[] triangles, Vector2[] uv, ref int indexVertices, ref int indexTriangles, ref int indexUv)
        {
            //      Log.Add("AddFace");
            /*
            int vBegin = vertices.Length;
            int tBegin = triangles.Length;
            int uvBegin = uv.Length;
*/
            var faceVertices = blockFace.vertices;
            var faceTriangles = blockFace.triangles;
            var faceUV = blockFace.uv;

            int indexVerticesBegin = indexVertices;

            var v = pos.GetVector3();

            for (int i = 0; i < faceVertices.Length; i++)
            {
                //       Log.Add(faceVertices [i].ToString());

                //float r = 0.01f;

//                vertices [indexVertices++] = faceVertices [i] + v + new Vector3(Random.Range(-r, r), Random.Range(-r, r), Random.Range(-r, r));
                vertices [indexVertices++] = faceVertices [i] + v;// + new Vector3(Random.Range(-r, r), Random.Range(-r, r), Random.Range(-r, r));



//                var faceVert = faceVertices [i];

                //              vertices [indexVertices++] = new Vector3(faceVert.x + v.x, faceVert.y + v.y, faceVert.z + v.z);
            }

            //          int indexTrianglesBegin = indexTriangles;

            for (int i = 0; i < faceTriangles.Length; i++)
            {
                triangles [indexTriangles++] = indexVerticesBegin + faceTriangles [i];

////                Log.Add("triangle: " + triangles [indexTriangles - 1].ToString());
            }

            //          Log.Add(indexUv.ToString());
//            Log.Add(Log.Props(mesh.uv));

            //    Log.Add("faceUV: " + faceUV);

//            if (faceUV != null)
            //          {
            //            Log.Add("faceUV.Length: " + faceUV.Length);
            //          Log.Add(Log.Props(faceUV));

            for (int i = 0; i < faceUV.Length; i++)
            {
                uv [indexUv++] = faceUV [i];
            }
        }

        public void UpdateFacesCount()
        {
            facesCount = 0;

            if (back == null || !(back as Block).hideNeighbourFace(this))
                facesCount++;

            if (front == null || !(front as Block).hideNeighbourFace(this))
                facesCount++;

            if (top == null || !(top as Block).hideNeighbourFace(this))
                facesCount++;

            if (bottom == null || !(bottom as Block).hideNeighbourFace(this))
                facesCount++;

            if (left == null || !(left as Block).hideNeighbourFace(this))
                facesCount++;

            if (right == null || !(right as Block).hideNeighbourFace(this))
                facesCount++;
        }

    }

    public enum Transparent : byte
    {
        Opaque,
        Transparent,
        TransparentAndFusionToFusion,
        TransparentAndFusionToSameType
    }
}