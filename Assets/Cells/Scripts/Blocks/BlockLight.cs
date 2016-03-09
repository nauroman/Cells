using UnityEngine;
using System.Collections;
using Flashunity.Cells;

namespace Flashunity.Cells
{

    public class BlockLight : Block
    {
        public readonly Light light;

        public BlockLight(Pos pos, ChunkCell parent, ushort type, ushort textureIndex, Light light) : base(pos, parent, type, textureIndex, false)
        {
            this.light = light;

            parent.AddLight(light, pos);
        }

        public override void UpdateFaces()
        {
            var greed = parent.childrenOpacityBinaryGreed;

            var x = pos.x;
            var y = pos.y;
            var z = pos.z;

            bool visibleFaces;

            if (pos.edge)
            {
                if (z == Pos.WIDTH - 1)
                {
                    if (parent.back != null)
                        visibleFaces = !((parent.back as ChunkCell).childrenOpacityBinaryGreed.IsSet(x, y, 0));
                    else
                        visibleFaces = true;
                } else
                    visibleFaces = !greed.IsSet(x, y, (byte)(z + 1));

                if (visibleFaces)
                {
                    AddAllFacesForLight();
                    return;
                }

                if (z == 0)
                {
                    if (parent.front != null)
                        visibleFaces = !((parent.front as ChunkCell).childrenOpacityBinaryGreed.IsSet(x, y, (byte)(Pos.WIDTH - 1)));
                    else
                        visibleFaces = true;
                } else
                    visibleFaces = !greed.IsSet(x, y, (byte)(z - 1));

                if (visibleFaces)
                {
                    AddAllFacesForLight();
                    return;
                }

                if (y == Pos.HEIGHT - 1)
                {
                    if (parent.top != null)
                        visibleFaces = !((parent.top as ChunkCell).childrenOpacityBinaryGreed.IsSet(x, 0, z));
                    else
                        visibleFaces = true;

                } else
                    visibleFaces = !greed.IsSet(x, (byte)(y + 1), z);

                if (visibleFaces)
                {
                    AddAllFacesForLight();
                    return;
                }

                if (y == 0)
                {
                    if (parent.bottom != null)
                        visibleFaces = !((parent.bottom as ChunkCell).childrenOpacityBinaryGreed.IsSet(x, (byte)(Pos.HEIGHT - 1), z));
                    else
                        visibleFaces = true;

                } else
                    visibleFaces = !greed.IsSet(x, (byte)(y - 1), z);

                if (visibleFaces)
                {
                    AddAllFacesForLight();
                    return;
                }

                if (x == 0)
                {
                    if (parent.left != null)
                        visibleFaces = !((parent.left as ChunkCell).childrenOpacityBinaryGreed.IsSet((byte)(Pos.WIDTH - 1), y, z));
                    else
                        visibleFaces = true;

                } else
                    visibleFaces = !greed.IsSet((byte)(x - 1), y, z);

                if (visibleFaces)
                {
                    AddAllFacesForLight();
                    return;
                }
                if (x == Pos.WIDTH - 1)
                {
                    if (parent.right != null)
                        visibleFaces = !((parent.right as ChunkCell).childrenOpacityBinaryGreed.IsSet(0, y, z));
                    else
                        visibleFaces = true;

                } else
                    visibleFaces = !greed.IsSet((byte)(x + 1), y, z);

                if (visibleFaces)
                {
                    AddAllFacesForLight();
                    return;
                }
            } else
            {
                if (!greed.IsSet(x, y, (byte)(z + 1)))
                {
                    AddAllFacesForLight();
                    return;
                }

                if (!greed.IsSet(x, y, (byte)(z - 1)))
                {
                    AddAllFacesForLight();
                    return;
                }

                if (!greed.IsSet(x, (byte)(y + 1), z))
                {
                    AddAllFacesForLight();
                    return;
                }

                if (!greed.IsSet(x, (byte)(y - 1), z))
                {
                    AddAllFacesForLight();
                    return;
                }

                if (!greed.IsSet((byte)(x - 1), y, z))
                {
                    AddAllFacesForLight();
                    return;
                }

                if (!greed.IsSet((byte)(x + 1), y, z))
                {
                    AddAllFacesForLight();
                    return;
                }
            }

            blockFaces.FreeCahcedFaces();
            facesCount = 0;
        }

        void AddAllFacesForLight()
        {
            blockFaces.CacheFaces(textureIndex, 0.35f);

            facesCount = 6;
        }
    }
}