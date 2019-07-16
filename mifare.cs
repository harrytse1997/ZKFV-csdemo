using System.Runtime.InteropServices;
namespace csdemo
{
    class mifare
    {




        public byte[] cID = new byte[256];



        [DllImport("mifare.dll")]
        public static extern int Mif_OpenPort(int num);

        [DllImport("mifare.dll")]
        public static extern int Mif_ClosePort(int num);

        [DllImport("mifare.dll")]
        public static extern int Mif_Request(int num);

        [DllImport("mifare.dll")]
        public static extern int Mif_RequestAll(int num);

        [DllImport("mifare.dll")]
        public static extern int Mif_LEDOn(int test);

        [DllImport("mifare.dll")]
        public static extern int Mif_LEDOff(int test);

        [DllImport("mifare.dll")]
        public static extern int Mif_Halt(int test);

        [DllImport("mifare.dll")]
        public static extern int Mif_BuzOn(int num);

        [DllImport("mifare.dll")]
        public static extern int Mif_BuzOff(int num);

        [DllImport("mifare.dll")]
        public static extern int Mif_Anticol(int num, byte[] ID);

        [DllImport("mifare.dll")]
        public static extern int Mif_Anticol2(int num, byte[] ID);

        [DllImport("mifare.dll")]
        public static extern int Mif_Select(int num, byte[] ID);

        [DllImport("mifare.dll")]
        public static extern int Mif_Select2(int num, byte[] ID);

        [DllImport("mifare.dll")]
        public static extern int Mif_key(int hComm, byte[] key);

        [DllImport("mifare.dll")]
        public static extern int Mif_Auth(int hComm, byte[] key);

        [DllImport("mifare.dll")]
        public static extern int Mif_Read(int hComm, byte[] key);

        [DllImport("mifare.dll")]
        public static extern int Mif_Write(int hComm, byte[] key);

        [DllImport("mifare.dll")]
        public static extern int Mif_Detect(int hCom);


    }
}
