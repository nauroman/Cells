using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Flashunity.Cells
{
    public class Cell
    {
        public readonly object owner;

        public Cell parent;
        public readonly SortedList<ushort, Cell> neighbors;
        public readonly SortedList<ushort, Cell> children;

        public readonly Pos pos;

        public Cell back;
        public Cell front;
        public Cell top;
        public Cell bottom;
        public Cell left;
        public Cell right;


        //public Cell(Pos pos, SortedList<ushort, Cell> neighbors, object owner)
        //        public Cell(Pos pos, SortedList<ushort, Cell> neighbors, Cell parent, object owner)
        public Cell(Pos pos, Cell parent, object owner)
        {
            this.pos = pos;
            this.parent = parent;
            //this.neighbors = neighbors == null ? new SortedList<ushort, Cell>() : neighbors;
            this.neighbors = parent == null ? new SortedList<ushort, Cell>() : parent.children;
            this.owner = owner == null ? this : owner;
            children = new SortedList<ushort, Cell>();

            this.neighbors [pos.index] = this;

            AddNeighbors();
            AddToNeighbors();
        }

        void AddNeighbors()
        {
            SortedList<ushort, Cell> nBack = neighbors;
            SortedList<ushort, Cell> nFront = neighbors;
            SortedList<ushort, Cell> nTop = neighbors;
            SortedList<ushort, Cell> nBottom = neighbors;
            SortedList<ushort, Cell> nLeft = neighbors;
            SortedList<ushort, Cell> nRight = neighbors;

            if (pos.edge && parent != null)
            {                
                if (pos.z == Pos.WIDTH - 1)
                {
                    if (parent.back != null)
                        nBack = parent.back.children;
                    else
                        nBack = null;
                }
/*                

                    if (parent != null && parent.back != null)
                        n = parent.back.neighbors;
                        parent.back.neighbors.TryGetValue(pos.indexBack, out back);
                    else
                        back = null;
                } 
                */

                if (pos.z == 0)
                {
                    if (parent.front != null)
                        nFront = parent.front.children;
                    else
                        nFront = null;
                }
                /*
                    if (parent != null && parent.front != null)
                        parent.front.neighbors.TryGetValue(pos.indexFront, out front);
                    else
                        front = null;
                }
                */

                if (pos.y == Pos.HEIGHT - 1)
                {
                    if (parent.top != null)
                        nTop = parent.top.children;
                    else
                        nTop = null;
                }
                /*
                    if (parent != null && parent.top != null)
                        parent.top.neighbors.TryGetValue(pos.indexTop, out top);
                    
//                        top = parent.top.neighbors [pos.indexTop] as Cell;
                    else
                        top = null;
                } 
                */

                if (pos.y == 0)
                {
                    if (parent.bottom != null)
                        nBottom = parent.bottom.children;
                    else
                        nBottom = null;

                }
                /*    
                    if (parent != null && parent.bottom != null)
                        parent.bottom.neighbors.TryGetValue(pos.indexBottom, out bottom);
                    
                    //    bottom = parent.bottom.neighbors [pos.indexBottom] as Cell;
                    else
                        bottom = null;
                } 
                */

                if (pos.x == 0)
                {
                    if (parent.left != null)
                        nLeft = parent.left.children;
                    else
                        nLeft = null;
                }
                /*
                    if (parent != null && parent.left != null)
                        parent.left.neighbors.TryGetValue(pos.indexLeft, out left);
                    
//                        left = parent.left.neighbors [pos.indexLeft] as Cell;
                    else
                        left = null;
                } 
*/
                if (pos.x == Pos.WIDTH - 1)
                {
                    if (parent.right != null)
                        nRight = parent.right.children;
                    else
                        nRight = null;
                }
                /*
                    if (parent != null && parent.right != null)
                        parent.right.neighbors.TryGetValue(pos.indexRight, out right);
                    
                    //    right = parent.right.neighbors [pos.indexRight] as Cell;
                    else
                        right = null;
                }
                */

            }

            if (nBack == null || !nBack.TryGetValue(pos.indexBack, out back))
                back = null;
            if (nFront == null || !nFront.TryGetValue(pos.indexFront, out front))
                front = null;
            if (nTop == null || !nTop.TryGetValue(pos.indexTop, out top))
                top = null;
            if (nBottom == null || !nBottom.TryGetValue(pos.indexBottom, out bottom))
                bottom = null;
            if (nLeft == null || !nLeft.TryGetValue(pos.indexLeft, out left))
                left = null;
            if (nRight == null || !nRight.TryGetValue(pos.indexRight, out right))
                right = null;

        }


        public void AddToNeighbors()
        {
            if (back != null && back.front == null)
            {
                back.front = this;
            }

            if (front != null && front.back == null)
            {
                front.back = this;
            }

            if (top != null && top.bottom == null)
            {
                top.bottom = this;
            }

            if (bottom != null && bottom.top == null)
            {
                bottom.top = this;
            }

            if (left != null && left.right == null)
            {
                left.right = this;
            }

            if (right != null && right.left == null)
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