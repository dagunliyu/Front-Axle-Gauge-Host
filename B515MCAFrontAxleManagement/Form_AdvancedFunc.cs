using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB;
using System.Data.OleDb;


namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    public partial class Form_AdvancedFunc : Form
    {
        public Form_AdvancedFunc()
        {
            InitializeComponent();
        }

        public bool m_bToleranceSettingFinished = false;


        DBOperate DBOperate_Obj = new DBOperate();
        public static double[] B515_Standard_Tolerance = new double[150];


        DataGridViewRow dgvRow = new DataGridViewRow();
        DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
        DataGridViewComboBoxCell comboxcell = new DataGridViewComboBoxCell();
        DataGridViewButtonCell buttoncell = new DataGridViewButtonCell();
        private void Form_AdvancedFunc_Load(object sender, EventArgs e)
        {
            DGV_ToleranceInit();

            // 在PersisData加载时就调用数据库函数, 读取最近一次保存的标准公差信息,将其赋值给 B515_Standard_Tolerance
        }
        private void btn_SaveCurrentSetting_Click(object sender, EventArgs e)
        {
            int cnt = -1;
            try
            {
                if (m_bInportExcelTabpage_Flag)
                {
                    for (int rows_idx = 0; rows_idx < dgv_ToleranceSetting.Rows.Count - 1; rows_idx++) // rows的数量会比实际数量多1个
                    {
                        B515_Standard_Tolerance[2 * rows_idx] = Convert.ToDouble(this.dataGridView1.Rows[rows_idx].Cells[3].Value.ToString());
                        B515_Standard_Tolerance[2 * rows_idx + 1] = Convert.ToDouble(this.dataGridView1.Rows[rows_idx].Cells[4].Value.ToString());
                    }
                    // 需保证保存的数值为数字


                    string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    // 获取到公差数据,存数据库
                    string CarType = "ACA";
                    cnt = DBOperate_Obj.Insert_ToleranceStandard_ModifiedDat(CarType,
                                                                            DateTime.Now,
                                                                            B515_Standard_Tolerance);
                }
                else
                {

                    for (int rows_idx = 0; rows_idx < dgv_ToleranceSetting.Rows.Count - 1; rows_idx++) // rows的数量会比实际数量多1个
                    {
                        B515_Standard_Tolerance[2 * rows_idx] = Convert.ToDouble(this.dgv_ToleranceSetting.Rows[rows_idx].Cells[3].Value.ToString());
                        B515_Standard_Tolerance[2 * rows_idx + 1] = Convert.ToDouble(this.dgv_ToleranceSetting.Rows[rows_idx].Cells[4].Value.ToString());
                    }
                    // 需保证保存的数值为数字


                    string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    // 获取到公差数据,存数据库
                    string CarType = "ACA";
                    cnt = DBOperate_Obj.Insert_ToleranceStandard_ModifiedDat(CarType,
                                                                            DateTime.Now,
                                                                            B515_Standard_Tolerance);

                    if (cnt == 0)
                    { }
                    else if (cnt == 1)
                    { }
                    else if (cnt == -1)
                    { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("1.异常来源:" + ex.Message + "\n2.系统分析:保存当前标准公差数据到数据库异常:\n请检查:\n\t1).保存的标准公差数据是否存在非法字符;\n\t2).数据库是否正常开启"
                    , "异常");
            }
            // 
        }

        private void btn_ReturnMainForm_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        DataTable dt = null;
        private void btn_CallHistory_Click(object sender, EventArgs e)
        {
            Form_AdvancedFunc_SelectRecord Fas = new Form_AdvancedFunc_SelectRecord();
            Fas.Show();
            Application.DoEvents();
            while (!Fas.IsDisposed)
            {
                Application.DoEvents();
                //this.Enabled = false;
            }
            this.Enabled = true;

            //
            string SelectDate = ""; 
            SelectDate = Fas.GetDateSelection;
            PersistentData.CallHistory_Date = Fas.GetDateSelection;
            dt = DBOperate_Obj.Query_ToleranceStandard_SingleDay(SelectDate);
            // 更新dgv中的数据
            if (dt.Rows.Count > 0 && dt.Rows.Count == 1)
            {
                for (int idx_Tolerance = 0; idx_Tolerance < (dt.Columns.Count - 2) / 2; idx_Tolerance++)//75
                {
                    dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[3].Value = B515_Standard_Tolerance[2 * idx_Tolerance] = Convert.ToDouble(dt.Rows[0][2 * idx_Tolerance + 2]);//上公差
                    dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[4].Value = B515_Standard_Tolerance[2 * idx_Tolerance + 1] = Convert.ToDouble(dt.Rows[0][2 * idx_Tolerance + 3]);//下公差 

                }
            }

        }

        #region string[] DGV_TolInfo
        string[] DGV_TolInfo = { 
                             "PTG_LH Datum X TO ABC",
                             "PTG_LH Datum Y TO ABC",
                             "PTG_LH Datum Z TO ABC",
                                                  
                             "PTG_RH Datum X TO ABC",
                             "PTG_RH Datum Y TO ABC",
                             "PTG_RH Datum Z TO ABC",
                                                    
                             "PTC Datum X TO ABC",
                             "PTC Datum Y TO ABC",
                             "PTC Datum Z TO ABC",
                                                    
                             "PTB Datum X TO ABC",
                             "PTB Datum Y TO ABC",
                             "PTB Datum Z TO ABC",
                                  
                             "PTA2_2 Datum X TO ABC",
                             "PTA2_2 Datum Y TO ABC",
                             "PTA2_2 Datum Z TO ABC",
                                                    
                             "PTA1_2 Datum X TO ABC",
                             "PTA1_2 Datum Y TO ABC",
                             "PTA1_2 Datum Z TO ABC",
                                                    
                             "PTH_RH/G Datum X TO ABC",
                             "PTH_RH/G Datum Y TO ABC",
                             "PTH_RH/G Datum Z TO ABC",
                                                    
                             "PTH_LH/G Datum X TO ABC",
                             "PTH_LH/G Datum Y TO ABC",
                             "PTH_LH/G Datum Z TO ABC",

                             "PTI_LH Datum X TO ABC",
                             "PTI_LH Datum Y TO ABC",
                             "PTI_LH Datum Z TO ABC",
                                                    
                             "PTI_RH Datum X TO ABC",
                             "PTI_RH Datum Y TO ABC",
                             "PTI_RH Datum Z TO ABC",
                                                   
                             "PTF_RH/F Datum X TO ABC",
                             "PTF_RH/F Datum Y TO ABC",
                             "PTF_RH/F Datum Z TO ABC",
                                                    
                             "PTF_LH/F Datum X TO ABC",
                             "PTF_LH/F Datum Y TO ABC",
                             "PTF_LH/F Datum Z TO ABC",

                             "PTJ_LH Datum X TO ABC",
                             "PTJ_LH Datum Y TO ABC",
                             "PTJ_LH Datum Z TO ABC",

                             "PTA2_1 Datum X TO ABC",
                             "PTA2_1 Datum Y TO ABC",
                             "PTA2_1 Datum Z TO ABC",

                             "PTA1_1 Datum X TO ABC",
                             "PTA1_1 Datum Y TO ABC",
                             "PTA1_1 Datum Z TO ABC",
                                                    
                             
                                                   
                             "PTA3/D Datum X TO ABC",
                             "PTA3/D Datum Y TO ABC",
                             "PTA3/D Datum Z TO ABC",
                                                   
                             "PTJ_RH Datum X TO ABC",
                             "PTJ_RH Datum Y TO ABC",
                             "PTJ_RH Datum Z TO ABC",
                                                   
                             "PTA4/D Datum X TO ABC",
                             "PTA4/D Datum Y TO ABC",
                             "PTA4/D Datum Z TO ABC",
                                                    
                             "PTD_LH Datum X TO ABC",
                             "PTD_LH Datum Y TO ABC",
                             "PTD_LH Datum Z TO ABC",
                                                    
                             "PTD_RH Datum X TO ABC",
                             "PTD_RH Datum Y TO ABC",
                             "PTD_RH Datum Z TO ABC",
                                                    
                             "PTE_LH Datum X TO ABC",
                             "PTE_LH Datum Y TO ABC",
                             "PTE_LH Datum Z TO ABC",
                                                    
                             "PTN_LH Datum X TO ABC",
                             "PTN_LH Datum Y TO ABC",
                             "PTN_LH Datum Z TO ABC",
                                                    
                             "PTP_LH Datum X TO ABC",
                             "PTP_LH Datum Y TO ABC",
                             "PTP_LH Datum Z TO ABC",
                                                    
                             "PTP_RH Datum X TO ABC",
                             "PTP_RH Datum Y TO ABC",
                             "PTP_RH Datum Z TO ABC",
                                                    
                             "PTE_RH Datum X TO ABC",
                             "PTE_RH Datum Y TO ABC",
                             "PTE_RH Datum Z TO ABC"
                             };
        #endregion
        private void DGV_ToleranceInit()
        {
            // 构建一个包含textbox的datagridview的行
            //dgvRow.Cells.Add(textboxcell);
            //dgvRow.Cells.Add(textboxcell);
            //dgvRow.Cells.Add(textboxcell);
            //dgvRow.Cells.Add(textboxcell);
            //textboxcell.Value = "0.3";
            //dgvRow.Cells.Add(textboxcell);
            //dgvRow.Cells.Add(comboxcell);
            //dgvRow.Cells.Add(buttoncell);
            //dgv_ToleranceSetting.Rows.Add(dgvRow);
            try
            {
                if (PersistentData.CallHistory_Date == "")
                {
                    dgv_ToleranceSetting.EditMode = DataGridViewEditMode.EditOnEnter;

                    // 获得最近时间的公差带数据
                    // 获得标准公差; 暂时模拟; 拟修改为 在PersisData加载时就调用数据库函数, 读取最近一次保存的标准公差信息,将其赋值给 B515_Standard_Tolerance
                    ZessiTolDatReadFromDB_Buffer zessiTolDatRdFromDBBuffer = new ZessiTolDatReadFromDB_Buffer();
                    zessiTolDatRdFromDBBuffer.CarType = ""; zessiTolDatRdFromDBBuffer.DataTime = "";
                    zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo_str = new string[zessiFileParseOperate.MeasurePoint * 3 * 2];
                    zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo = new double[zessiFileParseOperate.MeasurePoint * 3 * 2];

                    DBOperate_Obj.Query_ToleranceStandard_RecentDat(ref zessiTolDatRdFromDBBuffer); // 获得DB中最新的公差信息
                    //for (int rows_idx = 0; rows_idx < zessiFileParseOperate.MeasurePoint * 3; rows_idx++) // rows的数量会比实际数量多1个
                    //{
                    //    Form_AdvancedFunc.B515_Standard_Tolerance[2 * rows_idx] = zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo[2 * rows_idx]; //Convert.ToDouble(0.3);
                    //    Form_AdvancedFunc.B515_Standard_Tolerance[2 * rows_idx + 1] = zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo[2 * rows_idx + 1];//Convert.ToDouble(-0.3);
                    //}



                    for (int idx_Tolerance = 0; idx_Tolerance < 75; idx_Tolerance++)
                    {
                        int cur_line_idx = dgv_ToleranceSetting.Rows.Add();
                        //dgv_ToleranceSetting.Rows.Add();

                        dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[0].Value = idx_Tolerance + 1;
                        //dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[1].Value = "PTA1-1 Datum Z TO ABC";

                        ////读已经调用的数据
                        //dt = DBOperate_Obj.Query_ToleranceStandard_SingleDay(PersistentData.CallHistory_Date);
                        //// 更新dgv中的数据
                        //if (dt.Rows.Count > 0 && dt.Rows.Count == 1)
                        //{
                        //    for (int idx_Tolerance0 = 0; idx_Tolerance0 < (dt.Columns.Count - 2) / 2; idx_Tolerance0++)//75
                        //    {
                        //        dgv_ToleranceSetting.Rows[idx_Tolerance0].Cells[3].Value = B515_Standard_Tolerance[2 * idx_Tolerance0] = Convert.ToDouble(dt.Rows[0][2 * idx_Tolerance0 + 2]);//上公差
                        //        dgv_ToleranceSetting.Rows[idx_Tolerance0].Cells[4].Value = B515_Standard_Tolerance[2 * idx_Tolerance0 + 1] = Convert.ToDouble(dt.Rows[0][2 * idx_Tolerance0 + 3]);//下公差 

                        //    }
                        //}
                        dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[3].Value = zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo[2 * idx_Tolerance];// Convert.ToDouble(0.3);
                        dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[4].Value = zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo[2 * idx_Tolerance + 1]; //Convert.ToDouble(-0.3);

                    }
                    // 添加公差名字
                    for (int row_idx = 0; row_idx < zessiFileParseOperate.MeasurePoint * 3; row_idx++)
                    {
                        dgv_ToleranceSetting.Rows[row_idx].Cells[1].Value = DGV_TolInfo[row_idx];
                    }
                    //
                    dgv_ToleranceSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                }
                else if (PersistentData.CallHistory_Date != "")
                {
                    dgv_ToleranceSetting.EditMode = DataGridViewEditMode.EditOnEnter;
                    // 序号 0
                    for (int idx_Tolerance = 0; idx_Tolerance < 75; idx_Tolerance++)
                    {
                        int cur_line_idx = dgv_ToleranceSetting.Rows.Add();
                        dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[0].Value = idx_Tolerance + 1;
                    }
                    // 添加公差名字 1
                    for (int row_idx = 0; row_idx < zessiFileParseOperate.MeasurePoint * 3; row_idx++)
                    {
                        dgv_ToleranceSetting.Rows[row_idx].Cells[1].Value = DGV_TolInfo[row_idx];
                    }
                    dgv_ToleranceSetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    
                    //读已经调用的数据
                    dt = DBOperate_Obj.Query_ToleranceStandard_SingleDay(PersistentData.CallHistory_Date);
                    // 更新dgv中的数据
                    if (dt.Rows.Count > 0 && dt.Rows.Count == 1)
                    {
                        for (int idx_Tolerance = 0; idx_Tolerance < (dt.Columns.Count - 2) / 2; idx_Tolerance++)//75
                        {
                            dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[3].Value = B515_Standard_Tolerance[2 * idx_Tolerance] = Convert.ToDouble(dt.Rows[0][2 * idx_Tolerance + 2]);//上公差
                            dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[4].Value = B515_Standard_Tolerance[2 * idx_Tolerance + 1] = Convert.ToDouble(dt.Rows[0][2 * idx_Tolerance + 3]);//下公差 

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("当前调用历史记录时间为:"+ PersistentData.CallHistory_Date +"\n启动调用历史界面异常,异常信息:" + ex.Message);
            }
        }

        // 双击让控件可修改,获取主键和行号
        private void dgv_ToleranceSetting_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgv_ToleranceSetting.ReadOnly = false;
            dgv_ToleranceSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
        }

        private void tbx_BatchTol_Up_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double tTolUp = Convert.ToDouble(tbx_BatchTol_Up.Text);

                    for (int idx_Tolerance = 0; idx_Tolerance < 75; idx_Tolerance++)
                    {
                        dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[3].Value = (tTolUp);//0.3
                        //dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[4].Value = tTolUp;//Convert.ToDouble(-0.3);

                    }
                }
                catch (Exception ex)
                {
                    // 很
                    MessageBox.Show("请检查自定义的标准公差是否为数字");
                }
            }
            else
            { }
        }

        private void tbx_BatchTol_Dn_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double tTolDn = Convert.ToDouble(tbx_BatchTol_Dn.Text);

                    for (int idx_Tolerance = 0; idx_Tolerance < 75; idx_Tolerance++)
                    {
                        //dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[3].Value = (tTolDn);//0.3
                        dgv_ToleranceSetting.Rows[idx_Tolerance].Cells[4].Value = tTolDn;//Convert.ToDouble(-0.3);

                    }
                }
                catch (Exception ex)
                {
                    // 很
                    MessageBox.Show("请检查自定义的标准公差是否为数字");
                }
            }
            else
            { }
        }

        private void btn_InportExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "表格|*.xls";

            string strPath;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    strPath = fd.FileName;
                    string strCon = "provider=microsoft.jet.oledb.4.0;data source=" + strPath + ";extended properties=excel 8.0";
                    OleDbConnection Con = new OleDbConnection(strCon);

                    string strsql = "select * from [Sheet1$]";
                    OleDbCommand Cmd = new OleDbCommand(strsql, Con);

                    OleDbDataAdapter da = new OleDbDataAdapter(Cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "hh");

                    dataGridView1.DataSource = ds.Tables[0];
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        bool m_bInportExcelTabpage_Flag = false;
        private void btn_SaveCurrentTolFromExcel_Click(object sender, EventArgs e)
        {
            m_bInportExcelTabpage_Flag = true;
            btn_SaveCurrentSetting_Click(sender, e);
            m_bInportExcelTabpage_Flag = false;
        }

    }
}

