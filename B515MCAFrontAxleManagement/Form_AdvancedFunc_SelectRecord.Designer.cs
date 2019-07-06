namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    partial class Form_AdvancedFunc_SelectRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdvancedFunc_SelectRecord));
            this.btn_TakeCurrentSelectedRecord = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Return = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_TakeCurrentSelectedRecord
            // 
            this.btn_TakeCurrentSelectedRecord.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_TakeCurrentSelectedRecord.Location = new System.Drawing.Point(42, 156);
            this.btn_TakeCurrentSelectedRecord.Name = "btn_TakeCurrentSelectedRecord";
            this.btn_TakeCurrentSelectedRecord.Size = new System.Drawing.Size(103, 33);
            this.btn_TakeCurrentSelectedRecord.TabIndex = 2;
            this.btn_TakeCurrentSelectedRecord.Text = "确认";
            this.btn_TakeCurrentSelectedRecord.Click += new System.EventHandler(this.btn_TakeCurrentSelectedRecord_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(62, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Return
            // 
            this.btn_Return.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Return.Location = new System.Drawing.Point(164, 156);
            this.btn_Return.Name = "btn_Return";
            this.btn_Return.Size = new System.Drawing.Size(103, 33);
            this.btn_Return.TabIndex = 5;
            this.btn_Return.Text = "返回";
            this.btn_Return.Click += new System.EventHandler(this.btn_ReturnMainForm_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(29, 48);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(255, 100);
            this.listBox1.TabIndex = 9;
            // 
            // Form_AdvancedFunc_SelectRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 201);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_Return);
            this.Controls.Add(this.btn_TakeCurrentSelectedRecord);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AdvancedFunc_SelectRecord";
            this.Text = "标准公差保存记录";
            this.Load += new System.EventHandler(this.Form_AdvancedFunc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_TakeCurrentSelectedRecord;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_Return;
        private System.Windows.Forms.ListBox listBox1;
    }
}