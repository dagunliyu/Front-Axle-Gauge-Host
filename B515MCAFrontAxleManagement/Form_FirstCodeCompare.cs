using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> this class shows all records of the given datasets </summary>
	public class Form_FirstCodeCompare: System.Windows.Forms.Form
	{
		#region fields
		#region UI fields
		private System.Windows.Forms.Label label_OriginalCode;
        private System.Windows.Forms.Label label_PLCsendCode1st;
		private System.Windows.Forms.Button btn_ContinueMark;
		private new System.Windows.Forms.Button btn_CancelMark;
		private System.Windows.Forms.TextBox tbx_OriginalCode;
        private System.Windows.Forms.TextBox tbx_PLCSend1stCode;
		private System.ComponentModel.Container components = null;
		#endregion

		#region to control the inputs of the textfields
		private bool m_changeBack = false;
        private Button btn_SaveCodeAsPCOriginalCode;
        private Button btn_SaveCodeAsPLCSecondSendCode;
        private PictureBox pictureBox1;
		private string m_strDummy = "";
		#endregion
		#endregion

		#region construction, destruction
        public Form_FirstCodeCompare()
		{
			InitializeComponent();
            m_bContinueMark_Flag = false;
            m_bContinueMarkWithPLCCode_Flag = false;
            m_bContinueMarkWithPCCode_Flag = false;
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode f黵 die Designerunterst黷zung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor ge鋘dert werden.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_FirstCodeCompare));
            this.label_OriginalCode = new System.Windows.Forms.Label();
            this.label_PLCsendCode1st = new System.Windows.Forms.Label();
            this.tbx_OriginalCode = new System.Windows.Forms.TextBox();
            this.tbx_PLCSend1stCode = new System.Windows.Forms.TextBox();
            this.btn_ContinueMark = new System.Windows.Forms.Button();
            this.btn_CancelMark = new System.Windows.Forms.Button();
            this.btn_SaveCodeAsPCOriginalCode = new System.Windows.Forms.Button();
            this.btn_SaveCodeAsPLCSecondSendCode = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label_OriginalCode
            // 
            this.label_OriginalCode.Location = new System.Drawing.Point(42, 56);
            this.label_OriginalCode.Name = "label_OriginalCode";
            this.label_OriginalCode.Size = new System.Drawing.Size(62, 21);
            this.label_OriginalCode.TabIndex = 0;
            this.label_OriginalCode.Text = "原始码值";
            // 
            // label_PLCsendCode1st
            // 
            this.label_PLCsendCode1st.Location = new System.Drawing.Point(32, 93);
            this.label_PLCsendCode1st.Name = "label_PLCsendCode1st";
            this.label_PLCsendCode1st.Size = new System.Drawing.Size(88, 21);
            this.label_PLCsendCode1st.TabIndex = 0;
            this.label_PLCsendCode1st.Text = "PLC发送的码值";
            // 
            // tbx_OriginalCode
            // 
            this.tbx_OriginalCode.Location = new System.Drawing.Point(130, 53);
            this.tbx_OriginalCode.Name = "tbx_OriginalCode";
            this.tbx_OriginalCode.Size = new System.Drawing.Size(180, 21);
            this.tbx_OriginalCode.TabIndex = 3;
            this.tbx_OriginalCode.Text = "100";
            // 
            // tbx_PLCSend1stCode
            // 
            this.tbx_PLCSend1stCode.Location = new System.Drawing.Point(130, 87);
            this.tbx_PLCSend1stCode.Name = "tbx_PLCSend1stCode";
            this.tbx_PLCSend1stCode.Size = new System.Drawing.Size(180, 21);
            this.tbx_PLCSend1stCode.TabIndex = 4;
            this.tbx_PLCSend1stCode.Text = "100";
            // 
            // btn_ContinueMark
            // 
            this.btn_ContinueMark.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ContinueMark.Enabled = false;
            this.btn_ContinueMark.Location = new System.Drawing.Point(220, 125);
            this.btn_ContinueMark.Name = "btn_ContinueMark";
            this.btn_ContinueMark.Size = new System.Drawing.Size(90, 25);
            this.btn_ContinueMark.TabIndex = 1;
            this.btn_ContinueMark.Text = "继续打码";
            this.btn_ContinueMark.Click += new System.EventHandler(this.btn_ContinueMark_Click);
            // 
            // btn_CancelMark
            // 
            this.btn_CancelMark.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_CancelMark.Location = new System.Drawing.Point(220, 156);
            this.btn_CancelMark.Name = "btn_CancelMark";
            this.btn_CancelMark.Size = new System.Drawing.Size(90, 25);
            this.btn_CancelMark.TabIndex = 2;
            this.btn_CancelMark.Text = "取消当前打码";
            this.btn_CancelMark.Click += new System.EventHandler(this.btn_CancelMark_Click);
            // 
            // btn_SaveCodeAsPCOriginalCode
            // 
            this.btn_SaveCodeAsPCOriginalCode.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_SaveCodeAsPCOriginalCode.Location = new System.Drawing.Point(36, 125);
            this.btn_SaveCodeAsPCOriginalCode.Name = "btn_SaveCodeAsPCOriginalCode";
            this.btn_SaveCodeAsPCOriginalCode.Size = new System.Drawing.Size(99, 25);
            this.btn_SaveCodeAsPCOriginalCode.TabIndex = 8;
            this.btn_SaveCodeAsPCOriginalCode.Text = "以PC码值为准";
            this.btn_SaveCodeAsPCOriginalCode.Click += new System.EventHandler(this.btn_SaveCodeAsPCOriginalCode_Click);
            // 
            // btn_SaveCodeAsPLCSecondSendCode
            // 
            this.btn_SaveCodeAsPLCSecondSendCode.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_SaveCodeAsPLCSecondSendCode.Location = new System.Drawing.Point(36, 156);
            this.btn_SaveCodeAsPLCSecondSendCode.Name = "btn_SaveCodeAsPLCSecondSendCode";
            this.btn_SaveCodeAsPLCSecondSendCode.Size = new System.Drawing.Size(99, 25);
            this.btn_SaveCodeAsPLCSecondSendCode.TabIndex = 7;
            this.btn_SaveCodeAsPLCSecondSendCode.Text = "以PLC码值为准";
            this.btn_SaveCodeAsPLCSecondSendCode.Click += new System.EventHandler(this.btn_SaveCodeAsPLCSecondSendCode_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(60, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Form_FirstCodeCompare
            // 
            this.AcceptButton = this.btn_ContinueMark;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(341, 203);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_SaveCodeAsPCOriginalCode);
            this.Controls.Add(this.btn_SaveCodeAsPLCSecondSendCode);
            this.Controls.Add(this.btn_ContinueMark);
            this.Controls.Add(this.tbx_OriginalCode);
            this.Controls.Add(this.label_OriginalCode);
            this.Controls.Add(this.label_PLCsendCode1st);
            this.Controls.Add(this.tbx_PLCSend1stCode);
            this.Controls.Add(this.btn_CancelMark);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(347, 235);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(347, 235);
            this.Name = "Form_FirstCodeCompare";
            this.Text = "打标前第1次码值比较不同，是否继续打码？";
            this.Load += new System.EventHandler(this.Form_FirstCodeCompare_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

		#region properties
		public int OriginalCode
		{
			get { return int.Parse(this.tbx_OriginalCode.Text); }
		}
		public int PLCSend1stCode
		{
			get { return int.Parse(this.tbx_PLCSend1stCode.Text); }
		}
		#endregion

        public static bool m_bContinueMark_Flag = false;
        public static bool m_bContinueMarkWithPLCCode_Flag = false;
        public static bool m_bContinueMarkWithPCCode_Flag = false;
        private void btn_ContinueMark_Click(object sender, EventArgs e)
        {
            m_bContinueMark_Flag = true;

            Dispose();
        }

        private void btn_CancelMark_Click(object sender, EventArgs e)
        {
            Form_AdvancedFunc_Login F_Ad_Login = new Form_AdvancedFunc_Login();
            F_Ad_Login.Show();
            while (!F_Ad_Login.IsDisposed)
            {
                Application.DoEvents();
                this.Enabled = false;  // 主界面使能禁止 不可使用

            }
            this.Enabled = true;
            //
            
            m_bContinueMark_Flag = false;

            Dispose();
        }

        private void Form_FirstCodeCompare_Load(object sender, EventArgs e)
        {
            tbx_OriginalCode.Text = PersistentData.CodeSaveToDB;
            tbx_PLCSend1stCode.Text = PersistentData.CodePLCSend1st;
            m_bContinueMark_Flag = false;
            
            m_bContinueMarkWithPLCCode_Flag = false;
            m_bContinueMarkWithPCCode_Flag = false;
        }

        private void btn_SaveCodeAsPCOriginalCode_Click(object sender, EventArgs e)
        {
            Form_AdvancedFunc_Login F_Ad_Login = new Form_AdvancedFunc_Login();
            F_Ad_Login.Show();
            while (!F_Ad_Login.IsDisposed)
            {
                Application.DoEvents();
                this.Enabled = false;  // 主界面使能禁止 不可使用

            }
            this.Enabled = true;
            if (Form_AdvancedFunc_Login.m_bAdminLogin == false)
            { return; }
            //
            
            PersistentData.CodeSaveToDB = tbx_OriginalCode.Text;
            m_bContinueMark_Flag = true;
            m_bContinueMarkWithPLCCode_Flag = false;
            m_bContinueMarkWithPCCode_Flag = true;

            Dispose();
        }

        private void btn_SaveCodeAsPLCSecondSendCode_Click(object sender, EventArgs e)
        {
            Form_AdvancedFunc_Login F_Ad_Login = new Form_AdvancedFunc_Login();
            F_Ad_Login.Show();
            while (!F_Ad_Login.IsDisposed)
            {
                Application.DoEvents();
                this.Enabled = false;  // 主界面使能禁止 不可使用

            }
            this.Enabled = true;
            //
            if (Form_AdvancedFunc_Login.m_bAdminLogin == false)
            { return; }
            
            PersistentData.CodeSaveToDB = tbx_PLCSend1stCode.Text;
            m_bContinueMark_Flag = true;
            m_bContinueMarkWithPLCCode_Flag = true;
            m_bContinueMarkWithPCCode_Flag = false;

            Dispose();
        }
		/// <summary>The .NET-Textboxes have no property that only digits are valid characters.
		/// So we have to take care for it manually.</summary>
		#region Textbox handlers
        //private void UpdRateBRCVTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    // Check if only digits are entered.

        //    // get the last character of the keyeventargument e
        //    char a = e.KeyData.ToString().ToCharArray(e.KeyData.ToString().Length-1,1)[0];

        //    m_changeBack = false;
        //    if(e.KeyData.ToString().Length==1 && !char.IsDigit(a))
        //    {
        //        m_strDummy = this.tbx_OriginalCode.Text;
        //        m_changeBack = true;
        //    }
        //}

        //private void UpdRateBRCVTextBox_TextChanged(object sender, System.EventArgs e)
        //{
        //    if (m_changeBack)
        //    {
        //        m_changeBack = false;
        //        this.tbx_OriginalCode.Text = m_strDummy;
        //    }
        //}

        //private void UpdRateFVisuTextBox_TextChanged(object sender, System.EventArgs e)
        //{
        //    if (m_changeBack)
        //    {
        //        m_changeBack = false;
        //        this.tbx_PLCSend1stCode.Text = m_strDummy;
        //    }
        //}

        //private void UpdRateFVisuTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    // Check if only digits are entered.

        //    // get the last character of the keyeventargument e
        //    char a = e.KeyData.ToString().ToCharArray(e.KeyData.ToString().Length-1,1)[0];

        //    m_changeBack = false;
        //    if(e.KeyData.ToString().Length==1 && !char.IsDigit(a))
        //    {
        //        m_strDummy = this.tbx_PLCSend1stCode.Text;
        //        m_changeBack = true;
        //    }
        //}

        //private void UpdRateSVisuTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    // Check if only digits are entered.

        //    // get the last character of the keyeventargument e
        //    char a = e.KeyData.ToString().ToCharArray(e.KeyData.ToString().Length-1,1)[0];

        //    m_changeBack = false;
        //    if(e.KeyData.ToString().Length==1 && !char.IsDigit(a))
        //    {
        //        m_strDummy = this.UpdRateSVisuTextBox.Text;
        //        m_changeBack = true;
        //    }
        //}

        //private void UpdRateSVisuTextBox_TextChanged(object sender, System.EventArgs e)
        //{
        //    if (m_changeBack)
        //    {
        //        m_changeBack = false;
        //        this.UpdRateSVisuTextBox.Text = m_strDummy;
        //    }
        //}
		#endregion


    }
}
