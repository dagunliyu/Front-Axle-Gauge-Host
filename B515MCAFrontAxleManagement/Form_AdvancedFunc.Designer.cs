namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    partial class Form_AdvancedFunc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AdvancedFunc));
            this.btn_SaveCurrentSetting = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_CallHistory = new System.Windows.Forms.Button();
            this.btn_ReturnMainForm = new System.Windows.Forms.Button();
            this.dgv_ToleranceSetting = new System.Windows.Forms.DataGridView();
            this.CarType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarkCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_BatchTol_Dn = new System.Windows.Forms.TextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.tbx_BatchTol_Up = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_SaveCurrentTolFromExcel = new System.Windows.Forms.Button();
            this.btn_InportExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ToleranceSetting)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_SaveCurrentSetting
            // 
            this.btn_SaveCurrentSetting.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_SaveCurrentSetting.Location = new System.Drawing.Point(33, 17);
            this.btn_SaveCurrentSetting.Name = "btn_SaveCurrentSetting";
            this.btn_SaveCurrentSetting.Size = new System.Drawing.Size(103, 33);
            this.btn_SaveCurrentSetting.TabIndex = 2;
            this.btn_SaveCurrentSetting.Text = "保存当前设置";
            this.btn_SaveCurrentSetting.Click += new System.EventHandler(this.btn_SaveCurrentSetting_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btn_CallHistory
            // 
            this.btn_CallHistory.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_CallHistory.Location = new System.Drawing.Point(153, 17);
            this.btn_CallHistory.Name = "btn_CallHistory";
            this.btn_CallHistory.Size = new System.Drawing.Size(103, 33);
            this.btn_CallHistory.TabIndex = 4;
            this.btn_CallHistory.Text = "调用历史";
            this.btn_CallHistory.Click += new System.EventHandler(this.btn_CallHistory_Click);
            // 
            // btn_ReturnMainForm
            // 
            this.btn_ReturnMainForm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ReturnMainForm.Location = new System.Drawing.Point(274, 17);
            this.btn_ReturnMainForm.Name = "btn_ReturnMainForm";
            this.btn_ReturnMainForm.Size = new System.Drawing.Size(103, 33);
            this.btn_ReturnMainForm.TabIndex = 5;
            this.btn_ReturnMainForm.Text = "返回主界面";
            this.btn_ReturnMainForm.Click += new System.EventHandler(this.btn_ReturnMainForm_Click);
            // 
            // dgv_ToleranceSetting
            // 
            this.dgv_ToleranceSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ToleranceSetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CarType,
            this.TimeDate,
            this.SerialNumber,
            this.MarkCode,
            this.Item});
            this.dgv_ToleranceSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ToleranceSetting.Location = new System.Drawing.Point(3, 66);
            this.dgv_ToleranceSetting.Name = "dgv_ToleranceSetting";
            this.dgv_ToleranceSetting.RowTemplate.Height = 23;
            this.dgv_ToleranceSetting.Size = new System.Drawing.Size(659, 372);
            this.dgv_ToleranceSetting.TabIndex = 6;
            this.dgv_ToleranceSetting.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ToleranceSetting_CellDoubleClick);
            // 
            // CarType
            // 
            this.CarType.DataPropertyName = "CarType";
            this.CarType.HeaderText = "序  号";
            this.CarType.Name = "CarType";
            this.CarType.ReadOnly = true;
            // 
            // TimeDate
            // 
            this.TimeDate.DataPropertyName = "TimeDate";
            this.TimeDate.HeaderText = "名  称";
            this.TimeDate.Name = "TimeDate";
            this.TimeDate.ReadOnly = true;
            // 
            // SerialNumber
            // 
            this.SerialNumber.DataPropertyName = "SerialNumber";
            this.SerialNumber.HeaderText = "屏  蔽";
            this.SerialNumber.Name = "SerialNumber";
            // 
            // MarkCode
            // 
            this.MarkCode.DataPropertyName = "MarkCode";
            this.MarkCode.HeaderText = "上公差设定";
            this.MarkCode.Name = "MarkCode";
            this.MarkCode.ReadOnly = true;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "Item";
            this.Item.HeaderText = "下公差设定";
            this.Item.Name = "Item";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(679, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_ToleranceSetting, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(665, 410);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbx_BatchTol_Dn);
            this.groupBox1.Controls.Add(this.label106);
            this.groupBox1.Controls.Add(this.tbx_BatchTol_Up);
            this.groupBox1.Controls.Add(this.btn_SaveCurrentSetting);
            this.groupBox1.Controls.Add(this.btn_ReturnMainForm);
            this.groupBox1.Controls.Add(this.btn_CallHistory);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(659, 57);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(522, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 51;
            this.label1.Text = "批量下公差设定";
            // 
            // tbx_BatchTol_Dn
            // 
            this.tbx_BatchTol_Dn.Location = new System.Drawing.Point(538, 32);
            this.tbx_BatchTol_Dn.Name = "tbx_BatchTol_Dn";
            this.tbx_BatchTol_Dn.Size = new System.Drawing.Size(54, 21);
            this.tbx_BatchTol_Dn.TabIndex = 50;
            this.tbx_BatchTol_Dn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbx_BatchTol_Dn_KeyDown);
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(412, 17);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(89, 12);
            this.label106.TabIndex = 49;
            this.label106.Text = "批量上公差设定";
            // 
            // tbx_BatchTol_Up
            // 
            this.tbx_BatchTol_Up.Location = new System.Drawing.Point(428, 32);
            this.tbx_BatchTol_Up.Name = "tbx_BatchTol_Up";
            this.tbx_BatchTol_Up.Size = new System.Drawing.Size(54, 21);
            this.tbx_BatchTol_Up.TabIndex = 48;
            this.tbx_BatchTol_Up.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbx_BatchTol_Up_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(679, 441);
            this.tabControl1.TabIndex = 52;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(671, 416);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "界面1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(671, 416);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "导入excel";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(665, 410);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(659, 372);
            this.dataGridView1.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_SaveCurrentTolFromExcel);
            this.groupBox2.Controls.Add(this.btn_InportExcel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(659, 57);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作";
            // 
            // btn_SaveCurrentTolFromExcel
            // 
            this.btn_SaveCurrentTolFromExcel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_SaveCurrentTolFromExcel.Location = new System.Drawing.Point(159, 17);
            this.btn_SaveCurrentTolFromExcel.Name = "btn_SaveCurrentTolFromExcel";
            this.btn_SaveCurrentTolFromExcel.Size = new System.Drawing.Size(103, 33);
            this.btn_SaveCurrentTolFromExcel.TabIndex = 3;
            this.btn_SaveCurrentTolFromExcel.Text = "保存当前设置";
            this.btn_SaveCurrentTolFromExcel.Click += new System.EventHandler(this.btn_SaveCurrentTolFromExcel_Click);
            // 
            // btn_InportExcel
            // 
            this.btn_InportExcel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_InportExcel.Location = new System.Drawing.Point(33, 17);
            this.btn_InportExcel.Name = "btn_InportExcel";
            this.btn_InportExcel.Size = new System.Drawing.Size(103, 33);
            this.btn_InportExcel.TabIndex = 2;
            this.btn_InportExcel.Text = "导入excel";
            this.btn_InportExcel.Click += new System.EventHandler(this.btn_InportExcel_Click);
            // 
            // Form_AdvancedFunc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 465);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_AdvancedFunc";
            this.Text = "高级功能";
            this.Load += new System.EventHandler(this.Form_AdvancedFunc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ToleranceSetting)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_SaveCurrentSetting;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_CallHistory;
        private System.Windows.Forms.Button btn_ReturnMainForm;
        public System.Windows.Forms.DataGridView dgv_ToleranceSetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarkCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbx_BatchTol_Up;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_BatchTol_Dn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_InportExcel;
        private System.Windows.Forms.Button btn_SaveCurrentTolFromExcel;
    }
}