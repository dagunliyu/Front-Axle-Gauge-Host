using System;
using System.Runtime.InteropServices;
using Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB;
using DataConversion;
using Siemens.Automation.ServiceSupport.Applications.OPC.IOPCConnector;
using Siemens.ATS14.ToolBox.Performance;
using System.Collections;

using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using System.Data.SqlClient;//数据库添加文件
using System.IO.Ports;
using System.IO;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB
{
    class DBOperate
    {
        bool m_bDataBaseOpen = false;
        SqlConnection m_mycon = null;
        public DBOperate()
        {
            try
            {
                // MultipleActiveResultSets可以使数据库连接复用。
                // 新建数据库连接类,但是数据库并没有打开,打开操作放在具体的操作函数中
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "异常");
            }
        }
        DBConnector DB_Connector = new DBConnector("B515_MCA");

     

        public DataTable GetAllDataThroughDate(DateTime startTime, DateTime endTime)
        {
            string starttime_str = startTime.ToString("yyyy-MM-dd HH:mm:ss");
            string endtime_str = endTime.ToString("yyyy-MM-dd HH:mm:ss");
            DataTable dt = null;

            try
            {
                if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                {
                    DB_Connector.CurrentSqlCon.Open();
                }
                string sqlCmd = "select * from Collect_Info where [日期] between " + "'" + starttime_str + "'" + " and " + "'" + endtime_str + "'";
            
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlCmd, DB_Connector.CurrentSqlCon))
                {
                    sda.Fill(dt);
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }

            return dt;
        }

        public DataTable GetAllDataThroughID()
        {
            DataTable dt = null;

            try
            { }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        
        public bool CheckNewTable()
        {

            // lhc170116
            string tableCollect_Info = "Collect_Info";
            CreateTable(tableCollect_Info);

            string ToleranceStandard = "ToleranceStandard";
            CreateTable(ToleranceStandard);

            string tableBoxID_Store = "OpenMachineFlag";
            CreateTable(tableBoxID_Store);
            return true;
        }
        public void CreateTable(string tableName)
        {
            try
            {
                if (isTableExist(tableName) == 0)  // istableexist执行过后会将数据库关闭
                {
                    if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                        DB_Connector.CurrentSqlCon.Open();

                    string sqlCmd = "create table ";
                    if (tableName == "OpenMachineFlag")
                    {
                        sqlCmd = sqlCmd + tableName;
                        sqlCmd = sqlCmd + "([基础码] varchar(24),[日期] varchar(20))";
                        int cnt = -1;//数据库操作标志位
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = DB_Connector.CurrentSqlCon;
                            cmd.CommandText = sqlCmd;
                            cnt = cmd.ExecuteNonQuery();
                        }
                    }
                    else if (tableName == "Collect_Info")
                    {
                        sqlCmd = sqlCmd + tableName;
                        sqlCmd = sqlCmd + " ([车型] char(5), [日期] varchar(20), [序列号] varchar(24), [打标码] varchar(24) Primary Key,";
                        sqlCmd = sqlCmd + "[不合格数] varchar(3), [Item] varchar(6), ";
                        sqlCmd = sqlCmd + "[PTG_LH Datum X TO ABC] varchar(50),  [PTG_LH_X标准上公差]   varchar(10),  [PTG_LH_X标准下公差] varchar(10),  [PTG_LH Datum Y TO ABC] varchar(50),  [PTG_LH_Y标准上公差] varchar(10),  [PTG_LH_Y标准下公差] varchar(10),  [PTG_LH Datum Z TO ABC] varchar(50),  [PTG_LH_Z标准上公差] varchar(10),  [PTG_LH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTG_RH Datum X TO ABC] varchar(50),  [PTG_RH_X标准上公差]   varchar(10),  [PTG_RH_X标准下公差] varchar(10),  [PTG_RH Datum Y TO ABC] varchar(50),  [PTG_RH_Y标准上公差] varchar(10),  [PTG_RH_Y标准下公差] varchar(10),  [PTG_RH Datum Z TO ABC] varchar(50),  [PTG_RH_Z标准上公差] varchar(10),  [PTG_RH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTC Datum X TO ABC] varchar(50),     [PTC_X标准上公差]      varchar(10),     [PTC_X标准下公差] varchar(10),     [PTC Datum Y TO ABC] varchar(50),     [PTC_Y标准上公差] varchar(10),     [PTC_Y标准下公差] varchar(10),     [PTC Datum Z TO ABC] varchar(50),     [PTC_Z标准上公差] varchar(10),     [PTC_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTB Datum X TO ABC] varchar(50),     [PTB_X标准上公差]      varchar(10),     [PTB_X标准下公差] varchar(10),     [PTB Datum Y TO ABC] varchar(50),     [PTB_Y标准上公差] varchar(10),     [PTB_Y标准下公差] varchar(10),     [PTB Datum Z TO ABC] varchar(50),     [PTB_Z标准上公差] varchar(10),     [PTB_Z标准下公差] varchar(10),  ";

                        sqlCmd = sqlCmd + "[PTA2_2 Datum X TO ABC] varchar(50),  [PTA2_2_X标准上公差]   varchar(10),  [PTA2_2_X标准下公差] varchar(10),  [PTA2_2 Datum Y TO ABC] varchar(50),  [PTA2_2_Y标准上公差] varchar(10),  [PTA2_2_Y标准下公差] varchar(10),  [PTA2_2 Datum Z TO ABC] varchar(50),  [PTA2_2_Z标准上公差] varchar(10),  [PTA2_2_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTA1_2 Datum X TO ABC] varchar(50),  [PTA1_2_X标准上公差]   varchar(10),  [PTA1_2_X标准下公差] varchar(10),  [PTA1_2 Datum Y TO ABC] varchar(50),  [PTA1_2_Y标准上公差] varchar(10),  [PTA1_2_Y标准下公差] varchar(10),  [PTA1_2 Datum Z TO ABC] varchar(50),  [PTA1_2_Z标准上公差] varchar(10),  [PTA1_2_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTH_RH/G Datum X TO ABC] varchar(50),[PTH_RH/G_X标准上公差] varchar(10),[PTH_RH/G_X标准下公差] varchar(10),[PTH_RH/G Datum Y TO ABC] varchar(50),[PTH_RH/G_Y标准上公差] varchar(10),[PTH_RH/G_Y标准下公差] varchar(10),[PTH_RH/G Datum Z TO ABC] varchar(50),[PTH_RH/G_Z标准上公差] varchar(10),[PTH_RH/G_Z标准下公差] varchar(10),";
                        sqlCmd = sqlCmd + "[PTH_LH/G Datum X TO ABC] varchar(50),[PTH_LH/G_X标准上公差] varchar(10),[PTH_LH/G_X标准下公差] varchar(10),[PTH_LH/G Datum Y TO ABC] varchar(50),[PTH_LH/G_Y标准上公差] varchar(10),[PTH_LH/G_Y标准下公差] varchar(10),[PTH_LH/G Datum Z TO ABC] varchar(50),[PTH_LH/G_Z标准上公差] varchar(10),[PTH_LH/G_Z标准下公差] varchar(10),";
                        sqlCmd = sqlCmd + "[PTI_LH Datum X TO ABC] varchar(50),  [PTI_LH_X标准上公差]   varchar(10),  [PTI_LH_X标准下公差] varchar(10),  [PTI_LH Datum Y TO ABC] varchar(50),  [PTI_LH_Y标准上公差] varchar(10),  [PTI_LH_Y标准下公差] varchar(10),  [PTI_LH Datum Z TO ABC] varchar(50),  [PTI_LH_Z标准上公差] varchar(10),  [PTI_LH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTI_RH Datum X TO ABC] varchar(50),  [PTI_RH_X标准上公差]   varchar(10),  [PTI_RH_X标准下公差] varchar(10),  [PTI_RH Datum Y TO ABC] varchar(50),  [PTI_RH_Y标准上公差] varchar(10),  [PTI_RH_Y标准下公差] varchar(10),  [PTI_RH Datum Z TO ABC] varchar(50),  [PTI_RH_Z标准上公差] varchar(10),  [PTI_RH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTF_RH/F Datum X TO ABC] varchar(50),[PTF_RH/F_X标准上公差] varchar(10),[PTF_RH/F_X标准下公差] varchar(10),[PTF_RH/F Datum Y TO ABC] varchar(50),[PTF_RH/F_Y标准上公差] varchar(10),[PTF_RH/F_Y标准下公差] varchar(10),[PTF_RH/F Datum Z TO ABC] varchar(50),[PTF_RH/F_Z标准上公差] varchar(10),[PTF_RH/F_Z标准下公差] varchar(10),";
                        sqlCmd = sqlCmd + "[PTF_LH/F Datum X TO ABC] varchar(50),[PTF_LH/F_X标准上公差] varchar(10),[PTF_LH/F_X标准下公差] varchar(10),[PTF_LH/F Datum Y TO ABC] varchar(50),[PTF_LH/F_Y标准上公差] varchar(10),[PTF_LH/F_Y标准下公差] varchar(10),[PTF_LH/F Datum Z TO ABC] varchar(50),[PTF_LH/F_Z标准上公差] varchar(10),[PTF_LH/F_Z标准下公差] varchar(10),";
                        sqlCmd = sqlCmd + "[PTJ_LH Datum X TO ABC] varchar(50),  [PTJ_LH_X标准上公差]   varchar(10),  [PTJ_LH_X标准下公差] varchar(10),  [PTJ_LH Datum Y TO ABC] varchar(50),  [PTJ_LH_Y标准上公差] varchar(10),  [PTJ_LH_Y标准下公差] varchar(10),  [PTJ_LH Datum Z TO ABC] varchar(50),  [PTJ_LH_Z标准上公差] varchar(10),  [PTJ_LH_Z标准下公差] varchar(10), ";

                        sqlCmd = sqlCmd + "[PTA2_1 Datum X TO ABC] varchar(50),  [PTA2_1_X标准上公差]   varchar(10),  [PTA2_1_X标准下公差] varchar(10),  [PTA2_1 Datum Y TO ABC] varchar(50),  [PTA2_1_Y标准上公差] varchar(10),  [PTA2_1_Y标准下公差] varchar(10),  [PTA2_1 Datum Z TO ABC] varchar(50),  [PTA2_1_Z标准上公差] varchar(10),  [PTA2_1_Z标准下公差] varchar(10),  ";
                        
                        sqlCmd = sqlCmd + "[PTA1_1 Datum X TO ABC] varchar(50),  [PTA1_1_X标准上公差]   varchar(10),  [PTA1_1_X标准下公差] varchar(10),  [PTA1_1 Datum Y TO ABC] varchar(50),  [PTA1_1_Y标准上公差] varchar(10),  [PTA1_1_Y标准下公差] varchar(10),  [PTA1_1 Datum Z TO ABC] varchar(50),  [PTA1_1_Z标准上公差] varchar(10),  [PTA1_1_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTA3/D Datum X TO ABC] varchar(50),  [PTA3/D_X标准上公差]   varchar(10),  [PTA3/D_X标准下公差] varchar(10),  [PTA3/D Datum Y TO ABC] varchar(50),  [PTA3/D_Y标准上公差] varchar(10),  [PTA3/D_Y标准下公差] varchar(10),  [PTA3/D Datum Z TO ABC] varchar(50),  [PTA3/D_Z标准上公差] varchar(10),  [PTA3/D_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTJ_RH Datum X TO ABC] varchar(50),  [PTJ_RH_X标准上公差]   varchar(10),  [PTJ_RH_X标准下公差] varchar(10),  [PTJ_RH Datum Y TO ABC] varchar(50),  [PTJ_RH_Y标准上公差] varchar(10),  [PTJ_RH_Y标准下公差] varchar(10),  [PTJ_RH Datum Z TO ABC] varchar(50),  [PTJ_RH_Z标准上公差] varchar(10),  [PTJ_RH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTA4/D Datum X TO ABC] varchar(50),  [PTA4/D_X标准上公差]   varchar(10),  [PTA4/D_X标准下公差] varchar(10),  [PTA4/D Datum Y TO ABC] varchar(50),  [PTA4/D_Y标准上公差] varchar(10),  [PTA4/D_Y标准下公差] varchar(10),  [PTA4/D Datum Z TO ABC] varchar(50),  [PTA4/D_Z标准上公差] varchar(10),  [PTA4/D_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTD_LH Datum X TO ABC] varchar(50),  [PTD_LH_X标准上公差]   varchar(10),  [PTD_LH_X标准下公差] varchar(10),  [PTD_LH Datum Y TO ABC] varchar(50),  [PTD_LH_Y标准上公差] varchar(10),  [PTD_LH_Y标准下公差] varchar(10),  [PTD_LH Datum Z TO ABC] varchar(50),  [PTD_LH_Z标准上公差] varchar(10),  [PTD_LH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTD_RH Datum X TO ABC] varchar(50),  [PTD_RH_X标准上公差]   varchar(10),  [PTD_RH_X标准下公差] varchar(10),  [PTD_RH Datum Y TO ABC] varchar(50),  [PTD_RH_Y标准上公差] varchar(10),  [PTD_RH_Y标准下公差] varchar(10),  [PTD_RH Datum Z TO ABC] varchar(50),  [PTD_RH_Z标准上公差] varchar(10),  [PTD_RH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTE_LH Datum X TO ABC] varchar(50),  [PTE_LH_X标准上公差]   varchar(10),  [PTE_LH_X标准下公差] varchar(10),  [PTE_LH Datum Y TO ABC] varchar(50),  [PTE_LH_Y标准上公差] varchar(10),  [PTE_LH_Y标准下公差] varchar(10),  [PTE_LH Datum Z TO ABC] varchar(50),  [PTE_LH_Z标准上公差] varchar(10),  [PTE_LH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTN_LH Datum X TO ABC] varchar(50),  [PTN_LH_X标准上公差]   varchar(10),  [PTN_LH_X标准下公差] varchar(10),  [PTN_LH Datum Y TO ABC] varchar(50),  [PTN_LH_Y标准上公差] varchar(10),  [PTN_LH_Y标准下公差] varchar(10),  [PTN_LH Datum Z TO ABC] varchar(50),  [PTN_LH_Z标准上公差] varchar(10),  [PTN_LH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTP_LH Datum X TO ABC] varchar(50),  [PTP_LH_X标准上公差]   varchar(10),  [PTP_LH_X标准下公差] varchar(10),  [PTP_LH Datum Y TO ABC] varchar(50),  [PTP_LH_Y标准上公差] varchar(10),  [PTP_LH_Y标准下公差] varchar(10),  [PTP_LH Datum Z TO ABC] varchar(50),  [PTP_LH_Z标准上公差] varchar(10),  [PTP_LH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTP_RH Datum X TO ABC] varchar(50),  [PTP_RH_X标准上公差]   varchar(10),  [PTP_RH_X标准下公差] varchar(10),  [PTP_RH Datum Y TO ABC] varchar(50),  [PTP_RH_Y标准上公差] varchar(10),  [PTP_RH_Y标准下公差] varchar(10),  [PTP_RH Datum Z TO ABC] varchar(50),  [PTP_RH_Z标准上公差] varchar(10),  [PTP_RH_Z标准下公差] varchar(10),  ";
                        sqlCmd = sqlCmd + "[PTE_RH Datum X TO ABC] varchar(50),  [PTE_RH_X标准上公差]   varchar(10),  [PTE_RH_X标准下公差] varchar(10),  [PTE_RH Datum Y TO ABC] varchar(50),  [PTE_RH_Y标准上公差] varchar(10),  [PTE_RH_Y标准下公差] varchar(10),  [PTE_RH Datum Z TO ABC] varchar(50),  [PTE_RH_Z标准上公差] varchar(10),  [PTE_RH_Z标准下公差] varchar(10))  ";
                        
                        int cnt = -1;//数据库操作标志位
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = DB_Connector.CurrentSqlCon;
                            cmd.CommandText = sqlCmd;
                            cnt = cmd.ExecuteNonQuery();
                        }
                    }
                    else if (tableName == "ToleranceStandard")
                    {
                        sqlCmd = sqlCmd + tableName;
                        sqlCmd = sqlCmd + " ([车型] char(5), [日期] varchar(20) Primary Key, "; //表级别约束未指定列列表(Primary Key后没有前没有,)
                        sqlCmd = sqlCmd + "[PTG_LH_X 上公差] varchar(50), [PTG_LH_X 下公差] varchar(50), [PTG_LH_Y 上公差] varchar(50), [PTG_LH_Y 下公差] varchar(50),[PTG_LH_Z 上公差] varchar(50), [PTG_LH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTG_RH_X 上公差] varchar(50), [PTG_RH_X 下公差] varchar(50), [PTG_RH_Y 上公差] varchar(50), [PTG_RH_Y 下公差] varchar(50),[PTG_RH_Z 上公差] varchar(50), [PTG_RH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTC_X 上公差] varchar(50), [PTC_X 下公差] varchar(50), [PTC_Y 上公差] varchar(50), [PTC_Y 下公差] varchar(50), [PTC_Z 上公差] varchar(50), [PTC_Z 下公差] varchar(50),";
                        sqlCmd = sqlCmd + "[PTB_X 上公差] varchar(50), [PTB_X 下公差] varchar(50), [PTB_Y 上公差] varchar(50), [PTB_Y 下公差] varchar(50), [PTB_Z 上公差] varchar(50), [PTB_Z 下公差] varchar(50), ";

                        sqlCmd = sqlCmd + "[PTA2_2_X 上公差] varchar(50), [PTA2_2_X 下公差] varchar(50), [PTA2_2_Y 上公差] varchar(50), [PTA2_2_Y 下公差] varchar(50), [PTA2_2_Z 上公差] varchar(50), [PTA2_2_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTA1_2_X 上公差] varchar(50), [PTA1_2_X 下公差] varchar(50), [PTA1_2_Y 上公差] varchar(50), [PTA1_2_Y 下公差] varchar(50), [PTA1_2_Z 上公差] varchar(50), [PTA1_2_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTH_RH/G_X 上公差] varchar(50), [PTH_RH/G_X 下公差] varchar(50), [PTH_RH/G_Y 上公差] varchar(50), [PTH_RH/G_Y 下公差] varchar(50), [PTH_RH/G_Z 上公差] varchar(50), [PTH_RH/G_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTH_LH/G_X 上公差] varchar(50), [PTH_LH/G_X 下公差] varchar(50), [PTH_LH/G_Y 上公差] varchar(50), [PTH_LH/G_Y 下公差] varchar(50), [PTH_LH/G_Z 上公差] varchar(50), [PTH_LH/G_Z 下公差] varchar(50), ";

                        sqlCmd = sqlCmd + "[PTI_LH_X 上公差] varchar(50), [PTI_LH_X 下公差] varchar(50), [PTI_LH_Y 上公差] varchar(50), [PTI_LH_Y 下公差] varchar(50), [PTI_LH_Z 上公差] varchar(50), [PTI_LH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTI_RH_X 上公差] varchar(50), [PTI_RH_X 下公差] varchar(50), [PTI_RH_Y 上公差] varchar(50), [PTI_RH_Y 下公差] varchar(50), [PTI_RH_Z 上公差] varchar(50), [PTI_RH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTF_RH/F_X 上公差] varchar(50), [PTF_RH/F_X 下公差] varchar(50), [PTF_RH/F_Y 上公差] varchar(50), [PTF_RH/F_Y 下公差] varchar(50), [PTF_RH/F_Z 上公差] varchar(50), [PTF_RH/F_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTF_LH/F_X 上公差] varchar(50), [PTF_LH/F_X 下公差] varchar(50), [PTF_LH/F_Y 上公差] varchar(50), [PTF_LH/F_Y 下公差] varchar(50), [PTF_LH/F_Z 上公差] varchar(50), [PTF_LH/F_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTJ_LH_X 上公差] varchar(50), [PTJ_LH_X 下公差] varchar(50), [PTJ_LH_Y 上公差] varchar(50), [PTJ_LH_Y 下公差] varchar(50), [PTJ_LH_Z 上公差] varchar(50), [PTJ_LH_Z 下公差] varchar(50), ";

                        sqlCmd = sqlCmd + "[PTA2_1_X 上公差] varchar(50), [PTA2_1_X 下公差] varchar(50), [PTA2_1_Y 上公差] varchar(50), [PTA2_1_Y 下公差] varchar(50), [PTA2_1_Z 上公差] varchar(50), [PTA2_1_Z 下公差] varchar(50), ";
                        
                        sqlCmd = sqlCmd + "[PTA1_1_X 上公差] varchar(50), [PTA1_1_X 下公差] varchar(50), [PTA1_1_Y 上公差] varchar(50), [PTA1_1_Y 下公差] varchar(50), [PTA1_1_Z 上公差] varchar(50), [PTA1_1_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTA3/D_X 上公差] varchar(50), [PTA3/D_X 下公差] varchar(50), [PTA3/D_Y 上公差] varchar(50), [PTA3/D_Y 下公差] varchar(50), [PTA3/D_Z 上公差] varchar(50), [PTA3/D_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTJ_RH_X 上公差] varchar(50), [PTJ_RH_X 下公差] varchar(50), [PTJ_RH_Y 上公差] varchar(50), [PTJ_RH_Y 下公差] varchar(50), [PTJ_RH_Z 上公差] varchar(50), [PTJ_RH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTA4/D_X 上公差] varchar(50), [PTA4/D_X 下公差] varchar(50), [PTA4/D_Y 上公差] varchar(50), [PTA4/D_Y 下公差] varchar(50), [PTA4/D_Z 上公差] varchar(50), [PTA4/D_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTD_LH_X 上公差] varchar(50), [PTD_LH_X 下公差] varchar(50), [PTD_LH_Y 上公差] varchar(50), [PTD_LH_Y 下公差] varchar(50), [PTD_LH_Z 上公差] varchar(50), [PTD_LH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTD_RH_X 上公差] varchar(50), [PTD_RH_X 下公差] varchar(50), [PTD_RH_Y 上公差] varchar(50), [PTD_RH_Y 下公差] varchar(50), [PTD_RH_Z 上公差] varchar(50), [PTD_RH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTE_LH_X 上公差] varchar(50), [PTE_LH_X 下公差] varchar(50), [PTE_LH_Y 上公差] varchar(50), [PTE_LH_Y 下公差] varchar(50), [PTE_LH_Z 上公差] varchar(50), [PTE_LH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTN_LH_X 上公差] varchar(50), [PTN_LH_X 下公差] varchar(50), [PTN_LH_Y 上公差] varchar(50), [PTN_LH_Y 下公差] varchar(50), [PTN_LH_Z 上公差] varchar(50), [PTN_LH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTP_LH_X 上公差] varchar(50), [PTP_LH_X 下公差] varchar(50), [PTP_LH_Y 上公差] varchar(50), [PTP_LH_Y 下公差] varchar(50), [PTP_LH_Z 上公差] varchar(50), [PTP_LH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTP_RH_X 上公差] varchar(50), [PTP_RH_X 下公差] varchar(50), [PTP_RH_Y 上公差] varchar(50), [PTP_RH_Y 下公差] varchar(50), [PTP_RH_Z 上公差] varchar(50), [PTP_RH_Z 下公差] varchar(50), ";
                        sqlCmd = sqlCmd + "[PTE_RH_X 上公差] varchar(50), [PTE_RH_X 下公差] varchar(50), [PTE_RH_Y 上公差] varchar(50), [PTE_RH_Y 下公差] varchar(50), [PTE_RH_Z 上公差] varchar(50), [PTE_RH_Z 下公差] varchar(50)) ";
                        
                        
                        
                        int cnt = -1;//数据库操作标志位
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = DB_Connector.CurrentSqlCon;
                            cmd.CommandText = sqlCmd;
                            cnt = cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }
        }

        public int isTableExist(string TableName)
        {
            string proc_Sql = "Proc_IsTableExists";
            SqlParameter[] sqlparams = { new SqlParameter("@tableName", SqlDbType.NVarChar, 50) };
            sqlparams[0].Value = TableName;
            int cnt = -1;

            //测试
            try
            {
                // 此时再开数据库
                if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                    DB_Connector.CurrentSqlCon.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = DB_Connector.CurrentSqlCon;
                    cmd.CommandText = proc_Sql;// 存储过程的名字
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sqlparams[0] != null)
                        cmd.Parameters.Add(sqlparams[0]);

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); //CommandBehavior.CloseConnection
                    // dr关闭的时候连接也一同关闭// new SqlDataReader();
                    while (dr.Read())  // 等待datareader读到数据
                    { 
                        cnt = Convert.ToInt32(dr[0]); 
                    }  // 如果读到数据dr
                    dr.Close();   // 关掉,但是先不清除资源--
                }
                if (cnt == 0)
                    return 0;
                else if (cnt == 1)
                    return 1;
                else
                    return -1;

            }
            catch (Exception)
            {
                return -1;
                throw (new Exception("数据库操作:判断表是否存在异常"));
                //MessageBox.Show(ex.Message + "\n判断表是否存在异常");
            }
            //finally
            //{
            //    m_Sqlconn.Close();
            //}
        }

        #region 对表;ToleranceStandard操作
        // 查询ToleranceStanndard表中最近的数据(按时间排序)
        public int Query_ToleranceStandard_RecentDat(ref ZessiTolDatReadFromDB_Buffer zessiTolDatRdFromDBBuffer)
        {
            int cnt = -1;
            try
            {
                // 先初始化
                zessiTolDatRdFromDBBuffer.CarType = "";  zessiTolDatRdFromDBBuffer.DataTime = "";
                zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo_str = new string[zessiFileParseOperate.MeasurePoint * 3*2];
                zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo = new double[zessiFileParseOperate.MeasurePoint * 3*2];
                
                string TableName = "ToleranceStandard";
                //string ProcCosma_QueryAllDatByDate = "ProcCosma_QueryAllDatByDate";// 需要三个参数
                
                // 判断打开
                if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                    DB_Connector.CurrentSqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    string sqlCmd_str = "select top 1* from " + TableName + " order by [日期] desc";
                
                    sqlCmd.Connection = DB_Connector.CurrentSqlCon;
                    sqlCmd.CommandType = CommandType.Text;//CommandType.StoredProcedure;
                    sqlCmd.CommandText = sqlCmd_str;      //ProcCosma_QueryAllDatByDate;// 存储过程名字

                    //cnt = sqlCmd.ExecuteNonQuery();  //  查出来的是大量数据
                    // 需要填充到datatable中
                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlCmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        int nElem = dt.Rows.Count;
                        int nElemCol = dt.Columns.Count;
                        if (nElem >= 1 && nElemCol > 0)  // <= 0 改成
                        {
                            zessiTolDatRdFromDBBuffer.CarType = dt.Rows[0][0].ToString();
                            zessiTolDatRdFromDBBuffer.DataTime = dt.Rows[0][1].ToString();
                            for(int idx = 0; idx<nElemCol-2;idx++)
                            {
                            zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo_str[idx] = dt.Rows[0][idx+2].ToString();
                            zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo[idx] =  Convert.ToDouble(zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo_str[idx]);
                            }
                        }
                        dt.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                DB_Connector.CurrentSqlCon.Close();
                throw (new Exception("查询公差表中今日数据异常"));
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }

            return cnt;
        }


        public DataTable Query_ToleranceStandard_AllDat()
        {
            //DataTable dt = new DataTable();
            try
            {
                string TableName = "ToleranceStandard";
                // 判断打开
                if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                    DB_Connector.CurrentSqlCon.Open();

                string sqlCmd_str = "select * from " + TableName + " order by [日期] desc";

                dt = DB_Connector.GetDataTable(sqlCmd_str, TableName);
                
            }
            catch (Exception)
            {
                DB_Connector.CurrentSqlCon.Close();
                throw (new Exception("查询公差表中所有数据异常"));
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }
            return dt;
        }

        public DataTable Query_ToleranceStandard_SingleDay(string QueryDateDat)
        {
            try
            {
                string TableName = "ToleranceStandard";
                // 判断打开
                if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                    DB_Connector.CurrentSqlCon.Open();

                string sqlCmd_str = "select * from " + TableName + " where [日期] = '" + QueryDateDat + "'";

                dt = DB_Connector.GetDataTable(sqlCmd_str, TableName);

            }
            catch (Exception)
            {
                DB_Connector.CurrentSqlCon.Close();
                throw (new Exception("查询公差表中所有数据异常"));
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }
            return dt;
        }

        #region string[] sqlParams_TolDat
        string[] sqlParams_TolDat = { 
                                    "@PTG_LH_X_UpTol", 
                                    "@PTG_LH_X_DnTol", 
                                    "@PTG_LH_Y_UpTol", 
                                    "@PTG_LH_Y_DnTol", 
                                    "@PTG_LH_Z_UpTol", 
                                    "@PTG_LH_Z_DnTol", 
                                                  
                                    "@PTG_RH_X_UpTol", 
                                    "@PTG_RH_X_DnTol", 
                                    "@PTG_RH_Y_UpTol", 
                                    "@PTG_RH_Y_DnTol", 
                                    "@PTG_RH_Z_UpTol", 
                                    "@PTG_RH_Z_DnTol", 
                                                   
                                    "@PTC_X_UpTol",
                                    "@PTC_X_DnTol",
                                    "@PTC_Y_UpTol",
                                    "@PTC_Y_DnTol",
                                    "@PTC_Z_UpTol",
                                    "@PTC_Z_DnTol",
                                                  
                                    "@PTB_X_UpTol",
                                    "@PTB_X_DnTol",
                                    "@PTB_Y_UpTol",
                                    "@PTB_Y_DnTol",
                                    "@PTB_Z_UpTol",
                                    "@PTB_Z_DnTol",
                                        
                                    "@PTA2_2_X_UpTol", 
                                    "@PTA2_2_X_DnTol", 
                                    "@PTA2_2_Y_UpTol", 
                                    "@PTA2_2_Y_DnTol", 
                                    "@PTA2_2_Z_UpTol", 
                                    "@PTA2_2_Z_DnTol", 
                                    
                                    "@PTA1_2_X_UpTol", 
                                    "@PTA1_2_X_DnTol", 
                                    "@PTA1_2_Y_UpTol", 
                                    "@PTA1_2_Y_DnTol", 
                                    "@PTA1_2_Z_UpTol", 
                                    "@PTA1_2_Z_DnTol", 
                                    
                                    "@PTH_RHG_X_UpTol",
                                    "@PTH_RHG_X_DnTol",
                                    "@PTH_RHG_Y_UpTol",
                                    "@PTH_RHG_Y_DnTol",
                                    "@PTH_RHG_Z_UpTol",
                                    "@PTH_RHG_Z_DnTol",
                                    
                                    "@PTH_LHG_X_UpTol",
                                    "@PTH_LHG_X_DnTol",
                                    "@PTH_LHG_Y_UpTol",
                                    "@PTH_LHG_Y_DnTol",
                                    "@PTH_LHG_Z_UpTol",
                                    "@PTH_LHG_Z_DnTol",
                                    
                                    "@PTI_LH_X_UpTol",
                                    "@PTI_LH_X_DnTol",
                                    "@PTI_LH_Y_UpTol",
                                    "@PTI_LH_Y_DnTol",
                                    "@PTI_LH_Z_UpTol",
                                    "@PTI_LH_Z_DnTol",
                                    
                                    "@PTI_RH_X_UpTol",
                                    "@PTI_RH_X_DnTol",
                                    "@PTI_RH_Y_UpTol",
                                    "@PTI_RH_Y_DnTol",
                                    "@PTI_RH_Z_UpTol",
                                    "@PTI_RH_Z_DnTol",
                                    
                                    "@PTF_RHF_X_UpTol",
                                    "@PTF_RHF_X_DnTol",
                                    "@PTF_RHF_Y_UpTol",
                                    "@PTF_RHF_Y_DnTol",
                                    "@PTF_RHF_Z_UpTol",
                                    "@PTF_RHF_Z_DnTol",

                                    "@PTF_LHF_X_UpTol",
                                    "@PTF_LHF_X_DnTol",
                                    "@PTF_LHF_Y_UpTol",
                                    "@PTF_LHF_Y_DnTol",
                                    "@PTF_LHF_Z_UpTol",
                                    "@PTF_LHF_Z_DnTol",
                                    
                                    "@PTJ_LH_X_UpTol",
                                    "@PTJ_LH_X_DnTol",
                                    "@PTJ_LH_Y_UpTol",
                                    "@PTJ_LH_Y_DnTol",
                                    "@PTJ_LH_Z_UpTol",
                                    "@PTJ_LH_Z_DnTol",

                                    "@PTA2_1_X_UpTol", 
                                    "@PTA2_1_X_DnTol", 
                                    "@PTA2_1_Y_UpTol", 
                                    "@PTA2_1_Y_DnTol", 
                                    "@PTA2_1_Z_UpTol", 
                                    "@PTA2_1_Z_DnTol", 

                                    "@PTA1_1_X_UpTol", 
                                    "@PTA1_1_X_DnTol", 
                                    "@PTA1_1_Y_UpTol", 
                                    "@PTA1_1_Y_DnTol", 
                                    "@PTA1_1_Z_UpTol", 
                                    "@PTA1_1_Z_DnTol", 
                                                   
                                    "@PTA3D_X_UpTol",
                                    "@PTA3D_X_DnTol",
                                    "@PTA3D_Y_UpTol",
                                    "@PTA3D_Y_DnTol",
                                    "@PTA3D_Z_UpTol",
                                    "@PTA3D_Z_DnTol",
                                                  
                                    "@PTJ_RH_X_UpTol", 
                                    "@PTJ_RH_X_DnTol", 
                                    "@PTJ_RH_Y_UpTol", 
                                    "@PTJ_RH_Y_DnTol", 
                                    "@PTJ_RH_Z_UpTol", 
                                    "@PTJ_RH_Z_DnTol", 
                                                   
                                    "@PTA4D_X_UpTol",
                                    "@PTA4D_X_DnTol",
                                    "@PTA4D_Y_UpTol",
                                    "@PTA4D_Y_DnTol",
                                    "@PTA4D_Z_UpTol",
                                    "@PTA4D_Z_DnTol",
                                                   
                                    "@PTD_LH_X_UpTol", 
                                    "@PTD_LH_X_DnTol", 
                                    "@PTD_LH_Y_UpTol", 
                                    "@PTD_LH_Y_DnTol", 
                                    "@PTD_LH_Z_UpTol", 
                                    "@PTD_LH_Z_DnTol", 
                                                   
                                    "@PTD_RH_X_UpTol", 
                                    "@PTD_RH_X_DnTol", 
                                    "@PTD_RH_Y_UpTol", 
                                    "@PTD_RH_Y_DnTol", 
                                    "@PTD_RH_Z_UpTol", 
                                    "@PTD_RH_Z_DnTol", 
                                                   
                                    "@PTE_LH_X_UpTol", 
                                    "@PTE_LH_X_DnTol", 
                                    "@PTE_LH_Y_UpTol", 
                                    "@PTE_LH_Y_DnTol", 
                                    "@PTE_LH_Z_UpTol", 
                                    "@PTE_LH_Z_DnTol", 
                                    
                                    "@PTN_LH_X_UpTol", 
                                    "@PTN_LH_X_DnTol", 
                                    "@PTN_LH_Y_UpTol", 
                                    "@PTN_LH_Y_DnTol", 
                                    "@PTN_LH_Z_UpTol", 
                                    "@PTN_LH_Z_DnTol", 
                                    
                                    "@PTP_LH_X_UpTol", 
                                    "@PTP_LH_X_DnTol", 
                                    "@PTP_LH_Y_UpTol", 
                                    "@PTP_LH_Y_DnTol", 
                                    "@PTP_LH_Z_UpTol", 
                                    "@PTP_LH_Z_DnTol", 
                                    
                                    "@PTP_RH_X_UpTol", 
                                    "@PTP_RH_X_DnTol", 
                                    "@PTP_RH_Y_UpTol", 
                                    "@PTP_RH_Y_DnTol", 
                                    "@PTP_RH_Z_UpTol", 
                                    "@PTP_RH_Z_DnTol", 
                                    
                                    "@PTE_RH_X_UpTol", 
                                    "@PTE_RH_X_DnTol", 
                                    "@PTE_RH_Y_UpTol", 
                                    "@PTE_RH_Y_DnTol", 
                                    "@PTE_RH_Z_UpTol", 
                                    "@PTE_RH_Z_DnTol" 
                                    };
#endregion

        private SqlParameter[] initToleranceParams(string CarType, DateTime today, double[] ToleranceStandardDat)
        {
            ToleranceS_Paras = new SqlParameter[zessiFileParseOperate.MeasurePoint * 6 + 2];
            // 构造传入参数
            ToleranceS_Paras[0] = new SqlParameter("@CarType", SqlDbType.Char, 5);
            ToleranceS_Paras[0].Value = CarType;
            ToleranceS_Paras[1] = new SqlParameter("@DataTime", SqlDbType.Char, 50);
            ToleranceS_Paras[1].Value = today.ToString("yyyy-MM-dd HH:mm:ss");

            
            for (int tol_idx = 0; tol_idx < ToleranceStandardDat.Length; tol_idx++)
            {
                ToleranceS_Paras[tol_idx + 2] = new SqlParameter(sqlParams_TolDat[tol_idx], SqlDbType.VarChar, 50);
                ToleranceS_Paras[tol_idx + 2].Value = ToleranceStandardDat[tol_idx].ToString();
            }

            return ToleranceS_Paras;
        }

        // 插入ToleranceStanndard表 新数据
        SqlParameter[] ToleranceS_Paras = null;
        public int Insert_ToleranceStandard_ModifiedDat(string CarType, DateTime today, double[] ToleranceStandardDat) // 要检查输入数据是否为数字,进行判断,增加出错提示
        {
            int cnt = -1;
            try
            {
                ToleranceS_Paras = new SqlParameter[zessiFileParseOperate.MeasurePoint * 6 + 2];
                //string TableName = "ToleranceStandard";
                string ProcCosma = "ProcCosma_InsertDatToToleranceStandard"; // 25*6 + 2 = 152个参数

                ToleranceS_Paras = initToleranceParams(CarType, today, ToleranceStandardDat);
                // 调用DBConnector的基础函数
                cnt = DB_Connector.ExecDataByStoredProc(ProcCosma, ToleranceS_Paras);
            }
            catch (Exception)
            {
                DB_Connector.CurrentSqlCon.Close();
                throw (new Exception("插入公差表数据过程异常"));
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }

            return cnt;
        }
        #endregion




        #region 对表;CollectInfo操作
        // 查询ToleranceStanndard表中数据(根据日期和打标码)
        SqlParameter[] QueryCollectDataParams = null;
        DataTable dt = null;
        public DataTable Query_Collect_Dat(string QueryWay, string MarkCode, DateTime starttime, DateTime endtime, ref ZessiMeasureDatReadFromDB_Buffer zessiMeasDatRDFromDB_Buffer)
        {
            dt = null;
            try
            {
                // 先初始化
                zessiMeasDatRDFromDB_Buffer.zessiCarType = ""; 
                zessiMeasDatRDFromDB_Buffer.AllMeasurePoint_ReadFromDB = new double[zessiFileParseOperate.MeasurePoint * 3 * 2];
                
                string TableName = "Collect_Info";
                string ProcCosma1 = "ProcCosma_QueryAllDatByDate";// 需要三个参数
                string ProcCosma2 = "ProcCosma_QueryDatByCode";
                // 判断打开
                if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                    DB_Connector.CurrentSqlCon.Open();

                switch(QueryWay)
                {
                    case "打标码":
                        // 构建sqlparams参数
                        QueryCollectDataParams = new SqlParameter[1];
                        QueryCollectDataParams[0] = new SqlParameter("@Code", SqlDbType.VarChar, 24);
                        QueryCollectDataParams[0].Value = MarkCode;
                        dt = DB_Connector.GetDataTable(ProcCosma2, QueryCollectDataParams);
                        break;

                    case "日期":

                        // 构建sqlparams参数
                        string starttime_str = starttime.ToString("yyyy-MM-dd HH:mm:ss");
                        string endtime_str = endtime.ToString("yyyy-MM-dd HH:mm:ss");
                        QueryCollectDataParams = new SqlParameter[3];
                        QueryCollectDataParams[0] = new SqlParameter("@tableName", SqlDbType.VarChar, 50);
                        QueryCollectDataParams[0].Value = TableName;
                        QueryCollectDataParams[1] = new SqlParameter("@startTime", SqlDbType.VarChar, 20);
                        QueryCollectDataParams[1].Value = starttime_str;
                        QueryCollectDataParams[2] = new SqlParameter("@endTime", SqlDbType.VarChar, 20);
                        QueryCollectDataParams[2].Value = endtime_str;
                        dt = DB_Connector.GetDataTable(ProcCosma1, QueryCollectDataParams);
                        break;
                    
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                DB_Connector.CurrentSqlCon.Close();
                throw (new Exception("查询公差表中今日数据异常"));
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }
            return dt;
        }

        #region string[] sqlParams_CollectInfoDat
        string[] sqlParams_CollectInfoDat = { 
                                    "@PTG_LH_X",     "@PTG_LH_X_Up", "@PTG_LH_X_Dn",
                                    "@PTG_LH_Y",     "@PTG_LH_Y_Up", "@PTG_LH_Y_Dn",
                                    "@PTG_LH_Z",     "@PTG_LH_Z_Up", "@PTG_LH_Z_Dn",
                                    "@PTG_RH_X",     "@PTG_RH_X_Up", "@PTG_RH_X_Dn",
                                    "@PTG_RH_Y",     "@PTG_RH_Y_Up", "@PTG_RH_Y_Dn",
                                    "@PTG_RH_Z",     "@PTG_RH_Z_Up", "@PTG_RH_Z_Dn",
                                    "@PTC_X",        "@PTC_X_Up",    "@PTC_X_Dn",
                                    "@PTC_Y",        "@PTC_Y_Up",    "@PTC_Y_Dn",
                                    "@PTC_Z",        "@PTC_Z_Up",    "@PTC_Z_Dn",
                                    "@PTB_X",        "@PTB_X_Up",    "@PTB_X_Dn",
                                    "@PTB_Y",        "@PTB_Y_Up",    "@PTB_Y_Dn",
                                    "@PTB_Z",        "@PTB_Z_Up",    "@PTB_Z_Dn",
                                    
                                    "@PTA2_2_X",     "@PTA2_2_X_Up", "@PTA2_2_X_Dn",
                                    "@PTA2_2_Y",     "@PTA2_2_Y_Up", "@PTA2_2_Y_Dn",
                                    "@PTA2_2_Z",     "@PTA2_2_Z_Up", "@PTA2_2_Z_Dn",
                                    "@PTA1_2_X",     "@PTA1_2_X_Up", "@PTA1_2_X_Dn",
                                    "@PTA1_2_Y",     "@PTA1_2_Y_Up", "@PTA1_2_Y_Dn",
                                    "@PTA1_2_Z",     "@PTA1_2_Z_Up", "@PTA1_2_Z_Dn",
                                    "@PTH_RHG_X",    "@PTH_RHG_X_Up","@PTH_RHG_X_Dn",
                                    "@PTH_RHG_Y",    "@PTH_RHG_Y_Up","@PTH_RHG_Y_Dn",
                                    "@PTH_RHG_Z",    "@PTH_RHG_Z_Up","@PTH_RHG_Z_Dn",
                                    "@PTH_LHG_X",    "@PTH_LHG_X_Up","@PTH_LHG_X_Dn",
                                    "@PTH_LHG_Y",    "@PTH_LHG_Y_Up","@PTH_LHG_Y_Dn",
                                    "@PTH_LHG_Z",    "@PTH_LHG_Z_Up","@PTH_LHG_Z_Dn",
                                    "@PTI_LH_X",     "@PTI_LH_X_Up", "@PTI_LH_X_Dn",
                                    "@PTI_LH_Y",     "@PTI_LH_Y_Up", "@PTI_LH_Y_Dn",
                                    "@PTI_LH_Z",     "@PTI_LH_Z_Up", "@PTI_LH_Z_Dn",
                                    "@PTI_RH_X",     "@PTI_RH_X_Up", "@PTI_RH_X_Dn",
                                    "@PTI_RH_Y",     "@PTI_RH_Y_Up", "@PTI_RH_Y_Dn",
                                    "@PTI_RH_Z",     "@PTI_RH_Z_Up", "@PTI_RH_Z_Dn",
                                    "@PTF_RHF_X",    "@PTF_RHF_X_Up","@PTF_RHF_X_Dn",
                                    "@PTF_RHF_Y",    "@PTF_RHF_Y_Up","@PTF_RHF_Y_Dn",
                                    "@PTF_RHF_Z",    "@PTF_RHF_Z_Up","@PTF_RHF_Z_Dn",
                                    "@PTF_LHF_X",    "@PTF_LHF_X_Up","@PTF_LHF_X_Dn",
                                    "@PTF_LHF_Y",    "@PTF_LHF_Y_Up","@PTF_LHF_Y_Dn",
                                    "@PTF_LHF_Z",    "@PTF_LHF_Z_Up","@PTF_LHF_Z_Dn",
                                    "@PTJ_LH_X",     "@PTJ_LH_X_Up", "@PTJ_LH_X_Dn",
                                    "@PTJ_LH_Y",     "@PTJ_LH_Y_Up", "@PTJ_LH_Y_Dn",
                                    "@PTJ_LH_Z",     "@PTJ_LH_Z_Up", "@PTJ_LH_Z_Dn",
                                    
                                    "@PTA2_1_X",     "@PTA2_1_X_Up", "@PTA2_1_X_Dn",
                                    "@PTA2_1_Y",     "@PTA2_1_Y_Up", "@PTA2_1_Y_Dn",
                                    "@PTA2_1_Z",     "@PTA2_1_Z_Up", "@PTA2_1_Z_Dn",
                                    
                                    "@PTA1_1_X",     "@PTA1_1_X_Up", "@PTA1_1_X_Dn",
                                    "@PTA1_1_Y",     "@PTA1_1_Y_Up", "@PTA1_1_Y_Dn",
                                    "@PTA1_1_Z",     "@PTA1_1_Z_Up", "@PTA1_1_Z_Dn",
                                    "@PTA3D_X",      "@PTA3D_X_Up",  "@PTA3D_X_Dn",
                                    "@PTA3D_Y",      "@PTA3D_Y_Up",  "@PTA3D_Y_Dn",
                                    "@PTA3D_Z",      "@PTA3D_Z_Up",  "@PTA3D_Z_Dn",
                                    "@PTJ_RH_X",     "@PTJ_RH_X_Up", "@PTJ_RH_X_Dn",
                                    "@PTJ_RH_Y",     "@PTJ_RH_Y_Up", "@PTJ_RH_Y_Dn",
                                    "@PTJ_RH_Z",     "@PTJ_RH_Z_Up", "@PTJ_RH_Z_Dn",
                                    "@PTA4D_X",      "@PTA4D_X_Up",  "@PTA4D_X_Dn",
                                    "@PTA4D_Y",      "@PTA4D_Y_Up",  "@PTA4D_Y_Dn",
                                    "@PTA4D_Z",      "@PTA4D_Z_Up",  "@PTA4D_Z_Dn",
                                    "@PTD_LH_X",     "@PTD_LH_X_Up", "@PTD_LH_X_Dn",
                                    "@PTD_LH_Y",     "@PTD_LH_Y_Up", "@PTD_LH_Y_Dn",
                                    "@PTD_LH_Z",     "@PTD_LH_Z_Up", "@PTD_LH_Z_Dn",
                                    "@PTD_RH_X",     "@PTD_RH_X_Up", "@PTD_RH_X_Dn",
                                    "@PTD_RH_Y",     "@PTD_RH_Y_Up", "@PTD_RH_Y_Dn",
                                    "@PTD_RH_Z",     "@PTD_RH_Z_Up", "@PTD_RH_Z_Dn",
                                    "@PTE_LH_X",     "@PTE_LH_X_Up", "@PTE_LH_X_Dn",
                                    "@PTE_LH_Y",     "@PTE_LH_Y_Up", "@PTE_LH_Y_Dn",
                                    "@PTE_LH_Z",     "@PTE_LH_Z_Up", "@PTE_LH_Z_Dn",
                                    "@PTN_LH_X",     "@PTN_LH_X_Up", "@PTN_LH_X_Dn",
                                    "@PTN_LH_Y",     "@PTN_LH_Y_Up", "@PTN_LH_Y_Dn",
                                    "@PTN_LH_Z",     "@PTN_LH_Z_Up", "@PTN_LH_Z_Dn",
                                    "@PTP_LH_X",     "@PTP_LH_X_Up", "@PTP_LH_X_Dn",
                                    "@PTP_LH_Y",     "@PTP_LH_Y_Up", "@PTP_LH_Y_Dn",
                                    "@PTP_LH_Z",     "@PTP_LH_Z_Up", "@PTP_LH_Z_Dn",
                                    "@PTP_RH_X",     "@PTP_RH_X_Up", "@PTP_RH_X_Dn",
                                    "@PTP_RH_Y",     "@PTP_RH_Y_Up", "@PTP_RH_Y_Dn",
                                    "@PTP_RH_Z",     "@PTP_RH_Z_Up", "@PTP_RH_Z_Dn",
                                    "@PTE_RH_X",     "@PTE_RH_X_Up", "@PTE_RH_X_Dn",
                                    "@PTE_RH_Y",     "@PTE_RH_Y_Up", "@PTE_RH_Y_Dn",
                                    "@PTE_RH_Z",     "@PTE_RH_Z_Up", "@PTE_RH_Z_Dn"
                                    };
        #endregion

        // 插入Collect表 新数据
        SqlParameter[] CollectInfo_Paras = null;
        private SqlParameter[] initCollectInfoParams(ZessiDatFinalAdd2DB zessiDatSave2DB)
        {
            CollectInfo_Paras = new SqlParameter[zessiFileParseOperate.MeasurePoint * 3 * 3+ 6];//
            // 构造传入6个前头参数
            CollectInfo_Paras[0] = new SqlParameter("@CarType", SqlDbType.Char, 5);
            CollectInfo_Paras[0].Value = zessiDatSave2DB.zessiCarType;
            CollectInfo_Paras[1] = new SqlParameter("@DataTime", SqlDbType.VarChar, 20);
            CollectInfo_Paras[1].Value = zessiDatSave2DB.datetime.ToString("yyyy-MM-dd HH:mm:ss");
            CollectInfo_Paras[2] = new SqlParameter("@SerialNumber", SqlDbType.VarChar, 24);
            CollectInfo_Paras[2].Value = zessiDatSave2DB.zessiSerialNumber_SaveToDB;
            CollectInfo_Paras[3] = new SqlParameter("@Code", SqlDbType.VarChar, 24);
            CollectInfo_Paras[3].Value = zessiDatSave2DB.zessiMarkCode_SaveToDB;
            CollectInfo_Paras[4] = new SqlParameter("@NGnumber", SqlDbType.VarChar, 3);
            CollectInfo_Paras[4].Value = zessiDatSave2DB.NGnumber.ToString();
            CollectInfo_Paras[5] = new SqlParameter("@Item", SqlDbType.VarChar, 6);
            CollectInfo_Paras[5].Value = zessiDatSave2DB.Item.ToString();
            //75个值 
            for (int tol_idx = 0; tol_idx < zessiDatSave2DB.AllMeasurePoint_SaveToDB.Length; tol_idx++) // 
            {
                //if (tol_idx > 2)
                //{ continue; }

                CollectInfo_Paras[(3 * tol_idx) + 6] = new SqlParameter(sqlParams_CollectInfoDat[3 * tol_idx], SqlDbType.VarChar, 50);
                CollectInfo_Paras[(3 * tol_idx) + 6].Value = zessiDatSave2DB.AllMeasurePoint_SaveToDB[tol_idx].ToString();

                CollectInfo_Paras[(3 * tol_idx + 1) + 6] = new SqlParameter(sqlParams_CollectInfoDat[3 * tol_idx+1], SqlDbType.VarChar, 10);
                CollectInfo_Paras[(3 * tol_idx + 1) + 6].Value = zessiDatSave2DB.AllMeasurePoint_Tol_SaveToDB[2 * tol_idx].ToString(); //上公差

                CollectInfo_Paras[(3 * tol_idx + 2) + 6] = new SqlParameter(sqlParams_CollectInfoDat[3 * tol_idx+2], SqlDbType.VarChar, 10);
                CollectInfo_Paras[(3 * tol_idx + 2) + 6].Value = zessiDatSave2DB.AllMeasurePoint_Tol_SaveToDB[2 * tol_idx + 1].ToString();// 下公差

            }
            //75个值*3 + 6 = 225+6个值
            return CollectInfo_Paras;
        }
        public int Insert_Collect_Dat(ZessiDatFinalAdd2DB zessiDatSave2DB)
        {
            int cnt = -1;
            try
            {
                CollectInfo_Paras = new SqlParameter[zessiFileParseOperate.MeasurePoint * 3 * 3 + 6];//
                //string TableName = "CollectInfo";
                string ProcCosma = "ProcCosma_InsertDatToCollectInfo"; // 25*3*3+6 = 231个参数

                CollectInfo_Paras = initCollectInfoParams(zessiDatSave2DB);
                // 调用DBConnector的基础函数
                cnt = DB_Connector.ExecDataByStoredProc(ProcCosma, CollectInfo_Paras);
            }
            catch (Exception)
            {
                DB_Connector.CurrentSqlCon.Close();
                throw (new Exception("插入汇总表数据过程异常"));
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }

            return cnt;
        }
        #endregion

        #region 对表;OpenMachineFlag操作
        // 查询ToleranceStanndard表中数据(根据日期和打标码)
        
        public int Query_OpenMachineFlag_Dat(DateTime today)
        {
            //int cnt = -1;
            int nElem_Rows = -1; 
            try
            {
                string TableName = "OpenMachineFlag";
                string ProcCosma_QueryAllDatByDate = "ProcCosma_QueryAllDatByDate";// 需要三个参数

                // 判断打开
                if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                    DB_Connector.CurrentSqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = DB_Connector.CurrentSqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = ProcCosma_QueryAllDatByDate;// 存储过程名字

                    // 传入参数--需要和存储过程内容一致(前一天-后一天)
                    sqlCmd.Parameters.Add("@tableName", SqlDbType.VarChar, 50).Value = TableName;
                    sqlCmd.Parameters.Add("@startTime", SqlDbType.VarChar, 20).Value = today.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                    sqlCmd.Parameters.Add("@endTime", SqlDbType.VarChar, 20).Value = today.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");

                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlCmd))
                    {
                        DataTable dt = null;
                        dt = new DataTable();
                        sda.Fill(dt);
                        nElem_Rows = dt.Rows.Count;
                        dt.Dispose();
                    }
                    //cnt = sqlCmd.ExecuteNonQuery();  //  查出来的应该是1行就对了(或者大于0)
                    
                }
            }
            catch (Exception)
            {
                DB_Connector.CurrentSqlCon.Close();return -1;
                throw (new Exception("查询公差表中今日数据异常"));
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }
            return nElem_Rows;
        }
        // 插入ToleranceStanndard表 新数据
        public int Insert_OpenMachineFlag_Dat(string BaseCode, DateTime CurrentTime)
        {
            int cnt = -1;
            try
            {
                //string TableName = "OpenMachineFlag";
                string ProcCosma_InsertDatToOpenMachineFlag = "ProcCosma_InsertDatToOpenMachineFlag";// 需要三个参数

                // 判断打开
                if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                    DB_Connector.CurrentSqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = DB_Connector.CurrentSqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = ProcCosma_InsertDatToOpenMachineFlag;// 存储过程名字

                    // 传入参数--需要和存储过程内容一致(前一天-后一天)
                    sqlCmd.Parameters.Add("@BaseCode", SqlDbType.VarChar, 24).Value = BaseCode;
                    sqlCmd.Parameters.Add("@DataTime", SqlDbType.VarChar, 20).Value = CurrentTime.ToString("yyyy-MM-dd HH:mm:ss");
                    
                    cnt = sqlCmd.ExecuteNonQuery();  //  受影响行数理应为1
                }
            }
            catch (Exception)
            {
                DB_Connector.CurrentSqlCon.Close(); return -1;
                throw (new Exception("插入开机核实表异常"));
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }

            return cnt;
        }
        #endregion

        #region 对表LogIn操作
        public string[] Query_LogIn_Dat(bool ClearSignal)
        {
            //int cnt = -1;
            string[] Uname_Pwd = {"",""};
            int nElem_Rows = -1;
            try
            {
                string TableName = "LogIn";
                string ProcCosma_QueryAllDatByDate = "ProcCosma_QueryAllDatByDate";// 需要三个参数

                // 判断打开
                if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                    DB_Connector.CurrentSqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = DB_Connector.CurrentSqlCon;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "select * from LogIn";// 存储过程名字
                    // "select top 1* from LogIn"
                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlCmd))
                    {
                        DataTable dt = null;
                        dt = new DataTable();
                        sda.Fill(dt);
                        nElem_Rows = dt.Rows.Count;
                        if (ClearSignal)
                        {
                            Uname_Pwd[0] = dt.Rows[1][0].ToString();
                            Uname_Pwd[1] = dt.Rows[1][1].ToString();
                        }
                        else
                        {
                            Uname_Pwd[0] = dt.Rows[0][0].ToString();
                            Uname_Pwd[1] = dt.Rows[0][1].ToString();
                        }
                        dt.Dispose();
                    }
                    //cnt = sqlCmd.ExecuteNonQuery();  //  查出来的应该是1行就对了(或者大于0)

                }
            }
            catch (Exception)
            {
                DB_Connector.CurrentSqlCon.Close(); 
                throw (new Exception("查询用户名-密码表数据异常"));
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }
            return Uname_Pwd;
        }
        // 插入ToleranceStanndard表 新数据
        public int Update_LogIn_Dat(string username, string password, int idx)
        {
            int cnt = -1;
            try
            {
                //string TableName = "OpenMachineFlag";
                string ProcCosma = "ProcCosma_UpdateLogIn";// 需要三个参数

                // 判断打开
                if (DB_Connector.CurrentSqlCon.State == ConnectionState.Closed)
                    DB_Connector.CurrentSqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = DB_Connector.CurrentSqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = ProcCosma;// 存储过程名字

                    // 传入参数--需要和存储过程内容一致(前一天-后一天)
                    sqlCmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                    sqlCmd.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = password;
                    sqlCmd.Parameters.Add("@order", SqlDbType.VarChar, 50).Value = idx.ToString();

                    cnt = sqlCmd.ExecuteNonQuery();  //  受影响行数理应为1
                }
            }
            catch (Exception)
            {
                DB_Connector.CurrentSqlCon.Close(); return -1;
                throw (new Exception("更新用户名-密码表(LogIn)异常"));
            }
            finally
            {
                DB_Connector.CurrentSqlCon.Close();
            }

            return cnt;
        }


        #endregion
    }


}
