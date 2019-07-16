using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csdemo
{
    class User
    {
        public string name;
        public int id;
        public byte[][] fVein;
        public byte[] fPrint;
        public string department;

        User()
        {
            name = "None";
            id = 0;
            fVein = new byte[3][];
            for(int i = 0; i<3; i++)
            {
                fVein[i] = new byte[1024];
            }
            fPrint = new byte[2048];
            department = "None";
        }
     }
}
