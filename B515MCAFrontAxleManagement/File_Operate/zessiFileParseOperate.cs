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
using System.Diagnostics; // 使用process


namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    class zessiFileParseOperate
    {
        //DBOperate zessiFileOpr_DBOpreateClass = new DBOperate("B515_MCA");

        // \\192.168.0.216\VISUImages\Export

        //ZessiDatFromZessiFile zessi_DatFromZessiFile = new ZessiDatFromZessiFile();
        
        public bool SharedFileConnectState(string path, string Name, string Pwd)
        {
            bool status = false;
            //status = connectState(@"\\\\192.168.0.216\\VISUImages\\Export\\B515_MCA", "user","user");
            status = connectState(path, Name, Pwd);
            if(!status)
            {throw new Exception("访问Zessi共享文件夹失败,请查看局域网内共享文件夹访问是否正常！");}
            
            return status; // 连接正常则直接返回,不正常抛出异常,在程序启动时就自动退出
        }
        public bool connectState(string path, string Username, string Pwd)
        {
            bool status = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();

                string dosline = "net use " + path + " " + Pwd + " /user:" + Username;
                proc.StandardInput.WriteLine(dosline);
                proc.StandardInput.WriteLine("exit");

                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }

                string errorMsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();

                if (string.IsNullOrEmpty(errorMsg))
                {
                    status = true;
                }
                else
                {
                    throw new Exception(errorMsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return status;
        }


        public static int CodeLength = 23;

        
        ////D:\\ACIGIT2018\\[4]Zessi_Files   D:\\ACIGIT2018\\[4]Zessi_Files\\2
        ////DirectoryInfo必须直接用:@
        DirectoryInfo codedirinfo = new DirectoryInfo(@"\\\\192.168.0.216\\VISUImages\\Export\\B515_MCA");
        DirectoryInfo bak_filesinfo = new DirectoryInfo(@"\\\\192.168.0.216\\VISUImages\\Export\\B515_MCA\\2");
        static string SharedFloder_Path = "\\\\192.168.0.216\\VISUImages\\Export\\B515_MCA";
        static string SharedFloder_Path_bk = "\\\\192.168.0.216\\VISUImages\\Export\\B515_MCA\\2";

        

        //static string SharedFloder_Path = "D:\\ACIGIT2018\\[4]Zessi_Files";
        //static string SharedFloder_Path_bk = "D:\\ACIGIT2018\\[4]Zessi_Files\\2";
        //DirectoryInfo codedirinfo = new DirectoryInfo("D:\\ACIGIT2018\\[4]Zessi_Files");
        //DirectoryInfo bak_filesinfo = new DirectoryInfo("D:\\ACIGIT2018\\[4]Zessi_Files\\2");
       
        FileInfo[] FInfos = null;
        FileInfo[] BkFile_Infos = null;

        public static int MeasurePoint = 25;

        IEnumerable<XNode> measvaluect_failure = null;

        public bool[] On_ReadFromZessiFile(string zessiFileLoad_Path1,
                                         DataGridView dgvDatFromZessiFile,
                                         ref string errorstr,
                                         ref ZessiDatFromZessiFile zessi_DatFromZessiFile,
                                         ref bool m_bIfZessiFileHavProblem,
                                         ref bool m_bZessiMissDetect,
                                         ref bool m_bTolCompareResult)
        {
            #region 读xml并解析数据 //string xmlFileLoad = "C:\\[1]SimaticNetProj\\[2]蔡司数据\\GN15-5019-A3B 1801221710_20180122091952 - 副本.xml";
            //var strPath = Path.Combine("E:\渝江下位机\",xmlFileLoad);
            zessi_DatFromZessiFile.AllMeasurePoint_str = new string[MeasurePoint * 3];
            zessi_DatFromZessiFile.AllMeasurePoint = new double[MeasurePoint * 3];
            zessi_DatFromZessiFile.AllMeasurePoint_Tol = new double[MeasurePoint * 3 * 2];
            zessi_DatFromZessiFile.AllMeasurePoint_Tol_str = new string[MeasurePoint * 3 * 2];
            zessi_DatFromZessiFile.AllTolerace_CompareResults = new bool[MeasurePoint * 3];
            
            bool[] returnValues = {true, false }; // 比较ok,是否有异常
            //bool m_bTolCompareResult = true;
            try
            {
                //1. 调用数据库函数,获取每个点每个坐标的标准上下公差值
                // 暂时模拟

                //2. 对共享文件夹进行访问，对xml文件件数量进行判断
                /// <summary>
                /// 2.1共享文件夹访问
                /// "\\Dell_caffe-pc\\zessi_data\\GN15-5019-A3B 1801221710_20180122091952.xml";
                string zessiFileLoad_Path = "";
                //DirectoryInfo ZessiFileDirInfo = new DirectoryInfo(SharedFloder_Path);//(@"\\192.168.2.174\\zessi_data"); // SharedFloder_Path
                FInfos = codedirinfo.GetFiles();   // 获取文件夹中的所有文件

                // 保存文件的路径
                if (FInfos.Count() == 1)
                {
                    zessiFileLoad_Path = SharedFloder_Path + "\\" + FInfos[0].Name;
                }
                else if (FInfos.Count() > 1)
                {
                    string extension = "";
                    int XML_num = 0;
                    for (int i = 0; i < FInfos.Count(); i++)
                    {
                        extension = System.IO.Path.GetExtension(FInfos[i].DirectoryName + "\\" + FInfos[i].Name);
                        if (extension == ".xml")
                        {
                            XML_num = XML_num + 1;
                            zessiFileLoad_Path = FInfos[i].DirectoryName + "\\" + FInfos[i].Name;
                        }
                    }
                    if (XML_num == 1)
                    {
                        zessiFileLoad_Path = zessiFileLoad_Path;
                    }
                    else
                    {
                        m_bIfZessiFileHavProblem = true;
                        throw new Exception("开始读Zessi文件:ZESSI文件数量异常-" + "数量为:" + XML_num.ToString() + "\n\n建议检查文件生成步骤或重新开始流程/n"); 
                    }
                }
                else if (FInfos.Count() == 0)
                {
                    m_bIfZessiFileHavProblem = true;
                    throw new Exception("开始读Zessi文件:\n找不到Zessi默认生成目录: " + SharedFloder_Path + " 下的文件\n");
                }
                
                // 2.2文件夹内xml文件的判断


                //3.获取xml文件并分析

                XElement xe = XElement.Load(@zessiFileLoad_Path);     // 加载xml文件 使用linq从xml文件中查询信息
                var XMLLL = from pinfo in xe.Elements("HEADER") // 只能顺着找到MEASUREMENTS下面的2个节点：HEADER和MEASDATA  RESOURCE
                            select new
                            {
                                工件序列号 = pinfo.Element("PART").Element("PARTIDENTNR").Attribute("value").Value
                            };
                //dgvDatFromZessiFile.DataSource = XMLLL.ToList();
                zessi_DatFromZessiFile.zessiSerialNumber = xe.Element("HEADER").Element("PART").Element("PARTIDENTNR").Attribute("value").Value;

                var measvaluect = from mpinfo in xe.Element("MEASDATA").Element("MEASURES").Elements("MEASVALUECT")
                                  where mpinfo.Element("MEASPOINT").Attribute("value").Value.Contains("P")
                                  select mpinfo;
                //where PInfo.Attribute("RESOURCE").Value // Collections中
                int measpoint_idx = 0;
                int measpoint_idx_temp = 0;
                foreach (XElement xxe in measvaluect) // 25个元素
                {
                    measpoint_idx_temp++;//++measpoint_idx;    // 马上自加
                    if (measpoint_idx_temp >= 33) // 达到点数量的最大值后就退出
                    { /*return;*/ }
                    if (measpoint_idx_temp >= 15 && measpoint_idx_temp <= 22) // 在中间混入了建坐标所用的点--这里的15是凭印象写
                    { // measpoint_idx_temp是第*项(从序号14(第15项)开始,序号21(第22项)结束)
                        continue;       // 是无关点则继续
                    }
                    if (measpoint_idx_temp >= 23) // 找到了后面的可测点,为了保证测量点顺序的一致性,将前面紧接顺序的索引赋值给measpoint_idx
                    {// 要从第15项开始(但是要以序号的形式表示:14)-- 23-8=15
                         measpoint_idx = measpoint_idx_temp-8-1; // 15-1=14 
                    }
                    else if (measpoint_idx_temp >= 0 && measpoint_idx_temp < 15)
                    {
                        measpoint_idx = measpoint_idx_temp-1; // 以序号形式表示
                    }
                    // 文件中还有Element X_FAILURE 
                    measvaluect_failure = xxe.Nodes();
                    string xnd_ss = null;
                    
                    foreach (XNode xnd in measvaluect_failure)
                    {
                        xnd_ss = xnd.ToString();
                    }
                    if (xnd_ss.Contains("_FAILURE"))
                    {
                        m_bZessiMissDetect = true;
                        errorstr = "第" + (measpoint_idx + 1).ToString() + "个检测点漏检,请对照报告检查.\n";
                        zessi_DatFromZessiFile.AllMeasurePoint_str[3 * measpoint_idx]     = "9999";//xxe.Element("X_FAILURE").Attribute("value").Value.ToString();
                        zessi_DatFromZessiFile.AllMeasurePoint_str[3 * measpoint_idx + 1] = "9999";//xxe.Element("Y_FAILURE").Attribute("value").Value.ToString();
                        zessi_DatFromZessiFile.AllMeasurePoint_str[3 * measpoint_idx + 2] = "9999";//xxe.Element("Z_FAILURE").Attribute("value").Value.ToString();
                    }
                    else //measvaluect_failure[0]
                    {
                        zessi_DatFromZessiFile.AllMeasurePoint_str[3 * measpoint_idx]     = xxe.Element("X").Attribute("value").Value.ToString();
                        zessi_DatFromZessiFile.AllMeasurePoint_str[3 * measpoint_idx + 1] = xxe.Element("Y").Attribute("value").Value.ToString();
                        zessi_DatFromZessiFile.AllMeasurePoint_str[3 * measpoint_idx + 2] = xxe.Element("Z").Attribute("value").Value.ToString();
                    }
                    zessi_DatFromZessiFile.AllMeasurePoint[3 * measpoint_idx] = Convert.ToDouble(zessi_DatFromZessiFile.AllMeasurePoint_str[3 * measpoint_idx]);
                    zessi_DatFromZessiFile.AllMeasurePoint[3 * measpoint_idx + 1] = Convert.ToDouble(zessi_DatFromZessiFile.AllMeasurePoint_str[3 * measpoint_idx + 1]);
                    zessi_DatFromZessiFile.AllMeasurePoint[3 * measpoint_idx + 2] = Convert.ToDouble(zessi_DatFromZessiFile.AllMeasurePoint_str[3 * measpoint_idx + 2]);

                    // 公差模拟获取 - 用标准公差值代替
                    zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx] = Form_AdvancedFunc.B515_Standard_Tolerance[6 * measpoint_idx];//= 0.3;
                    zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx + 1] = Form_AdvancedFunc.B515_Standard_Tolerance[6 * measpoint_idx + 1];//= -0.3;
                    zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx + 2] = Form_AdvancedFunc.B515_Standard_Tolerance[6 * measpoint_idx + 2];//= 0.3;
                    zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx + 3] = Form_AdvancedFunc.B515_Standard_Tolerance[6 * measpoint_idx + 3];//= -0.3;
                    zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx + 4] = Form_AdvancedFunc.B515_Standard_Tolerance[6 * measpoint_idx + 4];//= 0.3;
                    zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx + 5] = Form_AdvancedFunc.B515_Standard_Tolerance[6 * measpoint_idx + 5];//= -0.3;
                    // 进行公差比较
                    if (((Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint[3 * measpoint_idx]) < Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx])) && (Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint[3 * measpoint_idx]) < Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx + 1])))
                        )
                    { zessi_DatFromZessiFile.AllTolerace_CompareResults[3 * measpoint_idx] = true; }
                    else { zessi_DatFromZessiFile.AllTolerace_CompareResults[3 * measpoint_idx] = false; }
                    if (
                            ((Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint[3 * measpoint_idx + 1]) < Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx + 2])) && (Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint[3 * measpoint_idx + 1]) < Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx + 3])))
                        )
                    { zessi_DatFromZessiFile.AllTolerace_CompareResults[3 * measpoint_idx + 1] = true; }
                    else { zessi_DatFromZessiFile.AllTolerace_CompareResults[3 * measpoint_idx + 1] = false; }
                    if (
                            ((Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint[3 * measpoint_idx + 2]) < Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx + 4])) && (Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint[3 * measpoint_idx + 2]) < Math.Abs(zessi_DatFromZessiFile.AllMeasurePoint_Tol[6 * measpoint_idx + 5])))
                        )
                    { zessi_DatFromZessiFile.AllTolerace_CompareResults[3 * measpoint_idx + 2] = true; }
                    else { zessi_DatFromZessiFile.AllTolerace_CompareResults[3 * measpoint_idx + 2] = false; }

                    //++measpoint_idx;// 等语句完成后，再自加
                    //if (measpoint_idx >= 25)
                    //{
                    //    continue;
                    //}
                }
                if (measpoint_idx_temp < 33) //25 + 8 = 33
                {
                    m_bIfZessiFileHavProblem = true;
                    //throw new Exception("Zessi检测有效点的数量为:" + measpoint_idx_temp.ToString() + "\n请检查是否存在漏检情况!");
                    errorstr = errorstr + "Zessi检测有效点的数量为:" + measpoint_idx_temp.ToString() + ".请检查是否存在漏检情况!\n";
                }

                m_bTolCompareResult = true;
                for (int i = 0; i < zessi_DatFromZessiFile.AllTolerace_CompareResults.Length; i++)
                {
                    if (zessi_DatFromZessiFile.AllTolerace_CompareResults[i] == false)
                    {
                        m_bTolCompareResult = false;
                        continue;
                    }
                }
                #region 测试

                //CodeOriginal_FromFile = "GN15-5019-A3B1707171030";
                //tbx_OriginalCode.Text = CodeOriginal_FromFile;
                //CodePLCSend1st = "";
                //CodePLCSend2nd = "";
                //byte[] CodeOriginal_FromExcel_Byte = 
                //int CodeOriginal_int;
                //CodeOriginal_int = System.Text.Encoding.ASCII.GetBytes(CodeOriginal_FromFile, 0, CodeOriginal_FromFile.Length, CodeOriginal_FromExcel_Byte, 0);

                //static void ReadParseXml2()
                //{
                //    XmlDocument xmlDoc = new XmlDocument();
                //    xmlDoc.Load("E:/Data/VisualStudio/C#/app001/ConsoleApp/App01/userlist.xml");
                //    //查找<users>
                //    XmlNode root = xmlDoc.SelectSingleNode("users");
                //    //获取到所有<users>的子节点
                //    XmlNodeList nodeList = xmlDoc.SelectSingleNode("users").ChildNodes;
                //    //遍历所有子节点
                //    foreach (XmlNode xn in nodeList)
                //    {
                //        XmlElement xe = (XmlElement)xn;
                //        Console.WriteLine("节点的ID为： " + xe.GetAttribute("id"));
                //        XmlNodeList subList = xe.ChildNodes;
                //        foreach (XmlNode xmlNode in subList)
                //        {
                //            Console.WriteLine(xmlNode.InnerText);
                //        }
                //    }
                //}
                //for (int i = 0; i < values.Length; i++)
                //{
                //    if (i >= 5 && i <= 44)
                //        values[i] = CodeOriginal_FromExcel_Byte[i - 5];// 给第一次DB5-44存
                //    else if (i < 5)
                //    {
                //        values[0] = true; values[1] = false; values[2] = false;
                //        values[3] = false; values[4] = false;
                //    }
                //    else
                //    {
                //        values[i] = 0;
                //    }
                //    //m_itmServerHandles_local[i] = i + 4;
                //}
                //int zessi_CompareResults_Sum = -1;
                //for (int i = 0; i < zessi_OffsetsValFromZESSIFile.Length; i++)
                //{
                //    if (Math.Abs(zessi_OffsetsValFromZESSIFile[i]) >
                //       Math.Abs(zessi_StandardTolerance[i]))
                //    { // 超出范围--比较ng
                //        zessi_ToleranceCompareResults[i] = 1;
                //    }
                //    else
                //    { zessi_ToleranceCompareResults[i] = 0; }
                //    zessi_CompareResults_Sum = zessi_CompareResults_Sum + zessi_ToleranceCompareResults[i];
                //}
                //// 公差ok-ng比较
                //if (zessi_CompareResults_Sum > 0)
                //{//公差比较ng
                //    WriteAsyncToOPCOtherBoolVar(27, true); // X3.3 公差比较NG信号 序号27

                //    // 弹出公差比较失败界面，进行操作
                //    Form_ToleranceCompareNG F_TC = new Form_ToleranceCompareNG();
                //    F_TC.Show();

                //    while (!F_TC.IsDisposed)
                //    {
                //        Application.DoEvents();
                //        this.Enabled = false;
                //    }
                //    this.Enabled = true;
                //}
                //else if (zessi_CompareResults_Sum == 0)
                //{// 公差比较ok
                //    WriteAsyncToOPCOtherBoolVar(20, true); // X2.4 公差比较ok信号 序号20
                //}
                #endregion
                    
                returnValues[0] = m_bTolCompareResult;
                returnValues[1] = false;
                return returnValues;
            }
            catch(Exception exx)
            {
                m_bIfZessiFileHavProblem = true;
                MessageBox.Show(exx.Message);
                returnValues[0] = m_bTolCompareResult;
                returnValues[1] = true;
                return returnValues;

                //throw exx;
            }
            #endregion
        }

        public void TransferFileDatToSaveDBDat(string CarType,
                                               DateTime today,
                                                bool ifNG,
                                                string item,
                                                double[] CurrentStandardTolerance,
                                                string MarkCode,
                                                ZessiDatFromZessiFile zessiFileDat, 
                                                ref ZessiDatFinalAdd2DB zessiSave2DBDat)
        {
            zessiSave2DBDat.zessiCarType = CarType;//  zessiFileDat.zessiSerialNumber
            zessiSave2DBDat.datetime = today;
            zessiSave2DBDat.zessiSerialNumber_SaveToDB = zessiFileDat.zessiSerialNumber;
            zessiSave2DBDat.zessiMarkCode_SaveToDB = MarkCode;//zessiFileDat.zessiMarkCode = "GN15-5019-A3B 1802012004";  // 该码值是测试用的
            if (ifNG) { zessiSave2DBDat.NGnumber = 0;}
            else {zessiSave2DBDat.NGnumber = 1;} 
            zessiSave2DBDat.Item = item;
            zessiSave2DBDat.AllMeasurePoint_SaveToDB = zessiFileDat.AllMeasurePoint;
            zessiSave2DBDat.AllMeasurePoint_Tol_SaveToDB = CurrentStandardTolerance;
        }


        public bool CheckBaseCode(string BaseCode)
        {
            try
            {
                if (BaseCode.Length != CodeLength)
                {
                    return false;
                }
                else
                {
                    double BaseCode_Tail3 = Convert.ToDouble(BaseCode.Substring(BaseCode.Length - 3, 3));// 如果转换不出问题的话就可以执行下一步了;
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("检查基础码异常:zessi类");
                return false;
            }
        }
        // 获取码值的byte信息
        public void GetCodeSaveToDBByte(string CodeSaveToDB, 
                                          int CodeOriginal_Length, 
                                        ref object[] values_Data)
        {
            //values_Data[1] = {0};
            int CodeOriginal_int;
            byte[] CodeSaveToDB_Byte = new byte[CodeOriginal_Length];  // 40个byte
            CodeOriginal_int = System.Text.Encoding.ASCII.GetBytes(CodeSaveToDB,
                                                                    0,
                                                                   CodeSaveToDB.Length,
                                                                   CodeSaveToDB_Byte, 0);
            for (int i = 0; i < CodeSaveToDB_Byte.Length; i++)
            {
                values_Data[i] = CodeSaveToDB_Byte[i];
            }
        }

        
        public void CheckZessiFileNumAdSharedFloder(string zessiFileLoad_Path)
        {
            string zessiFileLoad_Path_bk = zessiFileLoad_Path + "\\2";
         
            //codedirinfo   = new DirectoryInfo(SharedFloder_Path); //zessiFileLoad_Path_bk
            //bak_filesinfo = new DirectoryInfo(SharedFloder_Path_bk);//zessiFileLoad_Path_bk
            
            if (!codedirinfo.Exists)
            {
                codedirinfo.Create();
            }
            if (!bak_filesinfo.Exists)
            {
                bak_filesinfo.Create();
            }

            FileInfo[] FInfos = codedirinfo.GetFiles();   // 获取文件夹中的所有文件
            FileInfo[] BkFile_Infos = bak_filesinfo.GetFiles();   // 获取文件夹中的所有文件
            
            List<string> listExten = new List<string>();  // 创建泛型集合对象
            //string strExten = "";  // 定义一个变量用来存储文件扩展名
            //string txtCode = "";

            int file_num = FInfos.Count();  // 文件的数量，如果文件的数量大于1，就可以取文件
            if (file_num == 0)
            {
                //MessageBox.Show("文件夹中不存在蔡司生成的文件");
                return;
            }
            else if (file_num > 0)  // 初始化启动，如果里面有大于1个的文件，把现存的文件都删除
            {
                string OriginalFilePath, NewFilePath;
                string extension = "";
                for (int i = 0; i < file_num; i++)
                {
                    extension = System.IO.Path.GetExtension(FInfos[i].DirectoryName+"\\"+FInfos[i].Name);
                    //if (extension == ".xml")
                    //{
                        //extension = FInfos[i].DirectoryName + "\\" + FInfos[i].Name;
                        //File.Delete(@extension);
                    //}
                    OriginalFilePath = SharedFloder_Path + "\\" + FInfos[i].Name;
                    NewFilePath = SharedFloder_Path_bk + "\\" + FInfos[i].Name;

                    if (File.Exists(@NewFilePath))
                    {
                        File.Delete(@NewFilePath);
                    }

                    File.Move(OriginalFilePath, NewFilePath);


                    
                }
            }
        }

        public int[] CheckZessiFileNumAdProcessOver()
        {
            FInfos = codedirinfo.GetFiles();   // 获取文件夹中的所有文件
            BkFile_Infos = bak_filesinfo.GetFiles();   // 获取文件夹中的所有文件

            //string strExten = "";  // 定义一个变量用来存储文件扩展名
            //string txtCode = "";
            string OriginalFilePath, NewFilePath;

            int[] file_num_Check = {1,1};
            int file_num = FInfos.Count();  // 文件的数量，如果文件的数量大于1，就可以取文件
            
            if (file_num == 1)  // 这是正常状态
            {
                OriginalFilePath = SharedFloder_Path + "\\" + FInfos[0].Name;
                NewFilePath = SharedFloder_Path_bk + "\\" + FInfos[0].Name;

                //if (File.Exists(@NewFilePath)) // lhc180322屏蔽
                //{
                //    File.Delete(@NewFilePath);
                //}

                File.Move(OriginalFilePath, NewFilePath);

                file_num_Check[0] = file_num;

                FInfos = codedirinfo.GetFiles();   // 获取文件夹中的所有文件
                file_num_Check[1] = FInfos.Count();
                return file_num_Check;
            }
            else
            {
                for (int i = 0; i < file_num; i++)
                {
                    OriginalFilePath = SharedFloder_Path + "\\" + FInfos[i].Name;
                    NewFilePath = SharedFloder_Path_bk + "\\" + FInfos[i].Name;

                    //if (File.Exists(@NewFilePath))  // lhc180322屏蔽
                    //{
                    //    File.Delete(@NewFilePath);
                    //}

                    File.Move(OriginalFilePath, NewFilePath);
                }
                file_num_Check[0] = file_num;

                FInfos = codedirinfo.GetFiles();   // 获取文件夹中的所有文件
                file_num_Check[1] = FInfos.Count();
                
                throw (new Exception("流程收尾状态中处理ZESSI文件操作, ZESSI文件数量异常（数量为：" + file_num.ToString() + "） , 建议检查文件生成步骤或重新开始流程/n"));
                return file_num_Check;
            }
        }
    
        public int CheckZessiFileNumAdProcessOver_ForSignalClear()
        {
            FInfos = codedirinfo.GetFiles();   // 获取文件夹中的所有文件
            BkFile_Infos = bak_filesinfo.GetFiles();   // 获取文件夹中的所有文件

            //string strExten = "";  // 定义一个变量用来存储文件扩展名
            //string txtCode = "";
            string OriginalFilePath, NewFilePath;


            int file_num = FInfos.Count();  // 文件的数量，如果文件的数量大于1，就可以取文件
            
            #region 对file进行操作
            if (file_num == 1)  // 这是正常状态
            {
                OriginalFilePath = SharedFloder_Path + "\\" + FInfos[0].Name;
                NewFilePath = SharedFloder_Path_bk + "\\" + FInfos[0].Name;

                //if (File.Exists(@NewFilePath))  // lhc180322屏蔽
                //{
                //    File.Delete(@NewFilePath);
                //}

                File.Move(OriginalFilePath, NewFilePath);

                FInfos = codedirinfo.GetFiles();   // 获取文件夹中的所有文件
                file_num = FInfos.Count();
            }
            else
            {
                for (int i = 0; i < file_num; i++)
                {
                    OriginalFilePath = SharedFloder_Path + "\\" + FInfos[i].Name;
                    NewFilePath = SharedFloder_Path_bk + "\\" + FInfos[i].Name;

                    //if (File.Exists(@NewFilePath))  // lhc180322屏蔽
                    //{
                    //    File.Delete(@NewFilePath);
                    //}

                    File.Move(OriginalFilePath, NewFilePath);
                }
                FInfos = codedirinfo.GetFiles();   // 获取文件夹中的所有文件
                file_num = FInfos.Count();
            }
            #endregion

            return file_num;
        }
    }

    public struct ZessiTolDatReadFromDB_Buffer
    {
        public string CarType;
        public string DataTime;
        public double[] AllMeasurePoint_ToleranceInfo; // 测量的25点-75个偏差值对应的上下标准公差
        public string[] AllMeasurePoint_ToleranceInfo_str; // 测量的25点-75个偏差值对应的上下标准公差
    }
    public struct ZessiMeasureDatReadFromDB_Buffer
    {
        public string zessiCarType;               // 车型
        public string datetime;                 // 日期
        public string zessiSerialNumber_ReadFromDB; // 序列号
        public string zessiMarkCode_ReadFromDB;     // 打标码
        public int NGnumber;
        public string Item;
        // = new string [75]; // 25*3
        public double[] AllMeasurePoint_ReadFromDB; // 测量的25点-75个偏差值
        public double[] AllMeasurePoint_Tol_ReadFromDB; // 测量的25点-75个偏差值对应的上下标准公差
        public string[] AllMeasurePoint_ReadFromDB_str; // 测量的25点-75个偏差值
        public string[] AllMeasurePoint_Tol_ReadFromDB_str; // 测量的25点-75个偏差值对应的上下标准公差
    }

    public struct ZessiDatFinalAdd2DB
    {
        public string zessiCarType;               // 车型
        public DateTime datetime;                 // 日期
        public string zessiSerialNumber_SaveToDB; // 序列号
        public string zessiMarkCode_SaveToDB;     // 打标码
        public int NGnumber;
        public string Item;
        // = new string [75]; // 25*3
        public double[] AllMeasurePoint_SaveToDB; // 测量的25点-75个偏差值
        public double[] AllMeasurePoint_Tol_SaveToDB; // 测量的25点-75个偏差值对应的上下标准公差
    }
    public struct ZessiDatFromZessiFile
    {
        public string zessiSerialNumber;
        public string zessiMarkCode;

        public string[] AllMeasurePoint_str;// = new string [75]; // 25*3
        public double[] AllMeasurePoint;
        public string[] AllMeasurePoint_Tol_str;
        public double[] AllMeasurePoint_Tol;
        public bool[] AllTolerace_CompareResults;
    }
}
