namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    partial class Form_zessiMissDetect
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

            m_bContinueMark_UnderCompareNG = true;
            m_bInputFindCodeSaveDB_NoMark = false;  // 如果点关闭的话则默认是继续打码

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_zessiMissDetect));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_ContinumMark = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_ManualInputNumber = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.btn_ContinumMark.Location = new System.Drawing.Point(87, 179);
            this.btn_ContinumMark.Name = "btn_ContinumMark";
            this.btn_ContinumMark.Size = new System.Drawing.Size(309, 51);
            this.btn_ContinumMark.TabIndex = 6;
            this.btn_ContinumMark.Text = "继续执行打码操作";
            this.btn_ContinumMark.Click += new System.EventHandler(this.btn_ContinumMark_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(171, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "存在漏检点";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(96, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(288, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "请判断是否重新检查该件";
            // 
            // btn_ManualInputNumber
            // 
            this.btn_ManualInputNumber.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ManualInputNumber.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ManualInputNumber.Location = new System.Drawing.Point(87, 240);
            this.btn_ManualInputNumber.Name = "btn_ManualInputNumber";
            this.btn_ManualInputNumber.Size = new System.Drawing.Size(309, 51);
            this.btn_ManualInputNumber.TabIndex = 9;
            this.btn_ManualInputNumber.Text = "不打码，重新检查该工件";
            this.btn_ManualInputNumber.Click += new System.EventHandler(this.btn_ManualInputNumber_Click);
            // 
            // Form_zessiMissDetect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 310);
            this.Controls.Add(this.btn_ManualInputNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ContinumMark);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(469, 344);
            this.MinimumSize = new System.Drawing.Size(469, 344);
            this.Name = "Form_zessiMissDetect";
            this.Text = "读ZESSI文件阶段：存在漏检点";
            this.Load += new System.EventHandler(this.Form_ToleranceCompareNG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_ContinumMark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_ManualInputNumber;
    }
}