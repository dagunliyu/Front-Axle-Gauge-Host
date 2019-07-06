using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary>
	/// Zusammenfassung für AboutBox.
	/// </summary>
	public class AboutBox : System.Windows.Forms.Form
    {
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.LinkLabel LinkLabel1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button OKBtn;
        internal LinkLabel LinkLabel2;
        internal PictureBox PictureBox1;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutBox()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
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
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.LinkLabel2 = new System.Windows.Forms.LinkLabel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(10, 78);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(192, 17);
            this.Label9.TabIndex = 24;
            this.Label9.Text = "Service && Support Homepage:";
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(202, 190);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(211, 17);
            this.Label8.TabIndex = 23;
            this.Label8.Text = "01/05/2018";
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(10, 190);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(115, 17);
            this.Label7.TabIndex = 22;
            this.Label7.Text = "Release Date:";
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(10, 224);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(355, 17);
            this.Label6.TabIndex = 21;
            this.Label6.Text = "Copyright (c) 2018, CIGIT. All Rights Reserved";
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(202, 164);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(211, 17);
            this.Label5.TabIndex = 20;
            this.Label5.Text = "V1.0";
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(10, 164);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(115, 17);
            this.Label4.TabIndex = 19;
            this.Label4.Text = "Version:";
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.Location = new System.Drawing.Point(202, 138);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(211, 17);
            this.LinkLabel1.TabIndex = 18;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "########";
            this.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(10, 138);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(115, 17);
            this.Label3.TabIndex = 17;
            this.Label3.Text = "Entry ID:";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(202, 112);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(259, 17);
            this.Label2.TabIndex = 16;
            this.Label2.Text = "OPC¿Í»§¶Ë";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(10, 112);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(124, 17);
            this.Label1.TabIndex = 15;
            this.Label1.Text = "Sample Application:";
            // 
            // OKBtn
            // 
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OKBtn.Location = new System.Drawing.Point(10, 258);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(96, 26);
            this.OKBtn.TabIndex = 13;
            this.OKBtn.Text = "OK";
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // LinkLabel2
            // 
            this.LinkLabel2.Location = new System.Drawing.Point(202, 78);
            this.LinkLabel2.Name = "LinkLabel2";
            this.LinkLabel2.Size = new System.Drawing.Size(240, 17);
            this.LinkLabel2.TabIndex = 25;
            this.LinkLabel2.TabStop = true;
            this.LinkLabel2.Text = "Comimg";
            this.LinkLabel2.Visible = false;
            this.LinkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel2_LinkClicked);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(0, 0);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(499, 69);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 14;
            this.PictureBox1.TabStop = false;
            // 
            // AboutBox
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.OKBtn;
            this.ClientSize = new System.Drawing.Size(410, 294);
            this.ControlBox = false;
            this.Controls.Add(this.LinkLabel2);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.OKBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowInTaskbar = false;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private void OKBtn_Click(object sender, System.EventArgs e)
        {
            this.Close(); 
        }

        private void LinkLabel2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
           System.Diagnostics.Process.Start(LinkLabel2.Text);
        }

        private void LinkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://support.automation.siemens.com/WW/view/EN/" + LinkLabel1.Text);
        }
	}
}
