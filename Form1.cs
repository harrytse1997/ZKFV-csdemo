using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using zkfvcsharp;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using Sample;
using System.Data.SqlClient;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
namespace csdemo
{
    public partial class Form1 : Form
    {

        //v1.0 startfd
        //Declararation of all variables, vectors and haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;
        //v1.0 end

        //dllImport for the mifare dll

        //mifare card
        private byte[] cID = new byte[256];
        private byte[] idRead = new byte[256];
        private int hReader;
        private string cardID = "";
        private bool mcRead = false;
        private bool mcRecord = false;
        private bool isMute = false;
        private int figScore;
        private LoadKey veinKey = new LoadKey();
        private LoadKey key = new LoadKey();
        private int dataIndex;
        private byte devNumMC = 1;


        //Finger vein
        private IntPtr mDevHandle = IntPtr.Zero;        //Device Handle     
        private IntPtr mDBHandle = IntPtr.Zero;  //Database Handle     
        private byte[] mRegFPRegTemp = null;  //Last reg-fingerprint template       
        private int mRegFPRegTempLen = 0;//Last reg-fingerprint template length        
        private byte[][] mRegFVRegTemps = new byte[3][];//Last reg-fingervein templates(3 fingervein temlates)        
        private int[] mRegFVTempLen = null;//the length of reg-fingervein template 
        private static int mEnrollCnt = 3;             //Enroll count   
        private int mEnrollIdx = 0;      //Enroll index
        private int mFingerID = 1;        //register finger id(must > 0)

        private bool mbStop = true;//stop thread      
        private bool mbRegister = false;//Enroll        
        private bool mbIdentify = false;//Identify(!Verify)
        private bool mbRecord = false;//Recording(!recording)
        private bool isMCempty = true;
        private bool isMBempty = true;
        private bool cardForReg = false;
        private bool mcmbIdentify = false;

        //Max template length
        private static int mMaxTempLen = 1024;
        private static int mMaxFpTempLen = 2048;

        //mode bool
        private bool enrollmentMode;
        private bool recordMode;
        private bool settingMode;

        //DataTable object
        private DataTable recordData = new DataTable();
        private DataTable enrollData = new DataTable();
        private DataTable recordDataSearch = new DataTable();
        private DataTable enrollDataSearch = new DataTable();

        //identity Mode (1:Normal, 2:Anti-Fake, 3:Security, 4:mifare+card)
        private int idMode = 1;


        private string[] username = new string[100];
        private int[] staffID = new int[100];
        private string[] department = new string[100];
        private string[] mifareCardID = new string[100];
        private string[] isFVreg = new string[100];

        private int FVlength = 0;

        //From handle
        IntPtr FormHandle = IntPtr.Zero;

        //Acquire info form sdk begin
        private byte[] mfpImg = null;
        private byte[] mfvImg = null;
        private int mfpWidth = 0;
        private int mfpHeight = 0;
        private int mfvWidth = 0;
        private int mfvHeight = 0;
        private byte[] mfpTemp = null;
        private byte[] mfvTemp = null;
        private int mfpTempLen = 0;
        private int mfvTempLen = 0;

        private int rowSelect = -1;
        private int rowSelecting = -1;

        private byte[][] block4K = new byte[40][];
        private byte[][] block1K = new byte[16][];

        private byte[] FVrec;
        private byte[] FPrec;
        private byte[] FVresult;
        private byte[] FPresult;
        private byte[] FVtempMC = new byte[1024];
        private byte[] FVtempMB = new byte[1024];
        private byte[] FPtempMC = new byte[1024];
        private byte[] FPtempMB = new byte[1024];
        private byte[] byteIn = new byte[44];
        private int FVindex = 0;
        private byte sectorIndex = 0;
        private int blockIndex = 0;
        //prereg-fingerprint templates
        private byte[][] mPreRegFPRegTemps = new byte[3][];

        //the length of reg-fingervein template
        private int[] mPreRegFPTempLen = null;

        //Last reg-fingervein templates(3 fingervein temlates)
        private byte[][] mPreRegFVRegTemps = new byte[3][];

        //the length of reg-fingervein template
        private int[] mPreRegFVTempLen = null;

        //Acquire info form sdk end
        const int MESSAGE_CAPTURED_OK = 0x0400 + 6;





        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public Form1()
        {
            //initialize the GUI component

            //1.
            //Load haarcascades for face detection
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            //1.
            InitializeComponent();
            block1K[0] = new byte[0] { };
            block1K[1] = new byte[3] { 0x04, 0x05, 0x06 };
            block1K[2] = new byte[3] { 0x08, 0x09, 0x0A };
            block1K[3] = new byte[3] { 0x0C, 0x0D, 0x0E };
            block1K[4] = new byte[3] { 0x10, 0x11, 0x12 };
            block1K[5] = new byte[3] { 0x14, 0x15, 0x16 };
            block1K[6] = new byte[3] { 0x18, 0x19, 0x1A };
            block1K[7] = new byte[3] { 0x1C, 0x1D, 0x1E };
            block1K[8] = new byte[3] { 0x20, 0x21, 0x22 };
            block1K[9] = new byte[3] { 0x24, 0x25, 0x26 };
            block1K[10] = new byte[3] { 0x28, 0x29, 0x2A };
            block1K[11] = new byte[3] { 0x2C, 0x2D, 0x2E };
            block1K[12] = new byte[3] { 0x30, 0x31, 0x32 };
            block1K[13] = new byte[3] { 0x34, 0x35, 0x36 };
            block1K[14] = new byte[3] { 0x38, 0x39, 0x3A };
            block1K[15] = new byte[3] { 0x3C, 0x3D, 0x3E };

            block4K[0] = new byte[0] { };
            block4K[1] = new byte[3] { 0x04, 0x05, 0x06 };
            block4K[2] = new byte[3] { 0x08, 0x09, 0x0A };
            block4K[3] = new byte[3] { 0x0C, 0x0D, 0x0E };
            block4K[4] = new byte[3] { 0x10, 0x11, 0x12 };
            block4K[5] = new byte[3] { 0x14, 0x15, 0x16 };
            block4K[6] = new byte[3] { 0x18, 0x19, 0x1A };
            block4K[7] = new byte[3] { 0x1C, 0x1D, 0x1E };
            block4K[8] = new byte[3] { 0x20, 0x21, 0x22 };
            block4K[9] = new byte[3] { 0x24, 0x25, 0x26 };
            block4K[10] = new byte[3] { 0x28, 0x29, 0x2A };
            block4K[11] = new byte[3] { 0x2C, 0x2D, 0x2E };
            block4K[12] = new byte[3] { 0x30, 0x31, 0x32 };
            block4K[13] = new byte[3] { 0x34, 0x35, 0x36 };
            block4K[14] = new byte[3] { 0x38, 0x39, 0x3A };
            block4K[15] = new byte[3] { 0x3C, 0x3D, 0x3E };
            block4K[16] = new byte[0] { };
            block4K[17] = new byte[3] { 0x44, 0x45, 0x46 };
            block4K[18] = new byte[3] { 0x48, 0x49, 0x4A };
            block4K[19] = new byte[3] { 0x4C, 0x4D, 0x4E };
            block4K[20] = new byte[3] { 0x50, 0x51, 0x52 };
            block4K[21] = new byte[3] { 0x54, 0x55, 0x56 };
            block4K[22] = new byte[3] { 0x58, 0x59, 0x5A };
            block4K[23] = new byte[3] { 0x5C, 0x5D, 0x5E };
            block4K[24] = new byte[3] { 0x60, 0x61, 0x62 };
            block4K[25] = new byte[3] { 0x64, 0x65, 0x66 };
            block4K[26] = new byte[3] { 0x68, 0x69, 0x6A };
            block4K[27] = new byte[3] { 0x6C, 0x6D, 0x6E };
            block4K[28] = new byte[3] { 0x70, 0x71, 0x72 };
            block4K[29] = new byte[3] { 0x74, 0x75, 0x76 };
            block4K[30] = new byte[3] { 0x78, 0x79, 0x7A };
            block4K[31] = new byte[3] { 0x7C, 0x7D, 0x7E };
            block4K[32] = new byte[15] { 0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E };
            block4K[33] = new byte[15] { 0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E };
            block4K[34] = new byte[15] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE };
            block4K[35] = new byte[15] { 0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, 0xB9, 0xBA, 0xBB, 0xBC, 0xBD, 0xBE };
            block4K[36] = new byte[15] { 0xC0, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xCE };
            block4K[37] = new byte[15] { 0xD0, 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6, 0xD7, 0xD8, 0xD9, 0xDA, 0xDB, 0xDC, 0xDD, 0xDE };
            block4K[38] = new byte[15] { 0xE0, 0xE1, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7, 0xE8, 0xE9, 0xEA, 0xEB, 0xEC, 0xED, 0xEE };
            block4K[39] = new byte[15] { 0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB, 0xFC, 0xFD, 0xFE };

            //variable for finger vein device
            mRegFVTempLen = new int[mEnrollCnt];
            mPreRegFPTempLen = new int[mEnrollCnt];
            mPreRegFVTempLen = new int[mEnrollCnt];
            mfpTemp = new byte[mMaxFpTempLen];
            mfvTemp = new byte[mMaxTempLen];
            mRegFPRegTemp = new byte[mMaxFpTempLen];
            mRegFPRegTempLen = 0;

            for (int i = 0; i < mEnrollCnt; i++)
            {
                mRegFVRegTemps[i] = new byte[mMaxTempLen];
                mPreRegFPRegTemps[i] = new byte[mMaxFpTempLen];
                mPreRegFVRegTemps[i] = new byte[mMaxTempLen];
                mRegFVTempLen[i] = 0;
                mPreRegFPTempLen[i] = 0;
                mPreRegFVTempLen[i] = 0;
            }

            //record table component
            for (int i = 0; i < staffID.Length; i++)
            {
                staffID[i] = 0;
            }
            for (int i = 0; i < mifareCardID.Length; i++)
            {
                mifareCardID[i] = "Not Registered";
            }
            for (int i = 0; i < isFVreg.Length; i++)
            {
                isFVreg[i] = "Not Registered";
            }

            recordDataSearch.Columns.Add("Time");
            recordDataSearch.Columns.Add("ID");
            recordDataSearch.Columns.Add("Name");
            recordDataSearch.Columns.Add("Department");
            recordDataSearch.Columns.Add("Identification");
            recordDataSearch.Columns.Add("Score/Num");
            recordData.Columns.Add("Time");
            recordData.Columns.Add("ID");
            recordData.Columns.Add("Name");
            recordData.Columns.Add("Department");
            recordData.Columns.Add("Identification");
            recordData.Columns.Add("Score/Num");
            dataGridViewRec.DataSource = recordData;
            this.dataGridViewRec.Columns[0].FillWeight = 29;
            this.dataGridViewRec.Columns[1].FillWeight = 13;
            this.dataGridViewRec.Columns[2].FillWeight = 20;
            this.dataGridViewRec.Columns[3].FillWeight = 18;
            this.dataGridViewRec.Columns[4].FillWeight = 29;
            this.dataGridViewRec.Columns[5].FillWeight = 9;
            this.dataGridViewRec.Columns[0].ReadOnly = true;
            this.dataGridViewRec.Columns[1].ReadOnly = true;
            this.dataGridViewRec.Columns[2].ReadOnly = true;
            this.dataGridViewRec.Columns[3].ReadOnly = true;
            this.dataGridViewRec.Columns[4].ReadOnly = true;
            this.dataGridViewRec.Columns[5].ReadOnly = true;
            dataGridViewRec.Visible = true;


            //enrollment table component
            enrollData.Columns.Add("Index");
            enrollData.Columns.Add("UserID");
            enrollData.Columns.Add("Username");
            enrollData.Columns.Add("Department");
            enrollData.Columns.Add("Finger Print/Vein");
            enrollData.Columns.Add("Mifare Card");
            enrollDataSearch.Columns.Add("Index");
            enrollDataSearch.Columns.Add("UserID");
            enrollDataSearch.Columns.Add("Username");
            enrollDataSearch.Columns.Add("Department");
            enrollDataSearch.Columns.Add("Finger Print/Vein");
            enrollDataSearch.Columns.Add("Mifare Card");
            dataGridViewEN.DataSource = enrollData;
            this.dataGridViewEN.Columns[0].FillWeight = 1;
            this.dataGridViewEN.Columns[1].FillWeight = 15;
            this.dataGridViewEN.Columns[2].FillWeight = 20;
            this.dataGridViewEN.Columns[3].FillWeight = 16;
            this.dataGridViewEN.Columns[4].FillWeight = 29;
            this.dataGridViewEN.Columns[5].FillWeight = 20;
            this.dataGridViewEN.Columns[0].ReadOnly = true;
            this.dataGridViewEN.Columns[1].ReadOnly = true;
            this.dataGridViewEN.Columns[2].ReadOnly = true;
            this.dataGridViewEN.Columns[3].ReadOnly = true;
            this.dataGridViewEN.Columns[4].ReadOnly = true;
            this.dataGridViewEN.Columns[5].ReadOnly = true;
            dataGridViewEN.Visible = false;

            //component visible
            gboxSetting.Visible = true;
            gboxRecord.Visible = false;
            gboxEnroll.Visible = false;

            //default mode
            enrollmentMode = false;
            recordMode = false;
            settingMode = true;

            bnSettingMode.Enabled = false;
            bnEnrollMode.Enabled = true;
            bnRecordMode.Enabled = true;
            tboxMCID.Enabled = false;

            bnMute.Enabled = true;
            bnReinFV.Enabled = true;
            mFingerID = 1;

            //initalize finger vein machine
            SetTextBox("Finger vein machine initalizating...");
            int ret = zkfverrdef.ZKFV_ERR_OK;
            if (zkfverrdef.ZKFV_ERR_OK != (ret = zkfv.Init()))
            {
                SetTextBox("Initialize failed! retcode = " + ret.ToString());
                goto fvFailInit;
            }
            //Device list
            listDevFV.Enabled = false;
            listDevFV.Items.Clear();
            int nCount = zkfv.GetDeviceCount();
            if (nCount <= 0)
            {
                SetTextBox("No device connected!");
                zkfv.Terminate();
                goto fvFailInit;
            }

            for (int i = 0; i < nCount; i++)
            {
                listDevFV.Items.Add(i.ToString());
            }
            listDevFV.SelectedIndex = 0;
            listDevFV.Enabled = true;
            bnConFV.Enabled = true;
            SetTextBox("Finger vein device initalizating completed.");

        fvFailInit:;

            //mifare card part
            SetTextBox("Mifare card reader initalizating...");
            for (int i = 1; i <= 256; i++)
            {
                listDevMC.Items.Add(i.ToString());
            }
            listDevMC.SelectedIndex = 0;
            bnConMC.Enabled = true;
            SetTextBox( "Mifare card reader initalization completed.");

            //other components
            tboxSearch.Enabled = false;
            tboxSID.Enabled = false;
            tboxName.Enabled = false;
            tboxDep.Enabled = false;

            //mode list
            listIDmode.Items.Clear();
            listIDmode.Items.Add("(Either) [MifareCard Authentication], [Normal Hybrid Identify Mode]");
            listIDmode.Items.Add("(Either) [MifareCard Authentication], [Fake Hybrid Identify Mode]");
            listIDmode.Items.Add("(Either) [MifareCard Authentication], [Security Hybrid Identify Mode]");
            listIDmode.Items.Add("(Both) [MifareCard Authentication], [Finger Vein identify Mode]");
            listIDmode.SelectedIndex = 0;
            idMode = 1;

            //result
            SetTextBox("Initalization completed.");
        }

        //v1.0 start
        private void button1_Click(object sender, EventArgs e)
        {
            //Initialize the capture device
            grabber = new Capture();
            grabber.QueryFrame();
            //Initialize the FrameGraber event
            Application.Idle += new EventHandler(FrameGrabber);
            button1.Enabled = false;
        }

        void FrameGrabber(object sender, EventArgs e)
        {
            label3.Text = "0";
            NamePersons.Add("");


            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convert it to Grayscale
            gray = currentFrame.Convert<Gray, Byte>();

            //Face Detector
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
          face,
          1.2,
          10,
          Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
          new Size(20, 20));

            //Action for each element detected
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //draw the face detected in the 0th (gray) channel with blue color
                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);


                if (trainingImages.ToArray().Length != 0)
                {
                    //TermCriteria for face recognition with numbers of trained images like maxIteration
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);

                    //Eigen face recognizer
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
                       trainingImages.ToArray(),
                       labels.ToArray(), 6000
                       ,
                       ref termCrit);

                    name = recognizer.Recognize(result);

                    //Draw the label for each face detected and recognized
                    currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.LightGreen));

                }

                NamePersons[t - 1] = name;
                NamePersons.Add("");


                //Set the number of faces detected on the scene
                label3.Text = facesDetected[0].Length.ToString();

            }
            t = 0;

            //Names concatenation of persons recognized
            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
            {
                names = names + NamePersons[nnn] + ", ";
            }
            //Show the faces procesed and recognized
            imageBoxFrameGrabber.Image = currentFrame;
            label4.Text = names;
            names = "";
            //Clear the list(vector) of names
            NamePersons.Clear();

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            try
            {
                //Trained face counter
                ContTrain = ContTrain + 1;

                //Get a gray frame from capture device
                gray = grabber.QueryGrayFrame().Resize(200, 150, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                //Face Detector
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
                face,
                1.2,
                10,
                Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                new Size(20, 20));

                //Action for each element detected
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    TrainedFace = currentFrame.Copy(f.rect).Convert<Gray, byte>();
                    break;
                }

                //resize face detected image for force to compare the same size with the 
                //test image with cubic interpolation type method
                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labels.Add(textBox1.Text);

                //Show face added in gray scale
                imageBox1.Image = TrainedFace;

                //Write the number of triained faces in a file text for further load
                File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.ToArray().Length.ToString() + "%");

                //Write the labels of triained faces in a file text for further load
                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                {
                    trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
                    File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", labels.ToArray()[i - 1] + "%");
                }

                MessageBox.Show(textBox1.Text + "´s face detected and added :)", "Training OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Enable the face detection first", "Training Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        //v1.0 end
        private void bnConFV_Click(object sender, EventArgs e)
        {
            SetTextBox("FingerVein machine connecting...");

            if (IntPtr.Zero != mDevHandle)
            {
                SetTextBox("Please close the device first!");
                return;
            }
            if (listDevFV.SelectedIndex == -1)
            {
                SetTextBox( "Please select a device first!");
                return;
            }
            mDevHandle = zkfv.OpenDevice(Int32.Parse(listDevFV.SelectedItem.ToString()));

            if (IntPtr.Zero == mDevHandle)
            {
                SetTextBox("Device open failed!");
                return;
            }
            mDBHandle = zkfv.DBInit();

            if (IntPtr.Zero == mDevHandle)
            {
                SetTextBox( "Database initialize failed!");
                zkfv.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                return;
            }

            //component enable
            bnConFV.Enabled = false;
            bnReinFV.Enabled = false;
            bnDisconFV.Enabled = true;
            listDevFV.Enabled = false;

            if (bnDisconMC.Enabled)
            {
                listIDmode.Enabled = true;
            }

            //Initialize the image
            byte[] retParam = new byte[4];
            int size = 4;
            int ret = 0;
            ret = zkfv.GetParameters(mDevHandle, 5001, retParam, ref size);
            zkfv.ByteArray2Int(retParam, ref mfvWidth);
            size = 4;
            ret = zkfv.GetParameters(mDevHandle, 5002, retParam, ref size);
            zkfv.ByteArray2Int(retParam, ref mfvHeight);
            size = 4;
            ret = zkfv.GetParameters(mDevHandle, 5004, retParam, ref size);
            zkfv.ByteArray2Int(retParam, ref mfpWidth);
            size = 4;
            ret = zkfv.GetParameters(mDevHandle, 5005, retParam, ref size);
            zkfv.ByteArray2Int(retParam, ref mfpHeight);
            mfvImg = new byte[mfvWidth * mfvHeight];
            mfpImg = new byte[mfpWidth * mfpHeight];

            //start the thread
            mbStop = false;
            Thread captureThread = new Thread(new ThreadStart(DoCapture));
            captureThread.IsBackground = true;
            captureThread.Start();

            //print result
            SetTextBox("Finger vein device connected.");
        }

        private void CloseDevice()
        {
            if (!mbStop)
            {
                mbStop = true;
                Thread.Sleep(1000);
            }
            if (IntPtr.Zero != mDBHandle)
            {
                zkfv.DBFree(mDBHandle);
                mDBHandle = IntPtr.Zero;
            }
            if (IntPtr.Zero != mDevHandle)
            {
                zkfv.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
            }
            mRegFPRegTempLen = 0;
        }

        private void bnDisconFV_Click(object sender, EventArgs e)
        {
            SetTextBox( "FingerVein machine disconnecting..."); //print message
            CloseDevice();   
           
            //setting component
            bnConFV.Enabled = true;
            bnReinFV.Enabled = true;
            bnDisconFV.Enabled = false;

            //Clear the list
            listDevFV.Enabled = true;

            //enrollment component
            bnEnrollFV.Enabled = false;
            bnVerify.Enabled = false;

            //print message
            SetTextBox( "Finger vein device disconnected.");
        }

        private void bnGetCardID_Click(object sender, EventArgs e)
        {
            bnWrite4K.Enabled = false;
            bnReadVein.Enabled = false;
            bnWriteVein.Enabled = false;
            BnRead4K.Enabled = false;
            bnGetCardID.Enabled = false;
            bnCancel.Enabled = true;
            //information not fill in yet
            mcRead = true;
            Thread mifareThread = new Thread(new ThreadStart(MifareReadCapture));
            mifareThread.IsBackground = true;
            mifareThread.Start();
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            bnCancel.Enabled = false;
            mcRead = false;
            bnGetCardID.Enabled = true;
            SetTextBox("Cancelled");
        }

        private void bnEnrollFV_Click(object sender, EventArgs e)
        {
            bnEnrollFV.Enabled = false;
            if (IntPtr.Zero == mDevHandle)
            {
                bnEnrollFV.Enabled = true;
                return;
            }
            mbRegister = true;
            bnVerify.Enabled = true;

            SetTextBox( "Please press the finger on the machine for 3 times.");
        }

        private void bnVerify_Click(object sender, EventArgs e)
        {
            mEnrollIdx = 0;
            mbRegister = false;
            bnEnrollFV.Enabled = true;
            bnVerify.Enabled = false;
            SetTextBox( "Verified.");
        }

        //other thread setText
        public void SetTextBox(String text)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate () { SetTextBox(text); });
                return;
            }
            txtResult.AppendText("[" + DateTime.Now.ToString("MM/dd H:mm") + "] " + text);
            txtResult.AppendText(Environment.NewLine);
        }

        public void SetTboxEnable(TextBox a, bool enable)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate () { SetTboxEnable(a, enable); });
                return;
            }
            a.Enabled = enable;
        }

        public void updateDataGridRec(string a, string b, string c, string d, string e, string f)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate () { updateDataGridRec(a, b, c, d, e, f); });
                return;
            }
            recordData.Rows.Add(a, b, c, d, e, f);
            dataGridViewRec.DataSource = recordData;
            dataGridViewRec.Refresh();
        }

        public void SetBnEnable(Button a, bool enable)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate () { SetBnEnable(a, enable); });
                return;
            }
            a.Enabled = enable;
        }

        public void SetTbox(TextBox a, String text)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate () { SetTbox(a, text); });
                return;
            }
            a.Text = text;
        }

        private void MifareReadCapture()
        {
            cardForReg = true;
            while (mifare.Mif_Detect(hReader) == 1 && mcRead)
            {
                if (mifare.Mif_Request(hReader) >= 0)
                {
                    if (cardForReg)
                    {
                        cardForReg = false;
                        if (mifare.Mif_Anticol(hReader, idRead)<= 0)
                        {
                           goto readCardFail;
                        }                  
                       if( mifare.Mif_Select(hReader, idRead) <= 0)
                        {
                            goto readCardFail;
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            cardID += idRead[i].ToString("X");
                        }
                        if (cardID != "0000")
                        {
                            mifare.Mif_LEDOn(hReader);
                            mifare.Mif_LEDOff(hReader);
                            if (isMute)
                            {
                                mifare.Mif_BuzOn(hReader);
                                mifare.Mif_BuzOff(hReader);
                            }
                            SetTbox(tboxMCID, cardID);
                            SetTextBox("Card ID: " + cardID);

                            for (int i = 0; i < mifareCardID.Length; i++)
                            {
                                if (cardID == mifareCardID[i])
                                {
                                    SetTextBox("repeated card registed");
                                    goto readCardFail;
                                }
                            }
                            mcRead = false;
                            mifareCardID[rowSelect] = cardID;
                            SetBnEnable(bnGetCardID, true);
                            SetBnEnable(bnCancel, false);
                            SetTextBox("Enrolled");
                        }
                    readCardFail:;
                            cardID = "";
                    }
                }
                else
                {
                    cardForReg = true;
                }
            }

            SetBnEnable(bnCancel, false);
            SetBnEnable(bnGetCardID, true);
            SetBnEnable(bnWrite4K, true);
            SetBnEnable(BnRead4K, true);
            SetBnEnable(bnReadVein, true);
            SetBnEnable(bnWriteVein, true);

            if (mcRead)
            {
                SetBnEnable(bnGetCardID, false);
                SetBnEnable(bnConMC, true);
                SetBnEnable(bnDisconMC, false);
                SetBnEnable(bnGetCardID, false);
                SetBnEnable(bnWrite4K, false);
                SetBnEnable(BnRead4K, false);
                SetBnEnable(bnReadVein, false);
                SetBnEnable(bnWriteVein, false);
                SetTextBox("FingerVein machine disconnected accidentially.");
            }
        }

        private void MifareRecordCapture()
        {
            cardForReg = true;
            while (mifare.Mif_Detect(hReader) == 1 && mcRecord)
            {
                //request
                if (mifare.Mif_Request(hReader) >0) //-1: No Card on the reader
                {
                    if (cardForReg) //the card is recently put on the reader
                    {
                        cardForReg = false; //the current card defined as keep putting on the reader, avoid repeated read

                        //anti-Colilision 
                       mifare.Mif_Anticol(hReader, idRead);

                        //select
                        mifare.Mif_Select(hReader, idRead);

                        cardID = "";
                        for (int i = 0; i < 4; i++)
                        {
                            cardID += idRead[i].ToString("X");
                        }
                        if (cardID != "0000")
                        {
                            mifare.Mif_LEDOn(hReader);
                            mifare.Mif_LEDOff(hReader);

                            if (!isMute)
                            {
                                mifare.Mif_BuzOn(hReader);
                                mifare.Mif_BuzOff(hReader);
                            }
                        }

                        if (mcmbIdentify)
                        {
                            SetTextBox("Please keep the card on the reader for three seconds");
                            byte[] fvResultTemp;
                            var watch = new System.Diagnostics.Stopwatch();
                            watch.Start();
                            FVlength = 0;
                            FVindex = 0;

                            byteIn[0] = 0x01;
                            if (mifare.Mif_Auth(hReader, byteIn) <= 0)
                            {
                                SetTextBox("Authentication error in Sector - " + key.Sector);
                                goto mcRecordEnd;
                            }

                            byteIn[1] = block1K[1][0];
                            if (mifare.Mif_Read(hReader, byteIn) <= 0)
                            {
                                SetTextBox("Read data error Block: " + veinKey.Block);
                                goto mcRecordEnd;
                            }

                              FVlength = byteIn[3] * 256 + byteIn[4];
                            fvResultTemp = new byte[FVlength];
                            for (dataIndex = 5; dataIndex < 19; dataIndex++)
                            {
                                fvResultTemp[FVindex++] = byteIn[dataIndex];
                            }
                            byteIn[0] = 1;
                            blockIndex = 1;
                            for(;byteIn[0]<40;)
                            {
                                for (; blockIndex < 3;)
                                {
                                    byteIn[1] = block1K[byteIn[0]][blockIndex++];
                                    if(mifare.Mif_Read(hReader,byteIn) <= 0)
                                    {
                                        SetTextBox("Read data error block: " + byteIn[1]);
                                        goto mcRecordEnd;
                                    }
                                    for(dataIndex = 3; dataIndex < 19;)
                                    {
                                        fvResultTemp[FVindex++] = byteIn[dataIndex++];
                                        if(FVindex >= FVlength)
                                        {
                                            goto readVeinTempCompleted;
                                        }
                                    }
                                }
                                blockIndex = 0;
                                byteIn[0]++;
                                if (mifare.Mif_Auth(hReader, byteIn) <= 0)
                                {
                                    SetTextBox("Authentication error in Sector -" + byteIn[0]);
                                    goto mcRecordEnd;
                                }
                            }
                            readVeinTempCompleted:;
                            watch.Stop();
                            if (!isMute)
                            {
                                mifare.Mif_BuzOn(hReader);
                                mifare.Mif_BuzOff(hReader);
                                Thread.Sleep(100);
                                mifare.Mif_BuzOn(hReader);
                                mifare.Mif_BuzOff(hReader);
                            }
                            
                            fvResultTemp = compress.defDecompress(fvResultTemp);
                            FVtempMC = new byte[1024];
                            FVtempMC[0] = 0x5A;
                            FVtempMC[1] = 0x46;
                            FVtempMC[2] = 0x56;
                            FVtempMC[3] = 0x01;
                            FVtempMC[4] = 0x0C;
                            FVtempMC[5] = 0x0D;
                            FVtempMC[6] = 0x00;
                            FVtempMC[7] = 0x00;
                            FVtempMC[8] = 0x00;
                            FVtempMC[9] = 0x00;
                            FVtempMC[10] = 0x00;
                            FVtempMC[11] = 0x00;
                            FVtempMC[12] = 0x10;
                            FVtempMC[13] = 0x09;
                            FVtempMC[14] = 0x08;
                            FVtempMC[15] = 0x01;
                            FVtempMC[16] = 0x20;
                            FVtempMC[17] = 0x01;
                            FVtempMC[18] = 0x00;
                            FVtempMC[19] = 0x00;
                            for (FVindex = 0; FVindex < fvResultTemp.Length; FVindex++)
                            {
                                FVtempMC[FVindex + 20] = fvResultTemp[FVindex];
                            }
                            for (int i = FVindex; i < 1024; i++)
                            {
                                FVtempMC[i] = 0x00;
                            }
                            isMCempty = false;
                            SetTextBox(($"Execution Time: {watch.ElapsedMilliseconds} ms"));
                            SetTextBox("Read Complete.");
                      
                            if (!isMBempty)
                            {
                                    figScore = zkfv.DBMatchFV(mDBHandle, FVtempMB, FVtempMC);
                                    if (figScore >= 70)
                                    {
                                        SetTextBox("Access Granted! Score: " + figScore);
                                        isMBempty = true;
                                        isMCempty = true;
                                    }
                                    else
                                    {
                                        SetTextBox("Access Denied! Score:" + figScore);
                                    }                             
                            }
                            for (int i = 0; i < mifareCardID.Length; i++)
                            {
                                if (cardID.ToString() == mifareCardID[i])
                                {
                                    updateDataGridRec(DateTime.Now.ToString("MM /dd/yyyy H:mm:ss"), staffID[i].ToString(), username[i], department[i], "Mifare Card + Finger Vein", mifareCardID[i]);
                                    goto mcRecordEnd;
                                }
                            }
                            updateDataGridRec(DateTime.Now.ToString("MM /dd/yyyy H:mm:ss"), "N/A", "<Unknown>", "N/A", "Mifare Card + Finger Vein", cardID.ToString());
                        }
                        else
                        {
                            for (int i = 0; i < mifareCardID.Length; i++)
                            {
                                if (cardID.ToString() == mifareCardID[i])
                                {
                                    updateDataGridRec(DateTime.Now.ToString("MM /dd/yyyy H:mm:ss"), staffID[i].ToString(), username[i], department[i], "Mifare Card", mifareCardID[i]);
                                    goto mcRecordEnd;
                                }
                            }
                            updateDataGridRec(DateTime.Now.ToString("MM /dd/yyyy H:mm:ss"), "N/A", "<Unknown>", "N/A", "Mifare Card", cardID.ToString());
                        }
                        mcRecordEnd:;
                        cardID = "";
                    }
                }
                else
                {
                    cardForReg = true;
                }
            }
            if (mcRecord)
            {
                SetBnEnable(bnGetCardID, false);
                SetBnEnable(bnCancel, false);
                SetBnEnable(BnRead4K, false);
                SetBnEnable(bnWrite4K, false);
                SetBnEnable(bnReadVein, false);
                SetBnEnable(bnWriteVein, false);
                SetBnEnable(bnConMC, true);
                SetBnEnable(bnDisconMC, false);
                SetTextBox("FingerVein machine disconnected accidentially.");
            }
        }

        private void DoCapture()
        {
            while (!mbStop)
            {
                mfpTempLen = mfpTemp.Length;
                mfvTempLen = mfvTemp.Length;
                int ret = zkfv.AcquireFingerVein(mDevHandle, mfpImg, mfvImg, mfpTemp, ref mfpTempLen, mfvTemp, ref mfvTempLen);
                if (ret == zkfverrdef.ZKFV_ERR_OK)
                {
                    SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                }
                Thread.Sleep(200);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormHandle = this.Handle;
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MESSAGE_CAPTURED_OK:
                    {
                        if (mbRegister || mbIdentify)
                        {
                            MemoryStream msFP = new MemoryStream();
                            BitmapFormat.GetBitmap(mfpImg, mfpWidth, mfpHeight, ref msFP);
                            Bitmap bmpfp = new Bitmap(msFP);
                            this.picFP.Image = bmpfp;

                            MemoryStream msFV = new MemoryStream();
                            BitmapFormat.GetBitmap(mfvImg, mfvWidth, mfvHeight, ref msFV);
                            Bitmap bmpfv = new Bitmap(msFV);
                            this.picFV.Image = Function.rotateImage(bmpfv, 270);

                            if (mbRegister)
                            {
                                if (mEnrollIdx > 0)
                                {
                                    int nR1 = zkfv.DBMatchFP(mDBHandle, mfpTemp, mPreRegFPRegTemps[mEnrollIdx - 1]);
                                    int nR2 = zkfv.DBMatchFV(mDBHandle, mfvTemp, mPreRegFVRegTemps[mEnrollIdx - 1]);
                                    if (nR1 <= 0 || nR2 <= 0)
                                    {
                                        SetTextBox("Please press the same finger while registering!");
                                        return;
                                    }
                                }
                                Array.Copy(mfpTemp, mPreRegFPRegTemps[mEnrollIdx], mfpTempLen);
                                Array.Copy(mfvTemp, mPreRegFVRegTemps[mEnrollIdx], mfvTempLen);
                                mPreRegFPTempLen[mEnrollIdx] = mfpTempLen;
                                mPreRegFVTempLen[mEnrollIdx] = mfvTempLen;
                                mEnrollIdx++;
                                if (mEnrollIdx >= 3)
                                {
                                    mEnrollIdx = 0;
                                    mbRegister = false;
                                    byte[] temp = new byte[mMaxFpTempLen];
                                    int nTempLen = mMaxFpTempLen;
                                    if (zkfverrdef.ZKFV_ERR_OK != zkfv.DBMergeFP(mDBHandle, mPreRegFPRegTemps, temp, ref nTempLen) || nTempLen <= 0)
                                    {
                                        SetTextBox("Enrollment failed: repeated fingerPrint enrolled.");
                                        tboxDep.Enabled = true;
                                        tboxSID.Enabled = true;
                                        tboxName.Enabled = true;
                                        bnEnrollFV.Enabled = true;
                                        bnVerify.Enabled = true;
                                        return;
                                    }

                                    zkfv.DBDel(mDBHandle, rowSelect);
                                    int nRet = zkfv.DBAddEx(mDBHandle, rowSelect, mPreRegFVRegTemps, temp);

                                    int cutLengthFV = 0;
                                    for (int i = mPreRegFVRegTemps[0].Length - 1; i >= 0; i--)
                                    {
                                        if (mPreRegFVRegTemps[0][i] != 0x00)
                                        {

                                            cutLengthFV = i + 1;
                                            goto cutFV;
                                        }
                                    }
                                cutFV:;
                                    byte[] fvtemp = new byte[cutLengthFV - 20];
                                    for (int i = 20; i < cutLengthFV; i++)
                                    {
                                        fvtemp[i - 20] = mPreRegFVRegTemps[0][i];
                                    }
                                    fvtemp = compress.defCompress(fvtemp);

                                    FVrec = new byte[fvtemp.Length + 2];
                                    for (int i = 0; i < fvtemp.Length; i++)
                                    {
                                        FVrec[i + 2] = fvtemp[i];
                                    }
                                    FVrec[0] = (byte)(fvtemp.Length / 256);
                                    FVrec[1] = (byte)(fvtemp.Length % 256);


                                    //reduce the size of fingerprint
                                    int cutLengthFP = 0;
                                    for (int i = temp.Length - 1; i >= 0; i--)
                                    {
                                        if (temp[i] != 0x00)
                                        {                                         
                                            cutLengthFP = i + 1;
                                            goto cutFP;
                                        }
                                    }
                                cutFP:;
                                    FPrec = new byte[cutLengthFP + 2];
                                    for (int i = 2; i < cutLengthFP + 2; i++)
                                    {
                                        FPrec[i] = temp[i];
                                    }
                                    FPrec[0] = (byte)(cutLengthFP / 256);
                                    FPrec[1] = (byte)(cutLengthFP % 256);

                                    if (zkfverrdef.ZKFV_ERR_OK != nRet)
                                    {
                                        SetTextBox("Enrollment Failed: Database Addition Failed!");
                                        tboxDep.Enabled = true;
                                        tboxSID.Enabled = true;
                                        tboxName.Enabled = true;
                                        bnEnrollFV.Enabled = true;
                                        bnVerify.Enabled = true;
                                        return;
                                    }

                                    Array.Copy(temp, mRegFPRegTemp, nTempLen);
                                    mRegFPRegTempLen = nTempLen;
                                    for (int i = 0; i < mEnrollCnt; i++)
                                    {
                                        Array.Copy(mPreRegFVRegTemps[i], mRegFVRegTemps[i], mPreRegFVTempLen[i]);
                                        mRegFVTempLen[i] = mPreRegFVTempLen[i];
                                    }
                                    isFVreg[rowSelect] = "Registed";

                                    SetTextBox("Enrollment Completed.");
                                    bnEnrollFV.Enabled = true;
                                    bnVerify.Enabled = false;
                                    return;
                                }
                                else
                                {
                                    SetTextBox("Press the same finger on the machine for " + (mEnrollCnt - mEnrollIdx) + " more times.");
                                }
                            }
                            else
                            {
                                if (mbIdentify)
                                {
                                    int nFingerId = 0;
                                    int nScore = 0;
                                    int ret = 0;

                                    //zkfv.DBSecurityHybridIdentify
                                    //zkfv.DBFakeHybridIdentify
                                    //zkfv.DBIdentifyFV(mDBHandle, mfvTemp, ref nFingerId, ref nScore)
                                    //zkfv.DBIdentifyFP(mDBHandle, mfpTemp, ref nFingerId, ref nScore)
                                    if (idMode == 1)
                                    {
                                        ret = zkfv.DBNormalHybridIdentify(mDBHandle, mfpTemp, mfvTemp, ref nFingerId, ref nScore);
                                    }
                                    else if (idMode == 2)
                                    {
                                        ret = zkfv.DBFakeHybridIdentify(mDBHandle, mfpTemp, mfvTemp, ref nFingerId, ref nScore);
                                    }
                                    else if (idMode == 3)
                                    {
                                        ret = zkfv.DBSecurityHybridIdentify(mDBHandle, mfpTemp, mfvTemp, ref nFingerId, ref nScore);
                                    }
                                    else if (idMode == 4)
                                    {
                                        FVtempMB = mfvTemp;
                                        isMBempty = false;
                                        if (!isMCempty)
                                        {
                                            nScore = zkfv.DBMatchFV(mDBHandle, FVtempMB, FVtempMC);
                                            if (nScore >= 70)
                                            {
                                                SetTextBox("Access Granted. Matched Score: " + nScore);
                                                isMBempty = true;
                                                isMCempty = true;
                                            }
                                            else
                                            {
                                                SetTextBox("Access Denied. Matched Score:" + nScore);
                                            }
                                        }
                                        goto mbFingerTestEnd;
                                    }

                                    if (zkfverrdef.ZKFV_ERR_OK != ret)
                                    {
                                        SetTextBox("Identify Failed: No matched record found.");
                                    }
                                    else
                                    {
                                        if (idMode == 1)
                                        {
                                            SetTextBox("[Normal Hybrid Identify Mode] Username: " + username[nFingerId] + ",  Score = " + nScore);
                                        }
                                        else if (idMode == 2)
                                        {
                                            SetTextBox("[Fake Hybrid Identify Mode] Username: " + username[nFingerId] + ", Score = " + nScore);
                                        }
                                        else if (idMode == 3)
                                        {
                                            SetTextBox("[Security Hybrid Identify Mode] Username: " + username[nFingerId] + ",  Score = " + nScore);
                                        }
                                    }
                                mbFingerTestEnd:;
                                    if (mbRecord)
                                    {
                                        string modestr = "";
                                        if (idMode == 1)
                                        {
                                            modestr = "(Either) [MifareCard Authentication], [Normal Hybrid Identify Mode]";

                                        }
                                        else if (idMode == 2)
                                        {
                                            modestr = "(Either) [MifareCard Authentication], [Fake Hybrid Identify Mode]";
                                        }
                                        else if (idMode == 3)
                                        {
                                            modestr = "(Either) [MifareCard Authentication], [Security Hybrid Identify Mode]";

                                        }
                                        else if (idMode == 4)
                                        {
                                            modestr = "(Both) [MifareCard Authentication], [Finger Vein identify Mode]";
                                        }
                                        if (nFingerId == 0)
                                        {
                                            recordData.Rows.Add(DateTime.Now.ToString("MM/dd/yyyy H:mm:ss"), "N/A", "<Unknown Person>", "N/A", modestr, nScore);
                                        }
                                        else
                                        {
                                            recordData.Rows.Add(DateTime.Now.ToString("MM/dd/yyyy H:mm:ss"), staffID[nFingerId], username[nFingerId], department[nFingerId], modestr, nScore);
                                        }
                                        dataGridViewRec.DataSource = recordData;
                                    }
                                }
                                else
                                {
                                    int fpScore = 0; //finger print score
                                    int fvScore = 0; // finger vein score
                                    int nTemp = 0;   // Score temp for finger vein score
                                    fpScore = zkfv.DBMatchFP(mDBHandle, mRegFPRegTemp, mfpTemp);
                                    for (int i = 0; i < mEnrollCnt; i++)
                                    {
                                        nTemp = zkfv.DBMatchFV(mDBHandle, mRegFVRegTemps[i], mfvTemp);
                                        if (nTemp > fvScore) fvScore = nTemp;
                                    }
                                    SetTextBox("Matched fingerprint score = " + fpScore + ", matched fingervein score = " + fvScore);
                                }
                            }
                        }
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        private void bnRecordMode_Click(object sender, EventArgs e)
        {
            //reset the enrollment;
            mbIdentify = true;
            mcRecord = true;
            if(idMode == 4)
            {
                mcmbIdentify = true;
            }
            if (bnDisconMC.Enabled)
            {
                Thread mifareReadThread = new Thread(new ThreadStart(MifareRecordCapture));
                mifareReadThread.IsBackground = true;
                mifareReadThread.Start();
            }

            mbRecord = true;
            tboxDep.Enabled = false;
            tboxSID.Enabled = false;
            tboxName.Enabled = false;
            tboxDep.Text = "";
            tboxSID.Text = "";
            tboxName.Text = "";

            if (mbRegister)
            {
                mEnrollIdx = 0;
                mbRegister = false;
                bnEnrollFV.Enabled = true;
                bnVerify.Enabled = false;
            }
            bnAddNew.Enabled = false;
            bnDelete.Enabled = false;
            bnEdit.Enabled = false;
            //enable record button
            dataGridViewRec.Visible = true;
            bnSearch.Enabled = true;
            tboxSearch.Enabled = true;
            bnReturn.Enabled = false;
            tboxSearch.Text = "";
            gboxRecord.Visible = true;
            dataGridViewRec.DataSource = recordData;
            dataGridViewRec.Refresh();
            // invisible other mode component
            dataGridViewEN.Visible = false;
            gboxSetting.Visible = false;
            gboxEnroll.Visible = false;

            //menu button enable
            bnEnrollMode.Enabled = true;
            bnRecordMode.Enabled = false;
            bnSettingMode.Enabled = true;

            //mode switch
            recordMode = true;
            enrollmentMode = false;

            //print result
            SetTextBox("Switch to <Record Mode>");
        }

        private void bnSetting_Click(object sender, EventArgs e)
        {
            mcRecord = false;
            //reset the enrollment;
            dataGridViewRec.DataSource = recordData;
            dataGridViewRec.Refresh();

            if (mbRegister)
            {
                mEnrollIdx = 0;
                mbRegister = false;

                bnEnrollFV.Enabled = true;
                bnEnrollFV.Enabled = true;
            }

            //enable setting mode button
            gboxSetting.Visible = true;
            dataGridViewRec.Visible = true;

            //table component
            bnEdit.Enabled = false;
            bnDelete.Enabled = false;
            bnSearch.Enabled = false;
            bnAddNew.Enabled = false;
            bnReturn.Enabled = false;
            tboxSearch.Enabled = false;

            //Invisible other mode's component
            dataGridViewEN.Visible = false;
            gboxEnroll.Visible = false;
            gboxRecord.Visible = false;

            //mode switch
            recordMode = false;
            enrollmentMode = false;

            //menu button enable
            bnSettingMode.Enabled = false;
            bnEnrollMode.Enabled = true;
            bnRecordMode.Enabled = true;

            SetTextBox("Switch to <Setting Mode>");
        }

        private void bnUNconfirm_Click(object sender, EventArgs e)
        {
            //check if empty textbox
            if (string.IsNullOrWhiteSpace(tboxDep.Text) || string.IsNullOrWhiteSpace(tboxName.Text) || string.IsNullOrWhiteSpace(tboxSID.Text))
            {
                SetTextBox("Please fill in all the information.");
                return;
            }
            int regStaffID;

            //check if the staff id is int
            try
            {
                regStaffID = Int32.Parse(tboxSID.Text.Trim());
            }
            catch
            {
                SetTextBox("Invaild userID");
                return;
            }

            //check for taken staffId
            for (int i = 0; i < staffID.Length; i++)
            {
                if (staffID[i] != 0)
                {
                    if (staffID[i].ToString() == tboxSID.Text.Trim() && i != rowSelect)
                    {
                        SetTextBox("UserID has been taken.");
                        return;
                    }
                }
            }

            //get and clear the text box
            staffID[rowSelect] = Int32.Parse(tboxSID.Text);
            username[rowSelect] = tboxName.Text;
            department[rowSelect] = tboxDep.Text;
            tboxDep.Text = "";
            tboxSID.Text = "";
            tboxName.Text = "";
            tboxMCID.Text = "";

            //update the data table
            foreach (DataRow row in enrollData.Rows)
            {
                if (row[0].ToString() == rowSelect.ToString())
                {
                    row[1] = staffID[rowSelect];
                    row[2] = username[rowSelect];
                    row[3] = department[rowSelect];
                    row[4] = isFVreg[rowSelect];
                    if(mifareCardID[rowSelect] != "Not Registed")
                    {
                      row[5] = mifareCardID[rowSelect];                        
                    }

                    dataGridViewEN.DataSource = enrollData;
                    dataGridViewEN.Refresh();
                    goto loop;

                }
            }
        loop:;

            //termiate the enrollment
            if (bnCancel.Enabled)
            {
                bnCancel.Enabled = false;
                mcRead = false;
                SetTextBox(" MifareCard enrollment cancelled");
            }
            if (bnVerify.Enabled)
            {
                mEnrollIdx = 0;
                mbRegister = false;
                bnVerify.Enabled = false;
                SetTextBox("FingerVein enrollment cancelled.");
            }

            //button disable
            bnEnrollFV.Enabled = false;
            bnGetCardID.Enabled = false;
            bnUNconfirm.Enabled = false;
            tboxName.Enabled = false;
            tboxDep.Enabled = false;
            tboxSID.Enabled = false;
            tboxMCID.Enabled = false;
            BnRead4K.Enabled = false;
            bnWrite4K.Enabled = false;
            bnReadVein.Enabled = false;
            bnWriteVein.Enabled = false;
        }

        private void bnEnrollMode_Click(object sender, EventArgs e)
        {
            //reset the enrollment;
            mcRecord = false;

            tboxDep.Text = "";
            tboxSID.Text = "";
            tboxName.Text = "";

            //change the table and the groupbox
            dataGridViewEN.DataSource = enrollData;
            dataGridViewEN.Refresh();   
            dataGridViewEN.Visible = true;
            gboxEnroll.Visible = true;

            //table component enable
            bnReturn.Enabled = false;
            bnAddNew.Enabled = true;
            tboxSearch.Enabled = true;
            bnSearch.Enabled = true;
            tboxSearch.Text = "";

            //invisible other mode component
            dataGridViewRec.Visible = false;
            gboxSetting.Visible = false;
            gboxRecord.Visible = false;

            //Mode switch
            enrollmentMode = true;
            recordMode = false;
            settingMode = false;

            //menu button
            bnEnrollMode.Enabled = false;
            bnRecordMode.Enabled = true;
            bnSettingMode.Enabled = true;

            //print result
            SetTextBox( "Switch to <Enrollment Mode>");
        }

        private void bnReturn_Click(object sender, EventArgs e)
        {
            bnReturn.Enabled = false;
            if (enrollmentMode)
            {
                bnAddNew.Enabled = true;
                dataGridViewEN.DataSource = enrollDataSearch;
                dataGridViewEN.Refresh();

            }
            else if (recordMode)
            {
                dataGridViewRec.DataSource = recordData;
                dataGridViewRec.Refresh();
            }

        }

        private void bnConMC_Click(object sender, EventArgs e)
        {
            SetTextBox("Mifare card device connecting...");

            hReader = mifare.Mif_OpenPort(devNumMC);
            if (hReader == -1)
            {
                SetTextBox("Mifare card reader unable to connect.");
                return;
            }

            //component enable
            bnConMC.Enabled = false;
            listDevMC.Enabled = false;
            bnDisconMC.Enabled = true;

            //result
            SetTextBox("Mifare card device connected.");
        }

        private void bnReinFV_Click(object sender, EventArgs e)
        {

            SetTextBox("FingerVein machine reinitializing...");

            //Component disable and clear
            bnReadVein.Enabled = false;
            bnConFV.Enabled = false;
            listDevFV.Enabled = false;
            listDevFV.Items.Clear();


            int ret = zkfverrdef.ZKFV_ERR_OK;
            if (zkfverrdef.ZKFV_ERR_OK != (ret = zkfv.Init()))
            {
                SetTextBox("Initialize error, retcode = " + ret.ToString());
                bnReadVein.Enabled = true;
                return;
            }

            //Device list
            int nCount = zkfv.GetDeviceCount();
            if (nCount <= 0)
            {
                SetTextBox("No fingerVein machine connected!");
                zkfv.Terminate();
                bnReadVein.Enabled = true;
                return;
            }
            for (int i = 0; i < nCount; i++)
            {
                listDevFV.Items.Add(i.ToString());
            }
            listDevFV.SelectedIndex = 0;
            listDevFV.Enabled = true;


            //Component enable
            bnReinFV.Enabled = true;
            bnConFV.Enabled = true;

            SetTextBox("FingerVein machine reinitalized.");
        }

        private void bnDisconMC_Click(object sender, EventArgs e)
        {

            SetTextBox("Mifare card device disconnecting...");

            //component enable
            bnConMC.Enabled = true;
            bnDisconMC.Enabled = false;
            listDevMC.Enabled = true;
            mifare.Mif_ClosePort(hReader);

            bnGetCardID.Enabled = false;
            bnCancel.Enabled = false;

            //print result
            SetTextBox("Mifare card device disconnected.");
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            bnReturn.Enabled = true;
            string txt = tboxSearch.Text;
            if (enrollmentMode)
            {
                enrollDataSearch.Rows.Clear();
                bnAddNew.Enabled = false;
                foreach (DataRow row in enrollData.Rows)
                {
                    if (row[2].ToString() == txt)
                    {
                        enrollDataSearch.ImportRow(row);
                        dataGridViewEN.DataSource = enrollDataSearch;
                        dataGridViewEN.Refresh();
                        SetTextBox("Searching result of " + txt + ".");
                    }
                }
            }
            else if (recordMode)
            {
                enrollDataSearch.Rows.Clear();
                foreach (DataRow row in recordData.Rows)
                {
                    if (row[1].ToString() == txt)
                    {
                        recordDataSearch.ImportRow(row);
                        dataGridViewRec.DataSource = recordDataSearch;
                        dataGridViewRec.Refresh();
                        SetTextBox("Searching result of " + txt + ".");
                    }
                }
            }
        }

        private void bnAddNew_Click(object sender, EventArgs e)
        {
            username[mFingerID] = "";
            staffID[mFingerID] = 0;
            department[mFingerID] = "";

            enrollData.Rows.Add(mFingerID.ToString(), "", "", department[mFingerID], isFVreg[mFingerID], "-");
            dataGridViewEN.DataSource = enrollData;
            dataGridViewEN.Refresh();
            mFingerID++;
        }

        private void DataGridViewEN_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (tboxSID.Enabled)
            {
                tboxSID.Enabled = false;
                tboxName.Enabled = false;
                tboxMCID.Enabled = false;
                tboxDep.Enabled = false;
                tboxDep.Text = "";
                tboxMCID.Text = "";
                tboxName.Text = "";
                tboxSID.Text = "";
            }

            dataGridViewEN.CurrentRow.Selected = true;
            try
            {
                rowSelecting = Int32.Parse(dataGridViewEN.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
            bnEdit.Enabled = true;
            bnDelete.Enabled = true;
        }

        private void BnEdit_Click(object sender, EventArgs e)
        {
            if (rowSelecting != -1)
            {
                rowSelect = rowSelecting;
                if (bnDisconFV.Enabled)
                {
                    bnEnrollFV.Enabled = true;
                }
                if (bnDisconMC.Enabled)
                {
                    tboxMCID.Enabled = true;
                    bnGetCardID.Enabled = true;
                    bnReadVein.Enabled = true;
                    bnWriteVein.Enabled = true;
                    bnWrite4K.Enabled = true;
                    BnRead4K.Enabled = true;
                }
                tboxMCID.Text = mifareCardID[rowSelect];
                if(staffID[rowSelect]!= 0)
                {
                   tboxSID.Text = staffID[rowSelect].ToString();
                }
                else
                {
                    tboxSID.Text = "";
                }
                if(mifareCardID[rowSelect] != "0")
                {
                    tboxMCID.Text = mifareCardID[rowSelect];
                }
                else
                {
                    tboxMCID.Text = "";
                }
                tboxName.Text = username[rowSelect];
                tboxDep.Text = department[rowSelect];
                tboxSID.Enabled = true;
                tboxName.Enabled = true;
                tboxDep.Enabled = true;
                bnUNconfirm.Enabled = true;
            }
        }

        private void BnDelete_Click(object sender, EventArgs e)
        {
            if (rowSelecting != -1)
            {
                rowSelect = rowSelecting;

                staffID[rowSelect] = 0;
                username[rowSelect] = "";
                department[rowSelect] = "";
                mifareCardID[rowSelect] = "Not Registed";
                zkfv.DBDel(mDBHandle, rowSelect);
                foreach (DataRow row in enrollData.Rows)
                {
                    if (row[0].ToString() == rowSelect.ToString())
                    {
                        row.Delete();
                        dataGridViewEN.DataSource = enrollData;
                        dataGridViewEN.Refresh();
                        SetTextBox( "Enrollment deleted");
                        return;
                    }
                }
            }
        }



        private void BnMute_Click(object sender, EventArgs e)
        {
            if (!isMute)
            {
                isMute = true;
                bnMute.Text = "Unmute";
            }
            else
            {
                isMute = false;
                bnMute.Text = "Mute";
            }
        }

        private void BnWriteVein_Click(object sender, EventArgs e)
        {
            try
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                LoadKey myKey = new LoadKey();
                myKey.keyab = 0x00;
                int FVindex = 0;

                //Request
                if (mifare.Mif_Request(hReader) <= 0)
                {
                    SetTextBox("Request Error: No Card Dectected");
                    return;
                }


                for (sectorIndex = 1; sectorIndex < block1K.Length; sectorIndex++)
                {
                    myKey.Sector = sectorIndex;
                    if (mifare.Mif_Auth(hReader, LoadKey.KeyToByteArray(myKey)) <= 0)
                    {
                        SetTextBox("Authentication error in Sector - " + myKey.Sector);
                        return;
                    }

                    for (int blockIndex = 0; blockIndex < block1K[sectorIndex].Length; blockIndex++)
                    {
                        myKey.Block = block1K[sectorIndex][blockIndex];
                        for (int dataIndex = 0; dataIndex < 16; dataIndex++)
                        {

                            myKey.Data[dataIndex] = FVrec[FVindex++];
                            if (FVindex >= FVrec.Length)
                            {
                                for (int i = dataIndex + 1; i < 16; i++)
                                {
                                    myKey.Data[i] = 0x00;
                                }
                                if (mifare.Mif_Write(hReader, LoadKey.KeyToByteArray(myKey)) <= 0)
                                {
                                    SetTextBox("Write Data Error when writing Block - " + myKey.Block);
                                    return;
                                }
                                goto writeVeinCompleted;
                            }

                        }


                        if (mifare.Mif_Write(hReader, LoadKey.KeyToByteArray(myKey)) <= 0)
                        {
                            SetTextBox("Write Data Error when writing Block - " + myKey.Block);
                            return;
                        }
                    }
                }
            writeVeinCompleted:;
                if (!isMute)
                {
                    mifare.Mif_BuzOn(hReader);
                    mifare.Mif_BuzOff(hReader);
                }

                watch.Stop();
                SetTextBox(($"Execution Time: {watch.ElapsedMilliseconds} ms"));
                SetTextBox("Write Complete!");
                return;
                    }
            catch
            {
                SetTextBox("Please enroll your finger vein first");
            }

        }

        private void BnRead4K_Click(object sender, EventArgs e)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            LoadKey FVFPkey = new LoadKey();
            int FVlength = 0;
            int FPlength = 0;
            byte[] byteIn = new byte[44];
            int FVindex = 0;
            int FPindex = 0;
            //Request

            if (mifare.Mif_Request(hReader) <= 0)
            {
                SetTextBox("No Card");
                return;
            }
            //Anti-Col
            if (mifare.Mif_Anticol(hReader, cID) <= 0)
            {
                SetTextBox("Anti-Collision Error");
                return;
            }

            if (mifare.Mif_Select(hReader, cID) <= 0)
            {
                SetTextBox("Select Error");
                return;
            }

            //read how many byte need to read
            FVFPkey.Sector = 0x01;
            if (mifare.Mif_Auth(hReader, LoadKey.KeyToByteArray(FVFPkey)) <= 0)
            {
                SetTextBox("Authentication error in Sector - " + key.Sector);
                return;
            }
            FVFPkey.Block = block1K[1][0];
            byteIn = LoadKey.KeyToByteArray(FVFPkey);
            if (mifare.Mif_Read(hReader, byteIn) <= 0)
            {
                SetTextBox("Read data error Block: " + FVFPkey.Block);
                return;
            }
            FVFPkey = LoadKey.ByteArrayToKey(byteIn);
            FVlength = FVFPkey.Data[0] * 256 + FVFPkey.Data[1];

            FVresult = new byte[1024];
            int dataIndex = 2;
            for ( sectorIndex = 1; sectorIndex < 16; sectorIndex++)
            {
                FVFPkey.Sector = sectorIndex;
                if (mifare.Mif_Auth(hReader, LoadKey.KeyToByteArray(FVFPkey)) <= 0)
                {
                    SetTextBox("Authentication error in Sector - " + FVFPkey.Sector);
                    return;
                }

                for (int blockIndex = 0; blockIndex < block4K[sectorIndex].Length; blockIndex++)
                {
                    FVFPkey.Block = block1K[sectorIndex][blockIndex];
                    byteIn = LoadKey.KeyToByteArray(FVFPkey);
                    if (mifare.Mif_Read(hReader, byteIn) <= 0)
                    {
                        SetTextBox("Read data error Block: " + FVFPkey.Block);
                        return;
                    }
                    FVFPkey = LoadKey.ByteArrayToKey(byteIn);

                    for (; dataIndex < 16; dataIndex++)
                    {
                        FVresult[FVindex++] = FVFPkey.Data[dataIndex];
                        if (FVindex >= FVlength)
                        {
                            for (int i = FVindex; i < 1024; i++)
                            {
                                FVresult[i] = 0x00;
                            }
                            dataIndex = 0;
                            goto readFVCompleted;
                        }


                    }
                    dataIndex = 0;
                }
            }
        readFVCompleted:;

            //read how many byte need to read
            FVFPkey.Sector = 0x11;
            if (mifare.Mif_Auth(hReader, LoadKey.KeyToByteArray(FVFPkey)) <= 0)
            {
                SetTextBox("Authentication error in Sector - " + key.Sector);
                return;
            }

            FVFPkey.Block = block4K[17][0];
            byteIn = LoadKey.KeyToByteArray(FVFPkey);
            if (mifare.Mif_Read(hReader, byteIn) <= 0)
            {
                SetTextBox("Read data error Block: " + FVFPkey.Block);
                return;
            }
            FVFPkey = LoadKey.ByteArrayToKey(byteIn);
            FPlength = FVFPkey.Data[0] * 256 + FVFPkey.Data[1];

            FPresult = new byte[2048];
            dataIndex = 2;
            for (sectorIndex = 17; sectorIndex < 40; sectorIndex++)
            {
                FVFPkey.Sector = sectorIndex;
                if (mifare.Mif_Auth(hReader, LoadKey.KeyToByteArray(FVFPkey)) <= 0)
                {
                    SetTextBox("Authentication error in Sector - " + FVFPkey.Sector);
                    return;
                }

                for (int blockIndex = 0; blockIndex < block4K[sectorIndex].Length; blockIndex++)
                {
                    FVFPkey.Block = block4K[sectorIndex][blockIndex];
                    byteIn = LoadKey.KeyToByteArray(FVFPkey);
                    if (mifare.Mif_Read(hReader, byteIn) <= 0)
                    {
                        SetTextBox("Read data error Block: " + FVFPkey.Block);
                        return;
                    }
                    FVFPkey = LoadKey.ByteArrayToKey(byteIn);

                    for (; dataIndex < 16; dataIndex++)
                    {
                        FPresult[FPindex++] = FVFPkey.Data[dataIndex];
                        if (FPindex >= FPlength)
                        {

                            for (int i = FPindex; i < 2048; i++)
                            {
                                FPresult[i] = 0x00;
                            }
                            goto readFPCompleted;
                        }
                    }
                    dataIndex = 0;
                }
            }
        readFPCompleted:;
            SetTextBox("FP result: " + BitConverter.ToString(FPresult));
            if (!isMute)
            {
                mifare.Mif_BuzOn(hReader);
                mifare.Mif_BuzOff(hReader);
            }
            watch.Stop();
            SetTextBox("fv result: " + BitConverter.ToString(FVresult));
            SetTextBox("fp result: " + BitConverter.ToString(FPresult));
            SetTextBox(($"Execution Time: {watch.ElapsedMilliseconds} ms"));
            SetTextBox("Read Complete.");

        }

   

        private void BnReadVein_Click(object sender, EventArgs e)
        {
            byte[] fvResultTemp;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            FVlength = 0;
            FVindex = 0;

            //Request
            if (mifare.Mif_Request(hReader) <= 0)
            {
                SetTextBox("No Card Dectected.");
                return;
            }


            //read how many byte need to read
            byteIn[0] = 0x01;
            if (mifare.Mif_Auth(hReader, byteIn) <= 0)
            {
                SetTextBox("Authentication error in Sector - " + key.Sector);
                return;
            }

            byteIn[1] = block1K[1][0];

            if (mifare.Mif_Read(hReader, byteIn) <= 0)
            {
                SetTextBox("Read data error block: " + veinKey.Block);
                return;
            }

            FVlength = byteIn[3] * 256 + byteIn[4];
            fvResultTemp = new byte[FVlength];
            for (dataIndex = 5; dataIndex < 19; dataIndex++)
            {
                fvResultTemp[FVindex++] = byteIn[dataIndex];          
            }

            byteIn[0] = 1;
            blockIndex = 1;
            for (;;)
            {
                for (;blockIndex < 3;)
                {
                    byteIn[1] = block1K[byteIn[0]][blockIndex++];
                    if (mifare.Mif_Read(hReader, byteIn) <= 0)
                    {
                        SetTextBox("Read data error Block: " + byteIn[1]);
                        return;
                    }

                    for (dataIndex = 3; dataIndex < 19;)
                    {
                        fvResultTemp[FVindex++] = byteIn[dataIndex++];
                        if (FVindex >= FVlength)
                        {
                            goto readVeinCompleted;
                        }
                    }
                }
                blockIndex = 0;
                byteIn[0]++;
                if (mifare.Mif_Auth(hReader, byteIn) <= 0)
                {
                    SetTextBox("Authentication error in Sector - " + byteIn[0]);
                    return;
                }
            }
        readVeinCompleted:;

            if (!isMute)
            {
                mifare.Mif_BuzOn(hReader);
                mifare.Mif_BuzOff(hReader);
            }
            watch.Stop();
            fvResultTemp = compress.defDecompress(fvResultTemp);
            FVresult = new byte[1024];
            FVresult[0] = 0x5A;
            FVresult[1] = 0x46;
            FVresult[2] = 0x56;
            FVresult[3] = 0x01;
            FVresult[4] = 0x0C;
            FVresult[5] = 0x0D;
            FVresult[6] = 0x00;
            FVresult[7] = 0x00;
            FVresult[8] = 0x00;
            FVresult[9] = 0x00;
            FVresult[10] = 0x00;
            FVresult[11] = 0x00;
            FVresult[12] = 0x10;
            FVresult[13] = 0x09;
            FVresult[14] = 0x08;
            FVresult[15] = 0x01;
            FVresult[16] = 0x20;
            FVresult[17] = 0x01;
            FVresult[18] = 0x00;
            FVresult[19] = 0x00;
            for (FVindex = 0; FVindex<fvResultTemp.Length; FVindex++)
            {
                FVresult[FVindex+20] = fvResultTemp[FVindex];
            }
            for (int i = FVindex; i < 1024; i++)
            {
                FVresult[i] = 0x00;
            }
            SetTextBox(BitConverter.ToString(FVresult));
            SetTextBox(($"Execution Time: {watch.ElapsedMilliseconds} ms"));
            SetTextBox("Read Complete.");
        }

        private void bnWrite4K_Click(object sender, EventArgs e)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            LoadKey myKey = new LoadKey();
            myKey.keyab = 0x00;
            int FVindex = 0;
            int FPindex = 0;
            int blockIndex = 0;

            //Request
            if (mifare.Mif_Request(hReader) <= 0)
            {
                SetTextBox("Request Error: No Card Dectected");
                return;
            }

            //Anti-Collision
            if (mifare.Mif_Anticol(hReader, cID) <= 0)
            {
                SetTextBox("Anti-Collision Error");
                return;
            }

            //Select
            if (mifare.Mif_Select(hReader, cID) <= 0)
            {
                SetTextBox("Select Error");
                return;
            }

            for (sectorIndex = 1; sectorIndex < 16; sectorIndex++)
            {
                myKey.Sector = sectorIndex;
                if (mifare.Mif_Auth(hReader, LoadKey.KeyToByteArray(myKey)) <= 0)
                {
                    SetTextBox("Authentication error in Sector - " + myKey.Sector);
                    return;
                }

                blockIndex = 0;
                for (; blockIndex < block4K[sectorIndex].Length; blockIndex++)
                {
                    myKey.Block = block4K[sectorIndex][blockIndex];
                    for (int dataIndex = 0; dataIndex < 16; dataIndex++)
                    {

                        myKey.Data[dataIndex] = FVrec[FVindex++];
                        if (FVindex >= FVrec.Length)
                        {
                            for (int i = dataIndex; i < 16; i++)
                            {
                                myKey.Data[i] = 0x00;
                            }
                            if (mifare.Mif_Write(hReader, LoadKey.KeyToByteArray(myKey)) <= 0)
                            {
                                SetTextBox("Write Data Error when writing Block - " + myKey.Block);
                                return;
                            }
                            goto writeFVCompleted;
                        }
                    }



                        if (mifare.Mif_Write(hReader, LoadKey.KeyToByteArray(myKey)) <= 0)
                        {
                            SetTextBox("Write Data Error when writing Block - " + myKey.Block);
                            return;
                        }
                    }
                }
            
            writeFVCompleted:;
                sectorIndex = 0x11;


                for (; sectorIndex < block4K.Length; sectorIndex++)
                {
                    myKey.Sector = sectorIndex;
                    if (mifare.Mif_Auth(hReader, LoadKey.KeyToByteArray(myKey)) <= 0)
                    {
                        SetTextBox("Authentication error in Sector - " + myKey.Sector);
                        return;
                    }

                    blockIndex = 0;
                    for (; blockIndex < block4K[sectorIndex].Length; blockIndex++)
                    {
                        myKey.Block = block4K[sectorIndex][blockIndex];
                        for (int dataIndex = 0; dataIndex < 16; dataIndex++)
                        {

                            myKey.Data[dataIndex] = FPrec[FPindex++];
                            if (FPindex >= FPrec.Length)
                            {
                                for (int i = dataIndex; i < 16; i++)
                                {
                                    myKey.Data[i] = 0x00;
                                }
                                if (mifare.Mif_Write(hReader, LoadKey.KeyToByteArray(myKey)) <= 0)
                                {
                                    SetTextBox("Write Data Error when writing Block - " + myKey.Block);
                                    return;
                                }
                                goto writeFPCompleted;
                            }

                        }


                        if (mifare.Mif_Write(hReader, LoadKey.KeyToByteArray(myKey)) <= 0)
                        {
                            SetTextBox("Write Data Error when writing Block - " + myKey.Block);
                            return;
                        }
                    }
                }
            writeFPCompleted:;
            if (!isMute)
            {
                mifare.Mif_BuzOn(hReader);
                mifare.Mif_BuzOff(hReader);
            }
            watch.Stop();
                SetTextBox("FV record:" + BitConverter.ToString(FVrec));
                SetTextBox("FP record:" + BitConverter.ToString(FPrec));
                SetTextBox(($"Execution Time: {watch.ElapsedMilliseconds} ms"));
                SetTextBox("Write Complete!");
                return;
            }

        private void ListDevMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            devNumMC = (byte)Int32.Parse(listDevMC.SelectedItem.ToString());
        }

        private void ListIDmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listIDmode.SelectedItem.ToString() == "(Either) [MifareCard Authentication], [Normal Hybrid Identify Mode]")
            {
                idMode = 1;
                SetTextBox("(Either) [MifareCard Authentication], [Normal Hybrid Identify Mode]");
            }
            else if (listIDmode.SelectedItem.ToString() == "(Either) [MifareCard Authentication], [Fake Hybrid Identify Mode]")
            {
                idMode = 2;
                SetTextBox("(Either) [MifareCard Authentication], [Fake Hybrid Identify Mode]");
            }
            else if(listIDmode.SelectedItem.ToString() == "(Either) [MifareCard Authentication], [Security Hybrid Identify Mode]")
            {
                idMode = 3;
                SetTextBox("(Either) [MifareCard Authentication], [Security Hybrid Identify Mode]");
            }
            else if(listIDmode.SelectedItem.ToString() == "(Both) [MifareCard Authentication], [Finger Vein identify Mode]")
            { 
                idMode = 4;
                SetTextBox("(Both) [MifareCard Authentication], [Finger Vein identify  Mode]");
            }
      
            if (idMode != 4)
            {
                mcmbIdentify = false;
            }
        }
    }
    }


