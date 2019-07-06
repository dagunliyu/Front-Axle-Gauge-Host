using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> this class shows all records of the given datasets </summary>
	public class RecordsDialog : System.Windows.Forms.Form
	{
		#region UI fields
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.DataGrid AccessDataGrid;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.DataGrid SQLDataGrid;
		private System.Windows.Forms.DataGrid XMLDataGrid;
		private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter1;
		private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel6;
		private System.ComponentModel.Container components = null;
		#endregion

		#region construction, destruction
		public RecordsDialog(System.Data.DataSet dsAccess,System.Data.DataSet dsXML,System.Data.DataSet dsSQL)
		{
			InitializeComponent();

			AccessDataGrid.DataSource = dsAccess;
			XMLDataGrid.DataSource = dsXML;
			SQLDataGrid.DataSource = dsSQL;
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
		#endregion

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.AccessDataGrid = new System.Windows.Forms.DataGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.XMLDataGrid = new System.Windows.Forms.DataGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.SQLDataGrid = new System.Windows.Forms.DataGrid();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.collapsibleSplitter1 = new NJFLib.Controls.CollapsibleSplitter();
            this.collapsibleSplitter2 = new NJFLib.Controls.CollapsibleSplitter();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AccessDataGrid)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XMLDataGrid)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SQLDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AccessDataGrid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 121);
            this.panel1.TabIndex = 1;
            // 
            // AccessDataGrid
            // 
            this.AccessDataGrid.CaptionText = "Access data";
            this.AccessDataGrid.DataMember = "";
            this.AccessDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccessDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.AccessDataGrid.Location = new System.Drawing.Point(0, 0);
            this.AccessDataGrid.Name = "AccessDataGrid";
            this.AccessDataGrid.Size = new System.Drawing.Size(632, 121);
            this.AccessDataGrid.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.XMLDataGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 308);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(632, 121);
            this.panel2.TabIndex = 2;
            // 
            // XMLDataGrid
            // 
            this.XMLDataGrid.CaptionText = "XML data";
            this.XMLDataGrid.DataMember = "";
            this.XMLDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XMLDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.XMLDataGrid.Location = new System.Drawing.Point(0, 0);
            this.XMLDataGrid.Name = "XMLDataGrid";
            this.XMLDataGrid.Size = new System.Drawing.Size(632, 121);
            this.XMLDataGrid.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 121);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(632, 187);
            this.panel3.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.SQLDataGrid);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 9);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(632, 169);
            this.panel6.TabIndex = 6;
            // 
            // SQLDataGrid
            // 
            this.SQLDataGrid.CaptionText = "SQL data";
            this.SQLDataGrid.DataMember = "";
            this.SQLDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SQLDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.SQLDataGrid.Location = new System.Drawing.Point(0, 0);
            this.SQLDataGrid.Name = "SQLDataGrid";
            this.SQLDataGrid.Size = new System.Drawing.Size(632, 169);
            this.SQLDataGrid.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 178);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(632, 9);
            this.panel5.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(632, 9);
            this.panel4.TabIndex = 4;
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = this.panel1;
            this.collapsibleSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.collapsibleSplitter1.ExpandParentForm = true;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(0, 121);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.TabIndex = 7;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = true;
            this.collapsibleSplitter1.VisualStyle = NJFLib.Controls.VisualStyles.Mozilla;
            // 
            // collapsibleSplitter2
            // 
            this.collapsibleSplitter2.AnimationDelay = 20;
            this.collapsibleSplitter2.AnimationStep = 20;
            this.collapsibleSplitter2.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter2.ControlToHide = this.panel2;
            this.collapsibleSplitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.collapsibleSplitter2.ExpandParentForm = true;
            this.collapsibleSplitter2.Location = new System.Drawing.Point(0, 300);
            this.collapsibleSplitter2.Name = "collapsibleSplitter2";
            this.collapsibleSplitter2.TabIndex = 8;
            this.collapsibleSplitter2.TabStop = false;
            this.collapsibleSplitter2.UseAnimations = true;
            this.collapsibleSplitter2.VisualStyle = NJFLib.Controls.VisualStyles.Mozilla;
            // 
            // RecordsDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(632, 429);
            this.Controls.Add(this.collapsibleSplitter2);
            this.Controls.Add(this.collapsibleSplitter1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "RecordsDialog";
            this.Text = "RecordsDialog";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AccessDataGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.XMLDataGrid)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SQLDataGrid)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

	}
}
