using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    public partial class Form_RealTimeDataInterface : Form
    {
        public Form_RealTimeDataInterface()
        {
            InitializeComponent();

            
            

            
        }

        public bool m_bToleranceSettingFinished = false;
        private void btn_ReturnMainForm_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        private void tbx_Name_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void tbx_Code_KeyDown(object sender, KeyEventArgs e)
        {
        }

        public static bool m_bAdminLogin = false;
        private void btn_login_Click(object sender, EventArgs e)
        {
            this.errorInfo.Clear();

            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "软件提示");
                //throw ex;
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
        }

        private void btn_Return_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
