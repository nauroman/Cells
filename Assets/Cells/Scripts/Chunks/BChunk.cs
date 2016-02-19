using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

using Flashunity.Logs;

namespace Flashunity.Cells
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]

    public class BChunk : MonoBehaviour, IPointerClickHandler
    {
        public Cell cell;
        Mesh mesh;
        MeshCollider meshCollider;

        /*
        public void UpdatePosChunk()
        {
            //          var position = transform.position;

//            posChunk = new PosChunk((int)position.x / SIZE, (int)position.y / SIZE, (int)position.z / SIZE);
            //posChunk = new Pos((int)position.x / WIDTH, (int)position.y / HEIGHT, (int)position.z / WIDTH);

            //chunkNeighbors = new ChunkNeighbors(posChunk, bChunks.bChunksList);
        }
        */
            

        /*
        public void AddBlock(Block block, bool update)
        {
            blocks [block.posBlock.index] = block;

            if (update)
            {
                block.Update();
                //                block.UpdateNeighbors();
                //              block.UpdateFacesCount();
                UpdateMesh(false);
            }
        }
        */

        public Block AddBlock(Pos pos, ushort type, bool update)
        {
            //     Debug.Log("AddBlock");
            //   Debug.Log("pos:" + pos);
            // Debug.Log("type:" + type);




//            return block;
            //cell.cells


            // FOR DEBUG!!! It's not necessary!!!
            if (!cell.children.ContainsKey(pos.index))
            {
                var block = GetBlockByType(type, pos, cell);

                if (update)
                {
//                    block.UpdateNeighbors();
                    //block.AddToNeighbors();
                    block.UpdateFacesCount();

                    UpdateMesh();
                }

                return block;
            }

            return null;

            //       Log.Add("AddBlock, blocks [block.posBlock.index]:" + blocks.Count.ToString());
        }

        static Block GetBlockByType(ushort type, Pos pos, Cell parent)//, ushort textureIndex)
        {
            switch (type)
            {
                case 0:
                    return new B0(pos, parent, 0, 0, Transparent.Opaque);
                case 1:
                    return new B1(pos, parent, 1, 1, Transparent.Transparent);
            }

            return null;//new Block(pos, parent, type, false);
            
            //Type[] bs = { B0, B1 };

//            return new bs[type](pos, parent, type);
            //          Type t = Type.GetType("B" + type.ToString());
//            return t != null ? Activator.CreateInstance(t, pos, parent, type) as Block : null;

        }

        //        void AddBlock(PosIndex posIndex, int type)
        public void AddBlock(Block block, bool update)
        {

            /*
            // FOR DEBUG!!! It's not necessary!!!
            if (!blocks.ContainsKey(block.pos.index))
            {
                blocks [block.posBlock.index] = block;

                if (update)
                {
                    block.UpdateNeighbors();
                    block.AddToNeighbors();
                    block.UpdateFacesCount();

                    UpdateMesh(false);
                }
            }*/

            //       Log.Add("AddBlock, blocks [block.posBlock.index]:" + blocks.Count.ToString());
        }

        public void RemoveBlock(Block block, bool updateNeighbors)
        {
            /*
            blocks.Remove(block.posBlock.index);

            if (updateNeighbors)
            {                
                block.RemoveFromNeighbors();
//                block.BlockNeighbors.RemoveFromNeighbors();
//                RemoveBlockFromNeighbors(block);
            }
            */
        }

        /*
        public void RemoveBlocks()
        {
            //blocks.Clear();
        }

        public Block GetBlock(int posIndex)
        {
            //          Log.Add("GetBlock: " + posIndex);
            //        Log.Add("blocks.Count: " + blocks.Count.ToString());

            return blocks [posIndex] as Block;
        }
        */

        /*
        public BChunk GetChunk(int posIndex)
        {
            return bChunks.getchu [posIndex] as BChunk;
        }
        */

        /*
        void AddBlockNeighbors(Block block)
        {
            var neighbor = GetLeftBlockNeighbor(block);
            block.left = neighbor;

            if (neighbor != null)
                neighbor.right = block;
        }
        */

        /*
        void RemoveBlockFromNeighbors(Block block)
        {
            var neighbor = GetLeftBlockNeighbor(block);

            if (neighbor != null)
                neighbor.right = null;
            
        }
        */



        public void UpdateBlocks()
        {
            
            for (int i = 0; i < cell.children.Count; i++)
            {
                var block = cell.children.Values [i] as Block;

                //            block.AddToNeighbors();
                block.UpdateFacesCount();


//                Log.Add("pos: " + block.posBlock.GetVector3().ToString());
//                Log.Add("back: " + (block.BlockNeighbors.back != null).ToString());
//                Log.Add("front: " + (block.BlockNeighbors.front != null).ToString());

                //            block.UpdateFacesCount();
            }

        }

        public void UpdateMeshesNeighbours()
        {
            if (cell.back != null)
                (cell.back.owner as BChunk).UpdateMesh();
            
            if (cell.front != null)
                (cell.front.owner as BChunk).UpdateMesh();
            
            if (cell.top != null)
                (cell.top.owner as BChunk).UpdateMesh();
            
            if (cell.bottom != null)
                (cell.bottom.owner as BChunk).UpdateMesh();
            
            if (cell.left != null)
                (cell.left.owner as BChunk).UpdateMesh();
            
            if (cell.right != null)
                (cell.right.owner as BChunk).UpdateMesh();
        }

        public void UpdateMesh()
        {
            mesh.Clear();

            UpdateBlocks();
//            int blocksCount = blocks.Count;

            int facesCount = GetFacesCount();

            var vertices = new Vector3[facesCount * 4];
            var triangles = new int[facesCount * 6];
            var uv = new Vector2[facesCount * 4];

            int indexVertices = 0;
            int indexTriangles = 0;
            int indexUv = 0;

            var children = cell.children;

            for (int i = 0; i < children.Count; i++)
            {
//                var block = cells.Values. GetByIndex(i) as Block;
                var block = children.Values [i] as Block;
//                block.AddFaces(vertices, triangles, uv);
                block.AddFaces(vertices, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv);
            }


            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;

            mesh.Optimize();
            mesh.RecalculateNormals();

            meshCollider.sharedMesh = mesh;


            //        Log.Add(Log.Props(mesh));

            /*
            Log.Add(Log.Props(mesh.vertices));
            Log.Add(Log.Props(mesh.triangles));
            Log.Add(Log.Props(mesh.uv));
*/
            /*
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                Log.Add(mesh.vertices [i].ToString());
            }

            for (int i = 0; i < mesh.triangles.Length; i++)
            {
                Log.Add(mesh.triangles [i].ToString());
            }
            */
        }

        int GetFacesCount()
        {
            int facesCount = 0;

            var children = cell.children;

            for (int i = 0; i < children.Count; i++)
            {
                var block = children.Values [i] as Block;
                facesCount += block.FacesCount;
            }

            return facesCount;
        }

        /*
        void AddFace(BlockFace blockFace, Vector3[] vertices, int[] triangles, Vector2[] uv)
        {
            int vBegin = vertices.Length;
            int tBegin = triangles.Length;
            int uvBegin = uv.Length;

            var faceVertices = blockFace.vertices;
            var faceTriangles = blockFace.triangles;
            var faceUV = blockFace.uv;

            for (int i = 0; i < faceVertices.Length; i++)
            {
                vertices [vBegin + i] = faceVertices [i];
            }

            for (int i = 0; i < faceTriangles.Length; i++)
            {
                triangles [tBegin + i] = faceTriangles [i];
            }

            for (int i = 0; i < faceUV.Length; i++)
            {
                uv [uvBegin + i] = faceUV [i];
            }
        }
        */


        void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            meshCollider = GetComponent<MeshCollider>();
            //           bChunk = GetComponent<BChunk>();
        }

        void Start()
        {
//            UpdatePosChunk();
        }

        void Update()
        {
	
        }

        BChunk AddChunk(Pos posChunk)
        {
            // var chunk = new Chink;

            return null;
//            return new BChunk();
        }

        Vector3 GetCloseToCubeCenter(Vector3 worldPosition, Vector3 worldNormal)
        {
            var v = worldPosition;

            if (worldNormal.x == 1)
                v.x -= 0.5f;
            else if (worldNormal.x == -1)
                v.x += 0.5f;
            else if (worldNormal.y == 1)
                v.y -= 0.5f;
            else if (worldNormal.y == -1)
                v.y += 0.5f;
            else if (worldNormal.z == 1)
                v.z -= 0.5f;
            else if (worldNormal.z == -1)
                v.z += 0.5f;

            return v;
        }

        Pos GetPosBlock(Vector3 worldPosition, Vector3 worldNormal)
        {
            var closeToBlockCenter = GetCloseToCubeCenter(worldPosition, worldNormal);


//            var chunksPosition = transform.parent.position;

            //           var worldChunkPosition = transform.position - transform.parent.position;

            /*
            Log.Add("chunks: " + transform.parent.position.ToString());
            Log.Add("chink: " + transform.position.ToString());
            Log.Add("worldChunkPosition: " + worldChunkPosition.ToString());
            Log.Add("mouse: " + worldPosition.ToString());
*/
            var localPosition = closeToBlockCenter - transform.position;//worldChunkPosition;

            Log.Add("localPosition: " + localPosition.ToString());


            byte x = (byte)(localPosition.x - 0.5f);
            byte y = (byte)(localPosition.y - 0.5f);
            byte z = (byte)(localPosition.z - 0.5f);

            return new Pos(x, y, z);
        }

        void GetNewBChunkAndNewPosBlock(Pos posBlock, Vector3 worldNormal, ref BChunk newBChunk, ref Pos newPosBlock, ref bool isNewChunk)
        {
            /*
            if (posBlock.edge)
            {
                if (worldNormal.x == -1)
                {
                    if (posBlock.x == 0)
                    {
                        newPosBlock = new Pos(BChunk.WIDTH - 1, posBlock.y, posBlock.z);
                        if (chunkNeighbors.left != null)
                            newBChunk = chunkNeighbors.left;
                        else
                        {
                            newBChunk = bChunks.AddChunk(new Pos(posChunk.x - 1, posChunk.y, posChunk.z));
                            isNewChunk = true;
                        }
                        return;
                    } else
                    {
                        newPosBlock = new Pos(posBlock.x - 1, posBlock.y, posBlock.z);
                        return;
                    }
                } else if (worldNormal.x == 1)
                {
                    if (posBlock.x == BChunks.WIDTH - 1)
                    {
                        newPosBlock = new Pos(0, posBlock.y, posBlock.z);

                        if (chunkNeighbors.right != null)
                            newBChunk = chunkNeighbors.right;
                        else
                        {
                            newBChunk = bChunks.AddChunk(new Pos(posChunk.x + 1, posChunk.y, posChunk.z));
                            isNewChunk = true;
                        }
                        return;
                    } else
                    {
                        newPosBlock = new Pos(posBlock.x + 1, posBlock.y, posBlock.z);
                        return;
                    }
                }



                return;
            } else
            {
                newPosBlock = new Pos(posBlock.y + (int)worldNormal.x, posBlock.y + (int)worldNormal.y, posBlock.z + (int)worldNormal.z);
                return;
            }
            */
        }

        public void AddBlock(Vector3 worldPosition, Vector3 worldNormal, bool update)
        {

            /*
            //    Log.Add("AddBlock");

            var posBlock = GetPosBlock(worldPosition, worldNormal);

            var newBChunk = this;
            bool isNewChunk = false;
            Pos newPosBlock = null;


            GetNewBChunkAndNewPosBlock(posBlock, worldNormal, ref newBChunk, ref newPosBlock, ref isNewChunk);


            Log.Add("worldChunkPosition: " + posBlock.ToString());

            var block = new Block(newPosBlock, 0, 0, this, bChunks.bChunksList);

            newBChunk.AddBlock(block, update);

            if (update && newBChunk != this)
            {
                newBChunk.UpdateMesh(true);
//                UpdateBlocks();
                UpdateMesh(true);
            }
            */
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //     Log.Add("OnPointerClick");

            //Debug.Log("OnPointerClick");
            //          Log.Add(Log.Props(eventData));

//            AddBlock(eventData.pointerPress.GetComponent<BChunk>(), eventData.pointerPressRaycast.worldPosition, eventData.pointerPressRaycast.worldNormal);





            //        AddBlock(eventData.pointerPressRaycast.worldPosition, eventData.pointerPressRaycast.worldNormal, true);






            //var worldPosition = eventData.pointerPressRaycast.worldPosition;

            //            var block = new Block(GetPosBlock(worldPosition), 0, 0, this, bChunks);

            //          AddBlock(block, true);
        }


    }
}
