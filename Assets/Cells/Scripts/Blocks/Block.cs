using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Flashunity.Logs;
using System;

namespace Flashunity.Cells
{
    public class Block
    {
        public Pos pos;

        public ushort type;
        public readonly ushort textureIndex;

        public ChunkCell parent;

        public readonly bool opacity;

        protected BlockFaces blockFaces;
        protected int facesCount;

        Transform transform;



        //       public object MyProperty { get; }

        public Block(Pos pos, ChunkCell parent, ushort type, ushort textureIndex, bool opacity)
        {
            this.pos = pos;
            this.parent = parent;

            this.type = type;
            this.textureIndex = textureIndex;

            this.opacity = opacity;

            parent.childrenBinaryGrid.Set(pos.x, pos.y, pos.z);

            if (opacity)
                parent.childrenOpacityBinaryGreed.Set(pos.x, pos.y, pos.z);
            
            blockFaces = new BlockFaces();
        }

        /*
        public Block(Pos pos, ChunkCell parent, ushort type, ushort textureIndex, Light light)
        {
            this.pos = pos;
            this.parent = parent;

            this.type = type;
            this.textureIndex = textureIndex;

            this.opacity = false;

            this.light = light;

            parent.childrenBinaryGrid.Set(pos.x, pos.y, pos.z);

            parent.AddLight(light, pos);

            blockFaces = new BlockFaces();
        }
        */

        public virtual void UpdateFaces()
        {

            var greed = parent.childrenOpacityBinaryGreed;

            var x = pos.x;
            var y = pos.y;
            var z = pos.z;

            bool[] visibleFaces;

            if (pos.edge)
            {
                visibleFaces = new bool[6];
                    
                if (z == Pos.WIDTH - 1)
                {
                    if (parent.back != null)
                        visibleFaces [0] = !((parent.back as ChunkCell).childrenOpacityBinaryGreed.IsSet(x, y, 0));
                    else
                        visibleFaces [0] = true;
                } else
                    visibleFaces [0] = !greed.IsSet(x, y, (byte)(z + 1));

                if (z == 0)
                {
                    if (parent.front != null)
                        visibleFaces [1] = !((parent.front as ChunkCell).childrenOpacityBinaryGreed.IsSet(x, y, (byte)(Pos.WIDTH - 1)));
                    else
                        visibleFaces [1] = true;
                } else
                    visibleFaces [1] = !greed.IsSet(x, y, (byte)(z - 1));

                if (y == Pos.HEIGHT - 1)
                {
                    if (parent.top != null)
                        visibleFaces [2] = !((parent.top as ChunkCell).childrenOpacityBinaryGreed.IsSet(x, 0, z));
                    else
                        visibleFaces [2] = true;
                    
                } else
                    visibleFaces [2] = !greed.IsSet(x, (byte)(y + 1), z);

                if (y == 0)
                {
                    if (parent.bottom != null)
                        visibleFaces [3] = !((parent.bottom as ChunkCell).childrenOpacityBinaryGreed.IsSet(x, (byte)(Pos.HEIGHT - 1), z));
                    else
                        visibleFaces [3] = true;
                    
                } else
                    visibleFaces [3] = !greed.IsSet(x, (byte)(y - 1), z);

                if (x == 0)
                {
                    if (parent.left != null)
                        visibleFaces [4] = !((parent.left as ChunkCell).childrenOpacityBinaryGreed.IsSet((byte)(Pos.WIDTH - 1), y, z));
                    else
                        visibleFaces [4] = true;
                    
                } else
                    visibleFaces [4] = !greed.IsSet((byte)(x - 1), y, z);
                
                if (x == Pos.WIDTH - 1)
                {
                    if (parent.right != null)
                        visibleFaces [5] = !((parent.right as ChunkCell).childrenOpacityBinaryGreed.IsSet(0, y, z));
                    else
                        visibleFaces [5] = true;
                    
                } else
                    visibleFaces [5] = !greed.IsSet((byte)(x + 1), y, z);
                
            } else
            {
                visibleFaces = new bool[]
                {
                    !greed.IsSet(x, y, (byte)(z + 1)),
                    !greed.IsSet(x, y, (byte)(z - 1)),
                    !greed.IsSet(x, (byte)(y + 1), z),
                    !greed.IsSet(x, (byte)(y - 1), z),
                    !greed.IsSet((byte)(x - 1), y, z),
                    !greed.IsSet((byte)(x + 1), y, z)
                };
            }
                
            blockFaces.CacheFaces(visibleFaces, textureIndex, GetFacesLights(visibleFaces), new FaceDirection[0]);

            facesCount = blockFaces.count;
        }




        FaceLight[][] GetFacesLights(bool[] faces)
        {
            FaceLight[][] facesLights = new FaceLight[6][];

            if (faces [0])
                facesLights [0] = GetFaceLightsBack();

            if (faces [1])
                facesLights [1] = GetFaceLightsFront();
            
            if (faces [2])
                facesLights [2] = GetFaceLightsTop();

            if (faces [3])
                facesLights [3] = GetFaceLightsBottom();

            if (faces [4])
                facesLights [4] = GetFaceLightsLeft();

            if (faces [5])
                facesLights [5] = GetFaceLightsRight();
            
            return facesLights;
        }

        byte GetMaxLightDistance()
        {
            return 3;
        }

        FaceLight[] GetFaceLightsBack()
        {
            List<FaceLight> faceLighs = new List<FaceLight>();

            int maxDistance = GetMaxLightDistance();

            byte posx = pos.x;
            byte posy = pos.y;
            byte posz = pos.z;

            for (int x = posx - maxDistance; x <= posx + maxDistance; x++)
                for (int y = posy - maxDistance; y <= posy + maxDistance; y++)
                    for (int z = posz + 1; z <= posz + maxDistance; z++)
                    {
                        FaceLight[] fls = GetFaceLights(x, y, z);

                        for (int i = 0; i < fls.Length; i++)
                        {
                            FaceLight faceLight = fls [i];
                            faceLighs.Add(faceLight);
                        }
                    }   

            return faceLighs.ToArray();
        }

        FaceLight[] GetFaceLightsFront()
        {
            List<FaceLight> faceLighs = new List<FaceLight>();

            int maxDistance = GetMaxLightDistance();

            byte posx = pos.x;
            byte posy = pos.y;
            byte posz = pos.z;

            for (int x = posx - maxDistance; x <= posx + maxDistance; x++)
                for (int y = posy - maxDistance; y <= posy + maxDistance; y++)
                    for (int z = posz - maxDistance; z <= posz - 1; z++)
                    {
                        FaceLight[] fls = GetFaceLights(x, y, z);

                        for (int i = 0; i < fls.Length; i++)
                        {
                            FaceLight faceLight = fls [i];
                            faceLighs.Add(faceLight);
                        }
                    }   

            return faceLighs.ToArray();
        }

        FaceLight[] GetFaceLightsTop()
        {
            List<FaceLight> faceLighs = new List<FaceLight>();

            int maxDistance = GetMaxLightDistance();

            byte posx = pos.x;
            byte posy = pos.y;
            byte posz = pos.z;

            for (int x = posx - maxDistance; x <= posx + maxDistance; x++)
                for (int y = posy + 1; y <= posy + maxDistance; y++)
                    for (int z = posz - maxDistance; z <= posz + maxDistance; z++)
                    {
                        FaceLight[] fls = GetFaceLights(x, y, z);

                        for (int i = 0; i < fls.Length; i++)
                        {
                            FaceLight faceLight = fls [i];
                            faceLighs.Add(faceLight);
                        }
                    }   

            return faceLighs.ToArray();
        }

        FaceLight[] GetFaceLightsBottom()
        {
            List<FaceLight> faceLighs = new List<FaceLight>();

            int maxDistance = GetMaxLightDistance();

            byte posx = pos.x;
            byte posy = pos.y;
            byte posz = pos.z;

            for (int x = posx - maxDistance; x <= posx + maxDistance; x++)
                for (int y = posy - maxDistance; y <= posy - 1; y++)
                    for (int z = posz - maxDistance; z <= posz + maxDistance; z++)
                    {
                        FaceLight[] fls = GetFaceLights(x, y, z);

                        for (int i = 0; i < fls.Length; i++)
                        {
                            FaceLight faceLight = fls [i];
                            faceLighs.Add(faceLight);
                        }
                    }   

            return faceLighs.ToArray();
        }

        FaceLight[] GetFaceLightsLeft()
        {
            List<FaceLight> faceLighs = new List<FaceLight>();

            int maxDistance = GetMaxLightDistance();

            byte posx = pos.x;
            byte posy = pos.y;
            byte posz = pos.z;

            for (int x = posx - maxDistance; x <= posx - 1; x++)
                for (int y = posy - maxDistance; y <= posy + maxDistance; y++)
                    for (int z = posz - maxDistance; z <= posz + maxDistance; z++)
                    {
                        FaceLight[] fls = GetFaceLights(x, y, z);

                        for (int i = 0; i < fls.Length; i++)
                        {
                            FaceLight faceLight = fls [i];
                            faceLighs.Add(faceLight);
                        }
                    }   

            return faceLighs.ToArray();
        }

        FaceLight[] GetFaceLightsRight()
        {
            List<FaceLight> faceLighs = new List<FaceLight>();

            int maxDistance = GetMaxLightDistance();

            byte posx = pos.x;
            byte posy = pos.y;
            byte posz = pos.z;

            for (int x = posx + 1; x <= posx + maxDistance; x++)
                for (int y = posy - maxDistance; y <= posy + maxDistance; y++)
                    for (int z = posz - maxDistance; z <= posz + maxDistance; z++)
                    {
                        FaceLight[] fls = GetFaceLights(x, y, z);

                        for (int i = 0; i < fls.Length; i++)
                        {
                            FaceLight faceLight = fls [i];
                            faceLighs.Add(faceLight);
                        }
                    }   

            return faceLighs.ToArray();
        }

        FaceLight[] GetFaceLights(int x, int y, int z)
        {
            var faceLights = new List<FaceLight>();

            int parentX = x;
            int parentY = y;
            int parentZ = z;

            var parent = GetParent(ref parentX, ref parentY, ref parentZ);

            if (parent != null)
            {
//            if (x >= 0 && x < Pos.WIDTH && y >= 0 && y < Pos.HEIGHT && z >= 0 && z < Pos.WIDTH)
                //          {
                foreach (var pair in parent.childrenLightsTypes)
                {
                    var lightType = pair.Value;
                    var grid = lightType.binaryGrid;

                    if (grid.IsSet((byte)parentX, (byte)parentY, (byte)parentZ))
                    {
                        faceLights.Add(new FaceLight(new Vector3(x - pos.x + 0.5f, y - pos.y + 0.5f, z - pos.z + 0.5f), lightType.color32));
                    }
                }
            }

            return faceLights.ToArray();
        }

        ChunkCell GetParent(ref int x, ref int y, ref int z)
        {
            
            int parentX = 0;
            int parentY = 0;
            int parentZ = 0;

            if (z >= Pos.WIDTH)
            {
                z = z - Pos.WIDTH;
                parentZ = 1;
            } else if (z < 0)
            {
                z = Pos.WIDTH + z;
                parentZ = -1;
            }

            if (y >= Pos.HEIGHT)
            {
                y = y - Pos.HEIGHT;
                parentY = 1;
            } else if (y < 0)
            {
                y = Pos.HEIGHT + y;
                parentY = -1;
            }

            if (x >= Pos.WIDTH)
            {
                x = x - Pos.WIDTH;
                parentX = 1;
            } else if (x < 0)
            {
                x = Pos.WIDTH + x;
                parentX = -1;
            }

            return parent.GetNeighbor(parentX, parentY, parentZ);


            /*
            if (x >= 0 && x < Pos.WIDTH && y >= 0 && y < Pos.HEIGHT && z >= 0 && z < Pos.WIDTH)
            {                
                return parent;
            } 

            if (parent != null)
            {
                if (x >= 0 && x < Pos.WIDTH && y >= 0 && y < Pos.HEIGHT && z >= Pos.WIDTH)
                {
                    z = z - Pos.WIDTH;
                    return parent.back as ChunkCell;
                } 

                if (x >= 0 && x < Pos.WIDTH && y >= 0 && y < Pos.HEIGHT && z < 0)
                {
                    z = Pos.WIDTH + z;
                    return parent.front as ChunkCell;
                } 

                if (x >= 0 && x < Pos.WIDTH && y >= Pos.HEIGHT && z >= 0 && z < Pos.WIDTH)
                {
                    y = y - Pos.HEIGHT;
                    return parent.top as ChunkCell;
                } 

                if (x >= 0 && x < Pos.WIDTH && y < 0 && z >= 0 && z < Pos.WIDTH)
                {
                    y = Pos.HEIGHT + y;
                    return parent.bottom as ChunkCell;
                } 
                    
                if (x < 0 && y >= 0 && y < Pos.HEIGHT && z >= 0 && z < Pos.WIDTH)
                {
                    x = Pos.WIDTH + x;
                    return parent.left as ChunkCell;
                }

                if (x >= Pos.WIDTH && y >= 0 && y < Pos.HEIGHT && z >= 0 && z < Pos.WIDTH)
                {
                    x = x - Pos.WIDTH;
                    return parent.right as ChunkCell;
                }
            }

            return null;

            */

        }




        public int FacesCount
        {
            get
            {
                return facesCount;
            }
        }

        public virtual void AddFaces(Vector3[] vertices, Vector3[] normals, Color32[] colors32, int[] triangles, Vector2[] uv, ref int indexVertices, ref int indexTriangles, ref int indexUv)
        {
            for (int i = 0; i < 6; i++)
            {
                var face = blockFaces.blockFaces [i];

                if (face != null)
                    AddFace(face, vertices, normals, colors32, triangles, uv, ref indexVertices, ref indexTriangles, ref indexUv);
            }

        }

        protected void AddFace(BlockFace blockFace, Vector3[] vertices, Vector3[] normals, Color32[] colors32, int[] triangles, Vector2[] uv, ref int indexVertices, ref int indexTriangles, ref int indexUv)
        {
            //      Log.Add("AddFace");
            var faceVertices = blockFace.vertices;
            var faceNormals = blockFace.normals;
            var faceColors32 = blockFace.colors32;
            var faceTriangles = blockFace.triangles;
            var faceUV = blockFace.uv;

            int indexVerticesBegin = indexVertices;

            var v = pos.GetVector3();

            for (int i = 0; i < faceVertices.Length; i++)
            {
                vertices [indexVertices] = faceVertices [i] + v;
                normals [indexVertices] = faceNormals [i];
                colors32 [indexVertices] = faceColors32 [i];

                indexVertices++;
            }
                
            for (int i = 0; i < faceTriangles.Length; i++)
            {
                triangles [indexTriangles++] = indexVerticesBegin + faceTriangles [i];
            }

            for (int i = 0; i < faceUV.Length; i++)
            {
                uv [indexUv++] = faceUV [i];
            }
        }
            
    }
    /*
    public enum Transparent : byte
    {
        Opaque,
        Transparent,
        TransparentAndFusionToFusion,
        TransparentAndFusionToSameType
    }
    */
}