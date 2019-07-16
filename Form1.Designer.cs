using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace csdemo
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
       
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bnReturn = new System.Windows.Forms.Button();
            this.listIDmode = new System.Windows.Forms.ComboBox();
            this.listDevFV = new System.Windows.Forms.ComboBox();
            this.tboxName = new System.Windows.Forms.TextBox();
            this.picFP = new System.Windows.Forms.PictureBox();
            this.picFV = new System.Windows.Forms.PictureBox();
            this.bnConFV = new System.Windows.Forms.Button();
            this.bnDisconFV = new System.Windows.Forms.Button();
            this.bnEnrollFV = new System.Windows.Forms.Button();
            this.bnVerify = new System.Windows.Forms.Button();
            this.bnSettingMode = new System.Windows.Forms.Button();
            this.bnRecordMode = new System.Windows.Forms.Button();
            this.bnUNconfirm = new System.Windows.Forms.Button();
            this.labelIDMode = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelDLFV = new System.Windows.Forms.Label();
            this.bnEnrollMode = new System.Windows.Forms.Button();
            this.dataGridViewEN = new System.Windows.Forms.DataGridView();
            this.labelSID = new System.Windows.Forms.Label();
            this.tboxSID = new System.Windows.Forms.TextBox();
            this.labelDep = new System.Windows.Forms.Label();
            this.tboxDep = new System.Windows.Forms.TextBox();
            this.bnReinFV = new System.Windows.Forms.Button();
            this.bnDisconMC = new System.Windows.Forms.Button();
            this.labelFV = new System.Windows.Forms.Label();
            this.labelMC = new System.Windows.Forms.Label();
            this.labelDLMC = new System.Windows.Forms.Label();
            this.listDevMC = new System.Windows.Forms.ComboBox();
            this.bnConMC = new System.Windows.Forms.Button();
            this.bnSearch = new System.Windows.Forms.Button();
            this.tboxSearch = new System.Windows.Forms.TextBox();
            this.bnAddNew = new System.Windows.Forms.Button();
            this.dataGridViewRec = new System.Windows.Forms.DataGridView();
            this.bnEdit = new System.Windows.Forms.Button();
            this.labelMCen = new System.Windows.Forms.Label();
            this.labelFPV = new System.Windows.Forms.Label();
            this.bnGetCardID = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.bnDelete = new System.Windows.Forms.Button();
            this.gboxSetting = new System.Windows.Forms.GroupBox();
            this.gboxEnroll = new System.Windows.Forms.GroupBox();
            this.tboxMCID = new System.Windows.Forms.TextBox();
            this.label1Kcard = new System.Windows.Forms.Label();
            this.label4Kcard = new System.Windows.Forms.Label();
            this.BnRead4K = new System.Windows.Forms.Button();
            this.bnWrite4K = new System.Windows.Forms.Button();
            this.labelVein = new System.Windows.Forms.Label();
            this.bnReadVein = new System.Windows.Forms.Button();
            this.bnWriteVein = new System.Windows.Forms.Button();
            this.gboxRecord = new System.Windows.Forms.GroupBox();
            this.labelVeinPrint = new System.Windows.Forms.Label();
            this.bnMute = new System.Windows.Forms.Button();
            this.imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picFP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRec)).BeginInit();
            this.gboxSetting.SuspendLayout();
            this.gboxEnroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bnReturn
            // 
            this.bnReturn.Enabled = false;
            this.bnReturn.Font = new System.Drawing.Font("Arial", 10F);
            this.bnReturn.Location = new System.Drawing.Point(1145, 63);
            this.bnReturn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnReturn.Name = "bnReturn";
            this.bnReturn.Size = new System.Drawing.Size(70, 30);
            this.bnReturn.TabIndex = 46;
            this.bnReturn.Text = "Return";
            this.bnReturn.UseVisualStyleBackColor = true;
            this.bnReturn.Click += new System.EventHandler(this.bnReturn_Click);
            // 
            // listIDmode
            // 
            this.listIDmode.Font = new System.Drawing.Font("Consolas", 10F);
            this.listIDmode.FormattingEnabled = true;
            this.listIDmode.Location = new System.Drawing.Point(15, 150);
            this.listIDmode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listIDmode.Name = "listIDmode";
            this.listIDmode.Size = new System.Drawing.Size(450, 23);
            this.listIDmode.TabIndex = 18;
            this.listIDmode.SelectedIndexChanged += new System.EventHandler(this.ListIDmode_SelectedIndexChanged);
            // 
            // listDevFV
            // 
            this.listDevFV.Font = new System.Drawing.Font("Consolas", 10F);
            this.listDevFV.FormattingEnabled = true;
            this.listDevFV.Location = new System.Drawing.Point(304, 39);
            this.listDevFV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listDevFV.Name = "listDevFV";
            this.listDevFV.Size = new System.Drawing.Size(150, 23);
            this.listDevFV.TabIndex = 14;
            // 
            // tboxName
            // 
            this.tboxName.Enabled = false;
            this.tboxName.Location = new System.Drawing.Point(28, 74);
            this.tboxName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxName.Name = "tboxName";
            this.tboxName.Size = new System.Drawing.Size(200, 22);
            this.tboxName.TabIndex = 17;
            // 
            // picFP
            // 
            this.picFP.Location = new System.Drawing.Point(17, 105);
            this.picFP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picFP.Name = "picFP";
            this.picFP.Size = new System.Drawing.Size(230, 238);
            this.picFP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFP.TabIndex = 90;
            this.picFP.TabStop = false;
            // 
            // picFV
            // 
            this.picFV.Location = new System.Drawing.Point(266, 107);
            this.picFV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picFV.Name = "picFV";
            this.picFV.Size = new System.Drawing.Size(230, 236);
            this.picFV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFV.TabIndex = 91;
            this.picFV.TabStop = false;
            // 
            // bnConFV
            // 
            this.bnConFV.Enabled = false;
            this.bnConFV.Font = new System.Drawing.Font("Arial", 10F);
            this.bnConFV.Location = new System.Drawing.Point(7, 32);
            this.bnConFV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnConFV.Name = "bnConFV";
            this.bnConFV.Size = new System.Drawing.Size(90, 36);
            this.bnConFV.TabIndex = 11;
            this.bnConFV.Text = "Connect";
            this.bnConFV.Click += new System.EventHandler(this.bnConFV_Click);
            // 
            // bnDisconFV
            // 
            this.bnDisconFV.Enabled = false;
            this.bnDisconFV.Font = new System.Drawing.Font("Arial", 10F);
            this.bnDisconFV.Location = new System.Drawing.Point(104, 31);
            this.bnDisconFV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnDisconFV.Name = "bnDisconFV";
            this.bnDisconFV.Size = new System.Drawing.Size(90, 37);
            this.bnDisconFV.TabIndex = 12;
            this.bnDisconFV.Text = "Disconnect";
            this.bnDisconFV.Click += new System.EventHandler(this.bnDisconFV_Click);
            // 
            // bnEnrollFV
            // 
            this.bnEnrollFV.Enabled = false;
            this.bnEnrollFV.Font = new System.Drawing.Font("Arial", 10F);
            this.bnEnrollFV.Location = new System.Drawing.Point(373, 16);
            this.bnEnrollFV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnEnrollFV.Name = "bnEnrollFV";
            this.bnEnrollFV.Size = new System.Drawing.Size(60, 30);
            this.bnEnrollFV.TabIndex = 25;
            this.bnEnrollFV.Text = "Enroll";
            this.bnEnrollFV.Click += new System.EventHandler(this.bnEnrollFV_Click);
            // 
            // bnVerify
            // 
            this.bnVerify.Enabled = false;
            this.bnVerify.Font = new System.Drawing.Font("Arial", 10F);
            this.bnVerify.Location = new System.Drawing.Point(435, 16);
            this.bnVerify.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnVerify.Name = "bnVerify";
            this.bnVerify.Size = new System.Drawing.Size(60, 30);
            this.bnVerify.TabIndex = 26;
            this.bnVerify.Text = "Verify";
            this.bnVerify.UseVisualStyleBackColor = true;
            this.bnVerify.Click += new System.EventHandler(this.bnVerify_Click);
            // 
            // bnSettingMode
            // 
            this.bnSettingMode.Enabled = false;
            this.bnSettingMode.Font = new System.Drawing.Font("Arial", 10F);
            this.bnSettingMode.Location = new System.Drawing.Point(6, 61);
            this.bnSettingMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnSettingMode.Name = "bnSettingMode";
            this.bnSettingMode.Size = new System.Drawing.Size(150, 36);
            this.bnSettingMode.TabIndex = 2;
            this.bnSettingMode.Text = "Setting";
            this.bnSettingMode.Click += new System.EventHandler(this.bnSetting_Click);
            // 
            // bnRecordMode
            // 
            this.bnRecordMode.Enabled = false;
            this.bnRecordMode.Font = new System.Drawing.Font("Arial", 10F);
            this.bnRecordMode.Location = new System.Drawing.Point(318, 61);
            this.bnRecordMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnRecordMode.Name = "bnRecordMode";
            this.bnRecordMode.Size = new System.Drawing.Size(150, 36);
            this.bnRecordMode.TabIndex = 4;
            this.bnRecordMode.Text = "Record";
            this.bnRecordMode.Click += new System.EventHandler(this.bnRecordMode_Click);
            // 
            // bnUNconfirm
            // 
            this.bnUNconfirm.Enabled = false;
            this.bnUNconfirm.Font = new System.Drawing.Font("Arial", 10F);
            this.bnUNconfirm.Location = new System.Drawing.Point(146, 145);
            this.bnUNconfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnUNconfirm.Name = "bnUNconfirm";
            this.bnUNconfirm.Size = new System.Drawing.Size(90, 31);
            this.bnUNconfirm.TabIndex = 24;
            this.bnUNconfirm.Text = "Confirm";
            this.bnUNconfirm.UseVisualStyleBackColor = true;
            this.bnUNconfirm.Click += new System.EventHandler(this.bnUNconfirm_Click);
            // 
            // labelIDMode
            // 
            this.labelIDMode.AutoSize = true;
            this.labelIDMode.Font = new System.Drawing.Font("Arial", 10F);
            this.labelIDMode.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelIDMode.Location = new System.Drawing.Point(7, 134);
            this.labelIDMode.Name = "labelIDMode";
            this.labelIDMode.Size = new System.Drawing.Size(96, 16);
            this.labelIDMode.TabIndex = 92;
            this.labelIDMode.Text = "Identify Mode:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Arial", 10F);
            this.labelName.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelName.Location = new System.Drawing.Point(2, 55);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(76, 16);
            this.labelName.TabIndex = 93;
            this.labelName.Text = "Username:";
            // 
            // labelDLFV
            // 
            this.labelDLFV.AutoSize = true;
            this.labelDLFV.Font = new System.Drawing.Font("Arial", 10F);
            this.labelDLFV.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelDLFV.Location = new System.Drawing.Point(301, 16);
            this.labelDLFV.Name = "labelDLFV";
            this.labelDLFV.Size = new System.Drawing.Size(81, 16);
            this.labelDLFV.TabIndex = 94;
            this.labelDLFV.Text = "Device List:";
            // 
            // bnEnrollMode
            // 
            this.bnEnrollMode.Enabled = false;
            this.bnEnrollMode.Font = new System.Drawing.Font("Arial", 10F);
            this.bnEnrollMode.Location = new System.Drawing.Point(162, 61);
            this.bnEnrollMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnEnrollMode.Name = "bnEnrollMode";
            this.bnEnrollMode.Size = new System.Drawing.Size(150, 36);
            this.bnEnrollMode.TabIndex = 3;
            this.bnEnrollMode.Text = "Enrollment";
            this.bnEnrollMode.Click += new System.EventHandler(this.bnEnrollMode_Click);
            // 
            // dataGridViewEN
            // 
            this.dataGridViewEN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEN.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            this.dataGridViewEN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEN.Font = new System.Drawing.Font("Consolas", 10F);
            this.dataGridViewEN.Location = new System.Drawing.Point(515, 401);
            this.dataGridViewEN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridViewEN.Name = "dataGridViewEN";
            this.dataGridViewEN.RowTemplate.Height = 20;
            this.dataGridViewEN.Size = new System.Drawing.Size(720, 327);
            this.dataGridViewEN.TabIndex = 95;
            this.dataGridViewEN.TabStop = false;
            this.dataGridViewEN.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewEN_CellClick_1);
            // 
            // labelSID
            // 
            this.labelSID.AutoSize = true;
            this.labelSID.Font = new System.Drawing.Font("Arial", 10F);
            this.labelSID.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelSID.Location = new System.Drawing.Point(4, 12);
            this.labelSID.Name = "labelSID";
            this.labelSID.Size = new System.Drawing.Size(58, 16);
            this.labelSID.TabIndex = 96;
            this.labelSID.Text = "User ID:";
            // 
            // tboxSID
            // 
            this.tboxSID.Enabled = false;
            this.tboxSID.Font = new System.Drawing.Font("Arial", 10F);
            this.tboxSID.Location = new System.Drawing.Point(26, 30);
            this.tboxSID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxSID.Name = "tboxSID";
            this.tboxSID.Size = new System.Drawing.Size(210, 23);
            this.tboxSID.TabIndex = 21;
            // 
            // labelDep
            // 
            this.labelDep.AutoSize = true;
            this.labelDep.Font = new System.Drawing.Font("Arial", 10F);
            this.labelDep.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelDep.Location = new System.Drawing.Point(4, 100);
            this.labelDep.Name = "labelDep";
            this.labelDep.Size = new System.Drawing.Size(86, 16);
            this.labelDep.TabIndex = 97;
            this.labelDep.Text = "Department:";
            // 
            // tboxDep
            // 
            this.tboxDep.Enabled = false;
            this.tboxDep.Font = new System.Drawing.Font("Arial", 10F);
            this.tboxDep.Location = new System.Drawing.Point(25, 117);
            this.tboxDep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxDep.Name = "tboxDep";
            this.tboxDep.Size = new System.Drawing.Size(210, 23);
            this.tboxDep.TabIndex = 23;
            // 
            // bnReinFV
            // 
            this.bnReinFV.Enabled = false;
            this.bnReinFV.Font = new System.Drawing.Font("Arial", 10F);
            this.bnReinFV.Location = new System.Drawing.Point(198, 31);
            this.bnReinFV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnReinFV.Name = "bnReinFV";
            this.bnReinFV.Size = new System.Drawing.Size(90, 37);
            this.bnReinFV.TabIndex = 13;
            this.bnReinFV.Text = "Reinitialize";
            this.bnReinFV.UseVisualStyleBackColor = true;
            this.bnReinFV.Click += new System.EventHandler(this.bnReinFV_Click);
            // 
            // bnDisconMC
            // 
            this.bnDisconMC.Enabled = false;
            this.bnDisconMC.Font = new System.Drawing.Font("Arial", 10F);
            this.bnDisconMC.Location = new System.Drawing.Point(104, 94);
            this.bnDisconMC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnDisconMC.Name = "bnDisconMC";
            this.bnDisconMC.Size = new System.Drawing.Size(90, 34);
            this.bnDisconMC.TabIndex = 44;
            this.bnDisconMC.Text = "Disconnect";
            this.bnDisconMC.UseVisualStyleBackColor = true;
            this.bnDisconMC.Click += new System.EventHandler(this.bnDisconMC_Click);
            // 
            // labelFV
            // 
            this.labelFV.AutoSize = true;
            this.labelFV.Font = new System.Drawing.Font("Arial", 10F);
            this.labelFV.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelFV.Location = new System.Drawing.Point(7, 14);
            this.labelFV.Name = "labelFV";
            this.labelFV.Size = new System.Drawing.Size(137, 16);
            this.labelFV.TabIndex = 98;
            this.labelFV.Text = "FingerVein Machine:";
            // 
            // labelMC
            // 
            this.labelMC.AutoSize = true;
            this.labelMC.Font = new System.Drawing.Font("Arial", 10F);
            this.labelMC.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelMC.Location = new System.Drawing.Point(4, 75);
            this.labelMC.Name = "labelMC";
            this.labelMC.Size = new System.Drawing.Size(133, 16);
            this.labelMC.TabIndex = 99;
            this.labelMC.Text = "MifareCard Reader:";
            // 
            // labelDLMC
            // 
            this.labelDLMC.AutoSize = true;
            this.labelDLMC.Font = new System.Drawing.Font("Arial", 12F);
            this.labelDLMC.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelDLMC.Location = new System.Drawing.Point(301, 75);
            this.labelDLMC.Name = "labelDLMC";
            this.labelDLMC.Size = new System.Drawing.Size(90, 18);
            this.labelDLMC.TabIndex = 47;
            this.labelDLMC.Text = "Device List:";
            // 
            // listDevMC
            // 
            this.listDevMC.Font = new System.Drawing.Font("Consolas", 10F);
            this.listDevMC.FormattingEnabled = true;
            this.listDevMC.Location = new System.Drawing.Point(304, 113);
            this.listDevMC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listDevMC.Name = "listDevMC";
            this.listDevMC.Size = new System.Drawing.Size(150, 23);
            this.listDevMC.TabIndex = 17;
            this.listDevMC.SelectedIndexChanged += new System.EventHandler(this.ListDevMC_SelectedIndexChanged);
            // 
            // bnConMC
            // 
            this.bnConMC.Enabled = false;
            this.bnConMC.Font = new System.Drawing.Font("Arial", 10F);
            this.bnConMC.Location = new System.Drawing.Point(13, 94);
            this.bnConMC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnConMC.Name = "bnConMC";
            this.bnConMC.Size = new System.Drawing.Size(90, 34);
            this.bnConMC.TabIndex = 15;
            this.bnConMC.Text = "Connect";
            this.bnConMC.Click += new System.EventHandler(this.bnConMC_Click);
            // 
            // bnSearch
            // 
            this.bnSearch.Enabled = false;
            this.bnSearch.Font = new System.Drawing.Font("Arial", 10F);
            this.bnSearch.Location = new System.Drawing.Point(1069, 64);
            this.bnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(70, 30);
            this.bnSearch.TabIndex = 45;
            this.bnSearch.Text = "Search";
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // tboxSearch
            // 
            this.tboxSearch.Enabled = false;
            this.tboxSearch.Location = new System.Drawing.Point(743, 71);
            this.tboxSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxSearch.Name = "tboxSearch";
            this.tboxSearch.Size = new System.Drawing.Size(320, 22);
            this.tboxSearch.TabIndex = 44;
            // 
            // bnAddNew
            // 
            this.bnAddNew.Enabled = false;
            this.bnAddNew.Font = new System.Drawing.Font("Arial", 10F);
            this.bnAddNew.Location = new System.Drawing.Point(515, 67);
            this.bnAddNew.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnAddNew.Name = "bnAddNew";
            this.bnAddNew.Size = new System.Drawing.Size(70, 30);
            this.bnAddNew.TabIndex = 41;
            this.bnAddNew.Text = "Add New";
            this.bnAddNew.Click += new System.EventHandler(this.bnAddNew_Click);
            // 
            // dataGridViewRec
            // 
            this.dataGridViewRec.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRec.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            this.dataGridViewRec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRec.Font = new System.Drawing.Font("Consolas", 10F);
            this.dataGridViewRec.Location = new System.Drawing.Point(515, 401);
            this.dataGridViewRec.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridViewRec.Name = "dataGridViewRec";
            this.dataGridViewRec.RowTemplate.Height = 24;
            this.dataGridViewRec.Size = new System.Drawing.Size(721, 327);
            this.dataGridViewRec.TabIndex = 100;
            this.dataGridViewRec.TabStop = false;
            // 
            // bnEdit
            // 
            this.bnEdit.Enabled = false;
            this.bnEdit.Font = new System.Drawing.Font("Arial", 10F);
            this.bnEdit.Location = new System.Drawing.Point(591, 67);
            this.bnEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnEdit.Name = "bnEdit";
            this.bnEdit.Size = new System.Drawing.Size(70, 30);
            this.bnEdit.TabIndex = 42;
            this.bnEdit.Text = "Edit";
            this.bnEdit.Click += new System.EventHandler(this.BnEdit_Click);
            // 
            // labelMCen
            // 
            this.labelMCen.AutoSize = true;
            this.labelMCen.Font = new System.Drawing.Font("Arial", 10F);
            this.labelMCen.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelMCen.Location = new System.Drawing.Point(276, 58);
            this.labelMCen.Name = "labelMCen";
            this.labelMCen.Size = new System.Drawing.Size(82, 16);
            this.labelMCen.TabIndex = 101;
            this.labelMCen.Text = "Mifare Card";
            // 
            // labelFPV
            // 
            this.labelFPV.AutoSize = true;
            this.labelFPV.Font = new System.Drawing.Font("Arial", 10F);
            this.labelFPV.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelFPV.Location = new System.Drawing.Point(279, 19);
            this.labelFPV.Name = "labelFPV";
            this.labelFPV.Size = new System.Drawing.Size(80, 16);
            this.labelFPV.TabIndex = 102;
            this.labelFPV.Text = "Finger Vein";
            // 
            // bnGetCardID
            // 
            this.bnGetCardID.Enabled = false;
            this.bnGetCardID.Font = new System.Drawing.Font("Arial", 10F);
            this.bnGetCardID.Location = new System.Drawing.Point(373, 68);
            this.bnGetCardID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnGetCardID.Name = "bnGetCardID";
            this.bnGetCardID.Size = new System.Drawing.Size(60, 30);
            this.bnGetCardID.TabIndex = 27;
            this.bnGetCardID.Text = "Enroll";
            this.bnGetCardID.Click += new System.EventHandler(this.bnGetCardID_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.Enabled = false;
            this.bnCancel.Font = new System.Drawing.Font("Arial", 10F);
            this.bnCancel.Location = new System.Drawing.Point(435, 68);
            this.bnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(60, 30);
            this.bnCancel.TabIndex = 28;
            this.bnCancel.Text = "Cancel";
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtResult.Location = new System.Drawing.Point(17, 599);
            this.txtResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(480, 155);
            this.txtResult.TabIndex = 0;
            // 
            // bnDelete
            // 
            this.bnDelete.Enabled = false;
            this.bnDelete.Font = new System.Drawing.Font("Arial", 10F);
            this.bnDelete.Location = new System.Drawing.Point(667, 67);
            this.bnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnDelete.Name = "bnDelete";
            this.bnDelete.Size = new System.Drawing.Size(70, 30);
            this.bnDelete.TabIndex = 43;
            this.bnDelete.Text = "Delete";
            this.bnDelete.Click += new System.EventHandler(this.BnDelete_Click);
            // 
            // gboxSetting
            // 
            this.gboxSetting.Controls.Add(this.bnConFV);
            this.gboxSetting.Controls.Add(this.bnConMC);
            this.gboxSetting.Controls.Add(this.bnDisconFV);
            this.gboxSetting.Controls.Add(this.labelIDMode);
            this.gboxSetting.Controls.Add(this.labelDLFV);
            this.gboxSetting.Controls.Add(this.bnReinFV);
            this.gboxSetting.Controls.Add(this.bnDisconMC);
            this.gboxSetting.Controls.Add(this.labelFV);
            this.gboxSetting.Controls.Add(this.labelMC);
            this.gboxSetting.Controls.Add(this.labelDLMC);
            this.gboxSetting.Controls.Add(this.listDevFV);
            this.gboxSetting.Controls.Add(this.listDevMC);
            this.gboxSetting.Controls.Add(this.listIDmode);
            this.gboxSetting.Location = new System.Drawing.Point(5, 370);
            this.gboxSetting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxSetting.Name = "gboxSetting";
            this.gboxSetting.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxSetting.Size = new System.Drawing.Size(500, 184);
            this.gboxSetting.TabIndex = 102;
            this.gboxSetting.TabStop = false;
            // 
            // gboxEnroll
            // 
            this.gboxEnroll.Controls.Add(this.labelName);
            this.gboxEnroll.Controls.Add(this.tboxMCID);
            this.gboxEnroll.Controls.Add(this.label1Kcard);
            this.gboxEnroll.Controls.Add(this.label4Kcard);
            this.gboxEnroll.Controls.Add(this.BnRead4K);
            this.gboxEnroll.Controls.Add(this.bnWrite4K);
            this.gboxEnroll.Controls.Add(this.labelVein);
            this.gboxEnroll.Controls.Add(this.bnReadVein);
            this.gboxEnroll.Controls.Add(this.bnWriteVein);
            this.gboxEnroll.Controls.Add(this.labelSID);
            this.gboxEnroll.Controls.Add(this.tboxSID);
            this.gboxEnroll.Controls.Add(this.tboxName);
            this.gboxEnroll.Controls.Add(this.tboxDep);
            this.gboxEnroll.Controls.Add(this.bnGetCardID);
            this.gboxEnroll.Controls.Add(this.bnCancel);
            this.gboxEnroll.Controls.Add(this.labelMCen);
            this.gboxEnroll.Controls.Add(this.labelFPV);
            this.gboxEnroll.Controls.Add(this.labelDep);
            this.gboxEnroll.Controls.Add(this.bnEnrollFV);
            this.gboxEnroll.Controls.Add(this.bnVerify);
            this.gboxEnroll.Controls.Add(this.bnUNconfirm);
            this.gboxEnroll.Location = new System.Drawing.Point(3, 377);
            this.gboxEnroll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxEnroll.Name = "gboxEnroll";
            this.gboxEnroll.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxEnroll.Size = new System.Drawing.Size(500, 187);
            this.gboxEnroll.TabIndex = 104;
            this.gboxEnroll.TabStop = false;
            // 
            // tboxMCID
            // 
            this.tboxMCID.Font = new System.Drawing.Font("Arial", 10F);
            this.tboxMCID.Location = new System.Drawing.Point(268, 79);
            this.tboxMCID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxMCID.Name = "tboxMCID";
            this.tboxMCID.ReadOnly = true;
            this.tboxMCID.Size = new System.Drawing.Size(90, 23);
            this.tboxMCID.TabIndex = 69;
            // 
            // label1Kcard
            // 
            this.label1Kcard.AutoSize = true;
            this.label1Kcard.Font = new System.Drawing.Font("Arial", 10F);
            this.label1Kcard.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1Kcard.Location = new System.Drawing.Point(290, 128);
            this.label1Kcard.Name = "label1Kcard";
            this.label1Kcard.Size = new System.Drawing.Size(71, 16);
            this.label1Kcard.TabIndex = 105;
            this.label1Kcard.Text = " (1K card)";
            // 
            // label4Kcard
            // 
            this.label4Kcard.AutoSize = true;
            this.label4Kcard.Font = new System.Drawing.Font("Arial", 10F);
            this.label4Kcard.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4Kcard.Location = new System.Drawing.Point(290, 158);
            this.label4Kcard.Name = "label4Kcard";
            this.label4Kcard.Size = new System.Drawing.Size(71, 16);
            this.label4Kcard.TabIndex = 106;
            this.label4Kcard.Text = " (4K card)";
            // 
            // BnRead4K
            // 
            this.BnRead4K.Enabled = false;
            this.BnRead4K.Font = new System.Drawing.Font("Arial", 10F);
            this.BnRead4K.Location = new System.Drawing.Point(437, 150);
            this.BnRead4K.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BnRead4K.Name = "BnRead4K";
            this.BnRead4K.Size = new System.Drawing.Size(60, 30);
            this.BnRead4K.TabIndex = 61;
            this.BnRead4K.Text = "Read";
            this.BnRead4K.UseVisualStyleBackColor = true;
            this.BnRead4K.Click += new System.EventHandler(this.BnRead4K_Click);
            // 
            // bnWrite4K
            // 
            this.bnWrite4K.Enabled = false;
            this.bnWrite4K.Font = new System.Drawing.Font("Arial", 10F);
            this.bnWrite4K.Location = new System.Drawing.Point(374, 152);
            this.bnWrite4K.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnWrite4K.Name = "bnWrite4K";
            this.bnWrite4K.Size = new System.Drawing.Size(60, 30);
            this.bnWrite4K.TabIndex = 31;
            this.bnWrite4K.Text = "Write";
            this.bnWrite4K.Click += new System.EventHandler(this.bnWrite4K_Click);
            // 
            // labelVein
            // 
            this.labelVein.AutoSize = true;
            this.labelVein.Font = new System.Drawing.Font("Arial", 10F);
            this.labelVein.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelVein.Location = new System.Drawing.Point(328, 110);
            this.labelVein.Name = "labelVein";
            this.labelVein.Size = new System.Drawing.Size(35, 16);
            this.labelVein.TabIndex = 107;
            this.labelVein.Text = "Vein";
            // 
            // bnReadVein
            // 
            this.bnReadVein.Enabled = false;
            this.bnReadVein.Font = new System.Drawing.Font("Arial", 10F);
            this.bnReadVein.Location = new System.Drawing.Point(437, 113);
            this.bnReadVein.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnReadVein.Name = "bnReadVein";
            this.bnReadVein.Size = new System.Drawing.Size(60, 30);
            this.bnReadVein.TabIndex = 30;
            this.bnReadVein.Text = "Read";
            this.bnReadVein.UseVisualStyleBackColor = true;
            this.bnReadVein.Click += new System.EventHandler(this.BnReadVein_Click);
            // 
            // bnWriteVein
            // 
            this.bnWriteVein.Enabled = false;
            this.bnWriteVein.Font = new System.Drawing.Font("Arial", 10F);
            this.bnWriteVein.Location = new System.Drawing.Point(374, 111);
            this.bnWriteVein.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnWriteVein.Name = "bnWriteVein";
            this.bnWriteVein.Size = new System.Drawing.Size(60, 30);
            this.bnWriteVein.TabIndex = 29;
            this.bnWriteVein.Text = "Write";
            this.bnWriteVein.UseVisualStyleBackColor = true;
            this.bnWriteVein.Click += new System.EventHandler(this.BnWriteVein_Click);
            // 
            // gboxRecord
            // 
            this.gboxRecord.Location = new System.Drawing.Point(6, 370);
            this.gboxRecord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxRecord.Name = "gboxRecord";
            this.gboxRecord.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxRecord.Size = new System.Drawing.Size(500, 202);
            this.gboxRecord.TabIndex = 103;
            this.gboxRecord.TabStop = false;
            // 
            // labelVeinPrint
            // 
            this.labelVeinPrint.AutoSize = true;
            this.labelVeinPrint.Font = new System.Drawing.Font("Arial", 12F);
            this.labelVeinPrint.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelVeinPrint.Location = new System.Drawing.Point(262, 147);
            this.labelVeinPrint.Name = "labelVeinPrint";
            this.labelVeinPrint.Size = new System.Drawing.Size(100, 20);
            this.labelVeinPrint.TabIndex = 106;
            this.labelVeinPrint.Text = "Vein + Print";
            // 
            // bnMute
            // 
            this.bnMute.Enabled = false;
            this.bnMute.Font = new System.Drawing.Font("Arial", 10F);
            this.bnMute.Location = new System.Drawing.Point(1154, 14);
            this.bnMute.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnMute.Name = "bnMute";
            this.bnMute.Size = new System.Drawing.Size(60, 30);
            this.bnMute.TabIndex = 1;
            this.bnMute.Text = "Mute";
            this.bnMute.UseVisualStyleBackColor = true;
            this.bnMute.Click += new System.EventHandler(this.BnMute_Click);
            // 
            // imageBoxFrameGrabber
            // 
            this.imageBoxFrameGrabber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxFrameGrabber.Location = new System.Drawing.Point(515, 119);
            this.imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            this.imageBoxFrameGrabber.Size = new System.Drawing.Size(260, 214);
            this.imageBoxFrameGrabber.TabIndex = 105;
            this.imageBoxFrameGrabber.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(1017, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 222);
            this.groupBox2.TabIndex = 106;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(9, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "Persons present in the scene:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(9, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 16;
            this.label4.Text = "Nobody";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(163, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Number of faces detected: ";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(1248, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 49);
            this.button1.TabIndex = 2;
            this.button1.Text = "Detect and recognize";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.imageBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(798, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 223);
            this.groupBox1.TabIndex = 107;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Training: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 157);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(107, 22);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "harry";
            // 
            // imageBox1
            // 
            this.imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox1.Location = new System.Drawing.Point(11, 21);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(163, 124);
            this.imageBox1.TabIndex = 5;
            this.imageBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Location = new System.Drawing.Point(87, 186);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 29);
            this.button2.TabIndex = 3;
            this.button2.Text = "2. Add face";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1370, 881);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.imageBoxFrameGrabber);
            this.Controls.Add(this.gboxEnroll);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewEN);
            this.Controls.Add(this.bnMute);
            this.Controls.Add(this.bnDelete);
            this.Controls.Add(this.bnEdit);
            this.Controls.Add(this.bnAddNew);
            this.Controls.Add(this.tboxSearch);
            this.Controls.Add(this.bnSearch);
            this.Controls.Add(this.bnEnrollMode);
            this.Controls.Add(this.bnSettingMode);
            this.Controls.Add(this.bnRecordMode);
            this.Controls.Add(this.picFV);
            this.Controls.Add(this.picFP);
            this.Controls.Add(this.bnReturn);
            this.Controls.Add(this.dataGridViewRec);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.gboxRecord);
            this.Controls.Add(this.gboxSetting);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Finger Vein and Mifare Card Time Attendence System";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRec)).EndInit();
            this.gboxSetting.ResumeLayout(false);
            this.gboxSetting.PerformLayout();
            this.gboxEnroll.ResumeLayout(false);
            this.gboxEnroll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //Component 
        private System.Windows.Forms.ComboBox listIDmode;
        private System.Windows.Forms.TextBox tboxName;
        private System.Windows.Forms.Label labelDLFV;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelIDMode;
        private System.Windows.Forms.ComboBox listDevFV;
        private System.Windows.Forms.Button bnConFV;
        private System.Windows.Forms.Button bnDisconFV;
        private System.Windows.Forms.Button bnEnrollFV;
        private System.Windows.Forms.Button bnVerify;
        private System.Windows.Forms.PictureBox picFP;
        private System.Windows.Forms.PictureBox picFV;
        private System.Windows.Forms.Button bnSettingMode;
        private System.Windows.Forms.Button bnRecordMode;
        private System.Windows.Forms.Button bnUNconfirm;
        private System.Windows.Forms.Button bnEnrollMode;
        private System.Windows.Forms.DataGridView dataGridViewEN;
        private System.Windows.Forms.Button bnReturn;
        private System.Windows.Forms.Label labelSID;
        private System.Windows.Forms.TextBox tboxSID;
        private System.Windows.Forms.Label labelDep;
        private System.Windows.Forms.TextBox tboxDep;
        private System.Windows.Forms.Button bnReinFV;
        private System.Windows.Forms.Button bnDisconMC;
        private System.Windows.Forms.Label labelFV;
        private System.Windows.Forms.Label labelMC;
        private System.Windows.Forms.Label labelDLMC;
        private System.Windows.Forms.ComboBox listDevMC;
        private System.Windows.Forms.Button bnConMC;
        private System.Windows.Forms.Button bnSearch;
        private System.Windows.Forms.TextBox tboxSearch;
        private System.Windows.Forms.Button bnAddNew;
        private System.Windows.Forms.DataGridView dataGridViewRec;
        private System.Windows.Forms.Button bnEdit;
        private System.Windows.Forms.Label labelMCen;
        private System.Windows.Forms.Label labelFPV;
        private System.Windows.Forms.Button bnGetCardID;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button bnDelete;
        private System.Windows.Forms.GroupBox gboxSetting;
        private System.Windows.Forms.GroupBox gboxEnroll;
        private System.Windows.Forms.GroupBox gboxRecord;
        private System.Windows.Forms.Button bnWrite4K;
        private System.Windows.Forms.Button BnRead4K;
        private System.Windows.Forms.Button bnMute;
        private System.Windows.Forms.Button bnReadVein;
        private System.Windows.Forms.Button bnWriteVein;
        private System.Windows.Forms.Label label1Kcard;
        private System.Windows.Forms.Label label4Kcard;
        private System.Windows.Forms.Label labelVeinPrint;
        private System.Windows.Forms.Label labelVein;
        private System.Windows.Forms.TextBox tboxMCID;
        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber;
        private GroupBox groupBox2;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button button1;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox textBox1;
        private Emgu.CV.UI.ImageBox imageBox1;
        private Button button2;
    }
}

