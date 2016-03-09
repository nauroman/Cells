using UnityEngine;
using System.Collections;
using System;
using Flashunity.Logs;
using System.Runtime.InteropServices.Expando;

namespace Flashunity.Cells
{
    public class BWorld : MonoBehaviour
    {
        [SerializeField]
        Transform chunks;

        Cell cell;

        void Awake()
        {
            cell = new Cell(new CellPos(0, 0, 0), null, this);

            /*
            var greed = new BinaryGrid(16, 10);

            Debug.Log("greed: " + greed.IsSet(0, 0, 0));
            Debug.Log("greed: " + greed.IsSet(5, 5, 5));

            greed.Set(0, 0, 0);

            Debug.Log("_greed: " + greed.IsSet(0, 0, 0));
            Debug.Log("greed: " + greed.IsSet(1, 0, 0));
            Debug.Log("greed: " + greed.IsSet(0, 1, 0));
            Debug.Log("greed: " + greed.IsSet(0, 0, 1));
            Debug.Log("greed: " + greed.IsSet(1, 1, 1));

            greed.Set(1, 0, 0);

            Debug.Log("_greed: " + greed.IsSet(0, 0, 0));
            Debug.Log("_greed: " + greed.IsSet(1, 0, 0));
            Debug.Log("greed: " + greed.IsSet(0, 1, 0));
            Debug.Log("greed: " + greed.IsSet(0, 0, 1));
            Debug.Log("greed: " + greed.IsSet(1, 1, 1));

            greed.Set(0, 1, 0);

            Debug.Log("_greed: " + greed.IsSet(0, 0, 0));
            Debug.Log("_greed: " + greed.IsSet(1, 0, 0));
            Debug.Log("_greed: " + greed.IsSet(0, 1, 0));
            Debug.Log("greed: " + greed.IsSet(0, 0, 1));
            Debug.Log("greed: " + greed.IsSet(1, 1, 1));

            greed.Set(0, 0, 1);

            Debug.Log("_greed: " + greed.IsSet(0, 0, 0));
            Debug.Log("_greed: " + greed.IsSet(1, 0, 0));
            Debug.Log("_greed: " + greed.IsSet(0, 1, 0));
            Debug.Log("_greed: " + greed.IsSet(0, 0, 1));
            Debug.Log("greed: " + greed.IsSet(1, 1, 1));

            greed.Set(1, 1, 1);

            Debug.Log("_greed: " + greed.IsSet(0, 0, 0));
            Debug.Log("_greed: " + greed.IsSet(1, 0, 0));
            Debug.Log("_greed: " + greed.IsSet(0, 1, 0));
            Debug.Log("_greed: " + greed.IsSet(0, 0, 1));
            Debug.Log("_greed: " + greed.IsSet(1, 1, 1));


            greed.Reset(0, 1, 0);

            Debug.Log("_greed: " + greed.IsSet(0, 0, 0));
            Debug.Log("_greed: " + greed.IsSet(1, 0, 0));
            Debug.Log("greed: " + greed.IsSet(0, 1, 0));
            Debug.Log("_greed: " + greed.IsSet(0, 0, 1));
            Debug.Log("_greed: " + greed.IsSet(1, 1, 1));
            */
        }

        void PlatformOneLight()
        {
            var bChunks000 = AddChunks(new ChunksPos(0, 0, 0));


            var bChunks_100 = AddChunks(new ChunksPos(-1, 0, 0));
            var bChunks_10_1 = AddChunks(new ChunksPos(-1, 0, -1));
            /*
            var bChunks010 = AddChunks(new ChunksPos(0, 1, 0));
            var bChunks0_10 = AddChunks(new ChunksPos(0, -1, 0));

            var bChunks001 = AddChunks(new ChunksPos(0, 0, 1));
            var bChunks00_1 = AddChunks(new ChunksPos(0, 0, -1));
*/


            var bChunk000 = AddChunk(bChunks000, new CellPos(0, 0, 0));
            var bChunk1500 = AddChunk(bChunks_100, new CellPos(15, 0, 0));
            var bChunk15015 = AddChunk(bChunks_10_1, new CellPos(15, 0, 15));

            //FillBlocks(bChunk000, new ushort[]{ 3, 4, 5, 6 }, 16, 10, 16);


            FillBlocks(bChunk000, new ushort[]{ 3 }, 16, 1, 16);

            FillBlocks(bChunk1500, new ushort[]{ 3 }, 16, 1, 16);
            FillBlocks(bChunk15015, new ushort[]{ 3 }, 16, 1, 16);

            //            AddBlock(bChunk000, new Pos(1, 1, 1), 5);
            AddBlock(bChunk000, new Pos(0, 1, 0), 5);
            //          AddBlock(bChunk000, new Pos(9, 1, 7), 5);

            AddBlock(bChunk000, new Pos(3, 1, 3), 3);

            bChunk000.UpdateMesh();
            bChunk1500.UpdateMesh();
            bChunk15015.UpdateMesh();
        }

        void SolidChunk()
        {
            var bChunks000 = AddChunks(new ChunksPos(0, 0, 0));

            StartCoroutine(FillChunks(bChunks000, new ushort[]
            {
                3,
                4,
                5
            }, 1, 1, 1));
        }

        void Start()
        {

            //PlatformOneLight();

            SolidChunk();
            //     return;


            //         var bChunks_100 = AddChunks(new ChunksPos(-1, 0, 0));


            //     var bChunks000 = AddChunks(new ChunksPos(0, 0, 0));

            /*
            var bChunks_100 = AddChunks(new ChunksPos(-1, 0, 0));
            var bChunks010 = AddChunks(new ChunksPos(0, 1, 0));
            var bChunks0_10 = AddChunks(new ChunksPos(0, -1, 0));

            var bChunks001 = AddChunks(new ChunksPos(0, 0, 1));
            var bChunks00_1 = AddChunks(new ChunksPos(0, 0, -1));
*/


//            var bChunk000 = AddChunk(bChunks000, new CellPos(0, 0, 0));

            //FillBlocks(bChunk000, new ushort[]{ 3, 4, 5, 6 }, 16, 10, 16);

            /*
             * 
             * /*
            FillBlocks(bChunk000, new ushort[]{ 3 }, 16, 1, 16);


//            AddBlock(bChunk000, new Pos(1, 1, 1), 5);
            AddBlock(bChunk000, new Pos(8, 1, 8), 5);
            //          AddBlock(bChunk000, new Pos(9, 1, 7), 5);

            AddBlock(bChunk000, new Pos(3, 1, 3), 3);

            bChunk000.UpdateMesh();
*/


            /*

            AddBlock(bChunk000, new Pos(7, 1, 8), 5);
            AddBlock(bChunk000, new Pos(8, 1, 8), 5);
            AddBlock(bChunk000, new Pos(9, 1, 8), 5);
*/
//            AddBlock(bChunk000, new Pos(7, 1, 8), 5);
            //          AddBlock(bChunk000, new Pos(7, 1, 7), 5);


            /*
            AddBlock(bChunk000, new Pos(0, 1, 0), 6);


            bChunk000.UpdateMesh();
*/


            /*
            var bChunk1500 = AddChunk(bChunks_100, new CellPos(15, 0, 0));

            var block0 = AddBlock(bChunk000, new Pos(0, 0, 0), 0);


            AddBlock(bChunk000, new Pos(1, 0, 0), 1);
            AddBlock(bChunk000, new Pos(0, 0, 1), 2);
            AddBlock(bChunk000, new Pos(1, 0, 1), 3);


            var block15 = AddBlock(bChunk1500, new Pos(15, 0, 0), 0);
*/



            //bChunk000.cell.AddToNeighbors();
            //bChunk1500.cell.AddToNeighbors();





//            bChunk000.UpdateMesh();
//            bChunk1500.UpdateMesh();






//            bChunk000.UpdateMeshesNeighbours();

            //          AddBlock(bChunk000, new Pos(0, 0, 1), 0);
            //        AddBlock(bChunk000, new Pos(0, 0, 2), 0);

            //       bChunk000.UpdateMesh();
            //     bChunk000.UpdateMeshesNeighbours();
            //       var bChunks00_1 = AddChunks(new ChunksPos(0, 0, -1));


            //        var bChunk15 = AddChunk(bChunks_100, new Pos(Pos.WIDTH - 1, 0, 0));





//            StartCoroutine(FillChunks(bChunks000, new ushort[]{ 3, 4, 5 }, 1, 1, 1));

            /*
            StartCoroutine(FillChunks(bChunks000, new ushort[]
            {
                3,
                4,
                5
            }, 8, 8, 8));
*/

            /*
            StartCoroutine(FillChunks(bChunks_100, new ushort[]
            {
                3,
                3,
                3
            }, 16, 1, 1));

            StartCoroutine(FillChunks(bChunks010, new ushort[]
            {
                3,
                3,
                3
            }, 1, 10, 1));

            StartCoroutine(FillChunks(bChunks0_10, new ushort[]
            {
                3,
                3,
                3
            }, 1, 10, 1));

            StartCoroutine(FillChunks(bChunks001, new ushort[]
            {
                3,
                3,
                3
            }, 1, 1, 16));

            StartCoroutine(FillChunks(bChunks00_1, new ushort[]
            {
                3,
                3,
                3
            }, 1, 1, 16));
            */




//            StartCoroutine(FillChunks(bChunks000, 0, (byte)Pos.WIDTH, 2, (byte)Pos.WIDTH));
            //          StartCoroutine(FillChunks(bChunks_100, 0, (byte)Pos.WIDTH, 2, (byte)Pos.WIDTH));

            //       Debug.Log("started");

            //      FillChunks(bChunks_100, 0, (byte)Pos.WIDTH, (byte)Pos.HEIGHT, (byte)Pos.WIDTH);
            //    FillChunks(bChunks00_1, 0, (byte)Pos.WIDTH, (byte)Pos.HEIGHT, (byte)Pos.WIDTH);

//            bChunks000.UpdateMesh();
            //        bChunks_100.UpdateMesh();
            //      bChunks00_1.UpdateMesh();

//            bChunk0.UpdateMesh();
//            bChunk0.UpdateMeshesNeighbours();




            /*
            var bChunk0 = AddChunk(bChunks0, new Pos(Pos.WIDTH - 1, 0, 0));
            var bChunk1 = AddChunk(bChunks1, new Pos(0, 0, 0));

            FillBlocks(bChunk0, 0);
            FillBlocks(bChunk1, 1);
//            AddBlock(bChunk0, new Pos(Pos.WIDTH - 1, 0, 0), 1);
            //          AddBlock(bChunk1, new Pos(0, 0, 0), 1);

            bChunk0.UpdateMesh(true);
            bChunk1.UpdateMesh(true);

            bChunks0.Save();
            bChunks1.Save();
*/

//            Debug.Log(cell.ToString());
            //          Debug.Log(bChunks0.cell.ToString());
            //        Debug.Log(bChunks1.cell.ToString());

//            AddChunksToBChunks(bChunks0);
            //          AddChunksToBChunks(bChunks1);


            //bChunks0.cell.AddToNeighbors();
            //bChunks1.cell.AddToNeighbors();
            /*
            var str = GetStringCoord(0);


            int l = 65535;
            byte[] bytes = BitConverter.GetBytes(l);

            char[] padding = { '=' };
            var str = Convert.ToBase64String(bytes).TrimEnd(padding).Replace('+', '-').Replace('/', '_');

            Debug.Log(l);

            for (int i = 0; i < bytes.Length; i++)
            {
                Debug.Log(bytes [i]);
            }
            */

            /*
            Debug.Log(ChunksPos.GetStringCoord(0));
            Debug.Log(ChunksPos.GetStringCoord(1));
            Debug.Log(ChunksPos.GetStringCoord(255));
            Debug.Log(ChunksPos.GetStringCoord(65535));
            Debug.Log(ChunksPos.GetStringCoord((long)65535 * (long)65535));
            Debug.Log(ChunksPos.GetStringCoord(long.MaxValue));
            Debug.Log(ChunksPos.GetStringCoord(-1));
            Debug.Log(ChunksPos.GetStringCoord(-255));
            Debug.Log(ChunksPos.GetStringCoord(long.MinValue + 1));
            */
//            str = (l >= 0 ? "p" : "n") + str;

            //   AddChunks();
        }


        IEnumerator FillChunks(BChunks bChunks, ushort[] types, byte width = (byte)Pos.WIDTH, byte height = (byte)Pos.HEIGHT, byte depth = (byte)Pos.WIDTH)
        {
            for (byte x = 0; x < width; x++)
                for (byte y = 0; y < height; y++)
                    for (byte z = 0; z < depth; z++)
                    {
                        var bChunk = AddChunk(bChunks, new CellPos(x, y, z));

                        FillBlocks(bChunk, types);

                        bChunk.UpdateMesh();
                        bChunk.UpdateMeshesNeighbours();

                        yield return null;// new WaitForSeconds(0.1f);
                        //bChunk.UpdateMesh();
                        //bChunk.UpdateMeshesNeighbours();
                    }

            //     Debug.Log("save");
            //   bChunks.Save();
        }

        void FillBlocks(BChunk bChunk, ushort[] types, byte width = (byte)Pos.WIDTH, byte height = (byte)Pos.HEIGHT, byte depth = (byte)Pos.WIDTH)
        {
            for (byte x = 0; x < width; x++)
                for (byte y = 0; y < height; y++)
                    for (byte z = 0; z < depth; z++)
                    {
                        AddBlock(bChunk, new Pos(x, y, z), types [(ushort)UnityEngine.Random.Range(0, types.Length)]);
                    }
        }


        void Update()
        {
	
        }

        BChunks AddChunks(ChunksPos chunkPos)
        {
            var t = Instantiate(chunks);
            t.position = chunkPos.Vector3;
            t.parent = this.transform;
            //      t.gameObject.isStatic = true;

            var bChunks = t.GetComponent<BChunks>();

            bChunks.chunksPos = chunkPos;
            bChunks.cell = new ChunkCell(new CellPos(bChunks.CenterPos.x, bChunks.CenterPos.y, bChunks.CenterPos.z), cell, bChunks);

            //     bChunks.cell.AddToNeighbors();

            return bChunks;
        }

        BChunk AddChunk(BChunks bChunks, CellPos pos)
        {
            var bChunk = bChunks.AddChunk(pos);


            //bChunk.cell.AddToNeighbors();

            return bChunk;
            /*
            bChunk.AddBlock(new Pos(0, 0, 0), 0, false);
            bChunk.AddBlock(new Pos(1, 0, 0), 0, false);
            bChunk.AddBlock(new Pos(1, 0, 1), 0, false);
            bChunk.AddBlock(new Pos(2, 0, 0), 0, false);

            bChunk.AddBlock(new Pos(Pos.WIDTH - 1, 0, 0), 0, false);

            var bChunk1 = bChunks.AddChunk(new Pos(1, 0, 0));
            bChunk1.AddBlock(new Pos(0, 0, 0), 0, false);

            var bChunk2 = bChunks.AddChunk(new Pos(Pos.WIDTH - 1, 0, 0));
            bChunk2.AddBlock(new Pos(Pos.WIDTH - 1, 0, 0), 0, false);

            bChunk.cell.AddToNeighbors();
            bChunk1.cell.AddToNeighbors();
            bChunk2.cell.AddToNeighbors();

            bChunk.UpdateBlocks();
            bChunk1.UpdateBlocks();
            bChunk2.UpdateBlocks();

            bChunk.UpdateMesh(false);
            bChunk1.UpdateMesh(false);
            bChunk2.UpdateMesh(false);

            bChunks.Save();
*/
        }

        Block AddBlock(BChunk bChunk, Pos pos, ushort type)
        {
            var block = bChunk.AddBlock(pos, type, false);

            return block;
        }

        /*
        BChunks AddChunksToBChunks(BChunks bChunks)
        {
            var bChunk = bChunks.AddChunk(new Pos(0, 0, 0));

            bChunk.AddBlock(new Pos(0, 0, 0), 0, false);
            bChunk.AddBlock(new Pos(1, 0, 0), 0, false);
            bChunk.AddBlock(new Pos(1, 0, 1), 0, false);
            bChunk.AddBlock(new Pos(2, 0, 0), 0, false);

            bChunk.AddBlock(new Pos(Pos.WIDTH - 1, 0, 0), 0, false);

            var bChunk1 = bChunks.AddChunk(new Pos(1, 0, 0));
            bChunk1.AddBlock(new Pos(0, 0, 0), 0, false);

            var bChunk2 = bChunks.AddChunk(new Pos(Pos.WIDTH - 1, 0, 0));
            bChunk2.AddBlock(new Pos(Pos.WIDTH - 1, 0, 0), 0, false);

            bChunk.cell.AddToNeighbors();
            bChunk1.cell.AddToNeighbors();
            bChunk2.cell.AddToNeighbors();

            bChunk.UpdateBlocks();
            bChunk1.UpdateBlocks();
            bChunk2.UpdateBlocks();

            bChunk.UpdateMesh(false);
            bChunk1.UpdateMesh(false);
            bChunk2.UpdateMesh(false);

            bChunks.Save();

            return bChunks;
        }
        */

        void Load()
        {
        
        }
    }


}

[Serializable]
class BWorldData
{
    public string name;
    public long time;

    public long x;
    public long y;
    public long z;

    //DateTime.Now.Ticks;
    public Vector3 pos;

    public FileData[] files;
}

[Serializable]
class FileData
{
    public string name;
    public long time;

    public long x;
    public long y;
    public long z;
}