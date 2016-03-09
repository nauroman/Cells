using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Flashunity.Cells;

namespace Flashunity.Cells
{

    public class ChunkCell : Cell
    {
        public readonly BinaryGrid childrenOpacityBinaryGreed;
        public readonly Dictionary<byte, LightType> childrenLightsTypes;

        ChunkCell[] neighborsArray;

        public ChunkCell(CellPos pos, Cell parent, object owner) : base(pos, parent, owner)
        {
            childrenOpacityBinaryGreed = new BinaryGrid((byte)Pos.WIDTH, (byte)Pos.HEIGHT);
            childrenLightsTypes = new Dictionary<byte, LightType>();

            if (neighborsArray == null)
                neighborsArray = new ChunkCell[27];
        }

        public void AddLight(Light light, Pos pos)
        {
            if (!childrenLightsTypes.ContainsKey(light.type))
            {
                var lightType = new LightType(light.type, light.color32, light.distance);
                lightType.binaryGrid.Set(pos.x, pos.y, pos.z);
                childrenLightsTypes.Add(light.type, lightType);
            } else
            {
                childrenLightsTypes [light.type].binaryGrid.Set(pos.x, pos.y, pos.z);  
            }
        }





        protected override void AddNeighbors()
        {
            neighborsArray = new ChunkCell[27];

            var x = pos.x;
            var y = pos.y;
            var z = pos.z;

            if (pos.edge)
            {
                if (z == Pos.WIDTH - 1)
                {
                    if (parent != null && parent.back != null)
                        back = parent.back.FindCellInChildren(x, y, 0, pos.indexBack);
                    else
                        back = null;
                } else
                    back = FindCellInNeighbours(x, y, (byte)(z + 1), pos.indexBack);

                if (z == 0)
                {
                    if (parent != null && parent.front != null)
                        front = parent.front.FindCellInChildren(x, y, Pos.WIDTH - 1, pos.indexFront);
                    else
                        front = null;
                } else
                    front = FindCellInNeighbours(x, y, (byte)(z - 1), pos.indexFront); 

                if (y == Pos.HEIGHT - 1)
                {
                    if (parent != null && parent.top != null)
                        top = parent.top.FindCellInChildren(x, 0, z, pos.indexTop);
                    else
                        top = null;
                } else
                    top = FindCellInNeighbours(x, (byte)(y + 1), z, pos.indexTop); 

                if (y == 0)
                {
                    if (parent != null && parent.bottom != null)
                        bottom = parent.bottom.FindCellInChildren(x, Pos.HEIGHT - 1, z, pos.indexBottom);
                    else
                        bottom = null;
                } else
                    bottom = FindCellInNeighbours(x, (byte)(y - 1), z, pos.indexBottom); 

                if (x == 0)
                {
                    if (parent != null && parent.left != null)
                        left = parent.left.FindCellInChildren(Pos.WIDTH - 1, y, z, pos.indexLeft);
                    else
                        left = null;
                } else
                    left = FindCellInNeighbours((byte)(x - 1), y, z, pos.indexLeft); 

                if (x == Pos.WIDTH - 1)
                {
                    if (parent != null && parent.right != null)
                        right = parent.right.FindCellInChildren(0, y, z, pos.indexRight);
                    else
                        right = null;
                } else
                    right = FindCellInNeighbours((byte)(x + 1), y, z, pos.indexRight); 

            } else
            {
                back = FindCellInNeighbours(x, y, (byte)(z + 1), pos.indexBack);
                front = FindCellInNeighbours(x, y, (byte)(z - 1), pos.indexFront); 
                top = FindCellInNeighbours(x, (byte)(y + 1), z, pos.indexTop); 
                bottom = FindCellInNeighbours(x, (byte)(y - 1), z, pos.indexBottom); 
                left = FindCellInNeighbours((byte)(x - 1), y, z, pos.indexLeft); 
                right = FindCellInNeighbours((byte)(x + 1), y, z, pos.indexRight); 
            }

            UpdateNeighborsArray();
        }

        int GetIndexInNeighborsArray(int x, int y, int z)
        {
            //return (x + 1) + (z + 1) * 3 + (y + 1) * 9;
            return x + z * 3 + y * 9 + 13;
        }

        void UpdateNeighborsArray()
        {
            neighborsArray [GetIndexInNeighborsArray(0, 0, 0)] = this;
            neighborsArray [GetIndexInNeighborsArray(0, 0, 1)] = back as ChunkCell;
            neighborsArray [GetIndexInNeighborsArray(0, 0, -1)] = front as ChunkCell;
            neighborsArray [GetIndexInNeighborsArray(0, 1, 0)] = top as ChunkCell;
            neighborsArray [GetIndexInNeighborsArray(0, -1, 0)] = bottom as ChunkCell;
            neighborsArray [GetIndexInNeighborsArray(-1, 0, 0)] = left as ChunkCell;
            neighborsArray [GetIndexInNeighborsArray(1, 0, 0)] = right as ChunkCell;
        }

        public ChunkCell GetNeighbor(int x, int y, int z)
        {
            return neighborsArray [GetIndexInNeighborsArray(x, y, z)];
        }

    }
}