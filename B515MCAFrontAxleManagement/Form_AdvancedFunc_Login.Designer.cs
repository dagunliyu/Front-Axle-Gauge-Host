namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    partial class Form_AdvancedFunc_Login
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdvancedFunc_Login));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Return = new System.Windows.Forms.Button();
            this.btn_login = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_Name = new System.Windows.Forms.TextBox();
            this.tbx_Code = new System.Windows.Forms.TextBox();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.errorInfo = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(58, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Return
            // 
            this.btn_Return.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Return.Location = new System.Drawing.Point(203, 162);
            this.btn_Return.Name = "btn_Return";
            this.btn_Return.Size = new System.Drawing.Size(86, 25);
            this.btn_Return.TabIndex = 4;
            this.btn_Return.Text = "返回";
            this.btn_Return.Click += new System.EventHandler(this.btn_Return_Click);
            // 
            // btn_login
            // 
            this.btn_login.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_login.Location = new System.Drawing.Point(19, 162);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(86, 25);
            this.btn_login.TabIndex = 6;
            this.btn_login.Text = "登录";
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "用户名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "密码:";
            // 
            // tbx_Name
            // 
            this.tbx_Name.Location = new System.Drawing.Point(101, 73);
            this.tbx_Name.Name = "tbx_Name";
            this.tbx_Name.Size = new System.Drawing.Size(100, 21);
            this.tbx_Name.TabIndex = 9;
            this.tbx_Name.Text = "admin";
            this.tbx_Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbx_Name_KeyDown);
            // 
            // tbx_Code
            // 
            this.tbx_Code.Location = new System.Drawing.Point(101, 109);
            this.tbx_Code.Name = "tbx_Code";
            this.tbx_Code.Size = new System.Drawing.Size(100, 21);
            this.tbx_Code.TabIndex = 10;
            this.tbx_Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbx_Code_KeyDown);
            // 
            // btn_Reset
            // 
            this.btn_Reset.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Reset.Location = new System.Drawing.Point(111, 162);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(86, 25);
            this.btn_Reset.TabIndex = 11;
            this.btn_Reset.Text = "重置";
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // errorInfo
            // 
            this.errorInfo.ContainerControl = this;
            // 
            // Form_AdvancedFunc_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 201);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.tbx_Code);
            this.Controls.Add(this.tbx_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.btn_Return);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AdvancedFunc_Login";
            this.Text = "高级功能登录";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_Return;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_Name;
        private System.Windows.Forms.TextBox tbx_Code;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.ErrorProvider errorInfo;
    }
}