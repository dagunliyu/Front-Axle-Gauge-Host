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
    public partial class Form_zessiMissDetect : Form
    {
        public Form_zessiMissDetect()
        {
            InitializeComponent();
            m_bContinueMark_UnderCompareNG = false;
            m_bInputFindCodeSaveDB_NoMark = false;
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

            m_bInputFindCodeSaveDB_NoMark = true;
            m_bContinueMark_UnderCompareNG = false;
        }

        public static string str_ManualInput = "";
      
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

            //
            m_bContinueMark_UnderCompareNG = true;
            m_bInputFindCodeSaveDB_NoMark = false;
            
            Dispose();
        }

        private void Form_ToleranceCompareNG_Load(object sender, EventArgs e)
        {

        }
    }
}
