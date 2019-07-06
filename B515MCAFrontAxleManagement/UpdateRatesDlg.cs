using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> this class shows all records of the given datasets </summary>
	public class UpdateRatesDlg : System.Windows.Forms.Form
	{
		#region fields
		#region UI fields
        private System.Windows.Forms.Label BRCVLabel;
		private System.Windows.Forms.Button OkButton;
		private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TextBox UpdRateDBblockTextBox;
		private System.ComponentModel.Container components = null;
		#endregion

		#region to control the inputs of the textfields
		private bool m_changeBack = false;
		private string m_strDummy = "";
		#endregion
		#endregion

		#region construction, destruction
		public UpdateRatesDlg()
		{
			InitializeComponent();
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
            this.BRCVLabel = new System.Windows.Forms.Label();
            this.UpdRateDBblockTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BRCVLabel
            // 
            this.BRCVLabel.AutoSize = true;
            this.BRCVLabel.Location = new System.Drawing.Point(42, 54);
            this.BRCVLabel.Name = "BRCVLabel";
            this.BRCVLabel.Size = new System.Drawing.Size(113, 12);
            this.BRCVLabel.TabIndex = 0;
            this.BRCVLabel.Text = "DB块数据的更新速率";
            // 
            // UpdRateDBblockTextBox
            // 
            this.UpdRateDBblockTextBox.Location = new System.Drawing.Point(175, 51);
            this.UpdRateDBblockTextBox.Name = "UpdRateDBblockTextBox";
            this.UpdRateDBblockTextBox.Size = new System.Drawing.Size(77, 21);
            this.UpdRateDBblockTextBox.TabIndex = 3;
            this.UpdRateDBblockTextBox.Text = "100";
            this.UpdRateDBblockTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UpdRateDBblockTextBox.TextChanged += new System.EventHandler(this.UpdRateBRCVTextBox_TextChanged);
            this.UpdRateDBblockTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdRateBRCVTextBox_KeyDown);
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(44, 90);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(90, 25);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Ok";
            this.OkButton.Click += new System.EventHandler(this.UpdRateBRCVTextBox_TextChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(169, 90);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(90, 25);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            // 
            // UpdateRatesDlg
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(301, 140);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.UpdRateDBblockTextBox);
            this.Controls.Add(this.BRCVLabel);
            this.Controls.Add(this.CancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(307, 172);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(307, 172);
            this.Name = "UpdateRatesDlg";
            this.Text = "UpdateRates";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

		#region properties
		public int updateRateDBblock
		{
			get { return int.Parse(this.UpdRateDBblockTextBox.Text); }
		}
		
		#endregion
		/// <summary>The .NET-Textboxes have no property that only digits are valid characters.
		/// So we have to take care for it manually.</summary>
		#region Textbox handlers
		private void UpdRateBRCVTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Check if only digits are entered.

			// get the last character of the keyeventargument e
			char a = e.KeyData.ToString().ToCharArray(e.KeyData.ToString().Length-1,1)[0];

			m_changeBack = false;
			if(e.KeyData.ToString().Length==1 && !char.IsDigit(a))
			{
				m_strDummy = this.UpdRateDBblockTextBox.Text;
				m_changeBack = true;
			}
		}

		private void UpdRateBRCVTextBox_TextChanged(object sender, System.EventArgs e)
		{
			if (m_changeBack)
			{
				m_changeBack = false;
				this.UpdRateDBblockTextBox.Text = m_strDummy;
			}
		}

		private void UpdRateFVisuTextBox_TextChanged(object sender, System.EventArgs e)
		{
			if (m_changeBack)
			{
				m_changeBack = false;
			}
		}

		private void UpdRateFVisuTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Check if only digits are entered.

			// get the last character of the keyeventargument e
			char a = e.KeyData.ToString().ToCharArray(e.KeyData.ToString().Length-1,1)[0];

			m_changeBack = false;
			if(e.KeyData.ToString().Length==1 && !char.IsDigit(a))
			{
				m_changeBack = true;
			}
		}

		private void UpdRateSVisuTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Check if only digits are entered.

			// get the last character of the keyeventargument e
			char a = e.KeyData.ToString().ToCharArray(e.KeyData.ToString().Length-1,1)[0];

			m_changeBack = false;
			if(e.KeyData.ToString().Length==1 && !char.IsDigit(a))
			{
				m_changeBack = true;
			}
		}

		private void UpdRateSVisuTextBox_TextChanged(object sender, System.EventArgs e)
		{
			if (m_changeBack)
			{
				m_changeBack = false;
			}
		}
		#endregion


    }
}
