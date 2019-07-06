using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> this class shows the traces </summary>
	public class TraceToolBox : System.Windows.Forms.Form
	{
		#region fields
		bool m_closeButtonPressed  = false;
		
		private System.Windows.Forms.ListBox TraceListBox;
		private System.ComponentModel.Container components = null;
		#endregion

		#region construction, destruction
		public TraceToolBox()
		{
			InitializeComponent();

		}

		protected override void Dispose( bool disposing )
		{
			if (!m_closeButtonPressed)
			{
				m_closeButtonPressed = false;

				if( disposing )
				{
					if(components != null)
					{
						components.Dispose();
					}
				}
				base.Dispose( disposing );
			}
		}
		
		public new void Dispose()
		{
			m_closeButtonPressed = false;
		}
        
		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            this.TraceListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // TraceListBox
            // 
            this.TraceListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceListBox.ItemHeight = 12;
            this.TraceListBox.Location = new System.Drawing.Point(0, 0);
            this.TraceListBox.Name = "TraceListBox";
            this.TraceListBox.Size = new System.Drawing.Size(536, 56);
            this.TraceListBox.TabIndex = 0;
            // 
            // TraceToolBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(536, 56);
            this.Controls.Add(this.TraceListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "TraceToolBox";
            this.ShowInTaskbar = false;
            this.Text = "TraceToolBox";
            this.TopMost = true;
            this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region delegates, events
		public delegate void CloseButtonClickedEventHandler(object source, System.EventArgs e);
		/// <summary>event will be fired, if the user clicked the close button</summary>
		public event CloseButtonClickedEventHandler OnCloseClicked;
		#endregion

		#region public methods
		public void addTrace(string strTrace)
		{
			TraceListBox.Items.Add(strTrace);
			this.TraceListBox.Update();
		}
		#endregion

		#region eventhandler
		protected override void OnClosing(CancelEventArgs e)
		{
			// clicking the cross must not close the dialog; only hide it
			base.Hide();

			// necessary to provide dispose-functionality
			m_closeButtonPressed = true;

			OnCloseClicked(this,new System.EventArgs());
			//			base.OnClosing (e);
		}
		#endregion

	}
}
