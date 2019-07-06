using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB;


namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    public partial class Form_AdvancedFunc_SelectRecord : Form
    {
        public Form_AdvancedFunc_SelectRecord()
        {
            InitializeComponent();
        }

        public bool m_bToleranceSettingFinished = false;
        private void btn_ReturnMainForm_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        DataTable dt_TolInfo = null;
        DBOperate DBOperate_Obj = new DBOperate();
        private void Form_AdvancedFunc_Load(object sender, EventArgs e)
        {

            // 在PersisData加载时就调用数据库函数, 读取最近一次保存的标准公差信息,将其赋值给 B515_Standard_Tolerance
        
            // 在load中查询tolerance表,获取所有信息的日期,将日期add到listbox中

            Selected_Date = "";
            string[] DataTimes = null;// new string[] { };
            
            // 数据库查询
            dt_TolInfo = DBOperate_Obj.Query_ToleranceStandard_AllDat();
            int nElem = dt_TolInfo.Rows.Count;
            if (nElem > 0)
            {
                DataTimes = new string[nElem];
                for(int nElemRowIdx = 0;nElemRowIdx<nElem;nElemRowIdx++)
                {
                    DataTimes[nElemRowIdx] = dt_TolInfo.Rows[nElemRowIdx][1].ToString(); // 获取的是时间的值
                }
            }
            this.listBox1.DataSource = DataTimes;

        
        }

        public static string Selected_Date = "";
        public string GetDateSelection
        {
            get
            {
                return this.listBox1.SelectedItem as string;
            }
        }
        private void btn_TakeCurrentSelectedRecord_Click(object sender, EventArgs e)
        {
            //将选中的信息填入数据库查询函数中,进行以时间为基准的查询 
            Selected_Date = this.listBox1.SelectedItem.ToString();
            
            Dispose();
        }

        
        



    }
}
