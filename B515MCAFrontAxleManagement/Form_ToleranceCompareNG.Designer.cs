namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    partial class Form_ToleranceCompareNG
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {

            if (m_bContinueMark_UnderCompareNG == false &&
             m_bInputFindCodeSaveDB_NoMark == false)  // 如果点关闭的话则默认是继续打码
            {
                m_bContinueMark_UnderCompareNG = true;
                m_bInputFindCodeSaveDB_NoMark = false;
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ToleranceCompareNG));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_ContinumMark = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_ManualInputNumber = new System.Windows.Forms.Button();
            this.tbx_ManualInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_MissDetectInfo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(139, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btn_ContinumMark
            // 
            this.btn_ContinumMark.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ContinumMark.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ContinumMark.Location = new System.Drawing.Point(42, 251);
            this.btn_ContinumMark.Name = "btn_ContinumMark";
            this.btn_ContinumMark.Size = new System.Drawing.Size(425, 51);
            this.btn_ContinumMark.TabIndex = 6;
            this.btn_ContinumMark.Text = "存在漏检或偏差违例情况，但继续打码";
            this.btn_ContinumMark.Click += new System.EventHandler(this.btn_ContinumMark_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "偏差超过公差带，请确认是否继续打码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(134, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "或手动输入追溯序号";
            // 
            // btn_ManualInputNumber
            // 
            this.btn_ManualInputNumber.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ManualInputNumber.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ManualInputNumber.Location = new System.Drawing.Point(42, 308);
            this.btn_ManualInputNumber.Name = "btn_ManualInputNumber";
            this.btn_ManualInputNumber.Size = new System.Drawing.Size(425, 51);
            this.btn_ManualInputNumber.TabIndex = 9;
            this.btn_ManualInputNumber.Text = "不打码，手动输入追溯序号存数据库";
            this.btn_ManualInputNumber.Click += new System.EventHandler(this.btn_ManualInputNumber_Click);
            // 
            // tbx_ManualInput
            // 
            this.tbx_ManualInput.Enabled = false;
            this.tbx_ManualInput.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_ManualInput.Location = new System.Drawing.Point(226, 365);
            this.tbx_ManualInput.Name = "tbx_ManualInput";
            this.tbx_ManualInput.Size = new System.Drawing.Size(160, 26);
            this.tbx_ManualInput.TabIndex = 10;
            this.tbx_ManualInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbx_ManualInput_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(101, 371);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "手动输入序号:";
            // 
            // tbx_MissDetectInfo
            // 
            this.tbx_MissDetectInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_MissDetectInfo.Location = new System.Drawing.Point(3, 22);
            this.tbx_MissDetectInfo.Multiline = true;
            this.tbx_MissDetectInfo.Name = "tbx_MissDetectInfo";
            this.tbx_MissDetectInfo.Size = new System.Drawing.Size(425, 57);
            this.tbx_MissDetectInfo.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbx_MissDetectInfo);
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(39, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 82);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "漏检信息";
            // 
            // Form_ToleranceCompareNG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 404);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbx_ManualInput);
            this.Controls.Add(this.btn_ManualInputNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ContinumMark);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(469, 344);
            this.Name = "Form_ToleranceCompareNG";
            this.Text = "读ZESSI文件阶段：公差比较NG";
            this.Load += new System.EventHandler(this.Form_ToleranceCompareNG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_ContinumMark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_ManualInputNumber;
        private System.Windows.Forms.TextBox tbx_ManualInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_MissDetectInfo;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}