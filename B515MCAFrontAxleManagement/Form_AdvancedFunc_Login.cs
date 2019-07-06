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
    public partial class Form_AdvancedFunc_Login : Form
    {
        public Form_AdvancedFunc_Login()
        {
            InitializeComponent();

            m_bAdminLogin = false;
            
        }
        DBConnector dbconn = new DBConnector("");
        DBOperate DBOperate_Obj = new DBOperate();//"B515_MCA"

        public bool m_bToleranceSettingFinished = false;
        private void btn_ReturnMainForm_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        private void tbx_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbx_Code.Focus();
            }
        }
        private void tbx_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login_Click(sender,e);
            }
        }

        public static bool m_bAdminLogin = false;
        private void btn_login_Click(object sender, EventArgs e)
        {
            this.errorInfo.Clear();

            if (String.IsNullOrEmpty(this.tbx_Name.Text.Trim()))
            {
                try
                {
                    this.errorInfo.SetError(this.tbx_Name, "用户编码不能为空！");
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "软件提示");
                    //throw ex;
                }
                finally
                {

                }
            }

            if (String.IsNullOrEmpty(this.tbx_Code.Text.Trim()))
            {
                try
                {
                    this.errorInfo.SetError(this.tbx_Code, "用户密码不能为空！");
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "软件提示");
                  //  throw ex;
                }
                finally
                {

                }
            }
            // 比较用户名以及密码
            try
            {//if(true)
                if (PersistentData.m_bClearSignalRoot)
                {
                    if (tbx_Code.Text == DBOperate_Obj.Query_LogIn_Dat(PersistentData.m_bClearSignalRoot)[1] && tbx_Name.Text == DBOperate_Obj.Query_LogIn_Dat(PersistentData.m_bClearSignalRoot)[0]) //if (tbx_Code.Text == "888888" && tbx_Name.Text == "admin")
                    {
                        m_bAdminLogin = true;

                        Dispose();// 关闭窗口 
                    }
                    else
                    {
                        MessageBox.Show("用户编码或用户密码不正确！", "软件提示");
                    }
                }
                else
                {
                    if (tbx_Code.Text == DBOperate_Obj.Query_LogIn_Dat(PersistentData.m_bClearSignalRoot)[1] && tbx_Name.Text == DBOperate_Obj.Query_LogIn_Dat(PersistentData.m_bClearSignalRoot)[0]) //if (tbx_Code.Text == "888888" && tbx_Name.Text == "admin")
                    {
                        m_bAdminLogin = true;

                        Dispose();// 关闭窗口 
                    }
                    else
                    {
                        MessageBox.Show("用户编码或用户密码不正确！", "软件提示");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "软件提示");
                //throw ex;
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            tbx_Code.Text = "";
            tbx_Name.Text = "";
        }

        private void btn_Return_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
