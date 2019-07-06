namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    partial class Form_DataInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_DataInterface));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.errorInfo = new System.Windows.Forms.ErrorProvider(this.components);
            this.dgvCollectDatInfo = new System.Windows.Forms.DataGridView();
            this.label88 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.dtPickerAll_End = new System.Windows.Forms.DateTimePicker();
            this.dtPickerAll_Begin = new System.Windows.Forms.DateTimePicker();
            this.btn_QueryAllDatByTime = new System.Windows.Forms.Button();
            this.label81 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_ClearQueryID = new System.Windows.Forms.Button();
            this.btn_QueryAllDatByID = new System.Windows.Forms.Button();
            this.label82 = new System.Windows.Forms.Label();
            this.tbx_QueryID = new System.Windows.Forms.TextBox();
            this.btn_ReturnToMainForm = new System.Windows.Forms.Button();
            this.btn_ExportData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCollectDatInfo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(486, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(249, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // errorInfo
            // 
            this.errorInfo.ContainerControl = this;
            // 
            // dgvCollectDatInfo
            // 
            this.dgvCollectDatInfo.AllowUserToAddRows = false;
            this.dgvCollectDatInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCollectDatInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCollectDatInfo.Location = new System.Drawing.Point(3, 160);
            this.dgvCollectDatInfo.Name = "dgvCollectDatInfo";
            this.dgvCollectDatInfo.RowTemplate.Height = 23;
            this.dgvCollectDatInfo.Size = new System.Drawing.Size(738, 258);
            this.dgvCollectDatInfo.TabIndex = 5;
            this.dgvCollectDatInfo.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCollectDatInfo_ColumnHeaderMouseClick);
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(18, 47);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(383, 12);
            this.label88.TabIndex = 46;
            this.label88.Text = "(例：查询23日数据，则范围调整为 2017年1月22日 到 2017年1月24日)";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(210, 20);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(17, 12);
            this.label77.TabIndex = 45;
            this.label77.Text = "到";
            // 
            // dtPickerAll_End
            // 
            this.dtPickerAll_End.Location = new System.Drawing.Point(230, 16);
            this.dtPickerAll_End.Name = "dtPickerAll_End";
            this.dtPickerAll_End.Size = new System.Drawing.Size(122, 21);
            this.dtPickerAll_End.TabIndex = 44;
            // 
            // dtPickerAll_Begin
            // 
            this.dtPickerAll_Begin.Location = new System.Drawing.Point(86, 16);
            this.dtPickerAll_Begin.Name = "dtPickerAll_Begin";
            this.dtPickerAll_Begin.Size = new System.Drawing.Size(122, 21);
            this.dtPickerAll_Begin.TabIndex = 43;
            // 
            // btn_QueryAllDatByTime
            // 
            this.btn_QueryAllDatByTime.Location = new System.Drawing.Point(358, 13);
            this.btn_QueryAllDatByTime.Name = "btn_QueryAllDatByTime";
            this.btn_QueryAllDatByTime.Size = new System.Drawing.Size(103, 28);
            this.btn_QueryAllDatByTime.TabIndex = 42;
            this.btn_QueryAllDatByTime.Text = "查询工件";
            this.btn_QueryAllDatByTime.UseVisualStyleBackColor = true;
            this.btn_QueryAllDatByTime.Click += new System.EventHandler(this.btn_QueryAllDatByTime_Click);
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(18, 21);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(65, 12);
            this.label81.TabIndex = 41;
            this.label81.Text = "查询时间从";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvCollectDatInfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(744, 421);
            this.tableLayoutPanel1.TabIndex = 47;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btn_ReturnToMainForm);
            this.groupBox1.Controls.Add(this.btn_ExportData);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(738, 151);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label81);
            this.groupBox2.Controls.Add(this.dtPickerAll_Begin);
            this.groupBox2.Controls.Add(this.dtPickerAll_End);
            this.groupBox2.Controls.Add(this.btn_ClearQueryID);
            this.groupBox2.Controls.Add(this.btn_QueryAllDatByTime);
            this.groupBox2.Controls.Add(this.btn_QueryAllDatByID);
            this.groupBox2.Controls.Add(this.label77);
            this.groupBox2.Controls.Add(this.label82);
            this.groupBox2.Controls.Add(this.label88);
            this.groupBox2.Controls.Add(this.tbx_QueryID);
            this.groupBox2.Location = new System.Drawing.Point(5, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(477, 108);
            this.groupBox2.TabIndex = 53;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询操作";
            // 
            // btn_ClearQueryID
            // 
            this.btn_ClearQueryID.Location = new System.Drawing.Point(394, 71);
            this.btn_ClearQueryID.Name = "btn_ClearQueryID";
            this.btn_ClearQueryID.Size = new System.Drawing.Size(67, 28);
            this.btn_ClearQueryID.TabIndex = 50;
            this.btn_ClearQueryID.Text = "清除文本";
            this.btn_ClearQueryID.UseVisualStyleBackColor = true;
            // 
            // btn_QueryAllDatByID
            // 
            this.btn_QueryAllDatByID.Location = new System.Drawing.Point(298, 71);
            this.btn_QueryAllDatByID.Name = "btn_QueryAllDatByID";
            this.btn_QueryAllDatByID.Size = new System.Drawing.Size(90, 28);
            this.btn_QueryAllDatByID.TabIndex = 49;
            this.btn_QueryAllDatByID.Text = "查询工件";
            this.btn_QueryAllDatByID.UseVisualStyleBackColor = true;
            this.btn_QueryAllDatByID.Click += new System.EventHandler(this.btn_QueryAllDatByID_Click);
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(19, 80);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(65, 12);
            this.label82.TabIndex = 48;
            this.label82.Text = "打标码查询";
            // 
            // tbx_QueryID
            // 
            this.tbx_QueryID.Location = new System.Drawing.Point(88, 74);
            this.tbx_QueryID.Name = "tbx_QueryID";
            this.tbx_QueryID.Size = new System.Drawing.Size(204, 21);
            this.tbx_QueryID.TabIndex = 47;
            // 
            // btn_ReturnToMainForm
            // 
            this.btn_ReturnToMainForm.Location = new System.Drawing.Point(627, 80);
            this.btn_ReturnToMainForm.Name = "btn_ReturnToMainForm";
            this.btn_ReturnToMainForm.Size = new System.Drawing.Size(90, 28);
            this.btn_ReturnToMainForm.TabIndex = 52;
            this.btn_ReturnToMainForm.Text = "返回主界面";
            this.btn_ReturnToMainForm.UseVisualStyleBackColor = true;
            this.btn_ReturnToMainForm.Click += new System.EventHandler(this.btn_ReturnToMainForm_Click);
            // 
            // btn_ExportData
            // 
            this.btn_ExportData.Location = new System.Drawing.Point(511, 80);
            this.btn_ExportData.Name = "btn_ExportData";
            this.btn_ExportData.Size = new System.Drawing.Size(90, 28);
            this.btn_ExportData.TabIndex = 51;
            this.btn_ExportData.Text = "导出数据";
            this.btn_ExportData.UseVisualStyleBackColor = true;
            this.btn_ExportData.Click += new System.EventHandler(this.btn_ExportData_Click);
            // 
            // Form_DataInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 421);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_DataInterface";
            this.Text = "数据查询界面";
            this.Load += new System.EventHandler(this.Form_DataInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCollectDatInfo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ErrorProvider errorInfo;
        public System.Windows.Forms.DataGridView dgvCollectDatInfo;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.DateTimePicker dtPickerAll_End;
        private System.Windows.Forms.DateTimePicker dtPickerAll_Begin;
        private System.Windows.Forms.Button btn_QueryAllDatByTime;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_ClearQueryID;
        private System.Windows.Forms.Button btn_QueryAllDatByID;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.TextBox tbx_QueryID;
        private System.Windows.Forms.Button btn_ReturnToMainForm;
        private System.Windows.Forms.Button btn_ExportData;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}