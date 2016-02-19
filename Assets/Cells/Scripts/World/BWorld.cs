using UnityEngine;
using System.Collections;
using System;
using Flashunity.Logs;

namespace Flashunity.Cells
{
    public class BWorld : MonoBehaviour
    {
        [SerializeField]
        Transform chunks;

        Cell cell;

        void Awake()
        {
            cell = new Cell(new Pos(0, 0, 0), null, this);
        }

        void Start()
        {
            var bChunks_100 = AddChunks(new ChunksPos(-1, 0, 0));
            var bChunks000 = AddChunks(new ChunksPos(0, 0, 0));
            var bChunks00_1 = AddChunks(new ChunksPos(0, 0, -1));


            var bChunk15 = AddChunk(bChunks_100, new Pos(Pos.WIDTH - 1, 0, 0));

            FillBlocks(bChunk15, 0);

//            FillChunks(bChunks000, 0, (byte)Pos.WIDTH, (byte)Pos.HEIGHT, (byte)Pos.WIDTH);
            FillChunks(bChunks000, 0, (byte)Pos.WIDTH, 2, (byte)Pos.WIDTH);
            //      FillChunks(bChunks_100, 0, (byte)Pos.WIDTH, (byte)Pos.HEIGHT, (byte)Pos.WIDTH);
            //    FillChunks(bChunks00_1, 0, (byte)Pos.WIDTH, (byte)Pos.HEIGHT, (byte)Pos.WIDTH);

            bChunks000.UpdateMesh();
            bChunks_100.UpdateMesh();
            bChunks00_1.UpdateMesh();

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


        void FillChunks(BChunks bChunks, ushort type, byte width = (byte)Pos.WIDTH, byte height = (byte)Pos.HEIGHT, byte depth = (byte)Pos.WIDTH)
        {
            for (byte x = 0; x < width; x++)
                for (byte y = 0; y < height; y++)
                    for (byte z = 0; z < depth; z++)
                    {
                        var bChunk = AddChunk(bChunks, new Pos(x, y, z));

                        FillBlocks(bChunk, type);
                        //bChunk.UpdateMesh();
                        //bChunk.UpdateMeshesNeighbours();
                    }
        }

        void FillBlocks(BChunk bChunk, ushort type, byte width = (byte)Pos.WIDTH, byte height = (byte)Pos.HEIGHT, byte depth = (byte)Pos.WIDTH)
        {
            for (byte x = 0; x < width; x++)
                for (byte y = 0; y < height; y++)
                    for (byte z = 0; z < depth; z++)
                        AddBlock(bChunk, new Pos(x, y, z), type);
        }

        void Update()
        {
	
        }

        BChunks AddChunks(ChunksPos chunkPos)
        {
            var t = Instantiate(chunks);
            t.position = chunkPos.Vector3;
            t.parent = this.transform;

            var bChunks = t.GetComponent<BChunks>();

            bChunks.chunksPos = chunkPos;
            bChunks.cell = new Cell(bChunks.Pos, cell, bChunks);

            //     bChunks.cell.AddToNeighbors();

            return bChunks;
        }

        BChunk AddChunk(BChunks bChunks, Pos pos)
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