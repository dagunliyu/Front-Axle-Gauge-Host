using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB;
using System.Drawing;
using System.Threading;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    class CommonUse
    {

        public void TBX_Dat_Disp(List<TextBox> TextBox_list, 
                                 List<TextBox> TextBox_list_Sub4, 
                                 List<GroupBox> GrpBox_list,
                                 List<GroupBox> GrpBox_list_Sub4,
                                 double[] CurrentStandardTolerance,
                                 ZessiDatFromZessiFile zessi_DatFromZessiFile_Struct_local)
        {
            try
            {

                for (int tbxl_idx = 0; tbxl_idx < TextBox_list.Count; tbxl_idx++) //75ge
                {
                    // 12 20
                    if ((tbxl_idx >= 39 && tbxl_idx <= 47))// == 4 || tbxl_idx == 5 || tbxl_idx == 6)
                    {
                        TextBox_list[tbxl_idx].Text 
                            = TextBox_list_Sub4[tbxl_idx - 39].Text 
                            = zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx].ToString("F2");
                    } // 24 26
                    else if ((tbxl_idx >= 51 && tbxl_idx <= 53)) //9 10 11
                    {
                        TextBox_list[tbxl_idx].Text 
                            = TextBox_list_Sub4[tbxl_idx - 42].Text 
                            = zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx].ToString("F2");
                    }
                    else
                    {
                        TextBox_list[tbxl_idx].Text 
                            = zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx].ToString("F2");
                    }

                    if (((tbxl_idx + 1) % 3) == 0) // 3的倍数才进来
                    {//2  5  8 11
                        //double CTol_Z_Up = Math.Abs(CurrentStandardTolerance[tbxl_idx * 2]);//4
                        //double CTol_Z_Dn = Math.Abs(CurrentStandardTolerance[tbxl_idx * 2 + 1]);//5
                        //double CTol_Y_Up = Math.Abs(CurrentStandardTolerance[(tbxl_idx - 1) * 2]);//2
                        //double CTol_Y_Dn = Math.Abs(CurrentStandardTolerance[(tbxl_idx - 1) * 2 + 1]);//3
                        //double CTol_X_Up = Math.Abs(CurrentStandardTolerance[(tbxl_idx - 2) * 2]);//0
                        //double CTol_X_Dn = Math.Abs(CurrentStandardTolerance[(tbxl_idx - 2) * 2 + 1]);//1

                        if (((Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx]) < Math.Abs(CurrentStandardTolerance[tbxl_idx * 2])) && Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx]) < Math.Abs(CurrentStandardTolerance[tbxl_idx * 2 + 1])) &&
                             ((Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx - 1]) < Math.Abs(CurrentStandardTolerance[(tbxl_idx - 1) * 2])) && Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx - 1]) < Math.Abs(CurrentStandardTolerance[(tbxl_idx - 1) * 2 + 1])) &&
                             ((Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx - 2]) < Math.Abs(CurrentStandardTolerance[(tbxl_idx - 2) * 2])) && Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx - 2]) < Math.Abs(CurrentStandardTolerance[(tbxl_idx - 2) * 2 + 1])))
                        {

                            if ((tbxl_idx >= 39 && tbxl_idx <= 47)) //12 20
                            {
                                GrpBox_list_Sub4[(tbxl_idx - 39) / 3].BackColor = //12
                                    GrpBox_list[tbxl_idx / 3].BackColor =
                                    System.Drawing.Color.LimeGreen;
                            }
                            else if ((tbxl_idx >= 51 && tbxl_idx <= 53)) // 24 26
                            {
                                GrpBox_list_Sub4[(tbxl_idx - 42) / 3].BackColor = //15
                                    GrpBox_list[tbxl_idx / 3].BackColor =
                                    System.Drawing.Color.LimeGreen;
                            }
                            else
                            {
                                GrpBox_list[tbxl_idx / 3].BackColor =
                                    System.Drawing.Color.LimeGreen;
                            }
                        }
                        else//Grp变红
                        {
                            // 每3个点对应一个group
                            if ((tbxl_idx >= 39 && tbxl_idx <= 47))
                            {
                                GrpBox_list_Sub4[(tbxl_idx - 39) / 3].BackColor =
                                    GrpBox_list[tbxl_idx / 3].BackColor =
                                    System.Drawing.Color.Peru;
                            }
                            else if ((tbxl_idx >= 51 && tbxl_idx <= 47))
                            {
                                GrpBox_list_Sub4[(tbxl_idx - 42) / 3].BackColor =
                                    GrpBox_list[tbxl_idx / 3].BackColor =
                                    System.Drawing.Color.Peru;
                            }
                            else
                            {
                                GrpBox_list[tbxl_idx / 3].BackColor = System.Drawing.Color.Peru;
                            }
                        }

                        if ((Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx]) == 9999) &&
                             (Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx - 1]) == 9999) &&
                             (Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx - 2]) == 9999))
                        {
                            if ((tbxl_idx >= 39 && tbxl_idx <= 47))
                            {
                                GrpBox_list_Sub4[(tbxl_idx - 39) / 3].BackColor =
                                    GrpBox_list[tbxl_idx / 3].BackColor =
                                    System.Drawing.Color.Red;
                            }
                            else if ((tbxl_idx >= 51 && tbxl_idx <= 53))
                            {
                                GrpBox_list_Sub4[(tbxl_idx - 42) / 3].BackColor =
                                    GrpBox_list[tbxl_idx / 3].BackColor =
                                    System.Drawing.Color.Red;
                            }
                            else
                            {
                                GrpBox_list[tbxl_idx / 3].BackColor = System.Drawing.Color.Red;
                            }
 
                        }
                    }
                    else
                    { }

                    // 每次都进来--专门用来判断单点
                    if (((Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx]) < Math.Abs(CurrentStandardTolerance[tbxl_idx * 2])) && 
                          Math.Abs(zessi_DatFromZessiFile_Struct_local.AllMeasurePoint[tbxl_idx]) < Math.Abs(CurrentStandardTolerance[tbxl_idx * 2 + 1])) )
                    {
                        if ((tbxl_idx >=39 && tbxl_idx <= 47))// == 4 || tbxl_idx == 5 || tbxl_idx == 6)
                        {
                            //Font newFont = new Font(TextBox_list[tbxl_idx].Font, FontStyle.Bold);
                            //TextBox_list[tbxl_idx].Font = newFont;
                            TextBox_list[tbxl_idx].ForeColor
                                = TextBox_list_Sub4[tbxl_idx - 39].ForeColor 
                                = System.Drawing.Color.Black;
                        }
                        else if ((tbxl_idx >= 51 && tbxl_idx <= 53)) //9 10 11
                        {
                            TextBox_list[tbxl_idx].ForeColor
                                = TextBox_list_Sub4[tbxl_idx - 42].ForeColor 
                                = System.Drawing.Color.Black;
                        }
                        else
                        {
                            TextBox_list[tbxl_idx].ForeColor = System.Drawing.Color.Black;
                        }
                    }
                    else//字体加粗变红
                    {
                        if ((tbxl_idx >= 39 && tbxl_idx <= 47))// == 4 || tbxl_idx == 5 || tbxl_idx == 6)
                        {
                             //Font newFont = new Font(TextBox_list[tbxl_idx].Font, FontStyle.Bold);
                             //TextBox_list[tbxl_idx].Font = newFont;
                            TextBox_list[tbxl_idx].ForeColor =
                                TextBox_list_Sub4[tbxl_idx - 39].ForeColor 
                                = System.Drawing.Color.Red;
                        }
                        else if ((tbxl_idx >= 51 && tbxl_idx <= 53)) //9 10 11
                        {
                            TextBox_list[tbxl_idx].ForeColor
                                = TextBox_list_Sub4[tbxl_idx - 42].ForeColor 
                                = System.Drawing.Color.Red;
                        }
                        else
                        {
                            TextBox_list[tbxl_idx].ForeColor = System.Drawing.Color.Red; 
                        }
                    }
                    
                }
            }
            catch(Exception)
            {
                throw (new Exception("填充tbx异常"));
            }
        }

        public void grpBoxDispToolTip(ToolTip grpBoxToolTip, string ToolTipTitle,GroupBox grpBox,double[] B515_Standard_Tolerance, int idx, string tooltipContext)
        {
            grpBoxToolTip.ToolTipTitle = "";
            Thread.Sleep(200);
            grpBoxToolTip.ToolTipTitle = ToolTipTitle + " 标准公差提示";//"PTA3/D 标准公差提示"
            tooltip_context1 =   "X-上公差:" +  B515_Standard_Tolerance[idx * 6].ToString() +     " 下公差:" + Form_AdvancedFunc.B515_Standard_Tolerance[idx * 6 + 1].ToString() +
                                "\nY-上公差:" + B515_Standard_Tolerance[idx * 6 + 2].ToString() + " 下公差:" + Form_AdvancedFunc.B515_Standard_Tolerance[idx * 6 + 3].ToString() +
                                "\nZ-上公差:" + B515_Standard_Tolerance[idx * 6 + 4].ToString() + " 下公差:" + Form_AdvancedFunc.B515_Standard_Tolerance[idx * 6 + 5].ToString();
            Thread.Sleep(100);
            grpBoxToolTip.Show(tooltip_context1, grpBox);
            Thread.Sleep(200);
        }

        string tooltip_context1 = "";
        public void GroupBox_ToolTip_Disp(GroupBox grpBox, ToolTip grpBoxToolTip, Form form)
        {
            
            switch (grpBox.TabIndex.ToString())
            {
                case "101": //    grp_PTGLH_1 序号0 第1项
                    grpBoxDispToolTip(grpBoxToolTip,"PTG_LH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 0, tooltip_context1);
                    break;
                case "102": //    grp_PTGRH_2 序号1 第2项
                    grpBoxDispToolTip(grpBoxToolTip,"PTG_RH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 1, tooltip_context1);
                    break;
                case "34": //    grp_PTC_3 序号2 第3项
                    grpBoxDispToolTip(grpBoxToolTip,"PTC", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 2, tooltip_context1);
                    break;
                case "39": //    grp_PTB_4 序号3 第4项
                    grpBoxDispToolTip(grpBoxToolTip,"PTB", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 3, tooltip_context1);
                    break;


                case "24": // grp_PTA11_15 序号14 第15项
                    grpBoxDispToolTip(grpBoxToolTip,"PTA1_1", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 14, tooltip_context1);
                    break;
                case "23":
                    grpBoxDispToolTip(grpBoxToolTip,"PTA1_1", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 14, tooltip_context1);
                    break;

                case "31": // grp_PTA21_14 序号13 第14项
                    grpBoxDispToolTip(grpBoxToolTip,"PTA2_1", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 13, tooltip_context1);
                    break;
                case "26":
                    grpBoxDispToolTip(grpBoxToolTip,"PTA2_1", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 13, tooltip_context1);
                    break;

                case "100": // grp_PTA3D_16 序号15 第16项
                    grpBoxDispToolTip(grpBoxToolTip,"PTA3/D", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 15, tooltip_context1);
                    break;
                case "19":
                    grpBoxDispToolTip(grpBoxToolTip,"PTA3/D", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 15, tooltip_context1);
                    break;

                case "43":    // grp_PTJRH_17 序号16 第17项
                    grpBoxDispToolTip(grpBoxToolTip,"PTJ_RH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 16, tooltip_context1);
                    break;



                case ("30"): // grp_PTA4D_18 序号17 第18项
                    grpBoxDispToolTip(grpBoxToolTip,"PTA4/D", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 17, tooltip_context1);
                    break;
                case "29":
                    grpBoxDispToolTip(grpBoxToolTip,"PTA4/D", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 17, tooltip_context1);
                    break;

                case "27": //    grp_PTDLH_19 序号18 第19项
                    grpBoxDispToolTip(grpBoxToolTip,"PTD_LH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 18, tooltip_context1);
                    break;

                case "22": //    grp_PTDRH_20 序号19 第20项
                    grpBoxDispToolTip(grpBoxToolTip,"PTD_RH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 19, tooltip_context1);
                    break;

                case "103": //    grp_PTELH_21 序号20 第21项
                    grpBoxDispToolTip(grpBoxToolTip,"PTE_LH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 20, tooltip_context1);
                    break;
            
                case "25": //    grp_PTNLH_22 序号21 第22项
                    grpBoxDispToolTip(grpBoxToolTip,"PTN_LH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 21, tooltip_context1);
                    break;

                case "104": //    grp_PTPLH_23 序号22 第23项
                    grpBoxDispToolTip(grpBoxToolTip,"PTP_LH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 22, tooltip_context1);
                    break;

                case "20": //    grp_PTPRH_24 序号23 第24项
                    grpBoxDispToolTip(grpBoxToolTip,"PTP_RH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 23, tooltip_context1);
                    break;

                case "32": //    grp_PTERH_25 序号24 第25项
                    grpBoxDispToolTip(grpBoxToolTip,"PTE_RH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 24, tooltip_context1);
                    break;

                case "36": //    grp_PTA22_5 序号4 第5项
                    grpBoxDispToolTip(grpBoxToolTip,"PTA2_2", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 4, tooltip_context1);
                    break;

                case "45": //    grp_PTA12_6 序号5 第6项
                    grpBoxDispToolTip(grpBoxToolTip,"PTA1_2", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 5, tooltip_context1);
                    break;
            
                case "21": //    grp_PTHRHG_7 序号6 第7项
                    grpBoxDispToolTip(grpBoxToolTip,"PTH_RH/G", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 6, tooltip_context1);
                    break;

                case "28": //    grp_PTHLHG_8 序号7 第8项
                    grpBoxDispToolTip(grpBoxToolTip,"PTH_LH/G", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 7, tooltip_context1);
                    break;

                case "105": //    grp_PTILH_9 序号8 第9项
                    grpBoxDispToolTip(grpBoxToolTip,"PTI_LH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 8, tooltip_context1);
                    break;
          
                case "41": //    grp_PTIRH_10 序号9 第10项
                    grpBoxDispToolTip(grpBoxToolTip,"PTI_RH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 9, tooltip_context1);
                    break;

                case "106": //    grp_PTFRHF_11 序号10 第11项
                    grpBoxDispToolTip(grpBoxToolTip,"PTF_RH/F", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 10, tooltip_context1);
                    break;

                case "107": //    grp_PTFLHF_12 序号11 第12项
                    grpBoxDispToolTip(grpBoxToolTip,"PTF_LH/F", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 11, tooltip_context1);
                    break;

                case "108": //    grp_PTJLH_13 序号12 第13项
                    grpBoxDispToolTip(grpBoxToolTip,"PTJ_LH", grpBox, Form_AdvancedFunc.B515_Standard_Tolerance, 12, tooltip_context1);
                    break;

                default:
                    break;
            }
        }
    }
}
