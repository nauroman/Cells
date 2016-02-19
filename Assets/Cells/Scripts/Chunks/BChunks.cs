using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Flashunity.Logs;

namespace Flashunity.Cells
{
    public class BChunks : MonoBehaviour//, IPointerClickHandler//, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] Transform chunk;

        public ChunksPos chunksPos;

        public Cell cell;

        public long time;

        public bool updated;

        public BChunks back;
        public BChunks front;
        public BChunks top;
        public BChunks bottom;
        public BChunks left;
        public BChunks right;

        void Awake()
        {
//            cell = new Cell(new Pos(0, 0, 0), null, this);
        }

        /*
        ChunksPos ChunksPos
        {
            get
            {
                return _chunksPos;
            }
            set
            {
                _chunksPos = value;
//                transform.localPosition = _chunksPos.Vector3;
                cell = new Cell()
            }
        }
        */
        //       public SortedList bChunksList = new SortedList();

        /*
        private PosChunk GetChunkPosition(Vector3 v)
        {
            var pos = transform.position;

            var dv = v - transform.position;


            return new PosChunk(1, 1, 1);
        }
    */

        /*
        void AddBlock(Vector3 position)
        {
            
        }
        */


        /*

        //        void AddBlock(PosBlock posBlock, int type, int textureIndex)
        public void AddBlock(BChunk bChunk, Block block, bool update)
        {
            //     var posBlock = block.posBlock;

            //       var bChunk = GetBChunk(posBlock);

            //        if (bChunk)
            //      {
            bChunk.AddBlock(block, update);
            //    }
        }
        */

        /*
        BChunk GetBChunk(PosBlock posBlock)
        {
            var posChunk = PosChunk.GetPosChunkFromPosBlock(posBlock);

            var bChunk = bChunks [posChunk.index] as BChunk;

            if (bChunk == null)
            {
                bChunk = AddChunk(posChunk);
            }

            return bChunk;
        }
        */

        public BChunk AddChunk(Pos pos)
        {
            var v = new Vector3(pos.x * Pos.WIDTH, pos.y * Pos.HEIGHT, pos.z * Pos.WIDTH);

            var chunk = Instantiate(this.chunk, v + transform.position, Quaternion.identity) as Transform;

            chunk.parent = transform;

            var bChunk = chunk.GetComponent<BChunk>();

            bChunk.cell = new Cell(pos, cell, bChunk);

            //        cell.neighbors [pos.index] = bChunk.cell;


//            bChunksList [posChunk.index] = bChunk;

//            bChunk.BChunks = bChunksList;
            //    bChunk.UpdatePosChunk();

            return bChunk;
        }

        public BChunk GetChunk(Pos posChunk)
        {
            var cell = this.cell.neighbors.Values [posChunk.index];

            return cell == null ? null : cell.owner as BChunk;

//            return cell.cells [posChunk.index].owner as BChunk;
        }

        public Pos Pos
        {
            get
            {
                var pos = transform.localPosition;

                float x = pos.x / (Pos.WIDTH * Pos.WIDTH);
                float y = pos.y / (Pos.HEIGHT * Pos.HEIGHT);
                float z = pos.z / (Pos.WIDTH * Pos.WIDTH);

                return new Pos((byte)(x + Pos.WIDTH / 2), (byte)(y + Pos.HEIGHT / 2), (byte)(z + Pos.WIDTH / 2));
            }
        }


        void Start()
        {
            
            
            /*
            var bChunk = AddChunk(new PosChunk(0, 0, 0), false);

            var block = new Block(new PosBlock(0, 0, 31), 0, 1, bChunk, bChunks);

            AddBlock(bChunk, block, false);

            var bChunk1 = AddChunk(new PosChunk(0, 0, 1), false);

            var block1 = new Block(new PosBlock(0, 0, 0), 0, 1, bChunk1, bChunks);

            AddBlock(bChunk1, block1, false);
            UpdateMesh();
*/

            //AddTestChunk(new PosChunk(0, 0, 0));

            //       var cells = new SortedList<ushort, Cell>();

            //     cell = new Cell(new Pos(0, 0, 0), cells, this);

            //   AddTestChunk(new Pos(1, 1, 1));




            /*
            for (int x = 0; x < 3; x++)
            {
                for (int y = 1; y < 2; y++)
                {
                    for (int z = 1; z < 2; z++)
                    {
                        AddTestChunk(new PosChunk(x, y, z));
                    }
                }
            }

*/
            //       UpdateMesh();

            //         Log.Add(Log.Props(block));
        }

        public void UpdateMesh()
        {
            /*
            for (int i = 0; i < cell.cells.Count; i++)
            {
                var bChunk = cell.cells.Values [i].owner as BChunk;

                //   bChunk.ChunkNeighbors.Update();

                //            Log.Add("pos: " + bChunk.PosChunk.GetVector3().ToString());
//                Log.Add("back: " + (bChunk.ChunkNeighbors.back != null).ToString());
                //              Log.Add("front: " + (bChunk.ChunkNeighbors.front != null).ToString());
            }
            */

            /*
            for (int i = 0; i < cell.neighbors.Count; i++)
            {
                var bChunk = cell.neighbors.Values [i].owner as BChunk;

                bChunk.UpdateBlocks();
            }
            */

            for (int i = 0; i < cell.children.Count; i++)
            {
                var bChunk = cell.children.Values [i].owner as BChunk;

                bChunk.UpdateMesh();
            }

            /*
            for (int i = 0; i < bChunks.Count; i++)
            {
                var bChunk = bChunks.GetByIndex(i) as BChunk;

                //bChunk.RemoveBlocks();
            }
            */

        }

        void AddTestChunk(Pos posChunk)
        {
            var bChunk = AddChunk(posChunk);

            FillBlocks(bChunk, 1, 1, 1);

            //AddRandomBlocks(bChunk);


        }


        void FillBlocks(BChunk bChunk, int sx, int sy, int sz)
        {
            /*
            for (int x = 0; x < sx; x++)
            {
                for (int y = 0; y < sy; y++)
                {
                    for (int z = 0; z < sz; z++)
                    {
                        var posBlock = new Pos(x, y, z);

                        AddBlock(bChunk, new Block(posBlock, 0, Random.Range(0, 3), bChunk, bChunksList), false);
                    }
                }
            }
            */
        }

        void AddRandomBlocks(BChunk bChunk)
        {
            /*
            for (int i = 0; i < BChunk.WIDTH_X_HEIGHT * BChunk.WIDTH / 2; i++)
            {
                var posBlock = new Pos(Random.Range(0, BChunk.WIDTH), Random.Range(0, BChunk.HEIGHT), Random.Range(0, BChunk.WIDTH));

                if (bChunk.GetBlock(posBlock.index) == null)
                    AddBlock(bChunk, new Block(posBlock, 0, Random.Range(0, 3), bChunk, bChunksList), false);
            }
*/
        }

        void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                AddBlock();
            }


            return;


            if (cell.neighbors.Count > 0)
            {
                var bChunk = cell.neighbors.Values [0].owner as BChunk;

                //    bChunk.RemoveBlocks();

                AddRandomBlocks(bChunk);
                //  Debug.Log("Update");

                bChunk.UpdateMesh();
            }
        }

        void AddBlock()
        {
            //Screen.width / 2

            //         Log.Add(Camera.main.transform.position.ToString());

//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));


            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.parent = transform)
                {
                    var bChunk = hit.transform.gameObject.GetComponent<BChunk>();

                    if (bChunk != null)
                    {
                        //       Log.Add("ok");//hit.transform.gameObject.GetComponent<BChunks>);

                        bChunk.AddBlock(hit.point, hit.normal, true);


                        //hit.point

                        //Log.Add(hit.ToString());

                        //    ReplaceBlockAt(hit, block);
                        //Debug.DrawLine(ray.origin, ray.origin + (ray.direction * hit.distance),Color.green, 2);
                    }
                }

            }

        }


        /*
        void AddBlock(BChunk bChunk, Vector3 worldPosition, Vector3 worldNormal)
        {
            Log.Add("AddBlock");

            var posBlock = new PosBlock(1, 1, 1);
            var block = new Block(posBlock, 0, 0, bChunk, bChunks);


            Log.Add(Log.Props(bChunk));

            Log.Add(Log.Props(block));

            AddBlock(bChunk, block, true);
        }
        */

        /*
        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }
        */


        /*
        public void OnPointerClick(PointerEventData eventData)
        {
            //Debug.Log("OnPointerClick");

        }
        */

        BChunksData GetBChunksData()
        {
            var bChunksData = new BChunksData();

            bChunksData.time = DateTime.UtcNow.Ticks;

            var chunks = new BChunkData[cell.children.Count];

            for (int i = 0; i < cell.children.Count; i++)
            {
                chunks [i] = GetBChunkData(cell.children.Values [i]);
            }

            bChunksData.chunks = chunks;

            return bChunksData;
        }

        BChunkData GetBChunkData(Cell cell)
        {
            var chunkData = new BChunkData();

            chunkData.index = cell.pos.index;

            var blocks = new BlockData[cell.children.Count];

            for (int i = 0; i < cell.children.Count; i++)
            {
                blocks [i] = GetBlockData(cell.children.Values [i]);
            }
                
            chunkData.blocks = blocks;


            return chunkData;
        }

        BlockData GetBlockData(Cell cell)
        {
            var blockData = new BlockData();

            blockData.index = cell.pos.index;

            blockData.type = (cell.owner as Block).textureIndex;

            return blockData;
        }


        public void Save()
        {
            var bf = new BinaryFormatter();
            var file = File.Create(FilePath());//, FileMode.Create);

            bf.Serialize(file, GetBChunksData());
            file.Close();
        }

        public void Load()
        {
            if (File.Exists(FilePath()))
            {
                var bf = new BinaryFormatter();
                var file = File.Open(FilePath(), FileMode.Open);

                BChunksData bChunksData = bf.Deserialize(file) as BChunksData;

                //  version = bChunksData.version;
                time = bChunksData.time;

                file.Close();
            }
        }

        string FilePath()
        {
//            return Application.persistentDataPath + "/chunks/" + chunksPos.GetFileName();
            return Application.persistentDataPath + "/" + chunksPos.GetFileName();
        }

        /*
        string filename;

        public string Filename
        {
            get
            {
                if (filename == null)
                    filename = GetFileName();

                return filename;
            }
        }
        */

        /*
        public string GetFileNameFromIndex()
        {
            /*
             * from name back to index
            string incoming = returnValue
                .Replace('_', '/').Replace('-', '+');
            switch(returnValue.Length % 4) {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }
            byte[] bytes = Convert.FromBase64String(incoming);
            string originalText = Encoding.ASCII.GetString(bytes);
*/
        /*
            var index = cell.pos.index;

            byte[] bytes = BitConverter.GetBytes(index);
            char[] padding = { '=' };
            return Convert.ToBase64String(bytes).TrimEnd(padding).Replace('+', '-').Replace('/', '_');
        }
    */

        /*
        string GetFileName()
        {
            var fileName = GetFileNameFromIndex();

            var cell = this.cell;

            while (cell.parent != null)
            {
                cell = cell.parent;
                var bcell = cell.owner as BCell;
                fileName += "." + bcell.GetCellFileName();
            }

            return fileName;
        }
    */

    }
}

[Serializable]
class BChunksData
{
    //    public string fileName;
    // "p10.m5.m30";
    // (10,-5,-30);
    //    public ulong formatVersion = 0;

    //DateTime.Now.Ticks;
    public long time;

    /*
    public string back;
    public string front;
    public string top;
    public string bottom;
    public string left;
    public string right;
*/

    public BChunkData[] chunks;
}

[Serializable]
class BChunkData
{
    public ushort index;
    public BlockData[] blocks;
}

[Serializable]
class BlockData
{
    public ushort index;
    public ushort type;
}