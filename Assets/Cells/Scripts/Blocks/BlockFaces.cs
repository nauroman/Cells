using UnityEngine;
using System.Collections;
using Flashunity.Cells;
using System;

public class BlockFaces
{
    public BlockFace[] blockFaces;

    public int count = -1;

    public void CacheFaces(bool[] faces, int textureIndex, FaceLight[][] faceLights, FaceDirection[] shadedEdges)
    //public void CacheFaces(bool[] faces, int textureIndex)
    {
        if (faces.Length != 6)
            throw new ArgumentException("faces.Length should be 6 in this order back, front, top, bottom, left, right", "faces");


        if (faceLights.Length != 6)
            throw new ArgumentException("faceLights.Length should be 6 in this order back, front, top, bottom, left, right", "faceLights");
        
        blockFaces = new BlockFace[6];

        count = 0;

        for (int i = 0; i < 6; i++)
        {
            if (faces [i])
            {
                blockFaces [i] = new BlockFace((FaceDirection)i, textureIndex, faceLights [i], shadedEdges);
                count++;
            } else
                blockFaces [i] = null;            
        }
    }

    public void CacheFaces(int textureIndex, float size)
    {
        blockFaces = new BlockFace[6];

        count = 6;

        for (int i = 0; i < 6; i++)
        {
            blockFaces [i] = new BlockFace((FaceDirection)i, textureIndex, size);
        }
    }

    public void FreeCahcedFaces()
    {
        count = 0;
        blockFaces = new BlockFace[6];

        for (int i = 0; i < 6; i++)
        {
            blockFaces [i] = null;
        }
    }

    //    public BlockFace GetFace(FaceDirection faceDirection, int textureIndex, FaceLight[] faceLights, FaceDirection[] shadedEdges, float size)
    public BlockFace GetFace(FaceDirection faceDirection, int textureIndex, FaceLight[] faceLights, FaceDirection[] shadedEdges, float size)
    //public BlockFace GetFace(FaceDirection faceDirection, int textureIndex, float size)
    {
        if (blockFaces != null)
            return blockFaces [(int)faceDirection];
        else
        {
            if (size != 0)
                return new BlockFace(faceDirection, textureIndex, size);
            else
                return new BlockFace(faceDirection, textureIndex, faceLights, shadedEdges);
        }
    }

    public override string ToString()
    {
        return "count: " + count.ToString();
    }


}
