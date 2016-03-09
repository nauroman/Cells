using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//using UnityEditor;

namespace Flashunity.Cells
{
    public class Cell
    {
        public readonly object owner;

        public Cell parent;
        public readonly SortedList<ushort, Cell> neighbors;
        public readonly SortedList<ushort, Cell> children;

        public BinaryGrid childrenBinaryGrid;

        public readonly CellPos pos;

        public Cell back;
        public Cell front;
        public Cell top;
        public Cell bottom;
        public Cell left;
        public Cell right;

        //protected Greed childrenGreed;


        //public Cell(Pos pos, SortedList<ushort, Cell> neighbors, object owner)
        //        public Cell(Pos pos, SortedList<ushort, Cell> neighbors, Cell parent, object owner)
        public Cell(CellPos pos, Cell parent, object owner)
        {
            this.pos = pos;
            this.parent = parent;

            /*
            if (parent == null)
            {
                neighbors = new SortedList<ushort, Cell>();
                neighborsBinaryGrid = new BinaryGrid((byte)Pos.WIDTH, (byte)Pos.HEIGHT);
            } else
            {
                neighbors = parent.children;
                neighborsBinaryGrid = parent.neighborsBinaryGrid;
            }
            */

            childrenBinaryGrid = new BinaryGrid((byte)Pos.WIDTH, (byte)Pos.HEIGHT);


            this.neighbors = parent == null ? new SortedList<ushort, Cell>() : parent.children;

            this.owner = owner == null ? this : owner;
            children = new SortedList<ushort, Cell>();

            neighbors [pos.index] = this;
//            neighborsBinaryGrid.Set(pos.x, pos.y, pos.z);
            if (parent != null)
                parent.childrenBinaryGrid.Set(pos.x, pos.y, pos.z);

            AddNeighbors();
            AddToNeighbors();
        }

        protected Cell FindCellInNeighbours(byte x, byte y, byte z, ushort posIndex)
        {
            Cell cell;

            if (parent == null)
            {
                if (neighbors.TryGetValue(posIndex, out cell))
                    return cell;
            } else if (parent.childrenBinaryGrid.IsSet(x, y, z) && neighbors.TryGetValue(posIndex, out cell))
                return cell;

            return null;
        }

        public Cell FindCellInChildren(byte x, byte y, byte z, ushort posIndex)
        {
            Cell cell;

            if (childrenBinaryGrid.IsSet(x, y, z) && children.TryGetValue(posIndex, out cell))
                return cell;

            return null;
        }

        protected virtual void AddNeighbors()
        {
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
        }


        public void AddToNeighbors()
        {

            if (back != null)// && back.front == null)
            {
                back.front = this;
            }

            if (front != null)// && front.back == null)
            {
                front.back = this;
            }

            if (top != null)// && top.bottom == null)
            {
                top.bottom = this;
            }

            if (bottom != null)// && bottom.top == null)
            {
                bottom.top = this;
            }

            if (left != null)// && left.right == null)
            {
                left.right = this;
            }

            if (right != null)// && right.left == null)
            {
                right.left = this;
            }
        }

        public void RemoveFromNeighbors()
        {
            neighbors.Remove(pos.index);

            if (back != null && back.front == this)
            {
                back.front = null;
            }

            if (front != null && front.back == this)
            {
                front.back = null;
            }

            if (top != null && top.bottom == this)
            {
                top.bottom = null;
            }

            if (bottom != null && bottom.top == this)
            {
                bottom.top = null;
            }

            if (left != null && left.right == this)
            {
                left.right = null;
            }

            if (right != null && right.left == this)
            {
                right.left = null;
            }
        }

        public void RemoveFromGreed()
        {
            parent.childrenBinaryGrid.Reset(pos.x, pos.y, pos.z);
        }


        public new string ToString()
        {
            string str = "Cell:";
            if (owner != null)
                str += Environment.NewLine + "owner: " + owner.ToString();

            if (parent != null)
                str += Environment.NewLine + "parent: " + parent.ToString();

            if (pos != null)
                str += Environment.NewLine + "pos: " + pos.ToString();

            if (back != null)
                str += Environment.NewLine + "back";
           
            if (front != null)
                str += Environment.NewLine + "front";

            if (top != null)
                str += Environment.NewLine + "top";

            if (bottom != null)
                str += Environment.NewLine + "bottom";

            if (left != null)
                str += Environment.NewLine + "left";

            if (right != null)
                str += Environment.NewLine + "right";
            
            str += Environment.NewLine + "neighbors.Count: " + neighbors.Count.ToString();
            str += Environment.NewLine + "children.Count: " + children.Count.ToString();

            return str;
        }
        /*
        byte[] GetByteArray()
        {
            var list = new List<byte>();

            // Int16 version = 0;

            //   AddBytesToList(ref list, ref BitConverter.GetBytes(version));

            return list.ToArray();
        }
        */
    }
}