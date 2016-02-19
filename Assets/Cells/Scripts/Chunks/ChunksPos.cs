using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace Flashunity.Cells
{
    public class ChunksPos
    {
        public readonly long x;
        public readonly long y;
        public readonly long z;

        string filename;

        Vector3? v;

        public ChunksPos(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }



        public Vector3 Vector3
        {
            get
            {
                if (v == null)
                {
                    v = new Vector3(x * Pos.WIDTH * Pos.WIDTH, y * Pos.HEIGHT * Pos.HEIGHT, z * Pos.WIDTH * Pos.WIDTH);
                }
                return v.Value;
            }
        }

        public string GetFileName()
        {
            if (filename == null)
            {
                filename = GetFileNameFromPos(x, y, z);
            }

            return filename;
        }

        static string GetStringCoord(long coord)
        {
//            Debug.Log("===================================================");
            //          Debug.Log("GetStringCoord: " + coord);

            string str;

            if (coord < 0)
            {
                str = "n";
                coord = -coord;
            } else
            {
                str = "p";
            }

            byte[] bytes = GetBytes(coord);

            char[] padding = { '=' };
            str += Convert.ToBase64String(bytes).TrimEnd(padding).Replace('+', '-').Replace('/', '_');

//            str = (coord >= 0 ? "p" : "n") + str;

            return str;
        }

        static int GetFirstNonZerroByteIndex(byte[] bytes)
        {
            for (int i = bytes.Length - 1; i > 0; i--)
            {
                if (bytes [i] != 0)
                    return i;
            }
            return 0;
        }

        static byte[] GetBytes(long l)
        {
            //Debug.Log("GetBytes: " + l);

            byte[] bytes = BitConverter.GetBytes(l);

            for (int i = 0; i < bytes.Length; i++)
            {
                //       Debug.Log("byte: " + bytes [i]);
            }

            var endIndex = GetFirstNonZerroByteIndex(bytes);

            // Debug.Log("endIndex: " + endIndex);

            var b = new byte[endIndex + 1];

            for (int i = 0; i < endIndex + 1; i++)
            {
                b [i] = bytes [i];
            }

            for (int i = 0; i < b.Length; i++)
            {
                //  Debug.Log("b: " + b [i]);
            }

            return b;
        }

        public static string GetFileNameFromPos(long x, long y, long z)
        {
            return GetStringCoord(x) + "." + GetStringCoord(y) + "." + GetStringCoord(z);
        }

        void Tests()
        {
            Debug.Log(GetStringCoord(0));
            Debug.Log(GetStringCoord(1));
            Debug.Log(GetStringCoord(255));
            Debug.Log(GetStringCoord(65535));
            Debug.Log(GetStringCoord((long)65535 * (long)65535));
            Debug.Log(GetStringCoord(long.MaxValue));
            Debug.Log(GetStringCoord(-1));
            Debug.Log(GetStringCoord(-255));
            Debug.Log(GetStringCoord(long.MinValue + 1));
        }

        /*
        byte[] GetByteArray()
        {
            var list = new List<byte>();

            ushort version = 0;

            AddBytesToList(ref list, BitConverter.GetBytes(version));
            //            AddBytesToList(ref list, ref BitConverter.GetBytes(Filename));


            return list.ToArray();
        }

        void AddBytesToList(ref List<byte> list, byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                list.Add(bytes [i]);
            }            
        }
        */
    }
}
