using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csdemo
{
    class LoadKey
    { 
           public byte Sector;
           public byte Block;
           public byte keyab;
           public byte[] Data;
           public byte RequestAll;
     public LoadKey()
    {
        Sector = 0x00;
        Block = 0x00;
            keyab = 0x00;
            Data = new byte[40];
            RequestAll = 0x00;
            keyab = 0x00;
    }

        public static LoadKey ByteArrayToKey(byte[] byteIn)
        {
            LoadKey newKey = new LoadKey();
            newKey.Sector = byteIn[0];
            newKey.Block = byteIn[1];
            newKey.keyab = byteIn[2];
            for (int i = 0; i < 40; i++)
            {
                newKey.Data[i] = byteIn[i + 3];
            }
            newKey.RequestAll = byteIn[43];
            return newKey;
        }

        public static byte[] KeyToByteArray(LoadKey keyin)
        {
            byte[] b = new byte[44];
            b[0] = keyin.Sector;
            b[1] = keyin.Block;
            b[2] = keyin.keyab;
            for (int i = 0; i < 40; i++)
            {
                b[i + 3] = keyin.Data[i];
            }
            b[43] = keyin.RequestAll;
            return b;
        }
    }

}
