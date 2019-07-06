using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> this class shows all records of the given datasets </summary>
	public class Form_SecondCodeCompare: System.Windows.Forms.Form
	{
		#region fields
		#region UI fields
		private System.Windows.Forms.Label label_OriginalCode;
        private System.Windows.Forms.Label label_PLCsendCode2nd;
		private System.Windows.Forms.Button btn_SaveCodeAsPLCSecondSendCode;
		private System.Windows.Forms.Button btn_CancelMark;
		private System.Windows.Forms.TextBox tbx_OriginalCode;
        private System.Windows.Forms.TextBox tbx_PLCSend2ndCode;
		private System.ComponentModel.Container components = null;
		#endregion

		#region to control the inputs of the textfields
		private bool m_changeBack = false;
        private Button btn_ChangeCodeToOriginal;
        private Button btn_SaveCodeAsPCOriginalCode;
        private PictureBox pictureBox1;
		private string m_strDummy = "";
		#endregion
		#endregion

		#region construction, destruction
        public Form_SecondCodeCompare()
		{
			InitializeComponent();
            
            m_bContinueMark_btnFlag = false;
            tbx_OriginalCode.Text = PersistentData.CodeSaveToDB; // 传过来的就是入库打标码
            tbx_PLCSend2ndCode.Text = PersistentData.CodePLCSend2nd;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SecondCodeCompare));
            this.label_OriginalCode = new System.Windows.Forms.Label();
            this.label_PLCsendCode2nd = new System.Windows.Forms.Label();
            this.tbx_OriginalCode = new System.Windows.Forms.TextBox();
            this.tbx_PLCSend2ndCode = new System.Windows.Forms.TextBox();
            this.btn_SaveCodeAsPLCSecondSendCode = new System.Windows.Forms.Button();
            this.btn_CancelMark = new System.Windows.Forms.Button();
            this.btn_ChangeCodeToOriginal = new System.Windows.Forms.Button();
            this.btn_SaveCodeAsPCOriginalCode = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label_OriginalCode
            // 
            this.label_OriginalCode.AutoSize = true;
            this.label_OriginalCode.Location = new System.Drawing.Point(25, 46);
            this.label_OriginalCode.Name = "label_OriginalCode";
            this.label_OriginalCode.Size = new System.Drawing.Size(113, 12);
            this.label_OriginalCode.TabIndex = 0;
            this.label_OriginalCode.Text = "PC生成的入库打标码";
            // 
            // label_PLCsendCode2nd
            // 
            this.label_PLCsendCode2nd.AutoSize = true;
            this.label_PLCsendCode2nd.Location = new System.Drawing.Point(25, 80);
            this.label_PLCsendCode2nd.Name = "label_PLCsendCode2nd";
            this.label_PLCsendCode2nd.Size = new System.Drawing.Size(95, 12);
            this.label_PLCsendCode2nd.TabIndex = 0;
            this.label_PLCsendCode2nd.Text = "PLC回传打标内容";
            // 
            // tbx_OriginalCode
            // 
            this.tbx_OriginalCode.Location = new System.Drawing.Point(159, 43);
            this.tbx_OriginalCode.Name = "tbx_OriginalCode";
            this.tbx_OriginalCode.Size = new System.Drawing.Size(223, 21);
            this.tbx_OriginalCode.TabIndex = 3;
            this.tbx_OriginalCode.Text = "100";
            // 
            // tbx_PLCSend2ndCode
            // 
            this.tbx_PLCSend2ndCode.Enabled = false;
            this.tbx_PLCSend2ndCode.Location = new System.Drawing.Point(159, 77);
            this.tbx_PLCSend2ndCode.Name = "tbx_PLCSend2ndCode";
            this.tbx_PLCSend2ndCode.Size = new System.Drawing.Size(223, 21);
            this.tbx_PLCSend2ndCode.TabIndex = 4;
            this.tbx_PLCSend2ndCode.Text = "100";
            // 
            // btn_SaveCodeAsPLCSecondSendCode
            // 
            this.btn_SaveCodeAsPLCSecondSendCode.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_SaveCodeAsPLCSecondSendCode.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveCodeAsPLCSecondSendCode.Location = new System.Drawing.Point(23, 158);
            this.btn_SaveCodeAsPLCSecondSendCode.Name = "btn_SaveCodeAsPLCSecondSendCode";
            this.btn_SaveCodeAsPLCSecondSendCode.Size = new System.Drawing.Size(115, 25);
            this.btn_SaveCodeAsPLCSecondSendCode.TabIndex = 1;
            this.btn_SaveCodeAsPLCSecondSendCode.Text = "以PLC入库";
            this.btn_SaveCodeAsPLCSecondSendCode.Click += new System.EventHandler(this.btn_ContinueMark_Click);
            // 
            // btn_CancelMark
            // 
            this.btn_CancelMark.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_CancelMark.Enabled = false;
            this.btn_CancelMark.Location = new System.Drawing.Point(276, 159);
            this.btn_CancelMark.Name = "btn_CancelMark";
            this.btn_CancelMark.Size = new System.Drawing.Size(90, 25);
            this.btn_CancelMark.TabIndex = 2;
            this.btn_CancelMark.Text = "放弃入库";
            this.btn_CancelMark.Visible = false;
            // 
            // btn_ChangeCodeToOriginal
            // 
            this.btn_ChangeCodeToOriginal.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ChangeCodeToOriginal.Enabled = false;
            this.btn_ChangeCodeToOriginal.Location = new System.Drawing.Point(259, 117);
            this.btn_ChangeCodeToOriginal.Name = "btn_ChangeCodeToOriginal";
            this.btn_ChangeCodeToOriginal.Size = new System.Drawing.Size(122, 25);
            this.btn_ChangeCodeToOriginal.TabIndex = 5;
            this.btn_ChangeCodeToOriginal.Text = "修改码值至原始值";
            this.btn_ChangeCodeToOriginal.Click += new System.EventHandler(this.btn_ChangeCodeToOriginal_Click);
            // 
            // btn_SaveCodeAsPCOriginalCode
            // 
            this.btn_SaveCodeAsPCOriginalCode.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_SaveCodeAsPCOriginalCode.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveCodeAsPCOriginalCode.Location = new System.Drawing.Point(23, 116);
            this.btn_SaveCodeAsPCOriginalCode.Name = "btn_SaveCodeAsPCOriginalCode";
            this.btn_SaveCodeAsPCOriginalCode.Size = new System.Drawing.Size(115, 25);
            this.btn_SaveCodeAsPCOriginalCode.TabIndex = 6;
            this.btn_SaveCodeAsPCOriginalCode.Text = "以PC入库";
            this.btn_SaveCodeAsPCOriginalCode.Click += new System.EventHandler(this.btn_SaveCodeAsPCOriginalCode_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(118, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Form_SecondCodeCompare
            // 
            this.AcceptButton = this.btn_SaveCodeAsPLCSecondSendCode;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(408, 198);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_SaveCodeAsPCOriginalCode);
            this.Controls.Add(this.btn_ChangeCodeToOriginal);
            this.Controls.Add(this.btn_SaveCodeAsPLCSecondSendCode);
            this.Controls.Add(this.tbx_OriginalCode);
            this.Controls.Add(this.label_OriginalCode);
            this.Controls.Add(this.label_PLCsendCode2nd);
            this.Controls.Add(this.tbx_PLCSend2ndCode);
            this.Controls.Add(this.btn_CancelMark);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(414, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(414, 230);
            this.Name = "Form_SecondCodeCompare";
            this.Text = "第2次比较:打标内容不一致操作";
            this.Load += new System.EventHandler(this.Form_SecondCodeCompare_Load);
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
			get { return int.Parse(this.tbx_PLCSend2ndCode.Text); }
		}
		#endregion

        public static bool m_bContinueMark_btnFlag = false;
        private void btn_ContinueMark_Click(object sender, EventArgs e)
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
            
            m_bContinueMark_btnFlag = true;

            PersistentData.CodeSaveToDB = tbx_PLCSend2ndCode.Text;

            Dispose();

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
            //
            if (Form_AdvancedFunc_Login.m_bAdminLogin == false)
            { return; }
            
            
            m_bContinueMark_btnFlag = true;

            PersistentData.CodeSaveToDB = tbx_OriginalCode.Text;

            Dispose();
        }

        private void btn_ChangeCodeToOriginal_Click(object sender, EventArgs e)
        {
            tbx_PLCSend2ndCode.Text = tbx_OriginalCode.Text;
            PersistentData.CodePLCSend2nd = PersistentData.CodeOriginal_FromFile;
            btn_SaveCodeAsPLCSecondSendCode.Enabled = true;
        }

        private void Form_SecondCodeCompare_Load(object sender, EventArgs e)
        {
            m_bContinueMark_btnFlag = false;
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
