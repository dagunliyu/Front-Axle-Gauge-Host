using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB;


using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    public partial class Form_DataInterface : Form
    {
        DBConnector dbconn = new DBConnector("");
        DBOperate DBOperate_Obj = new DBOperate();//"B515_MCA"

        public Form_DataInterface()
        {
            InitializeComponent();

            m_bAdminLogin = false;


        }

        public bool m_bToleranceSettingFinished = false;
        private void btn_ReturnMainForm_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        public static bool m_bAdminLogin = false;

        private void btn_ReturnToMainForm_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        bool m_bQueryAllDatByDate = false;
        bool m_bQueryAllDatByID = false;
        ZessiMeasureDatReadFromDB_Buffer zessiMeaDatReadFromDB = new ZessiMeasureDatReadFromDB_Buffer();
        private void btn_QueryAllDatByTime_Click(object sender, EventArgs e)
        {
            m_bQueryAllDatByDate = true;
            m_bQueryAllDatByID = false;

            dgvCollectDatInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; //开启了 AutoSizecolumnsMode后DataGirdView会对所有数据进行遍历以决定每一列的宽度，所以绑定速度会受到极大的影响
            dgvCollectDatInfo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvCollectDatInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvCollectDatInfo.VirtualMode = true;

            dgvCollectDatInfo.DataSource = DBOperate_Obj.Query_Collect_Dat("日期",
                                                                            null,
                                                                            dtPickerAll_Begin.Value,
                                                                            dtPickerAll_End.Value,
                                                                            ref zessiMeaDatReadFromDB); //DBOperate_Obj.GetAllDataThroughDate(dtPickerAll_Begin.Value, dtPickerAll_End.Value);
            Thread.Sleep(500);
            // 固定前5项
            dgvCollectDatInfo.Columns[0].Frozen = true;   // 工件类型
            dgvCollectDatInfo.Columns[1].Frozen = true;   // 
            dgvCollectDatInfo.Columns[2].Frozen = true;
            dgvCollectDatInfo.Columns[3].Frozen = true;   // 
            dgvCollectDatInfo.Columns[4].Frozen = true;

            // 检查转到dgv中的数据是否有error项,有的话就标红
            Detect_AllDatError(dgvCollectDatInfo);
        }
        private void btn_QueryAllDatByID_Click(object sender, EventArgs e)
        {

            m_bQueryAllDatByDate = false;
            m_bQueryAllDatByID = true;

            dgvCollectDatInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; //开启了 AutoSizecolumnsMode后DataGirdView会对所有数据进行遍历以决定每一列的宽度，所以绑定速度会受到极大的影响
            dgvCollectDatInfo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvCollectDatInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvCollectDatInfo.VirtualMode = true;
            //Application.DoEvents();
            dgvCollectDatInfo.DataSource = DBOperate_Obj.Query_Collect_Dat("打标码",
                                                                            tbx_QueryID.Text,
                                                                            dtPickerAll_Begin.Value, dtPickerAll_End.Value,
                                                                            ref zessiMeaDatReadFromDB); //DBOperate_Obj.GetAllDataThroughDate(dtPickerAll_Begin.Value, dtPickerAll_End.Value);

            //Application.DoEvents();
            // 检查转到dgv中的数据是否有error项,有的话就标红
            Detect_AllDatError(dgvCollectDatInfo);
            // 固定前5项
            dgvCollectDatInfo.Columns[0].Frozen = true;   // 工件类型
            dgvCollectDatInfo.Columns[1].Frozen = true;   // 
            dgvCollectDatInfo.Columns[2].Frozen = true;
            dgvCollectDatInfo.Columns[3].Frozen = true;   // 
            dgvCollectDatInfo.Columns[4].Frozen = true;

        }
        public void Detect_AllDatError(DataGridView dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string PressJudgment = 
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i] != null)
                    {
                        if (j > 5) // 从i=6开始,第7项是PTG_LH的数据项
                        {
                            if ((j % 3) == 0)
                            {
                                double db1 = Math.Abs(Convert.ToDouble(dt.Rows[i].Cells[j].Value));
                                double db2 = Math.Abs(Convert.ToDouble(dt.Rows[i].Cells[j + 1].Value));
                                double db3 = Math.Abs(Convert.ToDouble(dt.Rows[i].Cells[j + 2].Value));

                                //if ((Math.Abs(Convert.ToDouble(dt.Rows[i].Cells[j].Value)) < Math.Abs(Convert.ToDouble(dt.Rows[i].Cells[j + 1].Value))) &&
                                //    (Math.Abs(Convert.ToDouble(dt.Rows[i].Cells[j].Value)) < Math.Abs(Convert.ToDouble(dt.Rows[i].Cells[j + 2].Value))))
                                //{ }
                                if( (db1 < db2) &&
                                    (db1 < db3) )
                                { dt.Rows[i].Cells[j].Style.ForeColor = Color.Black; }
                                else // 异常的数据
                                {
                                    dt.Rows[i].Cells[j].Style.BackColor = Color.Red;
                                    //dt.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                }
            }
        }
        DataGridViewRow dgvRow = new DataGridViewRow();
        DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
        DataGridViewComboBoxCell comboxcell = new DataGridViewComboBoxCell();
        private void Form_DataInterface_Load(object sender, EventArgs e)
        {
            //    dgvRow.Cells.Add(textboxcell);
            //    dgvRow.Cells.Add(comboxcell);
            //    dgvCollectDatInfo.Rows.Add(dgvRow);
        }

        private void btn_ExportData_Click(object sender, EventArgs e)
        {
            try
            {
                //没有数据的话就不往下执行  
                if (dgvCollectDatInfo.Rows.Count == 0)
                    return;
                //实例化一个Excel.Application对象  
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                //让后台执行设置为可见
                excel.Visible = true;

                //新增加一个工作簿，Workbook是直接保存，不会弹出保存对话框，加上Application会弹出保存对话框，值为false会报错  
               //  excel.Application.Workbooks.Add(true);

                Excel.Workbook wbook = excel.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)wbook.Worksheets[1];

                #region 生成Excel中列头名称  
                //生成Excel中列头名称  
                for (int i = 0; i < dgvCollectDatInfo.Columns.Count; i++)
                {
                    if (this.dgvCollectDatInfo.Columns[i].Visible == true)
                    {
                        worksheet.Cells[1, i + 1] = dgvCollectDatInfo.Columns[i].HeaderText; // worksheet - 从1行1列开始
                    }
                }
                #endregion

                #region 把DataGridView当前页的数据保存在Excel中  
                //把DataGridView当前页的数据保存在Excel中  
                for (int i = 0; i < dgvCollectDatInfo.Rows.Count; i++)
                {
                    System.Windows.Forms.Application.DoEvents();
                    for (int j = 0; j < dgvCollectDatInfo.Columns.Count; j++)
                    {
                        if (this.dgvCollectDatInfo.Columns[j].Visible == true)  //导出可见的列；
                        {
                            if (dgvCollectDatInfo[j, i].ValueType == typeof(string))
                            {
                                if (j <= 5) // 等先把excel项填满
                                {
                                    worksheet.Cells[i + 2, j + 1] = "'" + dgvCollectDatInfo[j, i].Value.ToString(); // 有了标题,从第2行开始(所以为i+2);列从1开始,为j+1
                                }
                                if (j > 5) // 从第7列开始是偏差值  j=0对应的是表格的1
                                {
                                    worksheet.Cells[i + 2, j + 1] = Convert.ToDouble( dgvCollectDatInfo[j, i].Value.ToString() );
                                }
                            }
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1] = dgvCollectDatInfo[j, i].Value.ToString();
                            double data = Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 1], worksheet.Cells[i + 2, j + 1]].value2);
                            if (data < (-0.3) || data > 0.3)
                            {
                                worksheet.Range[worksheet.Cells[i + 2, j + 1], worksheet.Cells[i + 2, j + 1]].Font.ColorIndex = 3;
                            }
                            else { }
                        }
                    }
                    #endregion

                #region 变红
                    //变红
                for(int j = 0; j < dgvCollectDatInfo.Columns.Count; j++)
                {
                    if(j>5)
                    {
                        if ((j % 3) == 0) // 每隔3个选中1个
                        {
                            double cdt = Convert.ToDouble(worksheet.Cells[i + 2, j + 1].value);
                            double cdtUp = Convert.ToDouble(worksheet.Cells[i + 2, j + 2].value);
                            double cdtDn = Convert.ToDouble(worksheet.Cells[i + 2, j + 3].value);

                            //double cdtDn= Math.Abs(Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 1].value2));
                            //double data = Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 1], worksheet.Cells[i + 2, j + 1]].value2);

                            //double data  = Math.Abs(Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 1], worksheet.Cells[i + 2, j + 1]].value2)) < Math.Abs(Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 2], worksheet.Cells[i + 2, j + 2]].value2));
                            //double data = Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, 7], worksheet.Cells[i + 2, 7]].Value);
                            //double data_Up = Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, 10], worksheet.Cells[i + 2, 10]].Value);
                            //double data_Dn = Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, 13], worksheet.Cells[i + 2, 13]].Value);
                            //if ((Math.Abs(Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 1], worksheet.Cells[i + 2, j + 1]].value2)) < Math.Abs(Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 2], worksheet.Cells[i + 2, j + 2]].value2))) &&
                            //    (Math.Abs(Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 1], worksheet.Cells[i + 2, j + 1]].value2)) < Math.Abs(Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 3], worksheet.Cells[i + 2, j + 3]].value2))))
                            //{ }
                            cdt = Math.Abs(Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 1], worksheet.Cells[i + 2, j + 1]].value2));
                            cdtUp = Math.Abs(Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 2], worksheet.Cells[i + 2, j + 2]].value2));
                            cdtDn = Math.Abs(Convert.ToDouble(worksheet.Range[worksheet.Cells[i + 2, j + 3], worksheet.Cells[i + 2, j + 3]].value2));
                            if((cdt < cdtUp) && (cdt < cdtDn))
                            {
                                worksheet.Range[worksheet.Cells[i + 2, j + 1], worksheet.Cells[i + 2, j + 1]].Font.ColorIndex = 0;
                            }
                            else
                            {
                                worksheet.Range[worksheet.Cells[i + 2, j + 1], worksheet.Cells[i + 2, j + 1]].Font.ColorIndex = 3;
                            }

                            // 漏检的数值为1 将其标红
                            if (true)
                            {
                            }

                        }
                    }
                }
                #endregion
             }
            
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误提示");
            }
      
        
        }

        private void dgvCollectDatInfo_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvCollectDatInfo != null)
            {
                // 检查转到dgv中的数据是否有error项,有的话就标红
                Detect_AllDatError(dgvCollectDatInfo);
            }
        }
        


    }
}