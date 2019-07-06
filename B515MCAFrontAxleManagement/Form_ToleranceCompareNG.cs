using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    public partial class Form_ToleranceCompareNG : Form
    {
        public Form_ToleranceCompareNG()
        {
            InitializeComponent();
            m_bContinueMark_UnderCompareNG = false;
            m_bInputFindCodeSaveDB_NoMark = false;
        }
        private void Form_ToleranceCompareNG_Load(object sender, EventArgs e)
        {
            this.tbx_MissDetectInfo.Text = PersistentData.errorstr_pubstatic; 
        }

        public bool m_bToleranceSettingFinished = false;
        public static bool m_bInputFindCodeSaveDB_NoMark = false;
        private void btn_ManualInputNumber_Click(object sender, EventArgs e)
        {
            Form_AdvancedFunc_Login F_Ad_Login = new Form_AdvancedFunc_Login();
            F_Ad_Login.Show();
            while (!F_Ad_Login.IsDisposed)
            {
                Application.DoEvents();
                this.Enabled = false;  // 主界面使能禁止 不可使用

            }
            this.Enabled = true;
            //
            if (Form_AdvancedFunc_Login.m_bAdminLogin == false)
            { return; }


            tbx_ManualInput.Enabled = true;
            m_bInputFindCodeSaveDB_NoMark = true;
            m_bContinueMark_UnderCompareNG = false;
        }

        public static string str_ManualInput = "";
        private void tbx_ManualInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MessageBox.Show("确认输入此追溯号？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    str_ManualInput = tbx_ManualInput.Text;
                else
                {
                    str_ManualInput = "";
                }
                Thread.Sleep(1000);
                Dispose();  // 自动退出
            }
        }
        public static bool m_bContinueMark_UnderCompareNG = false;
        private void btn_ContinumMark_Click(object sender, EventArgs e)
        {

            Form_AdvancedFunc_Login F_Ad_Login = new Form_AdvancedFunc_Login();
            F_Ad_Login.Show();
            while (!F_Ad_Login.IsDisposed)
            {
                Application.DoEvents();
                this.Enabled = false;  // 主界面使能禁止 不可使用
            }
            this.Enabled = true;

            if (Form_AdvancedFunc_Login.m_bAdminLogin == false)
            { return; }

            //
            m_bContinueMark_UnderCompareNG = true;
            m_bInputFindCodeSaveDB_NoMark = false;
            
            Dispose();
        }

        
    }
}
