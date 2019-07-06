using System;

// to use Forms
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

// to use VarEnums
using System.Runtime.InteropServices;

// to use IAsync, AsyncResult
using System.Threading;
using System.Runtime.Remoting.Messaging;

// necessary to connect user defined objects to opc items respectively the data delivered from datachange
using Siemens.Automation.ServiceSupport.Applications.OPC.IOPCConnector;

// to use the DBConnector class
using Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB;

using OpcRcw.Comn;
using OpcRcw.Da;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    /// <summary> this is the main class of this sample application </summary>
    public class PersistentData : System.Windows.Forms.Form
    {
        #region Constants
        /// <summary>These constants describe the number of items for each group for the simulation</summary>
        #region OPC-constants: settings, item-ids etc.
        private const string PROG_ID = "OPC.SimaticNet"; //private const string PROG_ID = "OPC.SimaticNet";

        #region opc-constants for the DBblock group
        private int NUM_DBblock_ITEMS = 0;
        private const int CLNT_HNDL_GRP_DBblock = 1;
        private const string GRP_NAME_DBblock = "DB1001";//DB1001 "DBblock";

        private char ITEM_Address = '0';
        private char ITEM_Offset = '0';
        private string OPCServerName = "S7:[S7_Connection_2]DB1001,";
        //const initialized with null

        #region S7_localserver ItemsName

        //private static string[] ITEM_DBblock = { 
        ////PLC->PC 0.0-1.7
        //"S7:[@LOCALSERVER]DB1,X0.0",//0  Value:1 PLC->PC 原始码OK信号
        //"S7:[@LOCALSERVER]DB1,X0.1",//1 Value:2 PLC->PC 开班检测系统确认OK信号
        //"S7:[@LOCALSERVER]DB1,X0.2",//2 Value:3 PLC->PC MCC检测完成信号
        //"S7:[@LOCALSERVER]DB1,X0.3",//3 Value:4 PLC->PC 数据读取请求信号
        //"S7:[@LOCALSERVER]DB1,X0.4",//4 Value:5 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X0.5",//5 Value:6 PLC->PC 
        //"S7:[@LOCALSERVER]DB1,X0.6",//6 Value:7 PLC->PC 
        //"S7:[@LOCALSERVER]DB1,X0.7",//7 Value:8 PLC->PC 
        //"S7:[@LOCALSERVER]DB1,X1.0",//8 Value:9 PLC->PC PLC第1次回传码值OK信号
        //"S7:[@LOCALSERVER]DB1,X1.1",//9 Value:10 PLC->PC PLC第2次回传码值OK信号
        //"S7:[@LOCALSERVER]DB1,X1.2",//10 Value:11 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X1.3",//11 Value:12 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X1.4",//12 Value:13 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X1.5",//13 Value:14 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X1.6",//14 Value:15 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X1.7",//15 Value:16 PLC->PC
        ////PC->PLC 2.0-3.7
        //"S7:[@LOCALSERVER]DB1,X2.0",//16 Value:17 PC->PLC 上位机准备OK信号-开机完成且OPC_items插入完毕后
        //"S7:[@LOCALSERVER]DB1,X2.1",//17 Value:18 PC->PLC 上位机准备OK信号-每隔500ms向PLC写一个true
        //"S7:[@LOCALSERVER]DB1,X2.2",//18 Value:19 PC->PLC 上位机工作Busy信号，每次进入On_DataChanged()后，对opc变化的量进行处理时,为true,执行结束后变为false
        //"S7:[@LOCALSERVER]DB1,X2.3",//19 Value:20 PC->PLC 数据读取开始信号 PC收到读信号
        //"S7:[@LOCALSERVER]DB1,X2.4",//20 Value:21 PC->PLC 公差比较OK信号(和NG信号分开)
        //"S7:[@LOCALSERVER]DB1,X2.5",//21 Value:22 PC->PLC 读取报告完成信号
        //"S7:[@LOCALSERVER]DB1,X2.6",//22 Value:23 PC->PLC 打标内容生成OK信号 公差比较OK，生成打标码且已经保存到B50.40中
        //"S7:[@LOCALSERVER]DB1,X2.7",//23 Value:24 PC->PLC 放弃打标入库信号  公差比较NG，且手动不打码，通知PLC当前操作放弃打标入库
        //"S7:[@LOCALSERVER]DB1,X3.0",//24 Value:25 PC->PLC 打标前打标内容核实OK信号
        //"S7:[@LOCALSERVER]DB1,X3.1",//25 Value:26 PC->PLC 打标前打标内容核实NG信号
        //"S7:[@LOCALSERVER]DB1,X3.2",//26 Value:27 PC->PLC 终止打标信号
        //"S7:[@LOCALSERVER]DB1,X3.3",//27 Value:28 PC->PLC 
        //"S7:[@LOCALSERVER]DB1,X3.4",//28 Value:29 PC->PLC 
        //"S7:[@LOCALSERVER]DB1,X3.5",//29 Value:30 PC->PLC
        //"S7:[@LOCALSERVER]DB1,X3.6",//30 Value:31 PC->PLC
        //"S7:[@LOCALSERVER]DB1,X3.7",//31 Value:32 PC->PLC 112
        //// 5-44 (length-80)->(length-41)    原始码值保存地址(以Byte保存)(用24，预留16-每个码值一共40Byte) PC->PLC 
        //序号：112-80->112-41 Value:112-80+1->112-41+1
        //"S7:[@LOCALSERVER]DB1,B50",/*5 32 V:33*/"S7:[@LOCALSERVER]DB1,B51","S7:[@LOCALSERVER]DB1,B52","S7:[@LOCALSERVER]DB1,B53","S7:[@LOCALSERVER]DB1,B54","S7:[@LOCALSERVER]DB1,B55","S7:[@LOCALSERVER]DB1,B56","S7:[@LOCALSERVER]DB1,B57","S7:[@LOCALSERVER]DB1,B58","S7:[@LOCALSERVER]DB1,B59",
        //"S7:[@LOCALSERVER]DB1,B60",/*15*/"S7:[@LOCALSERVER]DB1,B61","S7:[@LOCALSERVER]DB1,B62","S7:[@LOCALSERVER]DB1,B63","S7:[@LOCALSERVER]DB1,B64","S7:[@LOCALSERVER]DB1,B65","S7:[@LOCALSERVER]DB1,B66","S7:[@LOCALSERVER]DB1,B67","S7:[@LOCALSERVER]DB1,B68","S7:[@LOCALSERVER]DB1,B69",
        //"S7:[@LOCALSERVER]DB1,B70",/*25*/"S7:[@LOCALSERVER]DB1,B71","S7:[@LOCALSERVER]DB1,B72","S7:[@LOCALSERVER]DB1,B73","S7:[@LOCALSERVER]DB1,B74","S7:[@LOCALSERVER]DB1,B75","S7:[@LOCALSERVER]DB1,B76","S7:[@LOCALSERVER]DB1,B77","S7:[@LOCALSERVER]DB1,B78","S7:[@LOCALSERVER]DB1,B79",
        //"S7:[@LOCALSERVER]DB1,B80",/*35*/"S7:[@LOCALSERVER]DB1,B81","S7:[@LOCALSERVER]DB1,B82","S7:[@LOCALSERVER]DB1,B83","S7:[@LOCALSERVER]DB1,B84","S7:[@LOCALSERVER]DB1,B85","S7:[@LOCALSERVER]DB1,B86","S7:[@LOCALSERVER]DB1,B87","S7:[@LOCALSERVER]DB1,B88","S7:[@LOCALSERVER]DB1,B89",
        ////45-84 (length-40)->(length-1)
        //序号：112-40->112-1 Value:112-40+1->112-1+1
	//"S7:[@LOCALSERVER]DB1,B8",/*45*/"S7:[@LOCALSERVER]DB1,B9","S7:[@LOCALSERVER]DB1,B10","S7:[@LOCALSERVER]DB1,B11","S7:[@LOCALSERVER]DB1,B12","S7:[@LOCALSERVER]DB1,B13","S7:[@LOCALSERVER]DB1,B14","S7:[@LOCALSERVER]DB1,B15","S7:[@LOCALSERVER]DB1,B16","S7:[@LOCALSERVER]DB1,B17",
        //"S7:[@LOCALSERVER]DB1,B18",/*55*/"S7:[@LOCALSERVER]DB1,B19","S7:[@LOCALSERVER]DB1,B20","S7:[@LOCALSERVER]DB1,B21","S7:[@LOCALSERVER]DB1,B22","S7:[@LOCALSERVER]DB1,B23","S7:[@LOCALSERVER]DB1,B24","S7:[@LOCALSERVER]DB1,B25","S7:[@LOCALSERVER]DB1,B26","S7:[@LOCALSERVER]DB1,B27",
        //"S7:[@LOCALSERVER]DB1,B28",/*65*/"S7:[@LOCALSERVER]DB1,B29","S7:[@LOCALSERVER]DB1,B30","S7:[@LOCALSERVER]DB1,B31","S7:[@LOCALSERVER]DB1,B32","S7:[@LOCALSERVER]DB1,B33","S7:[@LOCALSERVER]DB1,B34","S7:[@LOCALSERVER]DB1,B35","S7:[@LOCALSERVER]DB1,B36","S7:[@LOCALSERVER]DB1,B37",
        //"S7:[@LOCALSERVER]DB1,B38",/*75*/"S7:[@LOCALSERVER]DB1,B39","S7:[@LOCALSERVER]DB1,B40","S7:[@LOCALSERVER]DB1,B41","S7:[@LOCALSERVER]DB1,B42","S7:[@LOCALSERVER]DB1,B43","S7:[@LOCALSERVER]DB1,B44","S7:[@LOCALSERVER]DB1,B45","S7:[@LOCALSERVER]DB1,B46","S7:[@LOCALSERVER]DB1,B47"
        //};// PLC发送的码值保存地址(以Byte发送) PLC->PC 用数据类型或的方式
        //// uint8[]的类型
        //private string[] ITEM_DBblock_Trigger = { 
        //                                  //PLC->PC 0.0-1.7
        //"S7:[@LOCALSERVER]DB1,X0.0",//0 PLC->PC 原始码OK信号
        //"S7:[@LOCALSERVER]DB1,X0.1",//1 PLC->PC 开班检测系统确认OK信号
        //"S7:[@LOCALSERVER]DB1,X0.2",//2 PLC->PC MCC检测完成信号
        //"S7:[@LOCALSERVER]DB1,X0.3",//3 PLC->PC 数据读取请求信号
        //"S7:[@LOCALSERVER]DB1,X0.4",//4 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X0.5",//5 PLC->PC 
        //"S7:[@LOCALSERVER]DB1,X0.6",//6 PLC->PC 
        //"S7:[@LOCALSERVER]DB1,X0.7",//7 PLC->PC 
        //"S7:[@LOCALSERVER]DB1,X1.0",//8 PLC->PC PLC第1次回传码值OK信号
        //"S7:[@LOCALSERVER]DB1,X1.1",//9 PLC->PC PLC第2次回传码值OK信号
        //"S7:[@LOCALSERVER]DB1,X1.2",//10 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X1.3",//11 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X1.4",//12 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X1.5",//13 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X1.6",//14 PLC->PC
        //"S7:[@LOCALSERVER]DB1,X1.7",//15 PLC->PC
        ////PC->PLC 2.0-3.7
        //"S7:[@LOCALSERVER]DB1,X2.0",//16 PC->PLC 上位机准备OK信号-开机完成且OPC_items插入完毕后
        //"S7:[@LOCALSERVER]DB1,X2.1",//17 PC->PLC 上位机准备OK信号-每隔500ms向PLC写一个true
        //"S7:[@LOCALSERVER]DB1,X2.2",//18 PC->PLC 上位机工作Busy信号，每次进入On_DataChanged()后，对opc变化的量进行处理时,为true,执行结束后变为false
        //"S7:[@LOCALSERVER]DB1,X2.3",//19 PC->PLC 数据读取开始信号 PC收到读信号
        //"S7:[@LOCALSERVER]DB1,X2.4",//20 PC->PLC 公差比较OK信号(和NG信号分开)
        //"S7:[@LOCALSERVER]DB1,X2.5",//21 PC->PLC 读取报告完成信号
        //"S7:[@LOCALSERVER]DB1,X2.6",//22 PC->PLC 打标内容生成OK信号 公差比较OK，生成打标码且已经保存到B50.40中
        //"S7:[@LOCALSERVER]DB1,X2.7",//23 PC->PLC 放弃打标入库信号  公差比较NG，且手动不打码，通知PLC当前操作放弃打标入库
        //"S7:[@LOCALSERVER]DB1,X3.0",//24 PC->PLC 打标前打标内容核实OK信号
        //"S7:[@LOCALSERVER]DB1,X3.1",//25 PC->PLC 打标前打标内容核实NG信号
        //"S7:[@LOCALSERVER]DB1,X3.2",//26 PC->PLC 终止打标信号
        //"S7:[@LOCALSERVER]DB1,X3.3",//27 PC->PLC 
        //"S7:[@LOCALSERVER]DB1,X3.4",//28 PC->PLC 
        //"S7:[@LOCALSERVER]DB1,X3.5",//29 PC->PLC
        //"S7:[@LOCALSERVER]DB1,X3.6",//30 PC->PLC
        //"S7:[@LOCALSERVER]DB1,X3.7"//31 PC->PLC
        //                                        };
        ////"S7:[@LOCALSERVER]DB1,B50.40",
        ////"S7:[@LOCALSERVER]DB1,B8.40"                                        
        //private string[] ITEM_DBblock_Mark = { 
        //                                  "S7:[@LOCALSERVER]DB1,B50","S7:[@LOCALSERVER]DB1,B51","S7:[@LOCALSERVER]DB1,B52","S7:[@LOCALSERVER]DB1,B53","S7:[@LOCALSERVER]DB1,B54","S7:[@LOCALSERVER]DB1,B55","S7:[@LOCALSERVER]DB1,B56","S7:[@LOCALSERVER]DB1,B57","S7:[@LOCALSERVER]DB1,B58","S7:[@LOCALSERVER]DB1,B59",
        //                                  "S7:[@LOCALSERVER]DB1,B60","S7:[@LOCALSERVER]DB1,B61","S7:[@LOCALSERVER]DB1,B62","S7:[@LOCALSERVER]DB1,B63","S7:[@LOCALSERVER]DB1,B64","S7:[@LOCALSERVER]DB1,B65","S7:[@LOCALSERVER]DB1,B66","S7:[@LOCALSERVER]DB1,B67","S7:[@LOCALSERVER]DB1,B68","S7:[@LOCALSERVER]DB1,B69",
        //                                  "S7:[@LOCALSERVER]DB1,B70","S7:[@LOCALSERVER]DB1,B71","S7:[@LOCALSERVER]DB1,B72","S7:[@LOCALSERVER]DB1,B73","S7:[@LOCALSERVER]DB1,B74","S7:[@LOCALSERVER]DB1,B75","S7:[@LOCALSERVER]DB1,B76","S7:[@LOCALSERVER]DB1,B77","S7:[@LOCALSERVER]DB1,B78","S7:[@LOCALSERVER]DB1,B79",
        //                                  "S7:[@LOCALSERVER]DB1,B80","S7:[@LOCALSERVER]DB1,B81","S7:[@LOCALSERVER]DB1,B82","S7:[@LOCALSERVER]DB1,B83","S7:[@LOCALSERVER]DB1,B84","S7:[@LOCALSERVER]DB1,B85","S7:[@LOCALSERVER]DB1,B86","S7:[@LOCALSERVER]DB1,B87","S7:[@LOCALSERVER]DB1,B88","S7:[@LOCALSERVER]DB1,B89",
        //                                  //
        //                                  "S7:[@LOCALSERVER]DB1,B8","S7:[@LOCALSERVER]DB1,B9","S7:[@LOCALSERVER]DB1,B10","S7:[@LOCALSERVER]DB1,B11","S7:[@LOCALSERVER]DB1,B12","S7:[@LOCALSERVER]DB1,B13","S7:[@LOCALSERVER]DB1,B14","S7:[@LOCALSERVER]DB1,B15","S7:[@LOCALSERVER]DB1,B16","S7:[@LOCALSERVER]DB1,B17",
        //                                  "S7:[@LOCALSERVER]DB1,B18","S7:[@LOCALSERVER]DB1,B19","S7:[@LOCALSERVER]DB1,B20","S7:[@LOCALSERVER]DB1,B21","S7:[@LOCALSERVER]DB1,B22","S7:[@LOCALSERVER]DB1,B23","S7:[@LOCALSERVER]DB1,B24","S7:[@LOCALSERVER]DB1,B25","S7:[@LOCALSERVER]DB1,B26","S7:[@LOCALSERVER]DB1,B27",
        //                                  "S7:[@LOCALSERVER]DB1,B28","S7:[@LOCALSERVER]DB1,B29","S7:[@LOCALSERVER]DB1,B30","S7:[@LOCALSERVER]DB1,B31","S7:[@LOCALSERVER]DB1,B32","S7:[@LOCALSERVER]DB1,B33","S7:[@LOCALSERVER]DB1,B34","S7:[@LOCALSERVER]DB1,B35","S7:[@LOCALSERVER]DB1,B36","S7:[@LOCALSERVER]DB1,B37",
        //                                  "S7:[@LOCALSERVER]DB1,B38","S7:[@LOCALSERVER]DB1,B39","S7:[@LOCALSERVER]DB1,B40","S7:[@LOCALSERVER]DB1,B41","S7:[@LOCALSERVER]DB1,B42","S7:[@LOCALSERVER]DB1,B43","S7:[@LOCALSERVER]DB1,B44","S7:[@LOCALSERVER]DB1,B45","S7:[@LOCALSERVER]DB1,B46","S7:[@LOCALSERVER]DB1,B47"   
        //                                     };

        #endregion

        #region S7Connection_1 ItemsName
        //S7:[S7_Connection_2]DB1001,X0.0
        int Items_Bool_Num = 32;
        int Items_Int_Num = 1;
        private static string[] ITEM_DBblock = { 
        //PLC->PC 0.0-1.7
        "S7:[S7_Connection_2]DB1001,X0.0", //0  Value:1    PLC->PC 原始码OK信号
        "S7:[S7_Connection_2]DB1001,X0.1", //1  Value:2    PLC->PC 开班检测系统确认OK信号
        "S7:[S7_Connection_2]DB1001,X0.2", //2  Value:3    PLC->PC MCC检测完成信号
        "S7:[S7_Connection_2]DB1001,X0.3", //3  Value:4    PLC->PC 数据读取请求信号
		"S7:[S7_Connection_2]DB1001,X0.4", //4  Value:5    PLC->PC
		"S7:[S7_Connection_2]DB1001,X0.5", //5  Value:6    PLC->PC 
		"S7:[S7_Connection_2]DB1001,X0.6", //6  Value:7    PLC->PC pick-up存取数据ok
		"S7:[S7_Connection_2]DB1001,X0.7", //7  Value:8    PLC->PC 
        "S7:[S7_Connection_2]DB1001,X1.0", //8  Value:9    PLC->PC PLC第1次回传码值OK信号
        "S7:[S7_Connection_2]DB1001,X1.1", //9  Value:10   PLC->PC PLC第2次回传码值OK信号
		"S7:[S7_Connection_2]DB1001,X1.2", //10 Value:11   PLC->PC
		"S7:[S7_Connection_2]DB1001,X1.3", //11 Value:12   PLC->PC
		"S7:[S7_Connection_2]DB1001,X1.4", //12 Value:13   PLC->PC 状态改变信号:由0-1或由1-0都去读INT4和INT5,把对应label变绿
		"S7:[S7_Connection_2]DB1001,X1.5", //13 Value:14   PLC->PC 状态故障信号:由0-1读INT4-5,把对应label变红;由1-0读INT4-5,把对应label变绿
		"S7:[S7_Connection_2]DB1001,X1.6", //14 Value:15   PLC->PC
		"S7:[S7_Connection_2]DB1001,X1.7", //15 Value:16   PLC->PC
        //PC->PLC 2.0-3.7                     
        "S7:[S7_Connection_2]DB1001,X2.0", //16 Value:17   PC->PLC 上位机准备OK信号-开机完成且OPC_items插入完毕后
        "S7:[S7_Connection_2]DB1001,X2.1", //17 Value:18   PC->PLC 上位机准备OK信号-每隔500ms向PLC写一个true
        "S7:[S7_Connection_2]DB1001,X2.2", //18 Value:19   PC->PLC 上位机工作Busy信号，每次进入On_DataChanged()后，对opc变化的量进行处理时,为true,执行结束后变为false
		"S7:[S7_Connection_2]DB1001,X2.3", //19 Value:20   PC->PLC 数据读取开始信号 PC收到读信号
        "S7:[S7_Connection_2]DB1001,X2.4", //20 Value:21   PC->PLC 公差比较OK信号(和NG信号分开)
        "S7:[S7_Connection_2]DB1001,X2.5", //21 Value:22   PC->PLC 读取报告完成信号
        "S7:[S7_Connection_2]DB1001,X2.6", //22 Value:23   PC->PLC 打标内容生成OK信号 公差比较OK，生成打标码且已经保存到B50.40中
        "S7:[S7_Connection_2]DB1001,X2.7", //23 Value:24   PC->PLC 放弃打标入库成功信号  公差比较NG，且手动输入追溯码值存数据库
        "S7:[S7_Connection_2]DB1001,X3.0", //24 Value:25   PC->PLC 打标前打标内容核实OK信号
        "S7:[S7_Connection_2]DB1001,X3.1", //25 Value:26   PC->PLC 打标前打标内容核实NG信号
        "S7:[S7_Connection_2]DB1001,X3.2", //26 Value:27   PC->PLC 终止打标信号
        "S7:[S7_Connection_2]DB1001,X3.3", //27 Value:28   PC->PLC 
        "S7:[S7_Connection_2]DB1001,X3.4", //28 Value:29   PC->PLC 
		"S7:[S7_Connection_2]DB1001,X3.5", //29 Value:30   PC->PLC
		"S7:[S7_Connection_2]DB1001,X3.6", //30 Value:31   PC->PLC
		"S7:[S7_Connection_2]DB1001,X3.7", //31 Value:32   PC->PLC
        "S7:[S7_Connection_2]DB1001,INT4", //32 Value:33   PLC->PC 2Byte-保存数字,数字即为当前需要操作的状态label
        // 5-44 (length-80)->(length-41)    原始码值保存地址(以Byte保存)(用24，预留16-每个码值一共40Byte) PC->PLC
        //序号：length-80->length-41 Value:length-80+1->length-41+1
        "S7:[S7_Connection_2]DB1001,B50",/*5*/"S7:[S7_Connection_2]DB1001,B51","S7:[S7_Connection_2]DB1001,B52","S7:[S7_Connection_2]DB1001,B53","S7:[S7_Connection_2]DB1001,B54","S7:[S7_Connection_2]DB1001,B55","S7:[S7_Connection_2]DB1001,B56","S7:[S7_Connection_2]DB1001,B57","S7:[S7_Connection_2]DB1001,B58","S7:[S7_Connection_2]DB1001,B59",
        "S7:[S7_Connection_2]DB1001,B60",/*15*/"S7:[S7_Connection_2]DB1001,B61","S7:[S7_Connection_2]DB1001,B62","S7:[S7_Connection_2]DB1001,B63","S7:[S7_Connection_2]DB1001,B64","S7:[S7_Connection_2]DB1001,B65","S7:[S7_Connection_2]DB1001,B66","S7:[S7_Connection_2]DB1001,B67","S7:[S7_Connection_2]DB1001,B68","S7:[S7_Connection_2]DB1001,B69",
        "S7:[S7_Connection_2]DB1001,B70",/*25*/"S7:[S7_Connection_2]DB1001,B71","S7:[S7_Connection_2]DB1001,B72","S7:[S7_Connection_2]DB1001,B73","S7:[S7_Connection_2]DB1001,B74","S7:[S7_Connection_2]DB1001,B75","S7:[S7_Connection_2]DB1001,B76","S7:[S7_Connection_2]DB1001,B77","S7:[S7_Connection_2]DB1001,B78","S7:[S7_Connection_2]DB1001,B79",
        "S7:[S7_Connection_2]DB1001,B80",/*35*/"S7:[S7_Connection_2]DB1001,B81","S7:[S7_Connection_2]DB1001,B82","S7:[S7_Connection_2]DB1001,B83","S7:[S7_Connection_2]DB1001,B84","S7:[S7_Connection_2]DB1001,B85","S7:[S7_Connection_2]DB1001,B86","S7:[S7_Connection_2]DB1001,B87","S7:[S7_Connection_2]DB1001,B88","S7:[S7_Connection_2]DB1001,B89",
        //45-84 (length-40)->(length-1)
        //序号：length-40->length-1 Value:length-40+1->length-1+1
        "S7:[S7_Connection_2]DB1001,B8",/*45*/"S7:[S7_Connection_2]DB1001,B9","S7:[S7_Connection_2]DB1001,B10","S7:[S7_Connection_2]DB1001,B11","S7:[S7_Connection_2]DB1001,B12","S7:[S7_Connection_2]DB1001,B13","S7:[S7_Connection_2]DB1001,B14","S7:[S7_Connection_2]DB1001,B15","S7:[S7_Connection_2]DB1001,B16","S7:[S7_Connection_2]DB1001,B17",
        "S7:[S7_Connection_2]DB1001,B18",/*55*/"S7:[S7_Connection_2]DB1001,B19","S7:[S7_Connection_2]DB1001,B20","S7:[S7_Connection_2]DB1001,B21","S7:[S7_Connection_2]DB1001,B22","S7:[S7_Connection_2]DB1001,B23","S7:[S7_Connection_2]DB1001,B24","S7:[S7_Connection_2]DB1001,B25","S7:[S7_Connection_2]DB1001,B26","S7:[S7_Connection_2]DB1001,B27",
        "S7:[S7_Connection_2]DB1001,B28",/*65*/"S7:[S7_Connection_2]DB1001,B29","S7:[S7_Connection_2]DB1001,B30","S7:[S7_Connection_2]DB1001,B31","S7:[S7_Connection_2]DB1001,B32","S7:[S7_Connection_2]DB1001,B33","S7:[S7_Connection_2]DB1001,B34","S7:[S7_Connection_2]DB1001,B35","S7:[S7_Connection_2]DB1001,B36","S7:[S7_Connection_2]DB1001,B37",
        "S7:[S7_Connection_2]DB1001,B38",/*75*/"S7:[S7_Connection_2]DB1001,B39","S7:[S7_Connection_2]DB1001,B40","S7:[S7_Connection_2]DB1001,B41","S7:[S7_Connection_2]DB1001,B42","S7:[S7_Connection_2]DB1001,B43","S7:[S7_Connection_2]DB1001,B44","S7:[S7_Connection_2]DB1001,B45","S7:[S7_Connection_2]DB1001,B46","S7:[S7_Connection_2]DB1001,B47"
        };// PLC发送的码值保存地址(以Byte发送) PLC->PC 用数据类型或的方式
        // uint8[]的类型
        private string[] ITEM_DBblock_Trigger = { 
        //PLC->PC 0.0-1.7
        "S7:[S7_Connection_2]DB1001,X0.0", //0  Value:1    PLC->PC 原始码OK信号
        "S7:[S7_Connection_2]DB1001,X0.1", //1  Value:2    PLC->PC 开班检测系统确认OK信号
        "S7:[S7_Connection_2]DB1001,X0.2", //2  Value:3    PLC->PC MCC检测完成信号
        "S7:[S7_Connection_2]DB1001,X0.3", //3  Value:4    PLC->PC 数据读取请求信号
		"S7:[S7_Connection_2]DB1001,X0.4", //4  Value:5    PLC->PC
		"S7:[S7_Connection_2]DB1001,X0.5", //5  Value:6    PLC->PC 
		"S7:[S7_Connection_2]DB1001,X0.6", //6  Value:7    PLC->PC 
		"S7:[S7_Connection_2]DB1001,X0.7", //7  Value:8    PLC->PC 
        "S7:[S7_Connection_2]DB1001,X1.0", //8  Value:9    PLC->PC PLC第1次回传码值OK信号
        "S7:[S7_Connection_2]DB1001,X1.1", //9  Value:10   PLC->PC PLC第2次回传码值OK信号
		"S7:[S7_Connection_2]DB1001,X1.2", //10 Value:11   PLC->PC
		"S7:[S7_Connection_2]DB1001,X1.3", //11 Value:12   PLC->PC
		"S7:[S7_Connection_2]DB1001,X1.4", //12 Value:13   PLC->PC 状态改变信号:由0-1或由1-0都去读B4
		"S7:[S7_Connection_2]DB1001,X1.5", //13 Value:14   PLC->PC 状态故障信号
		"S7:[S7_Connection_2]DB1001,X1.6", //14 Value:15   PLC->PC
		"S7:[S7_Connection_2]DB1001,X1.7", //15 Value:16   PLC->PC
        //PC->PLC 2.0-3.7                     
        "S7:[S7_Connection_2]DB1001,X2.0", //16 Value:17   PC->PLC 上位机准备OK信号-开机完成且OPC_items插入完毕后
        "S7:[S7_Connection_2]DB1001,X2.1", //17 Value:18   PC->PLC 上位机准备OK信号-每隔500ms向PLC写一个true
        "S7:[S7_Connection_2]DB1001,X2.2", //18 Value:19   PC->PLC 上位机工作Busy信号，每次进入On_DataChanged()后，对opc变化的量进行处理时,为true,执行结束后变为false
		"S7:[S7_Connection_2]DB1001,X2.3", //19 Value:20   PC->PLC 数据读取开始信号 PC收到读信号
        "S7:[S7_Connection_2]DB1001,X2.4", //20 Value:21   PC->PLC 公差比较OK信号(和NG信号分开)
        "S7:[S7_Connection_2]DB1001,X2.5", //21 Value:22   PC->PLC 读取报告完成信号
        "S7:[S7_Connection_2]DB1001,X2.6", //22 Value:23   PC->PLC 打标内容生成OK信号 公差比较OK，生成打标码且已经保存到B50.40中
        "S7:[S7_Connection_2]DB1001,X2.7", //23 Value:24   PC->PLC 放弃打标入库成功信号  公差比较NG，且手动输入追溯码值存数据库
        "S7:[S7_Connection_2]DB1001,X3.0", //24 Value:25   PC->PLC 打标前打标内容核实OK信号
        "S7:[S7_Connection_2]DB1001,X3.1", //25 Value:26   PC->PLC 打标前打标内容核实NG信号
        "S7:[S7_Connection_2]DB1001,X3.2", //26 Value:27   PC->PLC 终止打标信号
        "S7:[S7_Connection_2]DB1001,X3.3", //27 Value:28   PC->PLC 
        "S7:[S7_Connection_2]DB1001,X3.4", //28 Value:29   PC->PLC 
		"S7:[S7_Connection_2]DB1001,X3.5", //29 Value:30   PC->PLC
		"S7:[S7_Connection_2]DB1001,X3.6", //30 Value:31   PC->PLC
		"S7:[S7_Connection_2]DB1001,X3.7", //31 Value:32   PC->PLC
        "S7:[S7_Connection_2]DB1001,INT4", //32 Value:33   PLC->PC
        "S7:[S7_Connection_2]DB1001,INT5" //33 Value:34   PLC->PC
                                                };
        //"S7:[S7_Connection_2]DB1001,B50.40",
        //"S7:[S7_Connection_2]DB1001,B8.40"                                        
        private string[] ITEM_DBblock_Mark = { 
                                          "S7:[S7_Connection_2]DB1001,B50","S7:[S7_Connection_2]DB1001,B51","S7:[S7_Connection_2]DB1001,B52","S7:[S7_Connection_2]DB1001,B53","S7:[S7_Connection_2]DB1001,B54","S7:[S7_Connection_2]DB1001,B55","S7:[S7_Connection_2]DB1001,B56","S7:[S7_Connection_2]DB1001,B57","S7:[S7_Connection_2]DB1001,B58","S7:[S7_Connection_2]DB1001,B59",
                                          "S7:[S7_Connection_2]DB1001,B60","S7:[S7_Connection_2]DB1001,B61","S7:[S7_Connection_2]DB1001,B62","S7:[S7_Connection_2]DB1001,B63","S7:[S7_Connection_2]DB1001,B64","S7:[S7_Connection_2]DB1001,B65","S7:[S7_Connection_2]DB1001,B66","S7:[S7_Connection_2]DB1001,B67","S7:[S7_Connection_2]DB1001,B68","S7:[S7_Connection_2]DB1001,B69",
                                          "S7:[S7_Connection_2]DB1001,B70","S7:[S7_Connection_2]DB1001,B71","S7:[S7_Connection_2]DB1001,B72","S7:[S7_Connection_2]DB1001,B73","S7:[S7_Connection_2]DB1001,B74","S7:[S7_Connection_2]DB1001,B75","S7:[S7_Connection_2]DB1001,B76","S7:[S7_Connection_2]DB1001,B77","S7:[S7_Connection_2]DB1001,B78","S7:[S7_Connection_2]DB1001,B79",
                                          "S7:[S7_Connection_2]DB1001,B80","S7:[S7_Connection_2]DB1001,B81","S7:[S7_Connection_2]DB1001,B82","S7:[S7_Connection_2]DB1001,B83","S7:[S7_Connection_2]DB1001,B84","S7:[S7_Connection_2]DB1001,B85","S7:[S7_Connection_2]DB1001,B86","S7:[S7_Connection_2]DB1001,B87","S7:[S7_Connection_2]DB1001,B88","S7:[S7_Connection_2]DB1001,B89",
                                          //
                                          "S7:[S7_Connection_2]DB1001,B8","S7:[S7_Connection_2]DB1001,B9","S7:[S7_Connection_2]DB1001,B10","S7:[S7_Connection_2]DB1001,B11","S7:[S7_Connection_2]DB1001,B12","S7:[S7_Connection_2]DB1001,B13","S7:[S7_Connection_2]DB1001,B14","S7:[S7_Connection_2]DB1001,B15","S7:[S7_Connection_2]DB1001,B16","S7:[S7_Connection_2]DB1001,B17",
                                          "S7:[S7_Connection_2]DB1001,B18","S7:[S7_Connection_2]DB1001,B19","S7:[S7_Connection_2]DB1001,B20","S7:[S7_Connection_2]DB1001,B21","S7:[S7_Connection_2]DB1001,B22","S7:[S7_Connection_2]DB1001,B23","S7:[S7_Connection_2]DB1001,B24","S7:[S7_Connection_2]DB1001,B25","S7:[S7_Connection_2]DB1001,B26","S7:[S7_Connection_2]DB1001,B27",
                                          "S7:[S7_Connection_2]DB1001,B28","S7:[S7_Connection_2]DB1001,B29","S7:[S7_Connection_2]DB1001,B30","S7:[S7_Connection_2]DB1001,B31","S7:[S7_Connection_2]DB1001,B32","S7:[S7_Connection_2]DB1001,B33","S7:[S7_Connection_2]DB1001,B34","S7:[S7_Connection_2]DB1001,B35","S7:[S7_Connection_2]DB1001,B36","S7:[S7_Connection_2]DB1001,B37",
                                          "S7:[S7_Connection_2]DB1001,B38","S7:[S7_Connection_2]DB1001,B39","S7:[S7_Connection_2]DB1001,B40","S7:[S7_Connection_2]DB1001,B41","S7:[S7_Connection_2]DB1001,B42","S7:[S7_Connection_2]DB1001,B43","S7:[S7_Connection_2]DB1001,B44","S7:[S7_Connection_2]DB1001,B45","S7:[S7_Connection_2]DB1001,B46","S7:[S7_Connection_2]DB1001,B47"   
                                             };

        #endregion

        #endregion

        #region opc-constants for the fast visualization items
        private const int NUM_FASTVISU_ITEMS = 9;
        private const int CLNT_HANDLE_GRP_FVISU = 2;
        private const string GRP_NAME_FVISU = "FastVisualization";

        // read only items
        private const string ITEM_SENDBIT = "S7:[S7-Verbindung_1]DB15,X0.0,1";	// SendBit in DB15, DBX0.0
        private const string ITEM_BUFFULL1 = "S7:[S7-Verbindung_1]DB25,X0.0,1";	// full-status of alternation buffer 1
        private const string ITEM_BUFOV1 = "S7:[S7-Verbindung_1]DB25,X0.2,1";	// ov-status of alternation buffer 1
        private const string ITEM_ENTRIESBUF1 = "S7:[S7-Verbindung_1]DB25,W2,1";		// entries in buffer1
        private const string ITEM_BUFFULL2 = "S7:[S7-Verbindung_1]DB26,X0.0,1";	// full-status of alternation buffer 2
        private const string ITEM_BUFOV2 = "S7:[S7-Verbindung_1]DB26,X0.2,1";	// ov-status of alternation buffer 2
        private const string ITEM_ENTRIESBUF2 = "S7:[S7-Verbindung_1]DB26,W2,1";		// entries in buffer2
        private const string ITEM_TIMEDIFF = "AS 400.CPU 414-2 DP.TimeDiff";		// MD50
        // write only item
        private const string ITEM_ACKBIT = "AS 400.CPU 414-2 DP.AckBit";				// M2.0 --> acknowledge bit is write only
        #endregion

        #region opc-constants for slow visualization
        private const int NUM_SLOWVISU_ITEMS = 3;
        private const int CLNT_HNDL_GRP_SVISU = 3;
        private const string GRP_NAME_SVISU = "SlowVisualization";

        // item-ids
        private const string ITEM_START_SIM = "AS 400.CPU 414-2 DP.FillBuffer";			// to start the filling of the buffers
        private const string ITEM_STRUCT_LEN = "AS 400.CPU 414-2 DP.StructLength";		// to get the length of the send structure
        private const string ITEM_OB35FREQ = "AS 400.CPU 414-2 DP.OB35Frequency";	// MW74
        #endregion


        #endregion

        #endregion

        #region fields

        #region opc-manager fields
        // OPC服务器本身操作
        private OPCServerManagement m_mgtServer = null;
        // 对OPC组的操作
        private OPCGroupManagement m_mgtGrpDBblock = null;
        private OPCGroupManagement m_mgtGrpSlow = null;
        private OPCGroupManagement m_mgtGrpFast = null;
        #endregion

        #region itemextender-lists
        private OPCItemExtenderList m_itmExtDBblockList = null;
        #endregion

        #region statistic fields
        // this fields are necessary to calculate the mean values
        System.UInt32 m_meanSendTime = 0;
        System.UInt32 m_numberTelegrams = 0;
        int m_meanDataRate = 0;
        #endregion
        private IContainer components;

        #region UI fields

        private System.Windows.Forms.MenuItem DelSingleDBMenuItem;
        private System.Windows.Forms.MenuItem ConnMenu;
        private System.Windows.Forms.MenuItem ShowTracingMenuItem;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuItem ConnMenuItem;
        private System.Windows.Forms.MenuItem DisConnMenuItem;
        private System.Windows.Forms.MenuItem ClrSndBitMenuItem;
        private System.Windows.Forms.MenuItem ClrMeanVMenuItem;
        private System.Windows.Forms.MenuItem UpdtRtsMenuItem;
        private System.Windows.Forms.MenuItem ExitMenuItem;
        private System.Windows.Forms.MenuItem ShowRecMenuItem;
        private System.Windows.Forms.MenuItem DelRecMenuItem;
        private System.Windows.Forms.MenuItem InfoMenuItem;
        #endregion

        #region other dialog classes
        private TraceToolBox m_traceToolBox;
        #endregion

        #region connection to the database
        private DBConnector m_DBConnector;
        #endregion

        #region update rates
        private int m_updRateDBblock = 0;
        private int m_updRateFVisu = 0;
        private int m_updRateSVisu = 0;
        #endregion

        #region field necessary to wait for pending thread (saving data to database)
        private WaitHandle m_waitHandle;
        #endregion

        #region control flags to determines which database is available
        private bool m_AccessAvailable = true;
        private bool m_XMLFileAvailable = true;
        private GroupBox groupBox2;
        private Label label_SQLDBConState;
        private Label label_DBStatus;
        private GroupBox groupBox1;
        private Label label_OPCProcID;
        private Button btn_OPCCon;
        private TabControl tabControl2;
        private TabPage tabPage4;
        private TextBox tbx_IP4;
        private Label label36;
        private TextBox tbx_IP3;
        private Label label35;
        private TextBox tbx_IP2;
        private Label label34;
        private TextBox tbx_IP1;
        private Button btn_RecordServerIP;
        private TabPage tabPage5;
        private RadioButton rbt_ConRemoteOPC;
        private TabPage tabPage6;
        private PictureBox pictureBox1;
        private Label label_DBName;
        private Button btn_AdvancedFunc;
        private Button btn_DataInterface;
        private Button btn_logout;
        private Button btn_login;
        private Label label_Status4;
        private Label label_Status3;
        private Label label_Status2;
        private Label label_Status1;
        private Button button3;
        private Label label_Status5;
        private Label label_Status7;
        private Button btn_InquireDataInterface;
        private System.Windows.Forms.Timer timer_SendOK;
        private TabPage tabPage7;
        public DataGridView dgvDatFromZessiFile;
        private Button btn_clearsig;
        private Label label_Status6;
        private Label label_Status8;
        private SplitContainer splitContainer1;
        private GroupBox groupBox4;
        private Label label57;
        private Label label_idx1_X00;
        private TextBox tbx_PLC1stSendBKCode;
        private Label label60;
        private Label label_idx13_X14;
        private Label label_idx2_X01;
        private Label label63;
        private Label label64;
        private Label label_idx14_X15;
        private Label label_idx8_X10;
        private Label label67;
        private Label label68;
        private TextBox tbx_PLC2ndSendBKCode;
        private Label label_idx9_X11;
        private Label label72;
        private Label label_idx4_X03;
        private Label label59;
        private Label label58;
        private Label label55;
        private TextBox tbx_idx33_INT4;
        private GroupBox groupBox3;
        private GroupBox groupBox7;
        private Label label61;
        private Label label_idx25_X30;
        private Label label65;
        private Label label_idx29_X34;
        private Label label_idx26_X31;
        private Label label71;
        private Label label69;
        private Label label_idx27_X32;
        private GroupBox groupBox6;
        private Label label42;
        private Label label_idx21_X24;
        private Label label45;
        private Label label_idx28_X33;
        private Label label48;
        private Label label_idx22_X25;
        private Label label49;
        private Label label_idx23_X26;
        private Label label44;
        private Label label_idx24_X27;
        private GroupBox groupBox5;
        private Label label39;
        private Label label_idx20_X23;
        private Label label41;
        private Label label_idx18_X21;
        private Label label40;
        private Label label_idx17_X20;
        private Label label56;
        private TextBox tbx_OriginalCode;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label_idx7_X06;
        private Label label46;
        private VScrollBar vScrollBar1;
        private HScrollBar hScrollBar1;
        private TabPage tabPage8;
        private PictureBox pictureBox2;
        private TabPage tabPage9;
        private PictureBox pictureBox3;
        private TabPage tabPage10;
        private PictureBox pictureBox4;
        private GroupBox groupBox8;
        private Button button4;
        private GroupBox grp_PTA3D_16;
        private TextBox tbx_PTA3D_16_Z;
        private Label label43;
        private Label label52;
        private Label label53;
        private TextBox tbx_PTA3D_16_X;
        private TextBox tbx_PTA3D_16_Y;
        private GroupBox grp_PTERH_25;
        private TextBox tbx_PTERH_25_Z;
        private Label label105;
        private Label label106;
        private Label label107;
        private TextBox tbx_PTERH_25_X;
        private TextBox tbx_PTERH_25_Y;
        private GroupBox grp_PTPLH_23;
        private TextBox tbx_PTPLH_23_Z;
        private Label label102;
        private Label label103;
        private Label label104;
        private TextBox tbx_PTPLH_23_X;
        private TextBox tbx_PTPLH_23_Y;
        private GroupBox grp_PTELH_21;
        private TextBox tbx_PTELH_21_Z;
        private Label label99;
        private Label label100;
        private Label label101;
        private TextBox tbx_PTELH_21_X;
        private TextBox tbx_PTELH_21_Y;
        private GroupBox grp_PTA4D_18;
        private TextBox tbx_PTA4D_18_Z;
        private Label label96;
        private Label label97;
        private Label label98;
        private TextBox tbx_PTA4D_18_X;
        private TextBox tbx_PTA4D_18_Y;
        private GroupBox grp_PTHLHG_8;
        private TextBox tbx_PTHLHG_8_Z;
        private Label label93;
        private Label label94;
        private Label label95;
        private TextBox tbx_PTHLHG_8_X;
        private TextBox tbx_PTHLHG_8_Y;
        private GroupBox grp_PTDLH_19;
        private TextBox tbx_PTDLH_19_Z;
        private Label label90;
        private Label label91;
        private Label label92;
        private TextBox tbx_PTDLH_19_X;
        private TextBox tbx_PTDLH_19_Y;
        private GroupBox grp_PTA21_14;
        private TextBox tbx_PTA21_14_Z;
        private Label label87;
        private Label label88;
        private Label label89;
        private TextBox tbx_PTA21_14_X;
        private TextBox tbx_PTA21_14_Y;
        private GroupBox grp_PTNLH_22;
        private TextBox tbx_PTNLH_22_Z;
        private Label label84;
        private Label label85;
        private Label label86;
        private TextBox tbx_PTNLH_22_X;
        private TextBox tbx_PTNLH_22_Y;
        private GroupBox groupBox14;
        private TextBox textBox16;
        private Label label81;
        private Label label82;
        private Label label83;
        private TextBox textBox17;
        private TextBox textBox18;
        private GroupBox grp_PTA11_15;
        private TextBox tbx_PTA11_15_Z;
        private Label label78;
        private Label label79;
        private Label label80;
        private TextBox tbx_PTA11_15_X;
        private TextBox tbx_PTA11_15_Y;
        private GroupBox grp_PTDRH_20;
        private Label label75;
        private Label label76;
        private Label label77;
        private TextBox tbx_PTDRH_20_X;
        private TextBox tbx_PTDRH_20_Y;
        private GroupBox grp_PTHRHG_7;
        private TextBox tbx_PTHRHG_7_Z;
        private Label label70;
        private Label label73;
        private Label label74;
        private TextBox tbx_PTHRHG_7_X;
        private TextBox tbx_PTHRHG_7_Y;
        private GroupBox grp_PTPRH_24;
        private TextBox tbx_PTPRH_24_Z;
        private Label label54;
        private Label label62;
        private Label label66;
        private TextBox tbx_PTPRH_24_X;
        private TextBox tbx_PTPRH_24_Y;
        private GroupBox grp_PTGRH_2;
        private TextBox tbx_PTGRH_2_Z;
        private Label label117;
        private Label label118;
        private Label label119;
        private TextBox tbx_PTGRH_2_X;
        private TextBox tbx_PTGRH_2_Y;
        private GroupBox grp_PTFRHF_11;
        private TextBox tbx_PTFRHF_11_Z;
        private Label label114;
        private Label label115;
        private Label label116;
        private TextBox tbx_PTFRHF_11_X;
        private TextBox tbx_PTFRHF_11_Y;
        private GroupBox grp_PTGLH_1;
        private TextBox tbx_PTGLH_1_Z;
        private Label label111;
        private Label label112;
        private Label label113;
        private TextBox tbx_PTGLH_1_X;
        private TextBox tbx_PTGLH_1_Y;
        private GroupBox grp_PTFLHF_12;
        private TextBox tbx_PTFLHF_12_Z;
        private Label label108;
        private Label label109;
        private Label label110;
        private TextBox tbx_PTFLHF_12_X;
        private TextBox tbx_PTFLHF_12_Y;
        private GroupBox grp_PTILH_9;
        private TextBox tbx_PTILH_9_Z;
        private Label label126;
        private Label label127;
        private Label label128;
        private TextBox tbx_PTILH_9_X;
        private TextBox tbx_PTILH_9_Y;
        private GroupBox grp_PTC_3;
        private TextBox tbx_PTC_3_Z;
        private Label label123;
        private Label label124;
        private Label label125;
        private TextBox tbx_PTC_3_X;
        private TextBox tbx_PTC_3_Y;
        private GroupBox grp_PTA22_5;
        private TextBox tbx_PTA22_5_Z;
        private Label label138;
        private Label label139;
        private Label label140;
        private TextBox tbx_PTA22_5_X;
        private TextBox tbx_PTA22_5_Y;
        private GroupBox groupBox32;
        private TextBox textBox70;
        private Label label135;
        private Label label136;
        private Label label137;
        private TextBox textBox71;
        private TextBox textBox72;
        private GroupBox grp_PTJLH_13;
        private TextBox tbx_PTJLH_13_Z;
        private Label label132;
        private Label label133;
        private Label label134;
        private TextBox tbx_PTJLH_13_X;
        private TextBox tbx_PTJLH_13_Y;
        private GroupBox grp_PTB_4;
        private TextBox tbx_PTB_4_Z;
        private Label label147;
        private Label label148;
        private Label label149;
        private TextBox tbx_PTB_4_X;
        private TextBox tbx_PTB_4_Y;
        private GroupBox groupBox35;
        private TextBox textBox79;
        private Label label144;
        private Label label145;
        private Label label146;
        private TextBox textBox80;
        private TextBox textBox81;
        private GroupBox groupBox27;
        private TextBox textBox55;
        private Label label120;
        private Label label121;
        private Label label122;
        private TextBox textBox56;
        private TextBox textBox57;
        private GroupBox grp_PTIRH_10;
        private TextBox tbx_PTIRH_10_Z;
        private Label label150;
        private Label label151;
        private Label label152;
        private TextBox tbx_PTIRH_10_X;
        private TextBox tbx_PTIRH_10_Y;
        private GroupBox grp_PTA12_6;
        private TextBox tbx_PTA12_6_Z;
        private Label label165;
        private Label label166;
        private Label label167;
        private TextBox tbx_PTA12_6_X;
        private TextBox tbx_PTA12_6_Y;
        private GroupBox groupBox41;
        private TextBox textBox97;
        private Label label162;
        private Label label163;
        private Label label164;
        private TextBox textBox98;
        private TextBox textBox99;
        private GroupBox grp_PTJRH_17;
        private TextBox tbx_PTJRH_17_Z;
        private Label label159;
        private Label label160;
        private Label label161;
        private TextBox tbx_PTJRH_17_X;
        private TextBox tbx_PTJRH_17_Y;
        private GroupBox groupBox39;
        private TextBox textBox91;
        private Label label156;
        private Label label157;
        private Label label158;
        private TextBox textBox92;
        private TextBox textBox93;
        private GroupBox groupBox38;
        private TextBox textBox88;
        private Label label153;
        private Label label154;
        private Label label155;
        private TextBox textBox89;
        private TextBox textBox90;
        private ToolTip toolTip1;
        private Label label_idx32_X37;
        private Label label169;
        private Label label170;
        private Label label_idx31_X36;
        private Label label_idx8_X07;
        private Label label171;
        private GroupBox grp_PTA3D;
        private TextBox tbx_PTA3D_Z;
        private Label label168;
        private Label label172;
        private Label label173;
        private TextBox tbx_PTA3D_X;
        private TextBox tbx_PTA3D_Y;
        private GroupBox grp_PTA11;
        private TextBox tbx_PTA11_Z;
        private Label label174;
        private Label label175;
        private Label label176;
        private TextBox tbx_PTA11_X;
        private TextBox tbx_PTA11_Y;
        private GroupBox grp_PTA4D;
        private TextBox tbx_PTA4D_Z;
        private Label label177;
        private Label label178;
        private Label label179;
        private TextBox tbx_PTA4D_X;
        private TextBox tbx_PTA4D_Y;
        private GroupBox grp_PTA21;
        private TextBox tbx_PTA21_Z;
        private Label label180;
        private Label label181;
        private Label label182;
        private TextBox tbx_PTA21_X;
        private TextBox tbx_PTA21_Y;
        private Label label47;
        private TextBox tbx_PTDRH_20_Z;
        private TextBox textBox14;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button btn_ClearTraceList;
        private ListBox TraceListBox_Main;
        private TabPage tabPage2;
        private Panel panel2;
        private GroupBox groupBox9;
        private GroupBox groupBox10;
        private GroupBox groupBox34;
        private TextBox textBox76;
        private Label label141;
        private Label label142;
        private Label label143;
        private TextBox textBox77;
        private TextBox textBox78;
        private GroupBox groupBox30;
        private TextBox textBox64;
        private Label label129;
        private Label label130;
        private Label label131;
        private TextBox textBox65;
        private TextBox textBox66;
        private Label label_curPartMissDetect;
        private TabPage tabPage3;
        private GroupBox groupBox11;
        private GroupBox groupBox12;
        private Button btn_ConfirmChangeUsrNamePwd;
        private Label label3;
        private Label label4;
        private TextBox tbx_username;
        private TextBox tbx_password;
        private Button btn_WriteOPCItem;
        private ComboBox cbx_TriggerSignal;
        private ComboBox cbx_MarkInfo;
        private Label label38;
        private Label label37;
        private Button btn_ReadOPCItem;
        private GroupBox DBGroupBox;
        private RadioButton DBRadioButton;
        private RadioButton XMLRadioButton;
        private RadioButton SQLDBRadioButton;
        private GroupBox SimGroupBox;
        private Label label30;
        private Label SendBitLabel;
        private Label label1;
        private Button StartSimButton;
        private Label label33;
        private Label label32;
        private Label label31;
        private TextBox tbx_OPCItemTimeStamp;
        private TextBox tbx_OPCItemQuality;
        private TextBox tbx_OPCItemValue;
        private GroupBox groupBox13;
        private Button button1;
        private Label label2;
        private Label label5;
        private TextBox tbx_DebugUsername;
        private TextBox tbx_DebugPassword;
        private Button btn_Onlyclearsig;
        private bool m_SQLAvailable = true;
        #endregion

        #endregion

        #region cbx相关操作
        public void OPCItemsAd2CBX()
        {
            cbx_MarkInfo.Items.AddRange(ITEM_DBblock_Mark);
            cbx_TriggerSignal.Items.AddRange(ITEM_DBblock_Trigger);
        }
        string CurrentSelectedOPCItem = "";
        int OPCItem_Read_Idx;
        private void cbx_TriggerSignal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentSelectedOPCItem = cbx_TriggerSignal.SelectedItem.ToString();
            OPCItem_Read_Idx = cbx_TriggerSignal.SelectedIndex;
        }
        private void cbx_MarkInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentSelectedOPCItem = cbx_MarkInfo.SelectedItem.ToString();
            OPCItem_Read_Idx = cbx_MarkInfo.SelectedIndex + ITEM_DBblock_Trigger.Length;
        }
        #endregion

        // 构造函数前实例化类及结构体
        CommonUse commonUse_Obj = new CommonUse();
        DBOperate DBOperate_Obj = new DBOperate();
        //DBOperate zessi_DBOperater = new DBOperate("B515_MCA");
        zessiFileParseOperate zessi_FileParse_Obj = new zessiFileParseOperate();
        ZessiDatFromZessiFile zessi_DatFromZessiFile_Struct = new ZessiDatFromZessiFile();
        ZessiDatFinalAdd2DB zessi_DatFinalAdd2DB_Struct = new ZessiDatFinalAdd2DB();
        // 用于在异常状态下判断当前软件状态，执行相应操作
        OPCItemProcessStatus_Self OPCItemProcessStatus_Struct = new OPCItemProcessStatus_Self();
        
        #region construction/destruction
        
        public PersistentData()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; // 防Cross-thread operation not valid

            

            try
            {
                #region 纯为了测试
                //CodeOriginal_FromExcel = "GN15-5019-A3B1707171030";CodePLCSend1st = "";CodePLCSend2nd = "";
                //字符串转化成ASCII码数组
                //byte[] CodeOriginal_FromExcel_int = System.Text.Encoding.ASCII.GetBytes(CodeOriginal_FromExcel); 
                //Convert.ToSByte(CodeOriginal_FromExcel);

                //object vt =  VarEnum.VT_ARRAY | VarEnum.VT_UI1;
                //CodePLCSend2nd = "MN15-5019-A3B1707171030";
                //CodeOriginal_FromExcel = "GN15-5019-A3B1707171030";
                //Form_SecondCodeCompare F_FSC_Dlg = new Form_SecondCodeCompare();
                //F_FSC_Dlg.Show();
                //while (!F_FSC_Dlg.IsDisposed)
                //{
                //    Application.DoEvents();
                //    this.Enabled = false;
                //}

                //while (!Form_SecondCodeCompare.m_bContinueMark_btnFlag);
                //F_FSC_Dlg.Close();

                //btn_AdvancedFunc.Enabled = true;

                // 程序初始运行时需要对数据库进行检查
                // 数据库中已经存在当天的开机码

                //On_ReadFromExcel();
                //ClearOPCSignal_OnlyPC2PLC(true);
                //CodeSaveToDB = "GN15-5019-A3B 1801221710";
                //string CodeSaveToDB_Last3Num = CodeSaveToDB.Substring(CodeSaveToDB.Length - 3, 3); // 此时的码值必须非空
                //CodeSaveToDB = (Convert.ToInt16(CodeSaveToDB_Last3Num) + 1).ToString();
                //string substr = "GN15-5019-A3B 1802082984".Substring(10,3); 
                //======180307 测试
                //string str = "GN15-5019-A3B1803071001 \0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
                //string[] aa = str.Split(new string[] { " ", "\0" }, StringSplitOptions.RemoveEmptyEntries);
                //CodeOriginal_Standard = aa[0];
                //======
                //m_Tick_Start = Environment.TickCount;
                //Thread.Sleep(6000);
                //m_Tick_Diff = Environment.TickCount - m_Tick_Start;

                #endregion

                #region // 用于在异常状态下判断当前软件状态，执行相应操作 == 初始化
                OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeGenerated = false;
                OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeHasAdded = false;
                OPCItemProcessStatus_Struct.m_OPCItemStatus_PLCHasSendBack1stMarkCode = false;
                #endregion

                // opc_items 添加初始化
                
                #region tracebox初始化
                m_traceToolBox = new TraceToolBox();
                m_traceToolBox.OnCloseClicked +=
                    new TraceToolBox.CloseButtonClickedEventHandler(m_traceToolBox_OnCloseClicked);

                if (ShowTracingMenuItem.Checked)
                {
                    //m_traceToolBox.StartPosition = FormStartPosition.Manual;
                    //m_traceToolBox.Tag = ShowTracingMenuItem.Tag.ToString();
                    //m_traceToolBox.Show();
                }

                Trace("应用程序已启动.");
                #endregion

                
                //
                #region tabpage隐藏
                this.tabPage3.Parent = null;
                this.tabPage4.Parent = null;
                this.tabPage7.Parent = null;
                #endregion

                // 先检查共享文件夹能否访问
                bool SharedFile_status = zessi_FileParse_Obj.SharedFileConnectState(@"\\192.168.0.216\VISUImages\Export\B515_MCA", "user", "user");
                if (!SharedFile_status)
                { throw new Exception("访问Zessi共享文件夹失败,请查看局域网内共享文件夹访问是否正常！"); }

                #region 数据库连接 -- 试一试能否打开
                m_DBConnector = new DBConnector("B515_MCA"); // 如果打不开的话会在这里抛出异常
                if (m_DBConnector.m_Sqlconn == null) // 如果没打开 会把连接对象变为null
                {
                    Trace("数据库连接失败，请检查数据库是否开启");
                    label_DBStatus.Text = "数据库连接失败";
                    label_SQLDBConState.BackColor = System.Drawing.Color.Red;
                    label47.Visible = true;
                }
                else
                {
                    Trace("数据库连接成功，连接数据库为：" + m_DBConnector.m_Sqlconn.Database); //m_DBConnector.m_Sqlcon
                    label_SQLDBConState.BackColor = System.Drawing.Color.Green;
                    label_DBStatus.Text = "数据库可以连接";
                    label_DBName.Text = "数据库名称:" + m_DBConnector.m_Sqlconn.Database;
                    label47.Visible = false;
                }
                m_DBConnector.Dispose();
                //m_DBConnector.OnBRCVOccured +=
                //    new DBConnector.BRCVOccuredEventHandler(m_DBConnector_OnDBblockOccured);
                //m_DBConnector.activeDB = DBConnector.DBToSave.DBNothing;

                #region  其他数据库
                // 暂不连接
                //// connect to XML DB
                //try
                //{
                //    m_DBConnector.connectXMLDB(Application.StartupPath + "\\DB\\XMLDB.xml");
                //}
                //catch(Exception ex)
                //{
                //    MessageBox.Show("Error on connecting to database!\n" + ex.Message,"Error",MessageBoxButtons.OK,
                //        MessageBoxIcon.Stop);
                //    m_XMLFileAvailable = false;
                //}

                //// connect to ACCESS DB
                //try
                //{
                //    m_DBConnector.connectAccessDB(Application.StartupPath + "\\DB\\AccessDB.mdb");
                //}
                //catch(Exception ex)
                //{
                //    MessageBox.Show("Error on connecting to database!\n" + ex.Message,"Error",MessageBoxButtons.OK,
                //        MessageBoxIcon.Stop);
                //    m_AccessAvailable = false;
                //}

                //// connect to SQL DB
                //try
                //{
                //    m_DBConnector.connectSQLDB();
                //}
                //catch(Exception ex)
                //{
                //    MessageBox.Show("Error on connecting to database!\n" + ex.Message,"Error",MessageBoxButtons.OK,
                //        MessageBoxIcon.Stop);
                //    m_SQLAvailable = false;
                //}
                #endregion

                #endregion

                DBOperate_Obj.CheckNewTable();

                // 获得标准公差; 暂时模拟; 拟修改为 在PersisData加载时就调用数据库函数, 读取最近一次保存的标准公差信息,将其赋值给 B515_Standard_Tolerance
                ZessiTolDatReadFromDB_Buffer zessiTolDatRdFromDBBuffer = new ZessiTolDatReadFromDB_Buffer();
                zessiTolDatRdFromDBBuffer.CarType = ""; zessiTolDatRdFromDBBuffer.DataTime = "";
                zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo_str = new string[zessiFileParseOperate.MeasurePoint * 3 * 2];
                zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo = new double[zessiFileParseOperate.MeasurePoint * 3 * 2];

                DBOperate_Obj.Query_ToleranceStandard_RecentDat(ref zessiTolDatRdFromDBBuffer); // 获得DB中最新的公差信息
                for (int rows_idx = 0; rows_idx < zessiFileParseOperate.MeasurePoint * 3; rows_idx++) // rows的数量会比实际数量多1个
                {
                    Form_AdvancedFunc.B515_Standard_Tolerance[2 * rows_idx] = zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo[2 * rows_idx]; //Convert.ToDouble(0.3);
                    Form_AdvancedFunc.B515_Standard_Tolerance[2 * rows_idx + 1] = zessiTolDatRdFromDBBuffer.AllMeasurePoint_ToleranceInfo[2 * rows_idx + 1];//Convert.ToDouble(-0.3);
                }
                Trace("开机:加载最近编辑公差信息成功");
                
                // 添加启动软件即检测zessi文件数量 1.若为0则无操作,若大于0,则删除或放置在2文件夹
                zessi_FileParse_Obj.CheckZessiFileNumAdSharedFloder(null);

                #region zessi文件读取测试(快)
                ////zessi_FileParse_Obj.CheckZessiFileNumAdProcessOver(); // 状态机中的文件收尾工作
                //bool m_bIfZessiFileHavProblem = false;
                //string errorstr = "";
                //zessi_FileParse_Obj.On_ReadFromZessiFile(null, dgvDatFromZessiFile,ref errorstr, ref zessi_DatFromZessiFile_Struct, ref m_bIfZessiFileHavProblem);

                //zessi_FileParse_Obj.TransferFileDatToSaveDBDat("ACA", DateTime.Now, 
                //                                                true, "实测值",
                //                                               Form_AdvancedFunc.B515_Standard_Tolerance,
                //                                               "",
                //                                               zessi_DatFromZessiFile_Struct,
                //                                               ref zessi_DatFinalAdd2DB_Struct); // 将要保存的数据全都放到zessi_DatFinalAdd2DB_Struct中
                //int cnt = DBOperate_Obj.Insert_Collect_Dat(zessi_DatFinalAdd2DB_Struct);        // 执行存入操作



                // 获得今日是否已经有过开机信号(标志位为1) 暂时模拟; 拟修改为查询数据库,表: IfOpenMachine; 包含2项:日期和标志位; 查询是否已经存在原始码信号
                //if (DBOperate_Obj.Query_OpenMachineFlag_Dat(DateTime.Now) == 1)
                //{
                //    // 需要判断开机码的长度以及正确性
                //    m_bIfHvOriginalCodeFromPLC = true;
                //    Trace("开机:今日开机码存在,开机码为:" + "开机正常");
                //}
                //else
                //{
                //    m_bIfHvOriginalCodeFromPLC = false;
                //    Trace("开机:今日开机码不存在/数量不为1");
                //}

                #endregion

                // 开启定时器 
                // -- 是否要加上自动模式的切换,只有按下自动模式-开始运行,定时器才开始计时,PLC才能收到软件工作正常的信号
                this.timer_SendOK.Start();
            }
            catch (Exception ex) // 在此接收到向上抛出的异常
            {
                MessageBox.Show(ex.Message + "\n启动时异常:请检查数据库是否正常连接、Zessi系统共享文件能否正常访问");
            }
        }

        #region Dispose(bool disposing) -- frees objects
        /// <summary> frees objects </summary>
        protected override void Dispose(bool disposing)
        {
            this.Cursor = Cursors.WaitCursor;
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            if (m_waitHandle != null)
            {
                // wait for running worker thread
                Trace("Waiting for the end of the thread to save the data...");
                m_waitHandle.WaitOne();
                Trace("End of waiting");
            }

            try
            {
                disconnectFromOPCServer();

                m_DBConnector.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Disconnet from Server failed with: " + ex.ToString(), "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;
            base.Dispose(disposing);
        }

        #endregion

        #region Vom Windows Form-Designer generierter Code 找机会移动到Designer中
        /// <summary>
        /// Erforderliche Methode f黵 die Designerunterst黷zung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor ge鋘dert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersistentData));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.ShowTracingMenuItem = new System.Windows.Forms.MenuItem();
            this.UpdtRtsMenuItem = new System.Windows.Forms.MenuItem();
            this.ConnMenu = new System.Windows.Forms.MenuItem();
            this.ConnMenuItem = new System.Windows.Forms.MenuItem();
            this.DisConnMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.ClrSndBitMenuItem = new System.Windows.Forms.MenuItem();
            this.ClrMeanVMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.ShowRecMenuItem = new System.Windows.Forms.MenuItem();
            this.DelSingleDBMenuItem = new System.Windows.Forms.MenuItem();
            this.DelRecMenuItem = new System.Windows.Forms.MenuItem();
            this.InfoMenuItem = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Onlyclearsig = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btn_clearsig = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.btn_logout = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbt_ConRemoteOPC = new System.Windows.Forms.RadioButton();
            this.tbx_IP4 = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.tbx_IP3 = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.tbx_IP2 = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.tbx_IP1 = new System.Windows.Forms.TextBox();
            this.btn_RecordServerIP = new System.Windows.Forms.Button();
            this.label_OPCProcID = new System.Windows.Forms.Label();
            this.btn_OPCCon = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label_DBName = new System.Windows.Forms.Label();
            this.label_SQLDBConState = new System.Windows.Forms.Label();
            this.label_DBStatus = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.label_curPartMissDetect = new System.Windows.Forms.Label();
            this.grp_PTA21 = new System.Windows.Forms.GroupBox();
            this.tbx_PTA21_Z = new System.Windows.Forms.TextBox();
            this.label180 = new System.Windows.Forms.Label();
            this.label181 = new System.Windows.Forms.Label();
            this.label182 = new System.Windows.Forms.Label();
            this.tbx_PTA21_X = new System.Windows.Forms.TextBox();
            this.tbx_PTA21_Y = new System.Windows.Forms.TextBox();
            this.grp_PTA4D = new System.Windows.Forms.GroupBox();
            this.tbx_PTA4D_Z = new System.Windows.Forms.TextBox();
            this.label177 = new System.Windows.Forms.Label();
            this.label178 = new System.Windows.Forms.Label();
            this.label179 = new System.Windows.Forms.Label();
            this.tbx_PTA4D_X = new System.Windows.Forms.TextBox();
            this.tbx_PTA4D_Y = new System.Windows.Forms.TextBox();
            this.grp_PTA11 = new System.Windows.Forms.GroupBox();
            this.tbx_PTA11_Z = new System.Windows.Forms.TextBox();
            this.label174 = new System.Windows.Forms.Label();
            this.label175 = new System.Windows.Forms.Label();
            this.label176 = new System.Windows.Forms.Label();
            this.tbx_PTA11_X = new System.Windows.Forms.TextBox();
            this.tbx_PTA11_Y = new System.Windows.Forms.TextBox();
            this.grp_PTA3D = new System.Windows.Forms.GroupBox();
            this.tbx_PTA3D_Z = new System.Windows.Forms.TextBox();
            this.label168 = new System.Windows.Forms.Label();
            this.label172 = new System.Windows.Forms.Label();
            this.label173 = new System.Windows.Forms.Label();
            this.tbx_PTA3D_X = new System.Windows.Forms.TextBox();
            this.tbx_PTA3D_Y = new System.Windows.Forms.TextBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.label_Status8 = new System.Windows.Forms.Label();
            this.btn_InquireDataInterface = new System.Windows.Forms.Button();
            this.label_Status7 = new System.Windows.Forms.Label();
            this.label_Status5 = new System.Windows.Forms.Label();
            this.label_Status6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label_Status4 = new System.Windows.Forms.Label();
            this.label_Status3 = new System.Windows.Forms.Label();
            this.label_Status2 = new System.Windows.Forms.Label();
            this.label_Status1 = new System.Windows.Forms.Label();
            this.btn_DataInterface = new System.Windows.Forms.Button();
            this.btn_AdvancedFunc = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.grp_PTERH_25 = new System.Windows.Forms.GroupBox();
            this.tbx_PTERH_25_Z = new System.Windows.Forms.TextBox();
            this.label105 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.tbx_PTERH_25_X = new System.Windows.Forms.TextBox();
            this.tbx_PTERH_25_Y = new System.Windows.Forms.TextBox();
            this.grp_PTPLH_23 = new System.Windows.Forms.GroupBox();
            this.tbx_PTPLH_23_Z = new System.Windows.Forms.TextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.tbx_PTPLH_23_X = new System.Windows.Forms.TextBox();
            this.tbx_PTPLH_23_Y = new System.Windows.Forms.TextBox();
            this.grp_PTELH_21 = new System.Windows.Forms.GroupBox();
            this.tbx_PTELH_21_Z = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.tbx_PTELH_21_X = new System.Windows.Forms.TextBox();
            this.tbx_PTELH_21_Y = new System.Windows.Forms.TextBox();
            this.grp_PTA4D_18 = new System.Windows.Forms.GroupBox();
            this.tbx_PTA4D_18_Z = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.tbx_PTA4D_18_X = new System.Windows.Forms.TextBox();
            this.tbx_PTA4D_18_Y = new System.Windows.Forms.TextBox();
            this.grp_PTHLHG_8 = new System.Windows.Forms.GroupBox();
            this.tbx_PTHLHG_8_Z = new System.Windows.Forms.TextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.tbx_PTHLHG_8_X = new System.Windows.Forms.TextBox();
            this.tbx_PTHLHG_8_Y = new System.Windows.Forms.TextBox();
            this.grp_PTDLH_19 = new System.Windows.Forms.GroupBox();
            this.tbx_PTDLH_19_Z = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.tbx_PTDLH_19_X = new System.Windows.Forms.TextBox();
            this.tbx_PTDLH_19_Y = new System.Windows.Forms.TextBox();
            this.grp_PTA21_14 = new System.Windows.Forms.GroupBox();
            this.tbx_PTA21_14_Z = new System.Windows.Forms.TextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.tbx_PTA21_14_X = new System.Windows.Forms.TextBox();
            this.tbx_PTA21_14_Y = new System.Windows.Forms.TextBox();
            this.grp_PTNLH_22 = new System.Windows.Forms.GroupBox();
            this.tbx_PTNLH_22_Z = new System.Windows.Forms.TextBox();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.tbx_PTNLH_22_X = new System.Windows.Forms.TextBox();
            this.tbx_PTNLH_22_Y = new System.Windows.Forms.TextBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.grp_PTA11_15 = new System.Windows.Forms.GroupBox();
            this.tbx_PTA11_15_Z = new System.Windows.Forms.TextBox();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.tbx_PTA11_15_X = new System.Windows.Forms.TextBox();
            this.tbx_PTA11_15_Y = new System.Windows.Forms.TextBox();
            this.grp_PTDRH_20 = new System.Windows.Forms.GroupBox();
            this.tbx_PTDRH_20_Z = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.tbx_PTDRH_20_X = new System.Windows.Forms.TextBox();
            this.tbx_PTDRH_20_Y = new System.Windows.Forms.TextBox();
            this.grp_PTHRHG_7 = new System.Windows.Forms.GroupBox();
            this.tbx_PTHRHG_7_Z = new System.Windows.Forms.TextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.tbx_PTHRHG_7_X = new System.Windows.Forms.TextBox();
            this.tbx_PTHRHG_7_Y = new System.Windows.Forms.TextBox();
            this.grp_PTPRH_24 = new System.Windows.Forms.GroupBox();
            this.tbx_PTPRH_24_Z = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.tbx_PTPRH_24_X = new System.Windows.Forms.TextBox();
            this.tbx_PTPRH_24_Y = new System.Windows.Forms.TextBox();
            this.grp_PTA3D_16 = new System.Windows.Forms.GroupBox();
            this.tbx_PTA3D_16_Z = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.tbx_PTA3D_16_X = new System.Windows.Forms.TextBox();
            this.tbx_PTA3D_16_Y = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.grp_PTGRH_2 = new System.Windows.Forms.GroupBox();
            this.tbx_PTGRH_2_Z = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.tbx_PTGRH_2_X = new System.Windows.Forms.TextBox();
            this.tbx_PTGRH_2_Y = new System.Windows.Forms.TextBox();
            this.grp_PTFRHF_11 = new System.Windows.Forms.GroupBox();
            this.tbx_PTFRHF_11_Z = new System.Windows.Forms.TextBox();
            this.label114 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.tbx_PTFRHF_11_X = new System.Windows.Forms.TextBox();
            this.tbx_PTFRHF_11_Y = new System.Windows.Forms.TextBox();
            this.grp_PTGLH_1 = new System.Windows.Forms.GroupBox();
            this.tbx_PTGLH_1_Z = new System.Windows.Forms.TextBox();
            this.label111 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.tbx_PTGLH_1_X = new System.Windows.Forms.TextBox();
            this.tbx_PTGLH_1_Y = new System.Windows.Forms.TextBox();
            this.grp_PTFLHF_12 = new System.Windows.Forms.GroupBox();
            this.tbx_PTFLHF_12_Z = new System.Windows.Forms.TextBox();
            this.label108 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.tbx_PTFLHF_12_X = new System.Windows.Forms.TextBox();
            this.tbx_PTFLHF_12_Y = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.grp_PTA12_6 = new System.Windows.Forms.GroupBox();
            this.tbx_PTA12_6_Z = new System.Windows.Forms.TextBox();
            this.label165 = new System.Windows.Forms.Label();
            this.label166 = new System.Windows.Forms.Label();
            this.label167 = new System.Windows.Forms.Label();
            this.tbx_PTA12_6_X = new System.Windows.Forms.TextBox();
            this.tbx_PTA12_6_Y = new System.Windows.Forms.TextBox();
            this.groupBox41 = new System.Windows.Forms.GroupBox();
            this.textBox97 = new System.Windows.Forms.TextBox();
            this.label162 = new System.Windows.Forms.Label();
            this.label163 = new System.Windows.Forms.Label();
            this.label164 = new System.Windows.Forms.Label();
            this.textBox98 = new System.Windows.Forms.TextBox();
            this.textBox99 = new System.Windows.Forms.TextBox();
            this.grp_PTJRH_17 = new System.Windows.Forms.GroupBox();
            this.tbx_PTJRH_17_Z = new System.Windows.Forms.TextBox();
            this.label159 = new System.Windows.Forms.Label();
            this.label160 = new System.Windows.Forms.Label();
            this.label161 = new System.Windows.Forms.Label();
            this.tbx_PTJRH_17_X = new System.Windows.Forms.TextBox();
            this.tbx_PTJRH_17_Y = new System.Windows.Forms.TextBox();
            this.groupBox39 = new System.Windows.Forms.GroupBox();
            this.textBox91 = new System.Windows.Forms.TextBox();
            this.label156 = new System.Windows.Forms.Label();
            this.label157 = new System.Windows.Forms.Label();
            this.label158 = new System.Windows.Forms.Label();
            this.textBox92 = new System.Windows.Forms.TextBox();
            this.textBox93 = new System.Windows.Forms.TextBox();
            this.grp_PTIRH_10 = new System.Windows.Forms.GroupBox();
            this.groupBox38 = new System.Windows.Forms.GroupBox();
            this.textBox88 = new System.Windows.Forms.TextBox();
            this.label153 = new System.Windows.Forms.Label();
            this.label154 = new System.Windows.Forms.Label();
            this.label155 = new System.Windows.Forms.Label();
            this.textBox89 = new System.Windows.Forms.TextBox();
            this.textBox90 = new System.Windows.Forms.TextBox();
            this.tbx_PTIRH_10_Z = new System.Windows.Forms.TextBox();
            this.label150 = new System.Windows.Forms.Label();
            this.label151 = new System.Windows.Forms.Label();
            this.label152 = new System.Windows.Forms.Label();
            this.tbx_PTIRH_10_X = new System.Windows.Forms.TextBox();
            this.tbx_PTIRH_10_Y = new System.Windows.Forms.TextBox();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.textBox55 = new System.Windows.Forms.TextBox();
            this.label120 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.textBox56 = new System.Windows.Forms.TextBox();
            this.textBox57 = new System.Windows.Forms.TextBox();
            this.grp_PTB_4 = new System.Windows.Forms.GroupBox();
            this.tbx_PTB_4_Z = new System.Windows.Forms.TextBox();
            this.label147 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.label149 = new System.Windows.Forms.Label();
            this.tbx_PTB_4_X = new System.Windows.Forms.TextBox();
            this.tbx_PTB_4_Y = new System.Windows.Forms.TextBox();
            this.groupBox35 = new System.Windows.Forms.GroupBox();
            this.textBox79 = new System.Windows.Forms.TextBox();
            this.label144 = new System.Windows.Forms.Label();
            this.label145 = new System.Windows.Forms.Label();
            this.label146 = new System.Windows.Forms.Label();
            this.textBox80 = new System.Windows.Forms.TextBox();
            this.textBox81 = new System.Windows.Forms.TextBox();
            this.grp_PTA22_5 = new System.Windows.Forms.GroupBox();
            this.tbx_PTA22_5_Z = new System.Windows.Forms.TextBox();
            this.label138 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.tbx_PTA22_5_X = new System.Windows.Forms.TextBox();
            this.tbx_PTA22_5_Y = new System.Windows.Forms.TextBox();
            this.groupBox32 = new System.Windows.Forms.GroupBox();
            this.textBox70 = new System.Windows.Forms.TextBox();
            this.label135 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.textBox71 = new System.Windows.Forms.TextBox();
            this.textBox72 = new System.Windows.Forms.TextBox();
            this.grp_PTJLH_13 = new System.Windows.Forms.GroupBox();
            this.tbx_PTJLH_13_Z = new System.Windows.Forms.TextBox();
            this.label132 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.tbx_PTJLH_13_X = new System.Windows.Forms.TextBox();
            this.tbx_PTJLH_13_Y = new System.Windows.Forms.TextBox();
            this.groupBox34 = new System.Windows.Forms.GroupBox();
            this.textBox76 = new System.Windows.Forms.TextBox();
            this.label141 = new System.Windows.Forms.Label();
            this.label142 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.textBox77 = new System.Windows.Forms.TextBox();
            this.textBox78 = new System.Windows.Forms.TextBox();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.textBox64 = new System.Windows.Forms.TextBox();
            this.label129 = new System.Windows.Forms.Label();
            this.label130 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.textBox65 = new System.Windows.Forms.TextBox();
            this.textBox66 = new System.Windows.Forms.TextBox();
            this.grp_PTILH_9 = new System.Windows.Forms.GroupBox();
            this.tbx_PTILH_9_Z = new System.Windows.Forms.TextBox();
            this.label126 = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.tbx_PTILH_9_X = new System.Windows.Forms.TextBox();
            this.tbx_PTILH_9_Y = new System.Windows.Forms.TextBox();
            this.grp_PTC_3 = new System.Windows.Forms.GroupBox();
            this.tbx_PTC_3_Z = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.tbx_PTC_3_X = new System.Windows.Forms.TextBox();
            this.tbx_PTC_3_Y = new System.Windows.Forms.TextBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label_idx32_X37 = new System.Windows.Forms.Label();
            this.label169 = new System.Windows.Forms.Label();
            this.label170 = new System.Windows.Forms.Label();
            this.label_idx31_X36 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label_idx25_X30 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label_idx29_X34 = new System.Windows.Forms.Label();
            this.label_idx26_X31 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label_idx27_X32 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label_idx21_X24 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label_idx28_X33 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label_idx22_X25 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label_idx23_X26 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label_idx24_X27 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label_idx20_X23 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label_idx18_X21 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label_idx17_X20 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.tbx_OriginalCode = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label_idx8_X07 = new System.Windows.Forms.Label();
            this.label171 = new System.Windows.Forms.Label();
            this.label_idx7_X06 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.tbx_idx33_INT4 = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label_idx4_X03 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label_idx1_X00 = new System.Windows.Forms.Label();
            this.tbx_PLC1stSendBKCode = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.label_idx13_X14 = new System.Windows.Forms.Label();
            this.label_idx2_X01 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label_idx14_X15 = new System.Windows.Forms.Label();
            this.label_idx8_X10 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.tbx_PLC2ndSendBKCode = new System.Windows.Forms.TextBox();
            this.label_idx9_X11 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDatFromZessiFile = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbx_DebugUsername = new System.Windows.Forms.TextBox();
            this.tbx_DebugPassword = new System.Windows.Forms.TextBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btn_ConfirmChangeUsrNamePwd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_username = new System.Windows.Forms.TextBox();
            this.tbx_password = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btn_WriteOPCItem = new System.Windows.Forms.Button();
            this.cbx_TriggerSignal = new System.Windows.Forms.ComboBox();
            this.cbx_MarkInfo = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.btn_ReadOPCItem = new System.Windows.Forms.Button();
            this.DBGroupBox = new System.Windows.Forms.GroupBox();
            this.DBRadioButton = new System.Windows.Forms.RadioButton();
            this.XMLRadioButton = new System.Windows.Forms.RadioButton();
            this.SQLDBRadioButton = new System.Windows.Forms.RadioButton();
            this.SimGroupBox = new System.Windows.Forms.GroupBox();
            this.label30 = new System.Windows.Forms.Label();
            this.SendBitLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StartSimButton = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.tbx_OPCItemTimeStamp = new System.Windows.Forms.TextBox();
            this.tbx_OPCItemQuality = new System.Windows.Forms.TextBox();
            this.tbx_OPCItemValue = new System.Windows.Forms.TextBox();
            this.timer_SendOK = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_ClearTraceList = new System.Windows.Forms.Button();
            this.TraceListBox_Main = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.grp_PTA21.SuspendLayout();
            this.grp_PTA4D.SuspendLayout();
            this.grp_PTA11.SuspendLayout();
            this.grp_PTA3D.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage8.SuspendLayout();
            this.grp_PTERH_25.SuspendLayout();
            this.grp_PTPLH_23.SuspendLayout();
            this.grp_PTELH_21.SuspendLayout();
            this.grp_PTA4D_18.SuspendLayout();
            this.grp_PTHLHG_8.SuspendLayout();
            this.grp_PTDLH_19.SuspendLayout();
            this.grp_PTA21_14.SuspendLayout();
            this.grp_PTNLH_22.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.grp_PTA11_15.SuspendLayout();
            this.grp_PTDRH_20.SuspendLayout();
            this.grp_PTHRHG_7.SuspendLayout();
            this.grp_PTPRH_24.SuspendLayout();
            this.grp_PTA3D_16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage9.SuspendLayout();
            this.grp_PTGRH_2.SuspendLayout();
            this.grp_PTFRHF_11.SuspendLayout();
            this.grp_PTGLH_1.SuspendLayout();
            this.grp_PTFLHF_12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabPage10.SuspendLayout();
            this.grp_PTA12_6.SuspendLayout();
            this.groupBox41.SuspendLayout();
            this.grp_PTJRH_17.SuspendLayout();
            this.groupBox39.SuspendLayout();
            this.grp_PTIRH_10.SuspendLayout();
            this.groupBox38.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.grp_PTB_4.SuspendLayout();
            this.groupBox35.SuspendLayout();
            this.grp_PTA22_5.SuspendLayout();
            this.groupBox32.SuspendLayout();
            this.grp_PTJLH_13.SuspendLayout();
            this.groupBox34.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.grp_PTILH_9.SuspendLayout();
            this.grp_PTC_3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatFromZessiFile)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.DBGroupBox.SuspendLayout();
            this.SimGroupBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem9,
            this.InfoMenuItem});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ShowTracingMenuItem,
            this.UpdtRtsMenuItem,
            this.ConnMenu,
            this.menuItem6,
            this.menuItem15,
            this.ExitMenuItem});
            this.menuItem1.Text = "系统设置";
            // 
            // ShowTracingMenuItem
            // 
            this.ShowTracingMenuItem.Checked = true;
            this.ShowTracingMenuItem.Index = 0;
            this.ShowTracingMenuItem.Text = "使能提示框";
            this.ShowTracingMenuItem.Visible = false;
            this.ShowTracingMenuItem.Click += new System.EventHandler(this.ShowTracingMenuItem_Click);
            // 
            // UpdtRtsMenuItem
            // 
            this.UpdtRtsMenuItem.Index = 1;
            this.UpdtRtsMenuItem.Text = "设置更新频率";
            this.UpdtRtsMenuItem.Click += new System.EventHandler(this.UpdtRtsMenuItem_Click);
            // 
            // ConnMenu
            // 
            this.ConnMenu.Enabled = false;
            this.ConnMenu.Index = 2;
            this.ConnMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ConnMenuItem,
            this.DisConnMenuItem});
            this.ConnMenu.Text = "Connection to OPC-Server";
            this.ConnMenu.Visible = false;
            // 
            // ConnMenuItem
            // 
            this.ConnMenuItem.Index = 0;
            this.ConnMenuItem.Text = "Connect";
            // 
            // DisConnMenuItem
            // 
            this.DisConnMenuItem.Enabled = false;
            this.DisConnMenuItem.Index = 1;
            this.DisConnMenuItem.Text = "Disconnect";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ClrSndBitMenuItem,
            this.ClrMeanVMenuItem});
            this.menuItem6.Text = "Control";
            this.menuItem6.Visible = false;
            // 
            // ClrSndBitMenuItem
            // 
            this.ClrSndBitMenuItem.Enabled = false;
            this.ClrSndBitMenuItem.Index = 0;
            this.ClrSndBitMenuItem.Text = "Clear Sendbit";
            // 
            // ClrMeanVMenuItem
            // 
            this.ClrMeanVMenuItem.Index = 1;
            this.ClrMeanVMenuItem.Text = "Clear Meanvalues";
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 4;
            this.menuItem15.Text = "-";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Enabled = false;
            this.ExitMenuItem.Index = 5;
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ShowRecMenuItem,
            this.DelSingleDBMenuItem,
            this.DelRecMenuItem});
            this.menuItem9.Text = "数据库设置";
            // 
            // ShowRecMenuItem
            // 
            this.ShowRecMenuItem.Enabled = false;
            this.ShowRecMenuItem.Index = 0;
            this.ShowRecMenuItem.Text = "Show records";
            this.ShowRecMenuItem.Click += new System.EventHandler(this.ShowRecMenuItem_Click);
            // 
            // DelSingleDBMenuItem
            // 
            this.DelSingleDBMenuItem.Enabled = false;
            this.DelSingleDBMenuItem.Index = 1;
            this.DelSingleDBMenuItem.Text = "Delete selected DB";
            this.DelSingleDBMenuItem.Click += new System.EventHandler(this.DelSingleDBMenuItem_Click);
            // 
            // DelRecMenuItem
            // 
            this.DelRecMenuItem.Enabled = false;
            this.DelRecMenuItem.Index = 2;
            this.DelRecMenuItem.Text = "Delete all records";
            this.DelRecMenuItem.Click += new System.EventHandler(this.DelRecMenuItem_Click);
            // 
            // InfoMenuItem
            // 
            this.InfoMenuItem.Index = 2;
            this.InfoMenuItem.Text = "关于";
            this.InfoMenuItem.Click += new System.EventHandler(this.InfoMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1022, 512);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.24266F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.75734F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tabControl2, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1022, 512);
            this.tableLayoutPanel3.TabIndex = 32;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Onlyclearsig);
            this.panel2.Controls.Add(this.groupBox10);
            this.panel2.Controls.Add(this.groupBox9);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(159, 506);
            this.panel2.TabIndex = 0;
            // 
            // btn_Onlyclearsig
            // 
            this.btn_Onlyclearsig.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_Onlyclearsig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Onlyclearsig.Location = new System.Drawing.Point(6, 459);
            this.btn_Onlyclearsig.Name = "btn_Onlyclearsig";
            this.btn_Onlyclearsig.Size = new System.Drawing.Size(147, 37);
            this.btn_Onlyclearsig.TabIndex = 20;
            this.btn_Onlyclearsig.Text = "异常:手动复位";
            this.btn_Onlyclearsig.UseVisualStyleBackColor = false;
            this.btn_Onlyclearsig.Click += new System.EventHandler(this.btn_Onlyclearsig_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.groupBox10.Controls.Add(this.btn_clearsig);
            this.groupBox10.Location = new System.Drawing.Point(5, 383);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(151, 76);
            this.groupBox10.TabIndex = 21;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "调试:手动清PC测信号";
            // 
            // btn_clearsig
            // 
            this.btn_clearsig.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_clearsig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_clearsig.Location = new System.Drawing.Point(1, 16);
            this.btn_clearsig.Name = "btn_clearsig";
            this.btn_clearsig.Size = new System.Drawing.Size(147, 54);
            this.btn_clearsig.TabIndex = 19;
            this.btn_clearsig.Text = "清信号：软件回到状态1\r\n(除上位机准备OK信号)";
            this.btn_clearsig.UseVisualStyleBackColor = false;
            this.btn_clearsig.Click += new System.EventHandler(this.btn_ClearSignal_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.groupBox9.Controls.Add(this.btn_login);
            this.groupBox9.Controls.Add(this.btn_logout);
            this.groupBox9.Location = new System.Drawing.Point(5, 290);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(151, 87);
            this.groupBox9.TabIndex = 20;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "高级功能登录";
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_login.Location = new System.Drawing.Point(27, 20);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(89, 26);
            this.btn_login.TabIndex = 12;
            this.btn_login.Text = "登录";
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // btn_logout
            // 
            this.btn_logout.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_logout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_logout.Location = new System.Drawing.Point(27, 52);
            this.btn_logout.Name = "btn_logout";
            this.btn_logout.Size = new System.Drawing.Size(89, 26);
            this.btn_logout.TabIndex = 13;
            this.btn_logout.Text = "注销";
            this.btn_logout.UseVisualStyleBackColor = false;
            this.btn_logout.Click += new System.EventHandler(this.btn_logout_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.groupBox1.Controls.Add(this.rbt_ConRemoteOPC);
            this.groupBox1.Controls.Add(this.tbx_IP4);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.tbx_IP3);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.tbx_IP2);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.tbx_IP1);
            this.groupBox1.Controls.Add(this.btn_RecordServerIP);
            this.groupBox1.Controls.Add(this.label_OPCProcID);
            this.groupBox1.Controls.Add(this.btn_OPCCon);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 184);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OPC连接状态";
            // 
            // rbt_ConRemoteOPC
            // 
            this.rbt_ConRemoteOPC.Enabled = false;
            this.rbt_ConRemoteOPC.Location = new System.Drawing.Point(16, 89);
            this.rbt_ConRemoteOPC.Name = "rbt_ConRemoteOPC";
            this.rbt_ConRemoteOPC.Size = new System.Drawing.Size(99, 21);
            this.rbt_ConRemoteOPC.TabIndex = 1;
            this.rbt_ConRemoteOPC.Text = "连接远程OPC";
            this.rbt_ConRemoteOPC.Click += new System.EventHandler(this.rbt_ConRemoteOPC_Click);
            // 
            // tbx_IP4
            // 
            this.tbx_IP4.Location = new System.Drawing.Point(110, 116);
            this.tbx_IP4.Name = "tbx_IP4";
            this.tbx_IP4.Size = new System.Drawing.Size(23, 21);
            this.tbx_IP4.TabIndex = 16;
            this.tbx_IP4.Text = "0";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(101, 126);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(11, 12);
            this.label36.TabIndex = 15;
            this.label36.Text = ".";
            // 
            // tbx_IP3
            // 
            this.tbx_IP3.Location = new System.Drawing.Point(78, 116);
            this.tbx_IP3.Name = "tbx_IP3";
            this.tbx_IP3.Size = new System.Drawing.Size(23, 21);
            this.tbx_IP3.TabIndex = 14;
            this.tbx_IP3.Text = "0";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(68, 126);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(11, 12);
            this.label35.TabIndex = 13;
            this.label35.Text = ".";
            // 
            // tbx_IP2
            // 
            this.tbx_IP2.Location = new System.Drawing.Point(45, 116);
            this.tbx_IP2.Name = "tbx_IP2";
            this.tbx_IP2.Size = new System.Drawing.Size(23, 21);
            this.tbx_IP2.TabIndex = 12;
            this.tbx_IP2.Text = "0";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(36, 126);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(11, 12);
            this.label34.TabIndex = 11;
            this.label34.Text = ".";
            // 
            // tbx_IP1
            // 
            this.tbx_IP1.Location = new System.Drawing.Point(13, 116);
            this.tbx_IP1.Name = "tbx_IP1";
            this.tbx_IP1.Size = new System.Drawing.Size(23, 21);
            this.tbx_IP1.TabIndex = 11;
            this.tbx_IP1.Text = "0";
            // 
            // btn_RecordServerIP
            // 
            this.btn_RecordServerIP.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_RecordServerIP.Enabled = false;
            this.btn_RecordServerIP.Location = new System.Drawing.Point(32, 147);
            this.btn_RecordServerIP.Name = "btn_RecordServerIP";
            this.btn_RecordServerIP.Size = new System.Drawing.Size(69, 26);
            this.btn_RecordServerIP.TabIndex = 2;
            this.btn_RecordServerIP.Text = "确认IP";
            this.btn_RecordServerIP.UseVisualStyleBackColor = false;
            this.btn_RecordServerIP.Click += new System.EventHandler(this.btn_RecordServerIP_Click);
            // 
            // label_OPCProcID
            // 
            this.label_OPCProcID.AutoSize = true;
            this.label_OPCProcID.Location = new System.Drawing.Point(11, 58);
            this.label_OPCProcID.Name = "label_OPCProcID";
            this.label_OPCProcID.Size = new System.Drawing.Size(41, 12);
            this.label_OPCProcID.TabIndex = 1;
            this.label_OPCProcID.Text = "ProcID";
            this.label_OPCProcID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_OPCCon
            // 
            this.btn_OPCCon.BackColor = System.Drawing.Color.Gray;
            this.btn_OPCCon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_OPCCon.Location = new System.Drawing.Point(10, 24);
            this.btn_OPCCon.Name = "btn_OPCCon";
            this.btn_OPCCon.Size = new System.Drawing.Size(124, 26);
            this.btn_OPCCon.TabIndex = 0;
            this.btn_OPCCon.Text = "Connect/Disconnect";
            this.btn_OPCCon.UseVisualStyleBackColor = false;
            this.btn_OPCCon.Click += new System.EventHandler(this.btn_OPCCon_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.groupBox2.Controls.Add(this.label47);
            this.groupBox2.Controls.Add(this.label_DBName);
            this.groupBox2.Controls.Add(this.label_SQLDBConState);
            this.groupBox2.Controls.Add(this.label_DBStatus);
            this.groupBox2.Location = new System.Drawing.Point(4, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(151, 91);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据库连接状态";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(8, 67);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(113, 12);
            this.label47.TabIndex = 4;
            this.label47.Text = "检查数据库是否开启";
            // 
            // label_DBName
            // 
            this.label_DBName.AutoSize = true;
            this.label_DBName.Location = new System.Drawing.Point(8, 46);
            this.label_DBName.Name = "label_DBName";
            this.label_DBName.Size = new System.Drawing.Size(113, 12);
            this.label_DBName.TabIndex = 3;
            this.label_DBName.Text = "检查数据库是否开启";
            // 
            // label_SQLDBConState
            // 
            this.label_SQLDBConState.BackColor = System.Drawing.Color.Red;
            this.label_SQLDBConState.Location = new System.Drawing.Point(9, 20);
            this.label_SQLDBConState.Name = "label_SQLDBConState";
            this.label_SQLDBConState.Size = new System.Drawing.Size(15, 15);
            this.label_SQLDBConState.TabIndex = 2;
            // 
            // label_DBStatus
            // 
            this.label_DBStatus.AutoSize = true;
            this.label_DBStatus.Location = new System.Drawing.Point(30, 22);
            this.label_DBStatus.Name = "label_DBStatus";
            this.label_DBStatus.Size = new System.Drawing.Size(53, 12);
            this.label_DBStatus.TabIndex = 1;
            this.label_DBStatus.Text = "连接失败";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Controls.Add(this.tabPage10);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(168, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(851, 506);
            this.tabControl2.TabIndex = 9;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tabPage6.Controls.Add(this.label_curPartMissDetect);
            this.tabPage6.Controls.Add(this.grp_PTA21);
            this.tabPage6.Controls.Add(this.grp_PTA4D);
            this.tabPage6.Controls.Add(this.grp_PTA11);
            this.tabPage6.Controls.Add(this.grp_PTA3D);
            this.tabPage6.Controls.Add(this.vScrollBar1);
            this.tabPage6.Controls.Add(this.hScrollBar1);
            this.tabPage6.Controls.Add(this.label_Status8);
            this.tabPage6.Controls.Add(this.btn_InquireDataInterface);
            this.tabPage6.Controls.Add(this.label_Status7);
            this.tabPage6.Controls.Add(this.label_Status5);
            this.tabPage6.Controls.Add(this.label_Status6);
            this.tabPage6.Controls.Add(this.button3);
            this.tabPage6.Controls.Add(this.label_Status4);
            this.tabPage6.Controls.Add(this.label_Status3);
            this.tabPage6.Controls.Add(this.label_Status2);
            this.tabPage6.Controls.Add(this.label_Status1);
            this.tabPage6.Controls.Add(this.btn_DataInterface);
            this.tabPage6.Controls.Add(this.btn_AdvancedFunc);
            this.tabPage6.Controls.Add(this.pictureBox1);
            this.tabPage6.Location = new System.Drawing.Point(4, 21);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(843, 481);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "运行主界面";
            // 
            // label_curPartMissDetect
            // 
            this.label_curPartMissDetect.AutoSize = true;
            this.label_curPartMissDetect.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_curPartMissDetect.ForeColor = System.Drawing.Color.Red;
            this.label_curPartMissDetect.Location = new System.Drawing.Point(411, 13);
            this.label_curPartMissDetect.Name = "label_curPartMissDetect";
            this.label_curPartMissDetect.Size = new System.Drawing.Size(161, 16);
            this.label_curPartMissDetect.TabIndex = 101;
            this.label_curPartMissDetect.Text = "当前工件存在漏检点";
            this.label_curPartMissDetect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_curPartMissDetect.Visible = false;
            // 
            // grp_PTA21
            // 
            this.grp_PTA21.BackColor = System.Drawing.Color.IndianRed;
            this.grp_PTA21.Controls.Add(this.tbx_PTA21_Z);
            this.grp_PTA21.Controls.Add(this.label180);
            this.grp_PTA21.Controls.Add(this.label181);
            this.grp_PTA21.Controls.Add(this.label182);
            this.grp_PTA21.Controls.Add(this.tbx_PTA21_X);
            this.grp_PTA21.Controls.Add(this.tbx_PTA21_Y);
            this.grp_PTA21.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grp_PTA21.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grp_PTA21.Location = new System.Drawing.Point(605, 276);
            this.grp_PTA21.Name = "grp_PTA21";
            this.grp_PTA21.Size = new System.Drawing.Size(209, 60);
            this.grp_PTA21.TabIndex = 31;
            this.grp_PTA21.TabStop = false;
            this.grp_PTA21.Text = "PTA2_1";
            this.grp_PTA21.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTA21_Z
            // 
            this.tbx_PTA21_Z.Location = new System.Drawing.Point(137, 34);
            this.tbx_PTA21_Z.Name = "tbx_PTA21_Z";
            this.tbx_PTA21_Z.Size = new System.Drawing.Size(59, 23);
            this.tbx_PTA21_Z.TabIndex = 16;
            // 
            // label180
            // 
            this.label180.AutoSize = true;
            this.label180.Location = new System.Drawing.Point(158, 15);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(14, 14);
            this.label180.TabIndex = 15;
            this.label180.Text = "Z";
            // 
            // label181
            // 
            this.label181.AutoSize = true;
            this.label181.Location = new System.Drawing.Point(28, 15);
            this.label181.Name = "label181";
            this.label181.Size = new System.Drawing.Size(14, 14);
            this.label181.TabIndex = 8;
            this.label181.Text = "X";
            // 
            // label182
            // 
            this.label182.AutoSize = true;
            this.label182.Location = new System.Drawing.Point(96, 15);
            this.label182.Name = "label182";
            this.label182.Size = new System.Drawing.Size(14, 14);
            this.label182.TabIndex = 14;
            this.label182.Text = "Y";
            // 
            // tbx_PTA21_X
            // 
            this.tbx_PTA21_X.Location = new System.Drawing.Point(6, 34);
            this.tbx_PTA21_X.Name = "tbx_PTA21_X";
            this.tbx_PTA21_X.Size = new System.Drawing.Size(60, 23);
            this.tbx_PTA21_X.TabIndex = 9;
            // 
            // tbx_PTA21_Y
            // 
            this.tbx_PTA21_Y.Location = new System.Drawing.Point(72, 34);
            this.tbx_PTA21_Y.Name = "tbx_PTA21_Y";
            this.tbx_PTA21_Y.Size = new System.Drawing.Size(59, 23);
            this.tbx_PTA21_Y.TabIndex = 10;
            // 
            // grp_PTA4D
            // 
            this.grp_PTA4D.BackColor = System.Drawing.Color.LimeGreen;
            this.grp_PTA4D.Controls.Add(this.tbx_PTA4D_Z);
            this.grp_PTA4D.Controls.Add(this.label177);
            this.grp_PTA4D.Controls.Add(this.label178);
            this.grp_PTA4D.Controls.Add(this.label179);
            this.grp_PTA4D.Controls.Add(this.tbx_PTA4D_X);
            this.grp_PTA4D.Controls.Add(this.tbx_PTA4D_Y);
            this.grp_PTA4D.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grp_PTA4D.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grp_PTA4D.Location = new System.Drawing.Point(603, 30);
            this.grp_PTA4D.Name = "grp_PTA4D";
            this.grp_PTA4D.Size = new System.Drawing.Size(209, 60);
            this.grp_PTA4D.TabIndex = 30;
            this.grp_PTA4D.TabStop = false;
            this.grp_PTA4D.Text = "PTA4/D";
            this.grp_PTA4D.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTA4D_Z
            // 
            this.tbx_PTA4D_Z.Location = new System.Drawing.Point(139, 35);
            this.tbx_PTA4D_Z.Name = "tbx_PTA4D_Z";
            this.tbx_PTA4D_Z.Size = new System.Drawing.Size(59, 23);
            this.tbx_PTA4D_Z.TabIndex = 16;
            // 
            // label177
            // 
            this.label177.AutoSize = true;
            this.label177.Location = new System.Drawing.Point(158, 15);
            this.label177.Name = "label177";
            this.label177.Size = new System.Drawing.Size(14, 14);
            this.label177.TabIndex = 15;
            this.label177.Text = "Z";
            // 
            // label178
            // 
            this.label178.AutoSize = true;
            this.label178.Location = new System.Drawing.Point(28, 15);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(14, 14);
            this.label178.TabIndex = 8;
            this.label178.Text = "X";
            // 
            // label179
            // 
            this.label179.AutoSize = true;
            this.label179.Location = new System.Drawing.Point(93, 14);
            this.label179.Name = "label179";
            this.label179.Size = new System.Drawing.Size(14, 14);
            this.label179.TabIndex = 14;
            this.label179.Text = "Y";
            // 
            // tbx_PTA4D_X
            // 
            this.tbx_PTA4D_X.Location = new System.Drawing.Point(7, 35);
            this.tbx_PTA4D_X.Name = "tbx_PTA4D_X";
            this.tbx_PTA4D_X.Size = new System.Drawing.Size(60, 23);
            this.tbx_PTA4D_X.TabIndex = 9;
            // 
            // tbx_PTA4D_Y
            // 
            this.tbx_PTA4D_Y.Location = new System.Drawing.Point(74, 35);
            this.tbx_PTA4D_Y.Name = "tbx_PTA4D_Y";
            this.tbx_PTA4D_Y.Size = new System.Drawing.Size(59, 23);
            this.tbx_PTA4D_Y.TabIndex = 10;
            // 
            // grp_PTA11
            // 
            this.grp_PTA11.BackColor = System.Drawing.Color.Peru;
            this.grp_PTA11.Controls.Add(this.tbx_PTA11_Z);
            this.grp_PTA11.Controls.Add(this.label174);
            this.grp_PTA11.Controls.Add(this.label175);
            this.grp_PTA11.Controls.Add(this.label176);
            this.grp_PTA11.Controls.Add(this.tbx_PTA11_X);
            this.grp_PTA11.Controls.Add(this.tbx_PTA11_Y);
            this.grp_PTA11.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grp_PTA11.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grp_PTA11.Location = new System.Drawing.Point(70, 276);
            this.grp_PTA11.Name = "grp_PTA11";
            this.grp_PTA11.Size = new System.Drawing.Size(209, 60);
            this.grp_PTA11.TabIndex = 24;
            this.grp_PTA11.TabStop = false;
            this.grp_PTA11.Text = "PTA1_1";
            this.grp_PTA11.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTA11_Z
            // 
            this.tbx_PTA11_Z.Location = new System.Drawing.Point(139, 35);
            this.tbx_PTA11_Z.Name = "tbx_PTA11_Z";
            this.tbx_PTA11_Z.Size = new System.Drawing.Size(59, 23);
            this.tbx_PTA11_Z.TabIndex = 16;
            // 
            // label174
            // 
            this.label174.AutoSize = true;
            this.label174.Location = new System.Drawing.Point(162, 17);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(14, 14);
            this.label174.TabIndex = 15;
            this.label174.Text = "Z";
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Location = new System.Drawing.Point(32, 16);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(14, 14);
            this.label175.TabIndex = 8;
            this.label175.Text = "X";
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.Location = new System.Drawing.Point(100, 17);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(14, 14);
            this.label176.TabIndex = 14;
            this.label176.Text = "Y";
            // 
            // tbx_PTA11_X
            // 
            this.tbx_PTA11_X.Location = new System.Drawing.Point(8, 34);
            this.tbx_PTA11_X.Name = "tbx_PTA11_X";
            this.tbx_PTA11_X.Size = new System.Drawing.Size(60, 23);
            this.tbx_PTA11_X.TabIndex = 9;
            // 
            // tbx_PTA11_Y
            // 
            this.tbx_PTA11_Y.Location = new System.Drawing.Point(74, 35);
            this.tbx_PTA11_Y.Name = "tbx_PTA11_Y";
            this.tbx_PTA11_Y.Size = new System.Drawing.Size(59, 23);
            this.tbx_PTA11_Y.TabIndex = 10;
            // 
            // grp_PTA3D
            // 
            this.grp_PTA3D.BackColor = System.Drawing.Color.LightGreen;
            this.grp_PTA3D.Controls.Add(this.tbx_PTA3D_Z);
            this.grp_PTA3D.Controls.Add(this.label168);
            this.grp_PTA3D.Controls.Add(this.label172);
            this.grp_PTA3D.Controls.Add(this.label173);
            this.grp_PTA3D.Controls.Add(this.tbx_PTA3D_X);
            this.grp_PTA3D.Controls.Add(this.tbx_PTA3D_Y);
            this.grp_PTA3D.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grp_PTA3D.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grp_PTA3D.Location = new System.Drawing.Point(106, 30);
            this.grp_PTA3D.Name = "grp_PTA3D";
            this.grp_PTA3D.Size = new System.Drawing.Size(209, 60);
            this.grp_PTA3D.TabIndex = 100;
            this.grp_PTA3D.TabStop = false;
            this.grp_PTA3D.Text = "PTA3/D";
            this.grp_PTA3D.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTA3D_Z
            // 
            this.tbx_PTA3D_Z.Location = new System.Drawing.Point(140, 34);
            this.tbx_PTA3D_Z.Name = "tbx_PTA3D_Z";
            this.tbx_PTA3D_Z.Size = new System.Drawing.Size(59, 23);
            this.tbx_PTA3D_Z.TabIndex = 16;
            // 
            // label168
            // 
            this.label168.AutoSize = true;
            this.label168.Location = new System.Drawing.Point(158, 16);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(14, 14);
            this.label168.TabIndex = 15;
            this.label168.Text = "Z";
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Font = new System.Drawing.Font("SimHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label172.Location = new System.Drawing.Point(28, 16);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(14, 14);
            this.label172.TabIndex = 8;
            this.label172.Text = "X";
            // 
            // label173
            // 
            this.label173.AutoSize = true;
            this.label173.Location = new System.Drawing.Point(96, 16);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(14, 14);
            this.label173.TabIndex = 14;
            this.label173.Text = "Y";
            // 
            // tbx_PTA3D_X
            // 
            this.tbx_PTA3D_X.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_PTA3D_X.ForeColor = System.Drawing.Color.Red;
            this.tbx_PTA3D_X.Location = new System.Drawing.Point(9, 34);
            this.tbx_PTA3D_X.Name = "tbx_PTA3D_X";
            this.tbx_PTA3D_X.Size = new System.Drawing.Size(59, 23);
            this.tbx_PTA3D_X.TabIndex = 9;
            // 
            // tbx_PTA3D_Y
            // 
            this.tbx_PTA3D_Y.Location = new System.Drawing.Point(75, 34);
            this.tbx_PTA3D_Y.Name = "tbx_PTA3D_Y";
            this.tbx_PTA3D_Y.Size = new System.Drawing.Size(59, 23);
            this.tbx_PTA3D_Y.TabIndex = 10;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(817, 45);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(20, 241);
            this.vScrollBar1.TabIndex = 22;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(9, 337);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(828, 20);
            this.hScrollBar1.TabIndex = 21;
            // 
            // label_Status8
            // 
            this.label_Status8.BackColor = System.Drawing.Color.Goldenrod;
            this.label_Status8.Location = new System.Drawing.Point(770, 362);
            this.label_Status8.Name = "label_Status8";
            this.label_Status8.Size = new System.Drawing.Size(67, 38);
            this.label_Status8.TabIndex = 20;
            this.label_Status8.Text = "入库";
            this.label_Status8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_InquireDataInterface
            // 
            this.btn_InquireDataInterface.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_InquireDataInterface.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_InquireDataInterface.Location = new System.Drawing.Point(626, 412);
            this.btn_InquireDataInterface.Name = "btn_InquireDataInterface";
            this.btn_InquireDataInterface.Size = new System.Drawing.Size(89, 26);
            this.btn_InquireDataInterface.TabIndex = 18;
            this.btn_InquireDataInterface.Text = "数据查询界面";
            this.btn_InquireDataInterface.UseVisualStyleBackColor = false;
            this.btn_InquireDataInterface.Click += new System.EventHandler(this.btn_InquireDataInterface_Click);
            // 
            // label_Status7
            // 
            this.label_Status7.BackColor = System.Drawing.Color.Goldenrod;
            this.label_Status7.Location = new System.Drawing.Point(699, 362);
            this.label_Status7.Name = "label_Status7";
            this.label_Status7.Size = new System.Drawing.Size(65, 38);
            this.label_Status7.TabIndex = 17;
            this.label_Status7.Text = "打标";
            this.label_Status7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Status5
            // 
            this.label_Status5.BackColor = System.Drawing.Color.Goldenrod;
            this.label_Status5.Location = new System.Drawing.Point(411, 362);
            this.label_Status5.Name = "label_Status5";
            this.label_Status5.Size = new System.Drawing.Size(141, 38);
            this.label_Status5.TabIndex = 16;
            this.label_Status5.Text = "结果判定并进行声光报警";
            this.label_Status5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Status6
            // 
            this.label_Status6.BackColor = System.Drawing.Color.Goldenrod;
            this.label_Status6.Location = new System.Drawing.Point(558, 362);
            this.label_Status6.Name = "label_Status6";
            this.label_Status6.Size = new System.Drawing.Size(135, 38);
            this.label_Status6.TabIndex = 15;
            this.label_Status6.Text = "零件被转移到打标工位";
            this.label_Status6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightGreen;
            this.button3.Enabled = false;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(16, 412);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 26);
            this.button3.TabIndex = 14;
            this.button3.Text = "自动模式";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // label_Status4
            // 
            this.label_Status4.BackColor = System.Drawing.Color.Goldenrod;
            this.label_Status4.Location = new System.Drawing.Point(321, 362);
            this.label_Status4.Name = "label_Status4";
            this.label_Status4.Size = new System.Drawing.Size(84, 38);
            this.label_Status4.TabIndex = 11;
            this.label_Status4.Text = "接收ZESSI数据";
            this.label_Status4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Status3
            // 
            this.label_Status3.BackColor = System.Drawing.Color.Goldenrod;
            this.label_Status3.Location = new System.Drawing.Point(187, 362);
            this.label_Status3.Name = "label_Status3";
            this.label_Status3.Size = new System.Drawing.Size(128, 38);
            this.label_Status3.TabIndex = 10;
            this.label_Status3.Text = "ZESSI检测并传输检测数据";
            this.label_Status3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Status2
            // 
            this.label_Status2.BackColor = System.Drawing.Color.Goldenrod;
            this.label_Status2.Location = new System.Drawing.Point(97, 362);
            this.label_Status2.Name = "label_Status2";
            this.label_Status2.Size = new System.Drawing.Size(84, 38);
            this.label_Status2.TabIndex = 9;
            this.label_Status2.Text = "夹紧到位";
            this.label_Status2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Status1
            // 
            this.label_Status1.BackColor = System.Drawing.Color.Goldenrod;
            this.label_Status1.Location = new System.Drawing.Point(7, 362);
            this.label_Status1.Name = "label_Status1";
            this.label_Status1.Size = new System.Drawing.Size(84, 38);
            this.label_Status1.TabIndex = 8;
            this.label_Status1.Text = "零件放置到位";
            this.label_Status1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_DataInterface
            // 
            this.btn_DataInterface.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_DataInterface.Enabled = false;
            this.btn_DataInterface.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_DataInterface.Location = new System.Drawing.Point(510, 412);
            this.btn_DataInterface.Name = "btn_DataInterface";
            this.btn_DataInterface.Size = new System.Drawing.Size(89, 26);
            this.btn_DataInterface.TabIndex = 4;
            this.btn_DataInterface.Text = "数据界面";
            this.btn_DataInterface.UseVisualStyleBackColor = false;
            this.btn_DataInterface.Visible = false;
            this.btn_DataInterface.Click += new System.EventHandler(this.btn_DataInterface_Click);
            // 
            // btn_AdvancedFunc
            // 
            this.btn_AdvancedFunc.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_AdvancedFunc.Enabled = false;
            this.btn_AdvancedFunc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_AdvancedFunc.Location = new System.Drawing.Point(741, 412);
            this.btn_AdvancedFunc.Name = "btn_AdvancedFunc";
            this.btn_AdvancedFunc.Size = new System.Drawing.Size(89, 26);
            this.btn_AdvancedFunc.TabIndex = 3;
            this.btn_AdvancedFunc.Text = "高级功能";
            this.btn_AdvancedFunc.UseVisualStyleBackColor = false;
            this.btn_AdvancedFunc.Click += new System.EventHandler(this.btn_AdvancedFunc_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(100, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(691, 290);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPage8.Controls.Add(this.grp_PTERH_25);
            this.tabPage8.Controls.Add(this.grp_PTPLH_23);
            this.tabPage8.Controls.Add(this.grp_PTELH_21);
            this.tabPage8.Controls.Add(this.grp_PTA4D_18);
            this.tabPage8.Controls.Add(this.grp_PTHLHG_8);
            this.tabPage8.Controls.Add(this.grp_PTDLH_19);
            this.tabPage8.Controls.Add(this.grp_PTA21_14);
            this.tabPage8.Controls.Add(this.grp_PTNLH_22);
            this.tabPage8.Controls.Add(this.groupBox14);
            this.tabPage8.Controls.Add(this.grp_PTA11_15);
            this.tabPage8.Controls.Add(this.grp_PTDRH_20);
            this.tabPage8.Controls.Add(this.grp_PTHRHG_7);
            this.tabPage8.Controls.Add(this.grp_PTPRH_24);
            this.tabPage8.Controls.Add(this.grp_PTA3D_16);
            this.tabPage8.Controls.Add(this.pictureBox2);
            this.tabPage8.Location = new System.Drawing.Point(4, 21);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(843, 481);
            this.tabPage8.TabIndex = 5;
            this.tabPage8.Text = "B515MCA_FCM_1";
            // 
            // grp_PTERH_25
            // 
            this.grp_PTERH_25.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTERH_25.Controls.Add(this.tbx_PTERH_25_Z);
            this.grp_PTERH_25.Controls.Add(this.label105);
            this.grp_PTERH_25.Controls.Add(this.label106);
            this.grp_PTERH_25.Controls.Add(this.label107);
            this.grp_PTERH_25.Controls.Add(this.tbx_PTERH_25_X);
            this.grp_PTERH_25.Controls.Add(this.tbx_PTERH_25_Y);
            this.grp_PTERH_25.Location = new System.Drawing.Point(221, 6);
            this.grp_PTERH_25.Name = "grp_PTERH_25";
            this.grp_PTERH_25.Size = new System.Drawing.Size(72, 87);
            this.grp_PTERH_25.TabIndex = 32;
            this.grp_PTERH_25.TabStop = false;
            this.grp_PTERH_25.Text = "PTE_RH";
            this.grp_PTERH_25.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTERH_25_Z
            // 
            this.tbx_PTERH_25_Z.Location = new System.Drawing.Point(26, 60);
            this.tbx_PTERH_25_Z.Name = "tbx_PTERH_25_Z";
            this.tbx_PTERH_25_Z.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTERH_25_Z.TabIndex = 16;
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(9, 65);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(11, 12);
            this.label105.TabIndex = 15;
            this.label105.Text = "Z";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(9, 22);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(11, 12);
            this.label106.TabIndex = 8;
            this.label106.Text = "X";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(9, 43);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(11, 12);
            this.label107.TabIndex = 14;
            this.label107.Text = "Y";
            // 
            // tbx_PTERH_25_X
            // 
            this.tbx_PTERH_25_X.Location = new System.Drawing.Point(26, 18);
            this.tbx_PTERH_25_X.Name = "tbx_PTERH_25_X";
            this.tbx_PTERH_25_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTERH_25_X.TabIndex = 9;
            // 
            // tbx_PTERH_25_Y
            // 
            this.tbx_PTERH_25_Y.Location = new System.Drawing.Point(26, 39);
            this.tbx_PTERH_25_Y.Name = "tbx_PTERH_25_Y";
            this.tbx_PTERH_25_Y.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTERH_25_Y.TabIndex = 10;
            // 
            // grp_PTPLH_23
            // 
            this.grp_PTPLH_23.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTPLH_23.Controls.Add(this.tbx_PTPLH_23_Z);
            this.grp_PTPLH_23.Controls.Add(this.label102);
            this.grp_PTPLH_23.Controls.Add(this.label103);
            this.grp_PTPLH_23.Controls.Add(this.label104);
            this.grp_PTPLH_23.Controls.Add(this.tbx_PTPLH_23_X);
            this.grp_PTPLH_23.Controls.Add(this.tbx_PTPLH_23_Y);
            this.grp_PTPLH_23.Location = new System.Drawing.Point(390, 35);
            this.grp_PTPLH_23.Name = "grp_PTPLH_23";
            this.grp_PTPLH_23.Size = new System.Drawing.Size(136, 58);
            this.grp_PTPLH_23.TabIndex = 104;
            this.grp_PTPLH_23.TabStop = false;
            this.grp_PTPLH_23.Text = "PTP_LH";
            this.grp_PTPLH_23.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTPLH_23_Z
            // 
            this.tbx_PTPLH_23_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTPLH_23_Z.Name = "tbx_PTPLH_23_Z";
            this.tbx_PTPLH_23_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTPLH_23_Z.TabIndex = 16;
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(103, 16);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(11, 12);
            this.label102.TabIndex = 15;
            this.label102.Text = "Z";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(15, 15);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(11, 12);
            this.label103.TabIndex = 8;
            this.label103.Text = "X";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(62, 16);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(11, 12);
            this.label104.TabIndex = 14;
            this.label104.Text = "Y";
            // 
            // tbx_PTPLH_23_X
            // 
            this.tbx_PTPLH_23_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTPLH_23_X.Name = "tbx_PTPLH_23_X";
            this.tbx_PTPLH_23_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTPLH_23_X.TabIndex = 9;
            // 
            // tbx_PTPLH_23_Y
            // 
            this.tbx_PTPLH_23_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTPLH_23_Y.Name = "tbx_PTPLH_23_Y";
            this.tbx_PTPLH_23_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTPLH_23_Y.TabIndex = 10;
            // 
            // grp_PTELH_21
            // 
            this.grp_PTELH_21.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTELH_21.Controls.Add(this.tbx_PTELH_21_Z);
            this.grp_PTELH_21.Controls.Add(this.label99);
            this.grp_PTELH_21.Controls.Add(this.label100);
            this.grp_PTELH_21.Controls.Add(this.label101);
            this.grp_PTELH_21.Controls.Add(this.tbx_PTELH_21_X);
            this.grp_PTELH_21.Controls.Add(this.tbx_PTELH_21_Y);
            this.grp_PTELH_21.Location = new System.Drawing.Point(532, 30);
            this.grp_PTELH_21.Name = "grp_PTELH_21";
            this.grp_PTELH_21.Size = new System.Drawing.Size(130, 58);
            this.grp_PTELH_21.TabIndex = 103;
            this.grp_PTELH_21.TabStop = false;
            this.grp_PTELH_21.Text = "PTE_LH";
            this.grp_PTELH_21.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTELH_21_Z
            // 
            this.tbx_PTELH_21_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTELH_21_Z.Name = "tbx_PTELH_21_Z";
            this.tbx_PTELH_21_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTELH_21_Z.TabIndex = 16;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(103, 16);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(11, 12);
            this.label99.TabIndex = 15;
            this.label99.Text = "Z";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(21, 16);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(11, 12);
            this.label100.TabIndex = 8;
            this.label100.Text = "X";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(62, 16);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(11, 12);
            this.label101.TabIndex = 14;
            this.label101.Text = "Y";
            // 
            // tbx_PTELH_21_X
            // 
            this.tbx_PTELH_21_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTELH_21_X.Name = "tbx_PTELH_21_X";
            this.tbx_PTELH_21_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTELH_21_X.TabIndex = 9;
            // 
            // tbx_PTELH_21_Y
            // 
            this.tbx_PTELH_21_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTELH_21_Y.Name = "tbx_PTELH_21_Y";
            this.tbx_PTELH_21_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTELH_21_Y.TabIndex = 10;
            // 
            // grp_PTA4D_18
            // 
            this.grp_PTA4D_18.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTA4D_18.Controls.Add(this.tbx_PTA4D_18_Z);
            this.grp_PTA4D_18.Controls.Add(this.label96);
            this.grp_PTA4D_18.Controls.Add(this.label97);
            this.grp_PTA4D_18.Controls.Add(this.label98);
            this.grp_PTA4D_18.Controls.Add(this.tbx_PTA4D_18_X);
            this.grp_PTA4D_18.Controls.Add(this.tbx_PTA4D_18_Y);
            this.grp_PTA4D_18.Location = new System.Drawing.Point(680, 51);
            this.grp_PTA4D_18.Name = "grp_PTA4D_18";
            this.grp_PTA4D_18.Size = new System.Drawing.Size(136, 58);
            this.grp_PTA4D_18.TabIndex = 29;
            this.grp_PTA4D_18.TabStop = false;
            this.grp_PTA4D_18.Text = "PTA4/D";
            this.grp_PTA4D_18.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTA4D_18_Z
            // 
            this.tbx_PTA4D_18_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTA4D_18_Z.Name = "tbx_PTA4D_18_Z";
            this.tbx_PTA4D_18_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA4D_18_Z.TabIndex = 16;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(103, 16);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(11, 12);
            this.label96.TabIndex = 15;
            this.label96.Text = "Z";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(20, 16);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(11, 12);
            this.label97.TabIndex = 8;
            this.label97.Text = "X";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(62, 16);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(11, 12);
            this.label98.TabIndex = 14;
            this.label98.Text = "Y";
            // 
            // tbx_PTA4D_18_X
            // 
            this.tbx_PTA4D_18_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTA4D_18_X.Name = "tbx_PTA4D_18_X";
            this.tbx_PTA4D_18_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTA4D_18_X.TabIndex = 9;
            // 
            // tbx_PTA4D_18_Y
            // 
            this.tbx_PTA4D_18_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTA4D_18_Y.Name = "tbx_PTA4D_18_Y";
            this.tbx_PTA4D_18_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA4D_18_Y.TabIndex = 10;
            // 
            // grp_PTHLHG_8
            // 
            this.grp_PTHLHG_8.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTHLHG_8.Controls.Add(this.tbx_PTHLHG_8_Z);
            this.grp_PTHLHG_8.Controls.Add(this.label93);
            this.grp_PTHLHG_8.Controls.Add(this.label94);
            this.grp_PTHLHG_8.Controls.Add(this.label95);
            this.grp_PTHLHG_8.Controls.Add(this.tbx_PTHLHG_8_X);
            this.grp_PTHLHG_8.Controls.Add(this.tbx_PTHLHG_8_Y);
            this.grp_PTHLHG_8.Location = new System.Drawing.Point(683, 138);
            this.grp_PTHLHG_8.Name = "grp_PTHLHG_8";
            this.grp_PTHLHG_8.Size = new System.Drawing.Size(136, 58);
            this.grp_PTHLHG_8.TabIndex = 28;
            this.grp_PTHLHG_8.TabStop = false;
            this.grp_PTHLHG_8.Text = "PTH_LH/G";
            this.grp_PTHLHG_8.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTHLHG_8_Z
            // 
            this.tbx_PTHLHG_8_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTHLHG_8_Z.Name = "tbx_PTHLHG_8_Z";
            this.tbx_PTHLHG_8_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTHLHG_8_Z.TabIndex = 16;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(103, 16);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(11, 12);
            this.label93.TabIndex = 15;
            this.label93.Text = "Z";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(19, 16);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(11, 12);
            this.label94.TabIndex = 8;
            this.label94.Text = "X";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(62, 16);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(11, 12);
            this.label95.TabIndex = 14;
            this.label95.Text = "Y";
            // 
            // tbx_PTHLHG_8_X
            // 
            this.tbx_PTHLHG_8_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTHLHG_8_X.Name = "tbx_PTHLHG_8_X";
            this.tbx_PTHLHG_8_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTHLHG_8_X.TabIndex = 9;
            // 
            // tbx_PTHLHG_8_Y
            // 
            this.tbx_PTHLHG_8_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTHLHG_8_Y.Name = "tbx_PTHLHG_8_Y";
            this.tbx_PTHLHG_8_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTHLHG_8_Y.TabIndex = 10;
            // 
            // grp_PTDLH_19
            // 
            this.grp_PTDLH_19.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTDLH_19.Controls.Add(this.tbx_PTDLH_19_Z);
            this.grp_PTDLH_19.Controls.Add(this.label90);
            this.grp_PTDLH_19.Controls.Add(this.label91);
            this.grp_PTDLH_19.Controls.Add(this.label92);
            this.grp_PTDLH_19.Controls.Add(this.tbx_PTDLH_19_X);
            this.grp_PTDLH_19.Controls.Add(this.tbx_PTDLH_19_Y);
            this.grp_PTDLH_19.Location = new System.Drawing.Point(693, 201);
            this.grp_PTDLH_19.Name = "grp_PTDLH_19";
            this.grp_PTDLH_19.Size = new System.Drawing.Size(136, 58);
            this.grp_PTDLH_19.TabIndex = 27;
            this.grp_PTDLH_19.TabStop = false;
            this.grp_PTDLH_19.Text = "PTD_LH";
            this.grp_PTDLH_19.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTDLH_19_Z
            // 
            this.tbx_PTDLH_19_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTDLH_19_Z.Name = "tbx_PTDLH_19_Z";
            this.tbx_PTDLH_19_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTDLH_19_Z.TabIndex = 16;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(103, 16);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(11, 12);
            this.label90.TabIndex = 15;
            this.label90.Text = "Z";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(17, 16);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(11, 12);
            this.label91.TabIndex = 8;
            this.label91.Text = "X";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(62, 16);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(11, 12);
            this.label92.TabIndex = 14;
            this.label92.Text = "Y";
            // 
            // tbx_PTDLH_19_X
            // 
            this.tbx_PTDLH_19_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTDLH_19_X.Name = "tbx_PTDLH_19_X";
            this.tbx_PTDLH_19_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTDLH_19_X.TabIndex = 9;
            // 
            // tbx_PTDLH_19_Y
            // 
            this.tbx_PTDLH_19_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTDLH_19_Y.Name = "tbx_PTDLH_19_Y";
            this.tbx_PTDLH_19_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTDLH_19_Y.TabIndex = 10;
            // 
            // grp_PTA21_14
            // 
            this.grp_PTA21_14.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTA21_14.Controls.Add(this.tbx_PTA21_14_Z);
            this.grp_PTA21_14.Controls.Add(this.label87);
            this.grp_PTA21_14.Controls.Add(this.label88);
            this.grp_PTA21_14.Controls.Add(this.label89);
            this.grp_PTA21_14.Controls.Add(this.tbx_PTA21_14_X);
            this.grp_PTA21_14.Controls.Add(this.tbx_PTA21_14_Y);
            this.grp_PTA21_14.Location = new System.Drawing.Point(686, 320);
            this.grp_PTA21_14.Name = "grp_PTA21_14";
            this.grp_PTA21_14.Size = new System.Drawing.Size(136, 58);
            this.grp_PTA21_14.TabIndex = 26;
            this.grp_PTA21_14.TabStop = false;
            this.grp_PTA21_14.Text = "PTA2_1";
            this.grp_PTA21_14.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTA21_14_Z
            // 
            this.tbx_PTA21_14_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTA21_14_Z.Name = "tbx_PTA21_14_Z";
            this.tbx_PTA21_14_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA21_14_Z.TabIndex = 16;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(103, 16);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(11, 12);
            this.label87.TabIndex = 15;
            this.label87.Text = "Z";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(19, 16);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(11, 12);
            this.label88.TabIndex = 8;
            this.label88.Text = "X";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(62, 16);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(11, 12);
            this.label89.TabIndex = 14;
            this.label89.Text = "Y";
            // 
            // tbx_PTA21_14_X
            // 
            this.tbx_PTA21_14_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTA21_14_X.Name = "tbx_PTA21_14_X";
            this.tbx_PTA21_14_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTA21_14_X.TabIndex = 9;
            // 
            // tbx_PTA21_14_Y
            // 
            this.tbx_PTA21_14_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTA21_14_Y.Name = "tbx_PTA21_14_Y";
            this.tbx_PTA21_14_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA21_14_Y.TabIndex = 10;
            // 
            // grp_PTNLH_22
            // 
            this.grp_PTNLH_22.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTNLH_22.Controls.Add(this.tbx_PTNLH_22_Z);
            this.grp_PTNLH_22.Controls.Add(this.label84);
            this.grp_PTNLH_22.Controls.Add(this.label85);
            this.grp_PTNLH_22.Controls.Add(this.label86);
            this.grp_PTNLH_22.Controls.Add(this.tbx_PTNLH_22_X);
            this.grp_PTNLH_22.Controls.Add(this.tbx_PTNLH_22_Y);
            this.grp_PTNLH_22.Location = new System.Drawing.Point(511, 365);
            this.grp_PTNLH_22.Name = "grp_PTNLH_22";
            this.grp_PTNLH_22.Size = new System.Drawing.Size(136, 58);
            this.grp_PTNLH_22.TabIndex = 25;
            this.grp_PTNLH_22.TabStop = false;
            this.grp_PTNLH_22.Text = "PTN_LH";
            this.grp_PTNLH_22.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTNLH_22_Z
            // 
            this.tbx_PTNLH_22_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTNLH_22_Z.Name = "tbx_PTNLH_22_Z";
            this.tbx_PTNLH_22_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTNLH_22_Z.TabIndex = 16;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(103, 16);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(11, 12);
            this.label84.TabIndex = 15;
            this.label84.Text = "Z";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(19, 16);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(11, 12);
            this.label85.TabIndex = 8;
            this.label85.Text = "X";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(62, 16);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(11, 12);
            this.label86.TabIndex = 14;
            this.label86.Text = "Y";
            // 
            // tbx_PTNLH_22_X
            // 
            this.tbx_PTNLH_22_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTNLH_22_X.Name = "tbx_PTNLH_22_X";
            this.tbx_PTNLH_22_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTNLH_22_X.TabIndex = 9;
            // 
            // tbx_PTNLH_22_Y
            // 
            this.tbx_PTNLH_22_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTNLH_22_Y.Name = "tbx_PTNLH_22_Y";
            this.tbx_PTNLH_22_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTNLH_22_Y.TabIndex = 10;
            // 
            // groupBox14
            // 
            this.groupBox14.BackColor = System.Drawing.Color.PaleTurquoise;
            this.groupBox14.Controls.Add(this.textBox16);
            this.groupBox14.Controls.Add(this.label81);
            this.groupBox14.Controls.Add(this.label82);
            this.groupBox14.Controls.Add(this.label83);
            this.groupBox14.Controls.Add(this.textBox17);
            this.groupBox14.Controls.Add(this.textBox18);
            this.groupBox14.Location = new System.Drawing.Point(215, 365);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(136, 58);
            this.groupBox14.TabIndex = 24;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "PTN_RH";
            this.groupBox14.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(90, 32);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(39, 21);
            this.textBox16.TabIndex = 16;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(103, 16);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(11, 12);
            this.label81.TabIndex = 15;
            this.label81.Text = "Z";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(15, 16);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(11, 12);
            this.label82.TabIndex = 8;
            this.label82.Text = "X";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(62, 16);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(11, 12);
            this.label83.TabIndex = 14;
            this.label83.Text = "Y";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(6, 32);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(41, 21);
            this.textBox17.TabIndex = 9;
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(49, 32);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(39, 21);
            this.textBox18.TabIndex = 10;
            // 
            // grp_PTA11_15
            // 
            this.grp_PTA11_15.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTA11_15.Controls.Add(this.tbx_PTA11_15_Z);
            this.grp_PTA11_15.Controls.Add(this.label78);
            this.grp_PTA11_15.Controls.Add(this.label79);
            this.grp_PTA11_15.Controls.Add(this.label80);
            this.grp_PTA11_15.Controls.Add(this.tbx_PTA11_15_X);
            this.grp_PTA11_15.Controls.Add(this.tbx_PTA11_15_Y);
            this.grp_PTA11_15.Location = new System.Drawing.Point(27, 341);
            this.grp_PTA11_15.Name = "grp_PTA11_15";
            this.grp_PTA11_15.Size = new System.Drawing.Size(136, 58);
            this.grp_PTA11_15.TabIndex = 23;
            this.grp_PTA11_15.TabStop = false;
            this.grp_PTA11_15.Text = "PTA1_1";
            this.grp_PTA11_15.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTA11_15_Z
            // 
            this.tbx_PTA11_15_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTA11_15_Z.Name = "tbx_PTA11_15_Z";
            this.tbx_PTA11_15_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA11_15_Z.TabIndex = 16;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(103, 16);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(11, 12);
            this.label78.TabIndex = 15;
            this.label78.Text = "Z";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(15, 16);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(11, 12);
            this.label79.TabIndex = 8;
            this.label79.Text = "X";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(62, 16);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(11, 12);
            this.label80.TabIndex = 14;
            this.label80.Text = "Y";
            // 
            // tbx_PTA11_15_X
            // 
            this.tbx_PTA11_15_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTA11_15_X.Name = "tbx_PTA11_15_X";
            this.tbx_PTA11_15_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTA11_15_X.TabIndex = 9;
            // 
            // tbx_PTA11_15_Y
            // 
            this.tbx_PTA11_15_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTA11_15_Y.Name = "tbx_PTA11_15_Y";
            this.tbx_PTA11_15_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA11_15_Y.TabIndex = 10;
            // 
            // grp_PTDRH_20
            // 
            this.grp_PTDRH_20.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTDRH_20.Controls.Add(this.tbx_PTDRH_20_Z);
            this.grp_PTDRH_20.Controls.Add(this.label75);
            this.grp_PTDRH_20.Controls.Add(this.label76);
            this.grp_PTDRH_20.Controls.Add(this.label77);
            this.grp_PTDRH_20.Controls.Add(this.tbx_PTDRH_20_X);
            this.grp_PTDRH_20.Controls.Add(this.tbx_PTDRH_20_Y);
            this.grp_PTDRH_20.Location = new System.Drawing.Point(20, 201);
            this.grp_PTDRH_20.Name = "grp_PTDRH_20";
            this.grp_PTDRH_20.Size = new System.Drawing.Size(136, 58);
            this.grp_PTDRH_20.TabIndex = 22;
            this.grp_PTDRH_20.TabStop = false;
            this.grp_PTDRH_20.Text = "PTD_RH";
            this.grp_PTDRH_20.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTDRH_20_Z
            // 
            this.tbx_PTDRH_20_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTDRH_20_Z.Name = "tbx_PTDRH_20_Z";
            this.tbx_PTDRH_20_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTDRH_20_Z.TabIndex = 17;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(103, 16);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(11, 12);
            this.label75.TabIndex = 15;
            this.label75.Text = "Z";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(15, 16);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(11, 12);
            this.label76.TabIndex = 8;
            this.label76.Text = "X";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(62, 16);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(11, 12);
            this.label77.TabIndex = 14;
            this.label77.Text = "Y";
            // 
            // tbx_PTDRH_20_X
            // 
            this.tbx_PTDRH_20_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTDRH_20_X.Name = "tbx_PTDRH_20_X";
            this.tbx_PTDRH_20_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTDRH_20_X.TabIndex = 9;
            // 
            // tbx_PTDRH_20_Y
            // 
            this.tbx_PTDRH_20_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTDRH_20_Y.Name = "tbx_PTDRH_20_Y";
            this.tbx_PTDRH_20_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTDRH_20_Y.TabIndex = 10;
            // 
            // grp_PTHRHG_7
            // 
            this.grp_PTHRHG_7.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTHRHG_7.Controls.Add(this.tbx_PTHRHG_7_Z);
            this.grp_PTHRHG_7.Controls.Add(this.label70);
            this.grp_PTHRHG_7.Controls.Add(this.label73);
            this.grp_PTHRHG_7.Controls.Add(this.label74);
            this.grp_PTHRHG_7.Controls.Add(this.tbx_PTHRHG_7_X);
            this.grp_PTHRHG_7.Controls.Add(this.tbx_PTHRHG_7_Y);
            this.grp_PTHRHG_7.Location = new System.Drawing.Point(5, 122);
            this.grp_PTHRHG_7.Name = "grp_PTHRHG_7";
            this.grp_PTHRHG_7.Size = new System.Drawing.Size(136, 58);
            this.grp_PTHRHG_7.TabIndex = 21;
            this.grp_PTHRHG_7.TabStop = false;
            this.grp_PTHRHG_7.Text = "PTH_RH/G";
            this.grp_PTHRHG_7.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTHRHG_7_Z
            // 
            this.tbx_PTHRHG_7_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTHRHG_7_Z.Name = "tbx_PTHRHG_7_Z";
            this.tbx_PTHRHG_7_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTHRHG_7_Z.TabIndex = 16;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(103, 16);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(11, 12);
            this.label70.TabIndex = 15;
            this.label70.Text = "Z";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(15, 16);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(11, 12);
            this.label73.TabIndex = 8;
            this.label73.Text = "X";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(62, 16);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(11, 12);
            this.label74.TabIndex = 14;
            this.label74.Text = "Y";
            // 
            // tbx_PTHRHG_7_X
            // 
            this.tbx_PTHRHG_7_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTHRHG_7_X.Name = "tbx_PTHRHG_7_X";
            this.tbx_PTHRHG_7_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTHRHG_7_X.TabIndex = 9;
            // 
            // tbx_PTHRHG_7_Y
            // 
            this.tbx_PTHRHG_7_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTHRHG_7_Y.Name = "tbx_PTHRHG_7_Y";
            this.tbx_PTHRHG_7_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTHRHG_7_Y.TabIndex = 10;
            // 
            // grp_PTPRH_24
            // 
            this.grp_PTPRH_24.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTPRH_24.Controls.Add(this.tbx_PTPRH_24_Z);
            this.grp_PTPRH_24.Controls.Add(this.label54);
            this.grp_PTPRH_24.Controls.Add(this.label62);
            this.grp_PTPRH_24.Controls.Add(this.label66);
            this.grp_PTPRH_24.Controls.Add(this.tbx_PTPRH_24_X);
            this.grp_PTPRH_24.Controls.Add(this.tbx_PTPRH_24_Y);
            this.grp_PTPRH_24.Location = new System.Drawing.Point(305, 6);
            this.grp_PTPRH_24.Name = "grp_PTPRH_24";
            this.grp_PTPRH_24.Size = new System.Drawing.Size(72, 87);
            this.grp_PTPRH_24.TabIndex = 20;
            this.grp_PTPRH_24.TabStop = false;
            this.grp_PTPRH_24.Text = "PTP_RH";
            this.grp_PTPRH_24.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTPRH_24_Z
            // 
            this.tbx_PTPRH_24_Z.Location = new System.Drawing.Point(26, 60);
            this.tbx_PTPRH_24_Z.Name = "tbx_PTPRH_24_Z";
            this.tbx_PTPRH_24_Z.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTPRH_24_Z.TabIndex = 16;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(9, 65);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(11, 12);
            this.label54.TabIndex = 15;
            this.label54.Text = "Z";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(9, 22);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(11, 12);
            this.label62.TabIndex = 8;
            this.label62.Text = "X";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(9, 43);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(11, 12);
            this.label66.TabIndex = 14;
            this.label66.Text = "Y";
            // 
            // tbx_PTPRH_24_X
            // 
            this.tbx_PTPRH_24_X.Location = new System.Drawing.Point(26, 18);
            this.tbx_PTPRH_24_X.Name = "tbx_PTPRH_24_X";
            this.tbx_PTPRH_24_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTPRH_24_X.TabIndex = 9;
            // 
            // tbx_PTPRH_24_Y
            // 
            this.tbx_PTPRH_24_Y.Location = new System.Drawing.Point(26, 39);
            this.tbx_PTPRH_24_Y.Name = "tbx_PTPRH_24_Y";
            this.tbx_PTPRH_24_Y.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTPRH_24_Y.TabIndex = 10;
            // 
            // grp_PTA3D_16
            // 
            this.grp_PTA3D_16.BackColor = System.Drawing.Color.LimeGreen;
            this.grp_PTA3D_16.Controls.Add(this.tbx_PTA3D_16_Z);
            this.grp_PTA3D_16.Controls.Add(this.label43);
            this.grp_PTA3D_16.Controls.Add(this.label52);
            this.grp_PTA3D_16.Controls.Add(this.label53);
            this.grp_PTA3D_16.Controls.Add(this.tbx_PTA3D_16_X);
            this.grp_PTA3D_16.Controls.Add(this.tbx_PTA3D_16_Y);
            this.grp_PTA3D_16.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grp_PTA3D_16.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grp_PTA3D_16.Location = new System.Drawing.Point(44, 29);
            this.grp_PTA3D_16.Name = "grp_PTA3D_16";
            this.grp_PTA3D_16.Size = new System.Drawing.Size(136, 58);
            this.grp_PTA3D_16.TabIndex = 19;
            this.grp_PTA3D_16.TabStop = false;
            this.grp_PTA3D_16.Text = "PTA3/D";
            this.grp_PTA3D_16.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTA3D_16_Z
            // 
            this.tbx_PTA3D_16_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTA3D_16_Z.Name = "tbx_PTA3D_16_Z";
            this.tbx_PTA3D_16_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA3D_16_Z.TabIndex = 16;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(103, 16);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(11, 12);
            this.label43.TabIndex = 15;
            this.label43.Text = "Z";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(15, 16);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(11, 12);
            this.label52.TabIndex = 8;
            this.label52.Text = "X";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(62, 16);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(11, 12);
            this.label53.TabIndex = 14;
            this.label53.Text = "Y";
            // 
            // tbx_PTA3D_16_X
            // 
            this.tbx_PTA3D_16_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTA3D_16_X.Name = "tbx_PTA3D_16_X";
            this.tbx_PTA3D_16_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTA3D_16_X.TabIndex = 9;
            // 
            // tbx_PTA3D_16_Y
            // 
            this.tbx_PTA3D_16_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTA3D_16_Y.Name = "tbx_PTA3D_16_Y";
            this.tbx_PTA3D_16_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA3D_16_Y.TabIndex = 10;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.InitialImage")));
            this.pictureBox2.Location = new System.Drawing.Point(99, 76);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(640, 302);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // tabPage9
            // 
            this.tabPage9.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPage9.Controls.Add(this.grp_PTGRH_2);
            this.tabPage9.Controls.Add(this.grp_PTFRHF_11);
            this.tabPage9.Controls.Add(this.grp_PTGLH_1);
            this.tabPage9.Controls.Add(this.grp_PTFLHF_12);
            this.tabPage9.Controls.Add(this.pictureBox3);
            this.tabPage9.Location = new System.Drawing.Point(4, 21);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(843, 481);
            this.tabPage9.TabIndex = 6;
            this.tabPage9.Text = "B515MCA_FCM_2";
            // 
            // grp_PTGRH_2
            // 
            this.grp_PTGRH_2.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTGRH_2.Controls.Add(this.tbx_PTGRH_2_Z);
            this.grp_PTGRH_2.Controls.Add(this.label117);
            this.grp_PTGRH_2.Controls.Add(this.label118);
            this.grp_PTGRH_2.Controls.Add(this.label119);
            this.grp_PTGRH_2.Controls.Add(this.tbx_PTGRH_2_X);
            this.grp_PTGRH_2.Controls.Add(this.tbx_PTGRH_2_Y);
            this.grp_PTGRH_2.Location = new System.Drawing.Point(698, 253);
            this.grp_PTGRH_2.Name = "grp_PTGRH_2";
            this.grp_PTGRH_2.Size = new System.Drawing.Size(136, 58);
            this.grp_PTGRH_2.TabIndex = 102;
            this.grp_PTGRH_2.TabStop = false;
            this.grp_PTGRH_2.Text = "PTG_RH";
            this.grp_PTGRH_2.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTGRH_2_Z
            // 
            this.tbx_PTGRH_2_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTGRH_2_Z.Name = "tbx_PTGRH_2_Z";
            this.tbx_PTGRH_2_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTGRH_2_Z.TabIndex = 16;
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(103, 16);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(11, 12);
            this.label117.TabIndex = 15;
            this.label117.Text = "Z";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(15, 16);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(11, 12);
            this.label118.TabIndex = 8;
            this.label118.Text = "X";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(62, 16);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(11, 12);
            this.label119.TabIndex = 14;
            this.label119.Text = "Y";
            // 
            // tbx_PTGRH_2_X
            // 
            this.tbx_PTGRH_2_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTGRH_2_X.Name = "tbx_PTGRH_2_X";
            this.tbx_PTGRH_2_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTGRH_2_X.TabIndex = 9;
            // 
            // tbx_PTGRH_2_Y
            // 
            this.tbx_PTGRH_2_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTGRH_2_Y.Name = "tbx_PTGRH_2_Y";
            this.tbx_PTGRH_2_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTGRH_2_Y.TabIndex = 10;
            // 
            // grp_PTFRHF_11
            // 
            this.grp_PTFRHF_11.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTFRHF_11.Controls.Add(this.tbx_PTFRHF_11_Z);
            this.grp_PTFRHF_11.Controls.Add(this.label114);
            this.grp_PTFRHF_11.Controls.Add(this.label115);
            this.grp_PTFRHF_11.Controls.Add(this.label116);
            this.grp_PTFRHF_11.Controls.Add(this.tbx_PTFRHF_11_X);
            this.grp_PTFRHF_11.Controls.Add(this.tbx_PTFRHF_11_Y);
            this.grp_PTFRHF_11.Location = new System.Drawing.Point(698, 136);
            this.grp_PTFRHF_11.Name = "grp_PTFRHF_11";
            this.grp_PTFRHF_11.Size = new System.Drawing.Size(136, 58);
            this.grp_PTFRHF_11.TabIndex = 106;
            this.grp_PTFRHF_11.TabStop = false;
            this.grp_PTFRHF_11.Text = "PTF_RH/F";
            this.grp_PTFRHF_11.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTFRHF_11_Z
            // 
            this.tbx_PTFRHF_11_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTFRHF_11_Z.Name = "tbx_PTFRHF_11_Z";
            this.tbx_PTFRHF_11_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTFRHF_11_Z.TabIndex = 16;
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(103, 16);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(11, 12);
            this.label114.TabIndex = 15;
            this.label114.Text = "Z";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(15, 16);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(11, 12);
            this.label115.TabIndex = 8;
            this.label115.Text = "X";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(62, 16);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(11, 12);
            this.label116.TabIndex = 14;
            this.label116.Text = "Y";
            // 
            // tbx_PTFRHF_11_X
            // 
            this.tbx_PTFRHF_11_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTFRHF_11_X.Name = "tbx_PTFRHF_11_X";
            this.tbx_PTFRHF_11_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTFRHF_11_X.TabIndex = 9;
            // 
            // tbx_PTFRHF_11_Y
            // 
            this.tbx_PTFRHF_11_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTFRHF_11_Y.Name = "tbx_PTFRHF_11_Y";
            this.tbx_PTFRHF_11_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTFRHF_11_Y.TabIndex = 10;
            // 
            // grp_PTGLH_1
            // 
            this.grp_PTGLH_1.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTGLH_1.Controls.Add(this.tbx_PTGLH_1_Z);
            this.grp_PTGLH_1.Controls.Add(this.label111);
            this.grp_PTGLH_1.Controls.Add(this.label112);
            this.grp_PTGLH_1.Controls.Add(this.label113);
            this.grp_PTGLH_1.Controls.Add(this.tbx_PTGLH_1_X);
            this.grp_PTGLH_1.Controls.Add(this.tbx_PTGLH_1_Y);
            this.grp_PTGLH_1.Location = new System.Drawing.Point(10, 248);
            this.grp_PTGLH_1.Name = "grp_PTGLH_1";
            this.grp_PTGLH_1.Size = new System.Drawing.Size(136, 58);
            this.grp_PTGLH_1.TabIndex = 101;
            this.grp_PTGLH_1.TabStop = false;
            this.grp_PTGLH_1.Text = "PTG_LH";
            this.grp_PTGLH_1.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTGLH_1_Z
            // 
            this.tbx_PTGLH_1_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTGLH_1_Z.Name = "tbx_PTGLH_1_Z";
            this.tbx_PTGLH_1_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTGLH_1_Z.TabIndex = 16;
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(103, 16);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(11, 12);
            this.label111.TabIndex = 15;
            this.label111.Text = "Z";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(15, 16);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(11, 12);
            this.label112.TabIndex = 8;
            this.label112.Text = "X";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(62, 16);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(11, 12);
            this.label113.TabIndex = 14;
            this.label113.Text = "Y";
            // 
            // tbx_PTGLH_1_X
            // 
            this.tbx_PTGLH_1_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTGLH_1_X.Name = "tbx_PTGLH_1_X";
            this.tbx_PTGLH_1_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTGLH_1_X.TabIndex = 9;
            // 
            // tbx_PTGLH_1_Y
            // 
            this.tbx_PTGLH_1_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTGLH_1_Y.Name = "tbx_PTGLH_1_Y";
            this.tbx_PTGLH_1_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTGLH_1_Y.TabIndex = 10;
            // 
            // grp_PTFLHF_12
            // 
            this.grp_PTFLHF_12.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTFLHF_12.Controls.Add(this.tbx_PTFLHF_12_Z);
            this.grp_PTFLHF_12.Controls.Add(this.label108);
            this.grp_PTFLHF_12.Controls.Add(this.label109);
            this.grp_PTFLHF_12.Controls.Add(this.label110);
            this.grp_PTFLHF_12.Controls.Add(this.tbx_PTFLHF_12_X);
            this.grp_PTFLHF_12.Controls.Add(this.tbx_PTFLHF_12_Y);
            this.grp_PTFLHF_12.Location = new System.Drawing.Point(17, 137);
            this.grp_PTFLHF_12.Name = "grp_PTFLHF_12";
            this.grp_PTFLHF_12.Size = new System.Drawing.Size(136, 58);
            this.grp_PTFLHF_12.TabIndex = 107;
            this.grp_PTFLHF_12.TabStop = false;
            this.grp_PTFLHF_12.Text = "PTF_LH/F";
            this.grp_PTFLHF_12.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTFLHF_12_Z
            // 
            this.tbx_PTFLHF_12_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTFLHF_12_Z.Name = "tbx_PTFLHF_12_Z";
            this.tbx_PTFLHF_12_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTFLHF_12_Z.TabIndex = 16;
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(103, 16);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(11, 12);
            this.label108.TabIndex = 15;
            this.label108.Text = "Z";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(15, 16);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(11, 12);
            this.label109.TabIndex = 8;
            this.label109.Text = "X";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(62, 16);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(11, 12);
            this.label110.TabIndex = 14;
            this.label110.Text = "Y";
            // 
            // tbx_PTFLHF_12_X
            // 
            this.tbx_PTFLHF_12_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTFLHF_12_X.Name = "tbx_PTFLHF_12_X";
            this.tbx_PTFLHF_12_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTFLHF_12_X.TabIndex = 9;
            // 
            // tbx_PTFLHF_12_Y
            // 
            this.tbx_PTFLHF_12_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTFLHF_12_Y.Name = "tbx_PTFLHF_12_Y";
            this.tbx_PTFLHF_12_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTFLHF_12_Y.TabIndex = 10;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.InitialImage")));
            this.pictureBox3.Location = new System.Drawing.Point(130, 64);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(575, 295);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // tabPage10
            // 
            this.tabPage10.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPage10.Controls.Add(this.grp_PTA12_6);
            this.tabPage10.Controls.Add(this.groupBox41);
            this.tabPage10.Controls.Add(this.grp_PTJRH_17);
            this.tabPage10.Controls.Add(this.groupBox39);
            this.tabPage10.Controls.Add(this.grp_PTIRH_10);
            this.tabPage10.Controls.Add(this.groupBox27);
            this.tabPage10.Controls.Add(this.grp_PTB_4);
            this.tabPage10.Controls.Add(this.groupBox35);
            this.tabPage10.Controls.Add(this.grp_PTA22_5);
            this.tabPage10.Controls.Add(this.groupBox32);
            this.tabPage10.Controls.Add(this.grp_PTJLH_13);
            this.tabPage10.Controls.Add(this.groupBox34);
            this.tabPage10.Controls.Add(this.groupBox30);
            this.tabPage10.Controls.Add(this.grp_PTILH_9);
            this.tabPage10.Controls.Add(this.grp_PTC_3);
            this.tabPage10.Controls.Add(this.pictureBox4);
            this.tabPage10.Location = new System.Drawing.Point(4, 21);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(843, 481);
            this.tabPage10.TabIndex = 7;
            this.tabPage10.Text = "B515MCA_FCM_3";
            // 
            // grp_PTA12_6
            // 
            this.grp_PTA12_6.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTA12_6.Controls.Add(this.tbx_PTA12_6_Z);
            this.grp_PTA12_6.Controls.Add(this.label165);
            this.grp_PTA12_6.Controls.Add(this.label166);
            this.grp_PTA12_6.Controls.Add(this.label167);
            this.grp_PTA12_6.Controls.Add(this.tbx_PTA12_6_X);
            this.grp_PTA12_6.Controls.Add(this.tbx_PTA12_6_Y);
            this.grp_PTA12_6.Location = new System.Drawing.Point(698, 317);
            this.grp_PTA12_6.Name = "grp_PTA12_6";
            this.grp_PTA12_6.Size = new System.Drawing.Size(136, 58);
            this.grp_PTA12_6.TabIndex = 45;
            this.grp_PTA12_6.TabStop = false;
            this.grp_PTA12_6.Text = "PTA1_2";
            this.grp_PTA12_6.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTA12_6_Z
            // 
            this.tbx_PTA12_6_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTA12_6_Z.Name = "tbx_PTA12_6_Z";
            this.tbx_PTA12_6_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA12_6_Z.TabIndex = 16;
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Location = new System.Drawing.Point(103, 16);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(11, 12);
            this.label165.TabIndex = 15;
            this.label165.Text = "Z";
            // 
            // label166
            // 
            this.label166.AutoSize = true;
            this.label166.Location = new System.Drawing.Point(15, 16);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(11, 12);
            this.label166.TabIndex = 8;
            this.label166.Text = "X";
            // 
            // label167
            // 
            this.label167.AutoSize = true;
            this.label167.Location = new System.Drawing.Point(62, 16);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(11, 12);
            this.label167.TabIndex = 14;
            this.label167.Text = "Y";
            // 
            // tbx_PTA12_6_X
            // 
            this.tbx_PTA12_6_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTA12_6_X.Name = "tbx_PTA12_6_X";
            this.tbx_PTA12_6_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTA12_6_X.TabIndex = 9;
            // 
            // tbx_PTA12_6_Y
            // 
            this.tbx_PTA12_6_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTA12_6_Y.Name = "tbx_PTA12_6_Y";
            this.tbx_PTA12_6_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA12_6_Y.TabIndex = 10;
            // 
            // groupBox41
            // 
            this.groupBox41.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox41.Controls.Add(this.textBox97);
            this.groupBox41.Controls.Add(this.label162);
            this.groupBox41.Controls.Add(this.label163);
            this.groupBox41.Controls.Add(this.label164);
            this.groupBox41.Controls.Add(this.textBox98);
            this.groupBox41.Controls.Add(this.textBox99);
            this.groupBox41.Location = new System.Drawing.Point(698, 253);
            this.groupBox41.Name = "groupBox41";
            this.groupBox41.Size = new System.Drawing.Size(110, 40);
            this.groupBox41.TabIndex = 44;
            this.groupBox41.TabStop = false;
            this.groupBox41.Text = "PTA1_2_Align";
            // 
            // textBox97
            // 
            this.textBox97.Location = new System.Drawing.Point(90, 32);
            this.textBox97.Name = "textBox97";
            this.textBox97.Size = new System.Drawing.Size(39, 21);
            this.textBox97.TabIndex = 16;
            this.textBox97.Visible = false;
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Location = new System.Drawing.Point(103, 16);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(11, 12);
            this.label162.TabIndex = 15;
            this.label162.Text = "Z";
            this.label162.Visible = false;
            // 
            // label163
            // 
            this.label163.AutoSize = true;
            this.label163.Location = new System.Drawing.Point(15, 16);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(11, 12);
            this.label163.TabIndex = 8;
            this.label163.Text = "X";
            this.label163.Visible = false;
            // 
            // label164
            // 
            this.label164.AutoSize = true;
            this.label164.Location = new System.Drawing.Point(62, 16);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(11, 12);
            this.label164.TabIndex = 14;
            this.label164.Text = "Y";
            this.label164.Visible = false;
            // 
            // textBox98
            // 
            this.textBox98.Location = new System.Drawing.Point(6, 32);
            this.textBox98.Name = "textBox98";
            this.textBox98.Size = new System.Drawing.Size(41, 21);
            this.textBox98.TabIndex = 9;
            this.textBox98.Visible = false;
            // 
            // textBox99
            // 
            this.textBox99.Location = new System.Drawing.Point(49, 32);
            this.textBox99.Name = "textBox99";
            this.textBox99.Size = new System.Drawing.Size(39, 21);
            this.textBox99.TabIndex = 10;
            this.textBox99.Visible = false;
            // 
            // grp_PTJRH_17
            // 
            this.grp_PTJRH_17.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTJRH_17.Controls.Add(this.tbx_PTJRH_17_Z);
            this.grp_PTJRH_17.Controls.Add(this.label159);
            this.grp_PTJRH_17.Controls.Add(this.label160);
            this.grp_PTJRH_17.Controls.Add(this.label161);
            this.grp_PTJRH_17.Controls.Add(this.tbx_PTJRH_17_X);
            this.grp_PTJRH_17.Controls.Add(this.tbx_PTJRH_17_Y);
            this.grp_PTJRH_17.Location = new System.Drawing.Point(698, 180);
            this.grp_PTJRH_17.Name = "grp_PTJRH_17";
            this.grp_PTJRH_17.Size = new System.Drawing.Size(136, 58);
            this.grp_PTJRH_17.TabIndex = 43;
            this.grp_PTJRH_17.TabStop = false;
            this.grp_PTJRH_17.Text = "PTJ_RH";
            this.grp_PTJRH_17.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTJRH_17_Z
            // 
            this.tbx_PTJRH_17_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTJRH_17_Z.Name = "tbx_PTJRH_17_Z";
            this.tbx_PTJRH_17_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTJRH_17_Z.TabIndex = 16;
            // 
            // label159
            // 
            this.label159.AutoSize = true;
            this.label159.Location = new System.Drawing.Point(103, 16);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(11, 12);
            this.label159.TabIndex = 15;
            this.label159.Text = "Z";
            // 
            // label160
            // 
            this.label160.AutoSize = true;
            this.label160.Location = new System.Drawing.Point(15, 16);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(11, 12);
            this.label160.TabIndex = 8;
            this.label160.Text = "X";
            // 
            // label161
            // 
            this.label161.AutoSize = true;
            this.label161.Location = new System.Drawing.Point(62, 16);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(11, 12);
            this.label161.TabIndex = 14;
            this.label161.Text = "Y";
            // 
            // tbx_PTJRH_17_X
            // 
            this.tbx_PTJRH_17_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTJRH_17_X.Name = "tbx_PTJRH_17_X";
            this.tbx_PTJRH_17_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTJRH_17_X.TabIndex = 9;
            // 
            // tbx_PTJRH_17_Y
            // 
            this.tbx_PTJRH_17_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTJRH_17_Y.Name = "tbx_PTJRH_17_Y";
            this.tbx_PTJRH_17_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTJRH_17_Y.TabIndex = 10;
            // 
            // groupBox39
            // 
            this.groupBox39.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox39.Controls.Add(this.textBox91);
            this.groupBox39.Controls.Add(this.label156);
            this.groupBox39.Controls.Add(this.label157);
            this.groupBox39.Controls.Add(this.label158);
            this.groupBox39.Controls.Add(this.textBox92);
            this.groupBox39.Controls.Add(this.textBox93);
            this.groupBox39.Location = new System.Drawing.Point(700, 96);
            this.groupBox39.Name = "groupBox39";
            this.groupBox39.Size = new System.Drawing.Size(108, 38);
            this.groupBox39.TabIndex = 42;
            this.groupBox39.TabStop = false;
            this.groupBox39.Text = "PTJ_RH_Align";
            this.groupBox39.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // textBox91
            // 
            this.textBox91.Location = new System.Drawing.Point(90, 32);
            this.textBox91.Name = "textBox91";
            this.textBox91.Size = new System.Drawing.Size(39, 21);
            this.textBox91.TabIndex = 16;
            this.textBox91.Visible = false;
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Location = new System.Drawing.Point(103, 16);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(11, 12);
            this.label156.TabIndex = 15;
            this.label156.Text = "Z";
            this.label156.Visible = false;
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Location = new System.Drawing.Point(15, 16);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(11, 12);
            this.label157.TabIndex = 8;
            this.label157.Text = "X";
            this.label157.Visible = false;
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.Location = new System.Drawing.Point(62, 16);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(11, 12);
            this.label158.TabIndex = 14;
            this.label158.Text = "Y";
            this.label158.Visible = false;
            // 
            // textBox92
            // 
            this.textBox92.Location = new System.Drawing.Point(6, 32);
            this.textBox92.Name = "textBox92";
            this.textBox92.Size = new System.Drawing.Size(41, 21);
            this.textBox92.TabIndex = 9;
            this.textBox92.Visible = false;
            // 
            // textBox93
            // 
            this.textBox93.Location = new System.Drawing.Point(49, 32);
            this.textBox93.Name = "textBox93";
            this.textBox93.Size = new System.Drawing.Size(39, 21);
            this.textBox93.TabIndex = 10;
            this.textBox93.Visible = false;
            // 
            // grp_PTIRH_10
            // 
            this.grp_PTIRH_10.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTIRH_10.Controls.Add(this.groupBox38);
            this.grp_PTIRH_10.Controls.Add(this.tbx_PTIRH_10_Z);
            this.grp_PTIRH_10.Controls.Add(this.label150);
            this.grp_PTIRH_10.Controls.Add(this.label151);
            this.grp_PTIRH_10.Controls.Add(this.label152);
            this.grp_PTIRH_10.Controls.Add(this.tbx_PTIRH_10_X);
            this.grp_PTIRH_10.Controls.Add(this.tbx_PTIRH_10_Y);
            this.grp_PTIRH_10.Location = new System.Drawing.Point(621, 40);
            this.grp_PTIRH_10.Name = "grp_PTIRH_10";
            this.grp_PTIRH_10.Size = new System.Drawing.Size(72, 87);
            this.grp_PTIRH_10.TabIndex = 41;
            this.grp_PTIRH_10.TabStop = false;
            this.grp_PTIRH_10.Text = "PTI_RH";
            this.grp_PTIRH_10.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // groupBox38
            // 
            this.groupBox38.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox38.Controls.Add(this.textBox88);
            this.groupBox38.Controls.Add(this.label153);
            this.groupBox38.Controls.Add(this.label154);
            this.groupBox38.Controls.Add(this.label155);
            this.groupBox38.Controls.Add(this.textBox89);
            this.groupBox38.Controls.Add(this.textBox90);
            this.groupBox38.Location = new System.Drawing.Point(73, 68);
            this.groupBox38.Name = "groupBox38";
            this.groupBox38.Size = new System.Drawing.Size(136, 58);
            this.groupBox38.TabIndex = 42;
            this.groupBox38.TabStop = false;
            this.groupBox38.Text = "PTF_LH/F";
            // 
            // textBox88
            // 
            this.textBox88.Location = new System.Drawing.Point(90, 32);
            this.textBox88.Name = "textBox88";
            this.textBox88.Size = new System.Drawing.Size(39, 21);
            this.textBox88.TabIndex = 16;
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Location = new System.Drawing.Point(103, 16);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(11, 12);
            this.label153.TabIndex = 15;
            this.label153.Text = "Z";
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Location = new System.Drawing.Point(15, 16);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(11, 12);
            this.label154.TabIndex = 8;
            this.label154.Text = "X";
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Location = new System.Drawing.Point(62, 16);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(11, 12);
            this.label155.TabIndex = 14;
            this.label155.Text = "Y";
            // 
            // textBox89
            // 
            this.textBox89.Location = new System.Drawing.Point(6, 32);
            this.textBox89.Name = "textBox89";
            this.textBox89.Size = new System.Drawing.Size(41, 21);
            this.textBox89.TabIndex = 9;
            this.textBox89.Text = "admin";
            // 
            // textBox90
            // 
            this.textBox90.Location = new System.Drawing.Point(49, 32);
            this.textBox90.Name = "textBox90";
            this.textBox90.Size = new System.Drawing.Size(39, 21);
            this.textBox90.TabIndex = 10;
            // 
            // tbx_PTIRH_10_Z
            // 
            this.tbx_PTIRH_10_Z.Location = new System.Drawing.Point(26, 60);
            this.tbx_PTIRH_10_Z.Name = "tbx_PTIRH_10_Z";
            this.tbx_PTIRH_10_Z.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTIRH_10_Z.TabIndex = 16;
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Location = new System.Drawing.Point(9, 65);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(11, 12);
            this.label150.TabIndex = 15;
            this.label150.Text = "Z";
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Location = new System.Drawing.Point(9, 22);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(11, 12);
            this.label151.TabIndex = 8;
            this.label151.Text = "X";
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Location = new System.Drawing.Point(9, 43);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(11, 12);
            this.label152.TabIndex = 14;
            this.label152.Text = "Y";
            // 
            // tbx_PTIRH_10_X
            // 
            this.tbx_PTIRH_10_X.Location = new System.Drawing.Point(26, 18);
            this.tbx_PTIRH_10_X.Name = "tbx_PTIRH_10_X";
            this.tbx_PTIRH_10_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTIRH_10_X.TabIndex = 9;
            // 
            // tbx_PTIRH_10_Y
            // 
            this.tbx_PTIRH_10_Y.Location = new System.Drawing.Point(26, 39);
            this.tbx_PTIRH_10_Y.Name = "tbx_PTIRH_10_Y";
            this.tbx_PTIRH_10_Y.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTIRH_10_Y.TabIndex = 10;
            // 
            // groupBox27
            // 
            this.groupBox27.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox27.Controls.Add(this.textBox55);
            this.groupBox27.Controls.Add(this.label120);
            this.groupBox27.Controls.Add(this.label121);
            this.groupBox27.Controls.Add(this.label122);
            this.groupBox27.Controls.Add(this.textBox56);
            this.groupBox27.Controls.Add(this.textBox57);
            this.groupBox27.Location = new System.Drawing.Point(322, 78);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(79, 38);
            this.groupBox27.TabIndex = 40;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "PTC_Align";
            // 
            // textBox55
            // 
            this.textBox55.Location = new System.Drawing.Point(26, 60);
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new System.Drawing.Size(41, 21);
            this.textBox55.TabIndex = 16;
            this.textBox55.Visible = false;
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Location = new System.Drawing.Point(9, 65);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(11, 12);
            this.label120.TabIndex = 15;
            this.label120.Text = "Z";
            this.label120.Visible = false;
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Location = new System.Drawing.Point(9, 22);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(11, 12);
            this.label121.TabIndex = 8;
            this.label121.Text = "X";
            this.label121.Visible = false;
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(9, 43);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(11, 12);
            this.label122.TabIndex = 14;
            this.label122.Text = "Y";
            this.label122.Visible = false;
            // 
            // textBox56
            // 
            this.textBox56.Location = new System.Drawing.Point(26, 18);
            this.textBox56.Name = "textBox56";
            this.textBox56.Size = new System.Drawing.Size(41, 21);
            this.textBox56.TabIndex = 9;
            this.textBox56.Visible = false;
            // 
            // textBox57
            // 
            this.textBox57.Location = new System.Drawing.Point(26, 39);
            this.textBox57.Name = "textBox57";
            this.textBox57.Size = new System.Drawing.Size(41, 21);
            this.textBox57.TabIndex = 10;
            this.textBox57.Visible = false;
            // 
            // grp_PTB_4
            // 
            this.grp_PTB_4.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTB_4.Controls.Add(this.tbx_PTB_4_Z);
            this.grp_PTB_4.Controls.Add(this.label147);
            this.grp_PTB_4.Controls.Add(this.label148);
            this.grp_PTB_4.Controls.Add(this.label149);
            this.grp_PTB_4.Controls.Add(this.tbx_PTB_4_X);
            this.grp_PTB_4.Controls.Add(this.tbx_PTB_4_Y);
            this.grp_PTB_4.Location = new System.Drawing.Point(543, 29);
            this.grp_PTB_4.Name = "grp_PTB_4";
            this.grp_PTB_4.Size = new System.Drawing.Size(72, 87);
            this.grp_PTB_4.TabIndex = 39;
            this.grp_PTB_4.TabStop = false;
            this.grp_PTB_4.Text = "PTB";
            this.grp_PTB_4.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTB_4_Z
            // 
            this.tbx_PTB_4_Z.Location = new System.Drawing.Point(26, 60);
            this.tbx_PTB_4_Z.Name = "tbx_PTB_4_Z";
            this.tbx_PTB_4_Z.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTB_4_Z.TabIndex = 16;
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Location = new System.Drawing.Point(9, 65);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(11, 12);
            this.label147.TabIndex = 15;
            this.label147.Text = "Z";
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Location = new System.Drawing.Point(9, 22);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(11, 12);
            this.label148.TabIndex = 8;
            this.label148.Text = "X";
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Location = new System.Drawing.Point(9, 43);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(11, 12);
            this.label149.TabIndex = 14;
            this.label149.Text = "Y";
            // 
            // tbx_PTB_4_X
            // 
            this.tbx_PTB_4_X.Location = new System.Drawing.Point(26, 18);
            this.tbx_PTB_4_X.Name = "tbx_PTB_4_X";
            this.tbx_PTB_4_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTB_4_X.TabIndex = 9;
            // 
            // tbx_PTB_4_Y
            // 
            this.tbx_PTB_4_Y.Location = new System.Drawing.Point(26, 39);
            this.tbx_PTB_4_Y.Name = "tbx_PTB_4_Y";
            this.tbx_PTB_4_Y.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTB_4_Y.TabIndex = 10;
            // 
            // groupBox35
            // 
            this.groupBox35.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox35.Controls.Add(this.textBox79);
            this.groupBox35.Controls.Add(this.label144);
            this.groupBox35.Controls.Add(this.label145);
            this.groupBox35.Controls.Add(this.label146);
            this.groupBox35.Controls.Add(this.textBox80);
            this.groupBox35.Controls.Add(this.textBox81);
            this.groupBox35.Location = new System.Drawing.Point(459, 78);
            this.groupBox35.Name = "groupBox35";
            this.groupBox35.Size = new System.Drawing.Size(78, 38);
            this.groupBox35.TabIndex = 38;
            this.groupBox35.TabStop = false;
            this.groupBox35.Text = "PTB_Align";
            // 
            // textBox79
            // 
            this.textBox79.Location = new System.Drawing.Point(26, 60);
            this.textBox79.Name = "textBox79";
            this.textBox79.Size = new System.Drawing.Size(41, 21);
            this.textBox79.TabIndex = 16;
            this.textBox79.Visible = false;
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Location = new System.Drawing.Point(9, 65);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(11, 12);
            this.label144.TabIndex = 15;
            this.label144.Text = "Z";
            this.label144.Visible = false;
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.Location = new System.Drawing.Point(9, 22);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(11, 12);
            this.label145.TabIndex = 8;
            this.label145.Text = "X";
            this.label145.Visible = false;
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Location = new System.Drawing.Point(9, 43);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(11, 12);
            this.label146.TabIndex = 14;
            this.label146.Text = "Y";
            this.label146.Visible = false;
            // 
            // textBox80
            // 
            this.textBox80.Location = new System.Drawing.Point(26, 18);
            this.textBox80.Name = "textBox80";
            this.textBox80.Size = new System.Drawing.Size(41, 21);
            this.textBox80.TabIndex = 9;
            this.textBox80.Visible = false;
            // 
            // textBox81
            // 
            this.textBox81.Location = new System.Drawing.Point(26, 39);
            this.textBox81.Name = "textBox81";
            this.textBox81.Size = new System.Drawing.Size(41, 21);
            this.textBox81.TabIndex = 10;
            this.textBox81.Visible = false;
            // 
            // grp_PTA22_5
            // 
            this.grp_PTA22_5.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTA22_5.Controls.Add(this.tbx_PTA22_5_Z);
            this.grp_PTA22_5.Controls.Add(this.label138);
            this.grp_PTA22_5.Controls.Add(this.label139);
            this.grp_PTA22_5.Controls.Add(this.label140);
            this.grp_PTA22_5.Controls.Add(this.tbx_PTA22_5_X);
            this.grp_PTA22_5.Controls.Add(this.tbx_PTA22_5_Y);
            this.grp_PTA22_5.Location = new System.Drawing.Point(13, 301);
            this.grp_PTA22_5.Name = "grp_PTA22_5";
            this.grp_PTA22_5.Size = new System.Drawing.Size(136, 58);
            this.grp_PTA22_5.TabIndex = 36;
            this.grp_PTA22_5.TabStop = false;
            this.grp_PTA22_5.Text = "PTA2_2";
            this.grp_PTA22_5.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTA22_5_Z
            // 
            this.tbx_PTA22_5_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTA22_5_Z.Name = "tbx_PTA22_5_Z";
            this.tbx_PTA22_5_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA22_5_Z.TabIndex = 16;
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Location = new System.Drawing.Point(103, 16);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(11, 12);
            this.label138.TabIndex = 15;
            this.label138.Text = "Z";
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Location = new System.Drawing.Point(15, 16);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(11, 12);
            this.label139.TabIndex = 8;
            this.label139.Text = "X";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Location = new System.Drawing.Point(62, 16);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(11, 12);
            this.label140.TabIndex = 14;
            this.label140.Text = "Y";
            // 
            // tbx_PTA22_5_X
            // 
            this.tbx_PTA22_5_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTA22_5_X.Name = "tbx_PTA22_5_X";
            this.tbx_PTA22_5_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTA22_5_X.TabIndex = 9;
            // 
            // tbx_PTA22_5_Y
            // 
            this.tbx_PTA22_5_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTA22_5_Y.Name = "tbx_PTA22_5_Y";
            this.tbx_PTA22_5_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTA22_5_Y.TabIndex = 10;
            // 
            // groupBox32
            // 
            this.groupBox32.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox32.Controls.Add(this.textBox70);
            this.groupBox32.Controls.Add(this.label135);
            this.groupBox32.Controls.Add(this.label136);
            this.groupBox32.Controls.Add(this.label137);
            this.groupBox32.Controls.Add(this.textBox71);
            this.groupBox32.Controls.Add(this.textBox72);
            this.groupBox32.Location = new System.Drawing.Point(52, 253);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Size = new System.Drawing.Size(97, 40);
            this.groupBox32.TabIndex = 37;
            this.groupBox32.TabStop = false;
            this.groupBox32.Text = "PTA2_2_Align";
            // 
            // textBox70
            // 
            this.textBox70.Location = new System.Drawing.Point(90, 32);
            this.textBox70.Name = "textBox70";
            this.textBox70.Size = new System.Drawing.Size(39, 21);
            this.textBox70.TabIndex = 16;
            this.textBox70.Visible = false;
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(103, 16);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(11, 12);
            this.label135.TabIndex = 15;
            this.label135.Text = "Z";
            this.label135.Visible = false;
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Location = new System.Drawing.Point(15, 16);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(11, 12);
            this.label136.TabIndex = 8;
            this.label136.Text = "X";
            this.label136.Visible = false;
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Location = new System.Drawing.Point(62, 16);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(11, 12);
            this.label137.TabIndex = 14;
            this.label137.Text = "Y";
            this.label137.Visible = false;
            // 
            // textBox71
            // 
            this.textBox71.Location = new System.Drawing.Point(6, 32);
            this.textBox71.Name = "textBox71";
            this.textBox71.Size = new System.Drawing.Size(41, 21);
            this.textBox71.TabIndex = 9;
            this.textBox71.Visible = false;
            // 
            // textBox72
            // 
            this.textBox72.Location = new System.Drawing.Point(49, 32);
            this.textBox72.Name = "textBox72";
            this.textBox72.Size = new System.Drawing.Size(39, 21);
            this.textBox72.TabIndex = 10;
            this.textBox72.Visible = false;
            // 
            // grp_PTJLH_13
            // 
            this.grp_PTJLH_13.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTJLH_13.Controls.Add(this.tbx_PTJLH_13_Z);
            this.grp_PTJLH_13.Controls.Add(this.label132);
            this.grp_PTJLH_13.Controls.Add(this.label133);
            this.grp_PTJLH_13.Controls.Add(this.label134);
            this.grp_PTJLH_13.Controls.Add(this.tbx_PTJLH_13_X);
            this.grp_PTJLH_13.Controls.Add(this.tbx_PTJLH_13_Y);
            this.grp_PTJLH_13.Location = new System.Drawing.Point(13, 165);
            this.grp_PTJLH_13.Name = "grp_PTJLH_13";
            this.grp_PTJLH_13.Size = new System.Drawing.Size(136, 58);
            this.grp_PTJLH_13.TabIndex = 108;
            this.grp_PTJLH_13.TabStop = false;
            this.grp_PTJLH_13.Text = "PTJ_LH";
            this.grp_PTJLH_13.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTJLH_13_Z
            // 
            this.tbx_PTJLH_13_Z.Location = new System.Drawing.Point(90, 32);
            this.tbx_PTJLH_13_Z.Name = "tbx_PTJLH_13_Z";
            this.tbx_PTJLH_13_Z.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTJLH_13_Z.TabIndex = 16;
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(103, 16);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(11, 12);
            this.label132.TabIndex = 15;
            this.label132.Text = "Z";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(15, 16);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(11, 12);
            this.label133.TabIndex = 8;
            this.label133.Text = "X";
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(62, 16);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(11, 12);
            this.label134.TabIndex = 14;
            this.label134.Text = "Y";
            // 
            // tbx_PTJLH_13_X
            // 
            this.tbx_PTJLH_13_X.Location = new System.Drawing.Point(6, 32);
            this.tbx_PTJLH_13_X.Name = "tbx_PTJLH_13_X";
            this.tbx_PTJLH_13_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTJLH_13_X.TabIndex = 9;
            // 
            // tbx_PTJLH_13_Y
            // 
            this.tbx_PTJLH_13_Y.Location = new System.Drawing.Point(49, 32);
            this.tbx_PTJLH_13_Y.Name = "tbx_PTJLH_13_Y";
            this.tbx_PTJLH_13_Y.Size = new System.Drawing.Size(39, 21);
            this.tbx_PTJLH_13_Y.TabIndex = 10;
            // 
            // groupBox34
            // 
            this.groupBox34.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox34.Controls.Add(this.textBox76);
            this.groupBox34.Controls.Add(this.label141);
            this.groupBox34.Controls.Add(this.label142);
            this.groupBox34.Controls.Add(this.label143);
            this.groupBox34.Controls.Add(this.textBox77);
            this.groupBox34.Controls.Add(this.textBox78);
            this.groupBox34.Location = new System.Drawing.Point(52, 120);
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.Size = new System.Drawing.Size(97, 39);
            this.groupBox34.TabIndex = 35;
            this.groupBox34.TabStop = false;
            this.groupBox34.Text = "PTJ_LH_Align";
            // 
            // textBox76
            // 
            this.textBox76.Location = new System.Drawing.Point(90, 32);
            this.textBox76.Name = "textBox76";
            this.textBox76.Size = new System.Drawing.Size(39, 21);
            this.textBox76.TabIndex = 16;
            this.textBox76.Visible = false;
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(103, 16);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(11, 12);
            this.label141.TabIndex = 15;
            this.label141.Text = "Z";
            this.label141.Visible = false;
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Location = new System.Drawing.Point(15, 16);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(11, 12);
            this.label142.TabIndex = 8;
            this.label142.Text = "X";
            this.label142.Visible = false;
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Location = new System.Drawing.Point(62, 16);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(11, 12);
            this.label143.TabIndex = 14;
            this.label143.Text = "Y";
            this.label143.Visible = false;
            // 
            // textBox77
            // 
            this.textBox77.Location = new System.Drawing.Point(6, 32);
            this.textBox77.Name = "textBox77";
            this.textBox77.Size = new System.Drawing.Size(41, 21);
            this.textBox77.TabIndex = 9;
            this.textBox77.Visible = false;
            // 
            // textBox78
            // 
            this.textBox78.Location = new System.Drawing.Point(49, 32);
            this.textBox78.Name = "textBox78";
            this.textBox78.Size = new System.Drawing.Size(39, 21);
            this.textBox78.TabIndex = 10;
            this.textBox78.Visible = false;
            // 
            // groupBox30
            // 
            this.groupBox30.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox30.Controls.Add(this.textBox64);
            this.groupBox30.Controls.Add(this.label129);
            this.groupBox30.Controls.Add(this.label130);
            this.groupBox30.Controls.Add(this.label131);
            this.groupBox30.Controls.Add(this.textBox65);
            this.groupBox30.Controls.Add(this.textBox66);
            this.groupBox30.Location = new System.Drawing.Point(88, 129);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new System.Drawing.Size(61, 30);
            this.groupBox30.TabIndex = 35;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "PTF_LH/F";
            // 
            // textBox64
            // 
            this.textBox64.Location = new System.Drawing.Point(90, 32);
            this.textBox64.Name = "textBox64";
            this.textBox64.Size = new System.Drawing.Size(39, 21);
            this.textBox64.TabIndex = 16;
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(103, 16);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(11, 12);
            this.label129.TabIndex = 15;
            this.label129.Text = "Z";
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(15, 16);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(11, 12);
            this.label130.TabIndex = 8;
            this.label130.Text = "X";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(62, 16);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(11, 12);
            this.label131.TabIndex = 14;
            this.label131.Text = "Y";
            // 
            // textBox65
            // 
            this.textBox65.Location = new System.Drawing.Point(6, 32);
            this.textBox65.Name = "textBox65";
            this.textBox65.Size = new System.Drawing.Size(41, 21);
            this.textBox65.TabIndex = 9;
            this.textBox65.Text = "admin";
            // 
            // textBox66
            // 
            this.textBox66.Location = new System.Drawing.Point(49, 32);
            this.textBox66.Name = "textBox66";
            this.textBox66.Size = new System.Drawing.Size(39, 21);
            this.textBox66.TabIndex = 10;
            // 
            // grp_PTILH_9
            // 
            this.grp_PTILH_9.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTILH_9.Controls.Add(this.tbx_PTILH_9_Z);
            this.grp_PTILH_9.Controls.Add(this.label126);
            this.grp_PTILH_9.Controls.Add(this.label127);
            this.grp_PTILH_9.Controls.Add(this.label128);
            this.grp_PTILH_9.Controls.Add(this.tbx_PTILH_9_X);
            this.grp_PTILH_9.Controls.Add(this.tbx_PTILH_9_Y);
            this.grp_PTILH_9.Location = new System.Drawing.Point(166, 35);
            this.grp_PTILH_9.Name = "grp_PTILH_9";
            this.grp_PTILH_9.Size = new System.Drawing.Size(72, 87);
            this.grp_PTILH_9.TabIndex = 105;
            this.grp_PTILH_9.TabStop = false;
            this.grp_PTILH_9.Text = "PTI_LH";
            this.grp_PTILH_9.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTILH_9_Z
            // 
            this.tbx_PTILH_9_Z.Location = new System.Drawing.Point(26, 60);
            this.tbx_PTILH_9_Z.Name = "tbx_PTILH_9_Z";
            this.tbx_PTILH_9_Z.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTILH_9_Z.TabIndex = 16;
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(9, 65);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(11, 12);
            this.label126.TabIndex = 15;
            this.label126.Text = "Z";
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(9, 22);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(11, 12);
            this.label127.TabIndex = 8;
            this.label127.Text = "X";
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(9, 43);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(11, 12);
            this.label128.TabIndex = 14;
            this.label128.Text = "Y";
            // 
            // tbx_PTILH_9_X
            // 
            this.tbx_PTILH_9_X.Location = new System.Drawing.Point(26, 18);
            this.tbx_PTILH_9_X.Name = "tbx_PTILH_9_X";
            this.tbx_PTILH_9_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTILH_9_X.TabIndex = 9;
            // 
            // tbx_PTILH_9_Y
            // 
            this.tbx_PTILH_9_Y.Location = new System.Drawing.Point(26, 39);
            this.tbx_PTILH_9_Y.Name = "tbx_PTILH_9_Y";
            this.tbx_PTILH_9_Y.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTILH_9_Y.TabIndex = 10;
            // 
            // grp_PTC_3
            // 
            this.grp_PTC_3.BackColor = System.Drawing.Color.BurlyWood;
            this.grp_PTC_3.Controls.Add(this.tbx_PTC_3_Z);
            this.grp_PTC_3.Controls.Add(this.label123);
            this.grp_PTC_3.Controls.Add(this.label124);
            this.grp_PTC_3.Controls.Add(this.label125);
            this.grp_PTC_3.Controls.Add(this.tbx_PTC_3_X);
            this.grp_PTC_3.Controls.Add(this.tbx_PTC_3_Y);
            this.grp_PTC_3.Location = new System.Drawing.Point(244, 26);
            this.grp_PTC_3.Name = "grp_PTC_3";
            this.grp_PTC_3.Size = new System.Drawing.Size(72, 87);
            this.grp_PTC_3.TabIndex = 34;
            this.grp_PTC_3.TabStop = false;
            this.grp_PTC_3.Text = "PTC";
            this.grp_PTC_3.MouseHover += new System.EventHandler(this.grp_ToleranceDisp_MouseHover);
            // 
            // tbx_PTC_3_Z
            // 
            this.tbx_PTC_3_Z.Location = new System.Drawing.Point(26, 60);
            this.tbx_PTC_3_Z.Name = "tbx_PTC_3_Z";
            this.tbx_PTC_3_Z.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTC_3_Z.TabIndex = 16;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(9, 65);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(11, 12);
            this.label123.TabIndex = 15;
            this.label123.Text = "Z";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(9, 22);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(11, 12);
            this.label124.TabIndex = 8;
            this.label124.Text = "X";
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(9, 43);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(11, 12);
            this.label125.TabIndex = 14;
            this.label125.Text = "Y";
            // 
            // tbx_PTC_3_X
            // 
            this.tbx_PTC_3_X.Location = new System.Drawing.Point(26, 18);
            this.tbx_PTC_3_X.Name = "tbx_PTC_3_X";
            this.tbx_PTC_3_X.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTC_3_X.TabIndex = 9;
            // 
            // tbx_PTC_3_Y
            // 
            this.tbx_PTC_3_Y.Location = new System.Drawing.Point(26, 39);
            this.tbx_PTC_3_Y.Name = "tbx_PTC_3_Y";
            this.tbx_PTC_3_Y.Size = new System.Drawing.Size(41, 21);
            this.tbx_PTC_3_Y.TabIndex = 10;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.InitialImage")));
            this.pictureBox4.Location = new System.Drawing.Point(138, 108);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(573, 304);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.splitContainer1);
            this.tabPage5.Location = new System.Drawing.Point(4, 21);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(843, 481);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "信号交互界面";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Size = new System.Drawing.Size(837, 475);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 22;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.label56);
            this.groupBox3.Controls.Add(this.tbx_OriginalCode);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 475);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PC->PLC逻辑运行状态";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label_idx32_X37);
            this.groupBox7.Controls.Add(this.label169);
            this.groupBox7.Controls.Add(this.label170);
            this.groupBox7.Controls.Add(this.label_idx31_X36);
            this.groupBox7.Controls.Add(this.label61);
            this.groupBox7.Controls.Add(this.label_idx25_X30);
            this.groupBox7.Controls.Add(this.label65);
            this.groupBox7.Controls.Add(this.label_idx29_X34);
            this.groupBox7.Controls.Add(this.label_idx26_X31);
            this.groupBox7.Controls.Add(this.label71);
            this.groupBox7.Controls.Add(this.label69);
            this.groupBox7.Controls.Add(this.label_idx27_X32);
            this.groupBox7.Location = new System.Drawing.Point(6, 244);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(337, 144);
            this.groupBox7.TabIndex = 35;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "状态3";
            // 
            // label_idx32_X37
            // 
            this.label_idx32_X37.BackColor = System.Drawing.Color.Red;
            this.label_idx32_X37.Location = new System.Drawing.Point(217, 80);
            this.label_idx32_X37.Name = "label_idx32_X37";
            this.label_idx32_X37.Size = new System.Drawing.Size(9, 8);
            this.label_idx32_X37.TabIndex = 36;
            // 
            // label169
            // 
            this.label169.AutoSize = true;
            this.label169.Location = new System.Drawing.Point(14, 77);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(185, 12);
            this.label169.TabIndex = 35;
            this.label169.Text = "打标前核实NG,以PLC码打标(X3.7)";
            // 
            // label170
            // 
            this.label170.AutoSize = true;
            this.label170.Location = new System.Drawing.Point(14, 57);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(179, 12);
            this.label170.TabIndex = 33;
            this.label170.Text = "打标前核实NG,以PC码打标(X3.6)";
            // 
            // label_idx31_X36
            // 
            this.label_idx31_X36.BackColor = System.Drawing.Color.Red;
            this.label_idx31_X36.Location = new System.Drawing.Point(217, 60);
            this.label_idx31_X36.Name = "label_idx31_X36";
            this.label_idx31_X36.Size = new System.Drawing.Size(9, 8);
            this.label_idx31_X36.TabIndex = 34;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(15, 17);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(185, 12);
            this.label61.TabIndex = 25;
            this.label61.Text = "打标前打标内容核实OK信号(X3.0)";
            // 
            // label_idx25_X30
            // 
            this.label_idx25_X30.BackColor = System.Drawing.Color.Red;
            this.label_idx25_X30.Location = new System.Drawing.Point(217, 19);
            this.label_idx25_X30.Name = "label_idx25_X30";
            this.label_idx25_X30.Size = new System.Drawing.Size(9, 8);
            this.label_idx25_X30.TabIndex = 26;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(14, 38);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(185, 12);
            this.label65.TabIndex = 27;
            this.label65.Text = "打标前打标内容核实NG信号(X3.1)";
            // 
            // label_idx29_X34
            // 
            this.label_idx29_X34.BackColor = System.Drawing.Color.Red;
            this.label_idx29_X34.Location = new System.Drawing.Point(216, 120);
            this.label_idx29_X34.Name = "label_idx29_X34";
            this.label_idx29_X34.Size = new System.Drawing.Size(9, 8);
            this.label_idx29_X34.TabIndex = 32;
            // 
            // label_idx26_X31
            // 
            this.label_idx26_X31.BackColor = System.Drawing.Color.Red;
            this.label_idx26_X31.Location = new System.Drawing.Point(217, 40);
            this.label_idx26_X31.Name = "label_idx26_X31";
            this.label_idx26_X31.Size = new System.Drawing.Size(9, 8);
            this.label_idx26_X31.TabIndex = 28;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(14, 118);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(137, 12);
            this.label71.TabIndex = 31;
            this.label71.Text = "打标后入库OK信号(X3.4)";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(14, 98);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(149, 12);
            this.label69.TabIndex = 29;
            this.label69.Text = "打标前中止打标信号(X3.2)";
            // 
            // label_idx27_X32
            // 
            this.label_idx27_X32.BackColor = System.Drawing.Color.Red;
            this.label_idx27_X32.Location = new System.Drawing.Point(216, 100);
            this.label_idx27_X32.Name = "label_idx27_X32";
            this.label_idx27_X32.Size = new System.Drawing.Size(9, 8);
            this.label_idx27_X32.TabIndex = 30;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label42);
            this.groupBox6.Controls.Add(this.label_idx21_X24);
            this.groupBox6.Controls.Add(this.label45);
            this.groupBox6.Controls.Add(this.label_idx28_X33);
            this.groupBox6.Controls.Add(this.label48);
            this.groupBox6.Controls.Add(this.label_idx22_X25);
            this.groupBox6.Controls.Add(this.label49);
            this.groupBox6.Controls.Add(this.label_idx23_X26);
            this.groupBox6.Controls.Add(this.label44);
            this.groupBox6.Controls.Add(this.label_idx24_X27);
            this.groupBox6.Location = new System.Drawing.Point(6, 115);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(337, 123);
            this.groupBox6.TabIndex = 34;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "状态2";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(17, 21);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(125, 12);
            this.label42.TabIndex = 8;
            this.label42.Text = "公差比较OK信号(X2.4)";
            // 
            // label_idx21_X24
            // 
            this.label_idx21_X24.BackColor = System.Drawing.Color.Red;
            this.label_idx21_X24.Location = new System.Drawing.Point(217, 22);
            this.label_idx21_X24.Name = "label_idx21_X24";
            this.label_idx21_X24.Size = new System.Drawing.Size(9, 8);
            this.label_idx21_X24.TabIndex = 11;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(16, 42);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(125, 12);
            this.label45.TabIndex = 14;
            this.label45.Text = "公差比较NG信号(X3.3)";
            // 
            // label_idx28_X33
            // 
            this.label_idx28_X33.BackColor = System.Drawing.Color.Red;
            this.label_idx28_X33.Location = new System.Drawing.Point(217, 42);
            this.label_idx28_X33.Name = "label_idx28_X33";
            this.label_idx28_X33.Size = new System.Drawing.Size(9, 8);
            this.label_idx28_X33.TabIndex = 15;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(17, 62);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(137, 12);
            this.label48.TabIndex = 16;
            this.label48.Text = "读取报告完成信号(X2.5)";
            // 
            // label_idx22_X25
            // 
            this.label_idx22_X25.BackColor = System.Drawing.Color.Red;
            this.label_idx22_X25.Location = new System.Drawing.Point(217, 61);
            this.label_idx22_X25.Name = "label_idx22_X25";
            this.label_idx22_X25.Size = new System.Drawing.Size(9, 8);
            this.label_idx22_X25.TabIndex = 17;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(17, 82);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(149, 12);
            this.label49.TabIndex = 20;
            this.label49.Text = "打标内容生成OK信号(X2.6)";
            // 
            // label_idx23_X26
            // 
            this.label_idx23_X26.BackColor = System.Drawing.Color.Red;
            this.label_idx23_X26.Location = new System.Drawing.Point(217, 80);
            this.label_idx23_X26.Name = "label_idx23_X26";
            this.label_idx23_X26.Size = new System.Drawing.Size(9, 8);
            this.label_idx23_X26.TabIndex = 21;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(17, 100);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(173, 12);
            this.label44.TabIndex = 23;
            this.label44.Text = "放弃打标并入库成功信号(X2.7)";
            // 
            // label_idx24_X27
            // 
            this.label_idx24_X27.BackColor = System.Drawing.Color.Red;
            this.label_idx24_X27.Location = new System.Drawing.Point(217, 100);
            this.label_idx24_X27.Name = "label_idx24_X27";
            this.label_idx24_X27.Size = new System.Drawing.Size(9, 8);
            this.label_idx24_X27.TabIndex = 24;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label39);
            this.groupBox5.Controls.Add(this.label_idx20_X23);
            this.groupBox5.Controls.Add(this.label41);
            this.groupBox5.Controls.Add(this.label_idx18_X21);
            this.groupBox5.Controls.Add(this.label40);
            this.groupBox5.Controls.Add(this.label_idx17_X20);
            this.groupBox5.Location = new System.Drawing.Point(6, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(337, 91);
            this.groupBox5.TabIndex = 33;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "状态1";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(16, 25);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(137, 12);
            this.label39.TabIndex = 3;
            this.label39.Text = "上位机准备OK信号(X2.0)";
            // 
            // label_idx20_X23
            // 
            this.label_idx20_X23.BackColor = System.Drawing.Color.Red;
            this.label_idx20_X23.Location = new System.Drawing.Point(217, 66);
            this.label_idx20_X23.Name = "label_idx20_X23";
            this.label_idx20_X23.Size = new System.Drawing.Size(9, 8);
            this.label_idx20_X23.TabIndex = 7;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(16, 66);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(137, 12);
            this.label41.TabIndex = 6;
            this.label41.Text = "数据读取开始信号(X2.3)";
            // 
            // label_idx18_X21
            // 
            this.label_idx18_X21.BackColor = System.Drawing.Color.Red;
            this.label_idx18_X21.Location = new System.Drawing.Point(217, 46);
            this.label_idx18_X21.Name = "label_idx18_X21";
            this.label_idx18_X21.Size = new System.Drawing.Size(9, 8);
            this.label_idx18_X21.TabIndex = 5;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(16, 46);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(137, 12);
            this.label40.TabIndex = 4;
            this.label40.Text = "上位机工作OK信号(X2.1)";
            // 
            // label_idx17_X20
            // 
            this.label_idx17_X20.BackColor = System.Drawing.Color.Red;
            this.label_idx17_X20.Location = new System.Drawing.Point(217, 26);
            this.label_idx17_X20.Name = "label_idx17_X20";
            this.label_idx17_X20.Size = new System.Drawing.Size(9, 8);
            this.label_idx17_X20.TabIndex = 3;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(53, 398);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(257, 12);
            this.label56.TabIndex = 22;
            this.label56.Text = "基础码保存地/PC以此做工件码值累加(B50-B89)";
            // 
            // tbx_OriginalCode
            // 
            this.tbx_OriginalCode.Location = new System.Drawing.Point(100, 418);
            this.tbx_OriginalCode.Name = "tbx_OriginalCode";
            this.tbx_OriginalCode.Size = new System.Drawing.Size(155, 21);
            this.tbx_OriginalCode.TabIndex = 9;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label_idx8_X07);
            this.groupBox4.Controls.Add(this.label171);
            this.groupBox4.Controls.Add(this.label_idx7_X06);
            this.groupBox4.Controls.Add(this.label46);
            this.groupBox4.Controls.Add(this.tbx_idx33_INT4);
            this.groupBox4.Controls.Add(this.label59);
            this.groupBox4.Controls.Add(this.label58);
            this.groupBox4.Controls.Add(this.label55);
            this.groupBox4.Controls.Add(this.label_idx4_X03);
            this.groupBox4.Controls.Add(this.label72);
            this.groupBox4.Controls.Add(this.label57);
            this.groupBox4.Controls.Add(this.label_idx1_X00);
            this.groupBox4.Controls.Add(this.tbx_PLC1stSendBKCode);
            this.groupBox4.Controls.Add(this.label60);
            this.groupBox4.Controls.Add(this.label_idx13_X14);
            this.groupBox4.Controls.Add(this.label_idx2_X01);
            this.groupBox4.Controls.Add(this.label63);
            this.groupBox4.Controls.Add(this.label64);
            this.groupBox4.Controls.Add(this.label_idx14_X15);
            this.groupBox4.Controls.Add(this.label_idx8_X10);
            this.groupBox4.Controls.Add(this.label67);
            this.groupBox4.Controls.Add(this.label68);
            this.groupBox4.Controls.Add(this.tbx_PLC2ndSendBKCode);
            this.groupBox4.Controls.Add(this.label_idx9_X11);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(433, 475);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PLC->PC逻辑运行状态";
            // 
            // label_idx8_X07
            // 
            this.label_idx8_X07.BackColor = System.Drawing.Color.Red;
            this.label_idx8_X07.Location = new System.Drawing.Point(225, 129);
            this.label_idx8_X07.Name = "label_idx8_X07";
            this.label_idx8_X07.Size = new System.Drawing.Size(10, 8);
            this.label_idx8_X07.TabIndex = 31;
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Location = new System.Drawing.Point(24, 126);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(149, 12);
            this.label171.TabIndex = 30;
            this.label171.Text = "数据入库确认OK信号(X0.7)";
            // 
            // label_idx7_X06
            // 
            this.label_idx7_X06.BackColor = System.Drawing.Color.Red;
            this.label_idx7_X06.Location = new System.Drawing.Point(225, 109);
            this.label_idx7_X06.Name = "label_idx7_X06";
            this.label_idx7_X06.Size = new System.Drawing.Size(10, 8);
            this.label_idx7_X06.TabIndex = 29;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(24, 106);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(167, 12);
            this.label46.TabIndex = 28;
            this.label46.Text = "Pick-up存取数据OK信号(X0.6)";
            // 
            // tbx_idx33_INT4
            // 
            this.tbx_idx33_INT4.Location = new System.Drawing.Point(208, 253);
            this.tbx_idx33_INT4.Name = "tbx_idx33_INT4";
            this.tbx_idx33_INT4.Size = new System.Drawing.Size(41, 21);
            this.tbx_idx33_INT4.TabIndex = 27;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(24, 256);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(131, 12);
            this.label59.TabIndex = 26;
            this.label59.Text = "状态数字显示(INT4/2B)";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(25, 376);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(113, 12);
            this.label58.TabIndex = 25;
            this.label58.Text = "PLC第2次回传打码值";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(25, 325);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(113, 12);
            this.label55.TabIndex = 24;
            this.label55.Text = "PLC第1次回传打码值";
            // 
            // label_idx4_X03
            // 
            this.label_idx4_X03.BackColor = System.Drawing.Color.Red;
            this.label_idx4_X03.Location = new System.Drawing.Point(225, 87);
            this.label_idx4_X03.Name = "label_idx4_X03";
            this.label_idx4_X03.Size = new System.Drawing.Size(10, 8);
            this.label_idx4_X03.TabIndex = 23;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(24, 84);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(137, 12);
            this.label72.TabIndex = 22;
            this.label72.Text = "数据读取请求信号(X0.3)";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(24, 36);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(113, 12);
            this.label57.TabIndex = 3;
            this.label57.Text = "基础码OK信号(X0.0)";
            // 
            // label_idx1_X00
            // 
            this.label_idx1_X00.BackColor = System.Drawing.Color.Red;
            this.label_idx1_X00.Location = new System.Drawing.Point(225, 39);
            this.label_idx1_X00.Name = "label_idx1_X00";
            this.label_idx1_X00.Size = new System.Drawing.Size(9, 8);
            this.label_idx1_X00.TabIndex = 3;
            // 
            // tbx_PLC1stSendBKCode
            // 
            this.tbx_PLC1stSendBKCode.Location = new System.Drawing.Point(172, 321);
            this.tbx_PLC1stSendBKCode.Name = "tbx_PLC1stSendBKCode";
            this.tbx_PLC1stSendBKCode.Size = new System.Drawing.Size(158, 21);
            this.tbx_PLC1stSendBKCode.TabIndex = 18;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(24, 60);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(173, 12);
            this.label60.TabIndex = 4;
            this.label60.Text = "开班检测系统确认OK信号(X0.1)";
            // 
            // label_idx13_X14
            // 
            this.label_idx13_X14.BackColor = System.Drawing.Color.Red;
            this.label_idx13_X14.Location = new System.Drawing.Point(225, 209);
            this.label_idx13_X14.Name = "label_idx13_X14";
            this.label_idx13_X14.Size = new System.Drawing.Size(9, 8);
            this.label_idx13_X14.TabIndex = 17;
            // 
            // label_idx2_X01
            // 
            this.label_idx2_X01.BackColor = System.Drawing.Color.Red;
            this.label_idx2_X01.Location = new System.Drawing.Point(225, 63);
            this.label_idx2_X01.Name = "label_idx2_X01";
            this.label_idx2_X01.Size = new System.Drawing.Size(9, 8);
            this.label_idx2_X01.TabIndex = 5;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(24, 231);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(113, 12);
            this.label63.TabIndex = 16;
            this.label63.Text = "状态故障信号(X1.5)";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(25, 303);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(173, 12);
            this.label64.TabIndex = 6;
            this.label64.Text = "PLC第1次回传码值OK信号(X1.0)";
            // 
            // label_idx14_X15
            // 
            this.label_idx14_X15.BackColor = System.Drawing.Color.Red;
            this.label_idx14_X15.Location = new System.Drawing.Point(225, 232);
            this.label_idx14_X15.Name = "label_idx14_X15";
            this.label_idx14_X15.Size = new System.Drawing.Size(9, 8);
            this.label_idx14_X15.TabIndex = 15;
            // 
            // label_idx8_X10
            // 
            this.label_idx8_X10.BackColor = System.Drawing.Color.Red;
            this.label_idx8_X10.Location = new System.Drawing.Point(224, 305);
            this.label_idx8_X10.Name = "label_idx8_X10";
            this.label_idx8_X10.Size = new System.Drawing.Size(9, 8);
            this.label_idx8_X10.TabIndex = 7;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(24, 206);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(113, 12);
            this.label67.TabIndex = 14;
            this.label67.Text = "状态改变信号(X1.4)";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(25, 353);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(173, 12);
            this.label68.TabIndex = 8;
            this.label68.Text = "PLC第2次回传码值OK信号(X1.1)";
            // 
            // tbx_PLC2ndSendBKCode
            // 
            this.tbx_PLC2ndSendBKCode.Location = new System.Drawing.Point(172, 372);
            this.tbx_PLC2ndSendBKCode.Name = "tbx_PLC2ndSendBKCode";
            this.tbx_PLC2ndSendBKCode.Size = new System.Drawing.Size(158, 21);
            this.tbx_PLC2ndSendBKCode.TabIndex = 9;
            // 
            // label_idx9_X11
            // 
            this.label_idx9_X11.BackColor = System.Drawing.Color.Red;
            this.label_idx9_X11.Location = new System.Drawing.Point(226, 357);
            this.label_idx9_X11.Name = "label_idx9_X11";
            this.label_idx9_X11.Size = new System.Drawing.Size(9, 8);
            this.label_idx9_X11.TabIndex = 11;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.tableLayoutPanel1);
            this.tabPage7.Location = new System.Drawing.Point(4, 21);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(843, 481);
            this.tabPage7.TabIndex = 4;
            this.tabPage7.Text = "ZESSI文件数据显示";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvDatFromZessiFile, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox8, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.03203F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.96797F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(837, 475);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // dgvDatFromZessiFile
            // 
            this.dgvDatFromZessiFile.AllowUserToAddRows = false;
            this.dgvDatFromZessiFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatFromZessiFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDatFromZessiFile.Location = new System.Drawing.Point(3, 55);
            this.dgvDatFromZessiFile.Name = "dgvDatFromZessiFile";
            this.dgvDatFromZessiFile.RowTemplate.Height = 23;
            this.dgvDatFromZessiFile.Size = new System.Drawing.Size(831, 417);
            this.dgvDatFromZessiFile.TabIndex = 6;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.button4);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(3, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(831, 46);
            this.groupBox8.TabIndex = 7;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "按键处理";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(203, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "读取";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox13);
            this.tabPage4.Controls.Add(this.groupBox12);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(843, 481);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "登录操作-";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox13.Controls.Add(this.button1);
            this.groupBox13.Controls.Add(this.label2);
            this.groupBox13.Controls.Add(this.label5);
            this.groupBox13.Controls.Add(this.tbx_DebugUsername);
            this.groupBox13.Controls.Add(this.tbx_DebugPassword);
            this.groupBox13.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox13.ForeColor = System.Drawing.Color.Black;
            this.groupBox13.Location = new System.Drawing.Point(57, 218);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(324, 105);
            this.groupBox13.TabIndex = 109;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "调试权限";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(216, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 53);
            this.button1.TabIndex = 15;
            this.button1.Text = "修改确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "用户名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "密码";
            // 
            // tbx_DebugUsername
            // 
            this.tbx_DebugUsername.Location = new System.Drawing.Point(90, 31);
            this.tbx_DebugUsername.Name = "tbx_DebugUsername";
            this.tbx_DebugUsername.Size = new System.Drawing.Size(94, 26);
            this.tbx_DebugUsername.TabIndex = 9;
            // 
            // tbx_DebugPassword
            // 
            this.tbx_DebugPassword.Location = new System.Drawing.Point(90, 58);
            this.tbx_DebugPassword.Name = "tbx_DebugPassword";
            this.tbx_DebugPassword.Size = new System.Drawing.Size(94, 26);
            this.tbx_DebugPassword.TabIndex = 10;
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox12.Controls.Add(this.btn_ConfirmChangeUsrNamePwd);
            this.groupBox12.Controls.Add(this.label3);
            this.groupBox12.Controls.Add(this.label4);
            this.groupBox12.Controls.Add(this.tbx_username);
            this.groupBox12.Controls.Add(this.tbx_password);
            this.groupBox12.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox12.ForeColor = System.Drawing.Color.Black;
            this.groupBox12.Location = new System.Drawing.Point(57, 71);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(324, 105);
            this.groupBox12.TabIndex = 108;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "当前登录信息";
            // 
            // btn_ConfirmChangeUsrNamePwd
            // 
            this.btn_ConfirmChangeUsrNamePwd.Location = new System.Drawing.Point(216, 31);
            this.btn_ConfirmChangeUsrNamePwd.Name = "btn_ConfirmChangeUsrNamePwd";
            this.btn_ConfirmChangeUsrNamePwd.Size = new System.Drawing.Size(91, 53);
            this.btn_ConfirmChangeUsrNamePwd.TabIndex = 15;
            this.btn_ConfirmChangeUsrNamePwd.Text = "修改确认";
            this.btn_ConfirmChangeUsrNamePwd.UseVisualStyleBackColor = true;
            this.btn_ConfirmChangeUsrNamePwd.Click += new System.EventHandler(this.btn_ConfirmChangeUsrNamePwd_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "用户名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "密码";
            // 
            // tbx_username
            // 
            this.tbx_username.Location = new System.Drawing.Point(90, 31);
            this.tbx_username.Name = "tbx_username";
            this.tbx_username.Size = new System.Drawing.Size(94, 26);
            this.tbx_username.TabIndex = 9;
            // 
            // tbx_password
            // 
            this.tbx_password.Location = new System.Drawing.Point(90, 58);
            this.tbx_password.Name = "tbx_password";
            this.tbx_password.Size = new System.Drawing.Size(94, 26);
            this.tbx_password.TabIndex = 10;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox11);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(843, 481);
            this.tabPage3.TabIndex = 8;
            this.tabPage3.Text = "OPC读写测试";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.BackColor = System.Drawing.Color.Transparent;
            this.groupBox11.Controls.Add(this.btn_WriteOPCItem);
            this.groupBox11.Controls.Add(this.cbx_TriggerSignal);
            this.groupBox11.Controls.Add(this.cbx_MarkInfo);
            this.groupBox11.Controls.Add(this.label38);
            this.groupBox11.Controls.Add(this.label37);
            this.groupBox11.Controls.Add(this.btn_ReadOPCItem);
            this.groupBox11.Controls.Add(this.DBGroupBox);
            this.groupBox11.Controls.Add(this.SimGroupBox);
            this.groupBox11.Controls.Add(this.label33);
            this.groupBox11.Controls.Add(this.label32);
            this.groupBox11.Controls.Add(this.label31);
            this.groupBox11.Controls.Add(this.tbx_OPCItemTimeStamp);
            this.groupBox11.Controls.Add(this.tbx_OPCItemQuality);
            this.groupBox11.Controls.Add(this.tbx_OPCItemValue);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox11.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox11.ForeColor = System.Drawing.Color.Black;
            this.groupBox11.Location = new System.Drawing.Point(3, 3);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(837, 475);
            this.groupBox11.TabIndex = 106;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "登录操作";
            // 
            // btn_WriteOPCItem
            // 
            this.btn_WriteOPCItem.BackColor = System.Drawing.Color.Silver;
            this.btn_WriteOPCItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_WriteOPCItem.Location = new System.Drawing.Point(691, 109);
            this.btn_WriteOPCItem.Name = "btn_WriteOPCItem";
            this.btn_WriteOPCItem.Size = new System.Drawing.Size(84, 26);
            this.btn_WriteOPCItem.TabIndex = 121;
            this.btn_WriteOPCItem.Text = "Write";
            this.btn_WriteOPCItem.UseVisualStyleBackColor = false;
            // 
            // cbx_TriggerSignal
            // 
            this.cbx_TriggerSignal.FormattingEnabled = true;
            this.cbx_TriggerSignal.Location = new System.Drawing.Point(453, 37);
            this.cbx_TriggerSignal.Name = "cbx_TriggerSignal";
            this.cbx_TriggerSignal.Size = new System.Drawing.Size(221, 20);
            this.cbx_TriggerSignal.TabIndex = 120;
            // 
            // cbx_MarkInfo
            // 
            this.cbx_MarkInfo.FormattingEnabled = true;
            this.cbx_MarkInfo.Location = new System.Drawing.Point(453, 110);
            this.cbx_MarkInfo.Name = "cbx_MarkInfo";
            this.cbx_MarkInfo.Size = new System.Drawing.Size(221, 20);
            this.cbx_MarkInfo.TabIndex = 119;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(395, 94);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(53, 12);
            this.label38.TabIndex = 118;
            this.label38.Text = "打标信息";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(395, 22);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(53, 12);
            this.label37.TabIndex = 117;
            this.label37.Text = "触发信号";
            // 
            // btn_ReadOPCItem
            // 
            this.btn_ReadOPCItem.BackColor = System.Drawing.Color.Silver;
            this.btn_ReadOPCItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ReadOPCItem.Location = new System.Drawing.Point(690, 33);
            this.btn_ReadOPCItem.Name = "btn_ReadOPCItem";
            this.btn_ReadOPCItem.Size = new System.Drawing.Size(84, 26);
            this.btn_ReadOPCItem.TabIndex = 116;
            this.btn_ReadOPCItem.Text = "Read";
            this.btn_ReadOPCItem.UseVisualStyleBackColor = false;
            // 
            // DBGroupBox
            // 
            this.DBGroupBox.Controls.Add(this.DBRadioButton);
            this.DBGroupBox.Controls.Add(this.XMLRadioButton);
            this.DBGroupBox.Controls.Add(this.SQLDBRadioButton);
            this.DBGroupBox.Location = new System.Drawing.Point(367, 230);
            this.DBGroupBox.Name = "DBGroupBox";
            this.DBGroupBox.Size = new System.Drawing.Size(253, 57);
            this.DBGroupBox.TabIndex = 115;
            this.DBGroupBox.TabStop = false;
            this.DBGroupBox.Text = "数据库";
            // 
            // DBRadioButton
            // 
            this.DBRadioButton.Location = new System.Drawing.Point(12, 63);
            this.DBRadioButton.Name = "DBRadioButton";
            this.DBRadioButton.Size = new System.Drawing.Size(154, 17);
            this.DBRadioButton.TabIndex = 0;
            this.DBRadioButton.Text = "Save to access database";
            this.DBRadioButton.Visible = false;
            // 
            // XMLRadioButton
            // 
            this.XMLRadioButton.Location = new System.Drawing.Point(12, 43);
            this.XMLRadioButton.Name = "XMLRadioButton";
            this.XMLRadioButton.Size = new System.Drawing.Size(154, 18);
            this.XMLRadioButton.TabIndex = 0;
            this.XMLRadioButton.Text = "Save to XML-File";
            this.XMLRadioButton.Visible = false;
            // 
            // SQLDBRadioButton
            // 
            this.SQLDBRadioButton.Enabled = false;
            this.SQLDBRadioButton.Location = new System.Drawing.Point(12, 23);
            this.SQLDBRadioButton.Name = "SQLDBRadioButton";
            this.SQLDBRadioButton.Size = new System.Drawing.Size(212, 17);
            this.SQLDBRadioButton.TabIndex = 0;
            this.SQLDBRadioButton.Text = "Save to SQL Server database";
            // 
            // SimGroupBox
            // 
            this.SimGroupBox.Controls.Add(this.label30);
            this.SimGroupBox.Controls.Add(this.SendBitLabel);
            this.SimGroupBox.Controls.Add(this.label1);
            this.SimGroupBox.Controls.Add(this.StartSimButton);
            this.SimGroupBox.Location = new System.Drawing.Point(367, 297);
            this.SimGroupBox.Name = "SimGroupBox";
            this.SimGroupBox.Size = new System.Drawing.Size(236, 51);
            this.SimGroupBox.TabIndex = 114;
            this.SimGroupBox.TabStop = false;
            this.SimGroupBox.Text = "Simulation";
            this.SimGroupBox.Visible = false;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(216, 26);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(9, 8);
            this.label30.TabIndex = 2;
            // 
            // SendBitLabel
            // 
            this.SendBitLabel.BackColor = System.Drawing.Color.Red;
            this.SendBitLabel.Location = new System.Drawing.Point(195, 26);
            this.SendBitLabel.Name = "SendBitLabel";
            this.SendBitLabel.Size = new System.Drawing.Size(9, 8);
            this.SendBitLabel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(134, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Send Bit";
            // 
            // StartSimButton
            // 
            this.StartSimButton.BackColor = System.Drawing.Color.Gray;
            this.StartSimButton.Enabled = false;
            this.StartSimButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartSimButton.Location = new System.Drawing.Point(10, 17);
            this.StartSimButton.Name = "StartSimButton";
            this.StartSimButton.Size = new System.Drawing.Size(115, 26);
            this.StartSimButton.TabIndex = 0;
            this.StartSimButton.Text = "Start/Stop";
            this.StartSimButton.UseVisualStyleBackColor = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(659, 161);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(59, 12);
            this.label33.TabIndex = 113;
            this.label33.Text = "TimeStamp";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(536, 161);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(47, 12);
            this.label32.TabIndex = 112;
            this.label32.Text = "Quality";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(399, 160);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(35, 12);
            this.label31.TabIndex = 111;
            this.label31.Text = "Value";
            // 
            // tbx_OPCItemTimeStamp
            // 
            this.tbx_OPCItemTimeStamp.Location = new System.Drawing.Point(634, 179);
            this.tbx_OPCItemTimeStamp.Name = "tbx_OPCItemTimeStamp";
            this.tbx_OPCItemTimeStamp.Size = new System.Drawing.Size(100, 21);
            this.tbx_OPCItemTimeStamp.TabIndex = 110;
            // 
            // tbx_OPCItemQuality
            // 
            this.tbx_OPCItemQuality.Location = new System.Drawing.Point(511, 179);
            this.tbx_OPCItemQuality.Name = "tbx_OPCItemQuality";
            this.tbx_OPCItemQuality.Size = new System.Drawing.Size(100, 21);
            this.tbx_OPCItemQuality.TabIndex = 109;
            // 
            // tbx_OPCItemValue
            // 
            this.tbx_OPCItemValue.Location = new System.Drawing.Point(367, 179);
            this.tbx_OPCItemValue.Name = "tbx_OPCItemValue";
            this.tbx_OPCItemValue.Size = new System.Drawing.Size(100, 21);
            this.tbx_OPCItemValue.TabIndex = 108;
            // 
            // timer_SendOK
            // 
            this.timer_SendOK.Interval = 5000;
            this.timer_SendOK.Tick += new System.EventHandler(this.timer_SendOK_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 1000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 500;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(6, 32);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(41, 21);
            this.textBox14.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.38284F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.61716F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1028, 661);
            this.tableLayoutPanel2.TabIndex = 32;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 521);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1022, 137);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_ClearTraceList);
            this.tabPage1.Controls.Add(this.TraceListBox_Main);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1014, 112);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "操作提示";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_ClearTraceList
            // 
            this.btn_ClearTraceList.BackColor = System.Drawing.Color.LightGray;
            this.btn_ClearTraceList.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ClearTraceList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ClearTraceList.Location = new System.Drawing.Point(968, 3);
            this.btn_ClearTraceList.Name = "btn_ClearTraceList";
            this.btn_ClearTraceList.Size = new System.Drawing.Size(43, 106);
            this.btn_ClearTraceList.TabIndex = 6;
            this.btn_ClearTraceList.Text = "Clear";
            this.btn_ClearTraceList.UseVisualStyleBackColor = false;
            this.btn_ClearTraceList.Click += new System.EventHandler(this.btn_ClearTraceList_Click);
            // 
            // TraceListBox_Main
            // 
            this.TraceListBox_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceListBox_Main.ItemHeight = 12;
            this.TraceListBox_Main.Location = new System.Drawing.Point(3, 3);
            this.TraceListBox_Main.Name = "TraceListBox_Main";
            this.TraceListBox_Main.Size = new System.Drawing.Size(1008, 106);
            this.TraceListBox_Main.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1014, 112);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "输出";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // PersistentData
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1028, 661);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "PersistentData";
            this.Text = "B515_MCA前桥数据管理软件V1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PersistentData_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PersistentData_FormClosed);
            this.Load += new System.EventHandler(this.PersistentData_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.grp_PTA21.ResumeLayout(false);
            this.grp_PTA21.PerformLayout();
            this.grp_PTA4D.ResumeLayout(false);
            this.grp_PTA4D.PerformLayout();
            this.grp_PTA11.ResumeLayout(false);
            this.grp_PTA11.PerformLayout();
            this.grp_PTA3D.ResumeLayout(false);
            this.grp_PTA3D.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage8.ResumeLayout(false);
            this.grp_PTERH_25.ResumeLayout(false);
            this.grp_PTERH_25.PerformLayout();
            this.grp_PTPLH_23.ResumeLayout(false);
            this.grp_PTPLH_23.PerformLayout();
            this.grp_PTELH_21.ResumeLayout(false);
            this.grp_PTELH_21.PerformLayout();
            this.grp_PTA4D_18.ResumeLayout(false);
            this.grp_PTA4D_18.PerformLayout();
            this.grp_PTHLHG_8.ResumeLayout(false);
            this.grp_PTHLHG_8.PerformLayout();
            this.grp_PTDLH_19.ResumeLayout(false);
            this.grp_PTDLH_19.PerformLayout();
            this.grp_PTA21_14.ResumeLayout(false);
            this.grp_PTA21_14.PerformLayout();
            this.grp_PTNLH_22.ResumeLayout(false);
            this.grp_PTNLH_22.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.grp_PTA11_15.ResumeLayout(false);
            this.grp_PTA11_15.PerformLayout();
            this.grp_PTDRH_20.ResumeLayout(false);
            this.grp_PTDRH_20.PerformLayout();
            this.grp_PTHRHG_7.ResumeLayout(false);
            this.grp_PTHRHG_7.PerformLayout();
            this.grp_PTPRH_24.ResumeLayout(false);
            this.grp_PTPRH_24.PerformLayout();
            this.grp_PTA3D_16.ResumeLayout(false);
            this.grp_PTA3D_16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage9.ResumeLayout(false);
            this.grp_PTGRH_2.ResumeLayout(false);
            this.grp_PTGRH_2.PerformLayout();
            this.grp_PTFRHF_11.ResumeLayout(false);
            this.grp_PTFRHF_11.PerformLayout();
            this.grp_PTGLH_1.ResumeLayout(false);
            this.grp_PTGLH_1.PerformLayout();
            this.grp_PTFLHF_12.ResumeLayout(false);
            this.grp_PTFLHF_12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabPage10.ResumeLayout(false);
            this.grp_PTA12_6.ResumeLayout(false);
            this.grp_PTA12_6.PerformLayout();
            this.groupBox41.ResumeLayout(false);
            this.groupBox41.PerformLayout();
            this.grp_PTJRH_17.ResumeLayout(false);
            this.grp_PTJRH_17.PerformLayout();
            this.groupBox39.ResumeLayout(false);
            this.groupBox39.PerformLayout();
            this.grp_PTIRH_10.ResumeLayout(false);
            this.grp_PTIRH_10.PerformLayout();
            this.groupBox38.ResumeLayout(false);
            this.groupBox38.PerformLayout();
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.grp_PTB_4.ResumeLayout(false);
            this.grp_PTB_4.PerformLayout();
            this.groupBox35.ResumeLayout(false);
            this.groupBox35.PerformLayout();
            this.grp_PTA22_5.ResumeLayout(false);
            this.grp_PTA22_5.PerformLayout();
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            this.grp_PTJLH_13.ResumeLayout(false);
            this.grp_PTJLH_13.PerformLayout();
            this.groupBox34.ResumeLayout(false);
            this.groupBox34.PerformLayout();
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.grp_PTILH_9.ResumeLayout(false);
            this.grp_PTILH_9.PerformLayout();
            this.grp_PTC_3.ResumeLayout(false);
            this.grp_PTC_3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatFromZessiFile)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.DBGroupBox.ResumeLayout(false);
            this.SimGroupBox.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary> Entry point of this application </summary>
        [STAThread]
        static void Main()   // 程序入口
        {
            Application.Run(new PersistentData());
        }

        #endregion

        #region opc initialization

        /// <summary> creates OPCManagement-objects 
        /// (they manage the opc-handling of the OPCGroupStateMgt and OPCItemStateMgt) </summary>
        /// <param name="theGroupManager"> the OPCGroupManagement-object to create </param>
        /// <param name="grpName"> the group name </param>
        /// <param name="grpActive"> determines whether the group is active </param>
        /// <param name="grpClntHndl"> the client handles of the group </param>
        /// <param name="reqUpdateRate"> the requested update rate of the group </param>
        /// <param name="setSync"> determines whether a sync-interface is needed (for sync read/write) </param>
        /// <param name="connectCallback"> determines whether the group shall receive callbacks. </param>
        /// <param name="setAsync"> determines whether a async-interface is needed (for async read/write, datachange) </param>
        /// <exception cref="Exception">forwards any exception (with short error description)</exception>
        private void initOPCGroup(
            ref	OPCGroupManagement theGroupManager, string grpName, bool grpActive,
            int grpClntHndl, int reqUpdateRate, bool setSync,
            bool connectCallback, bool setAsync)
        {
            try
            {
                // let the manager create a opc-group
                int revUpdateRate;
                Trace("添加OPC组: " + grpName);
                theGroupManager = m_mgtServer.addGroup(grpName, grpActive, grpClntHndl, reqUpdateRate,
                    out revUpdateRate);
                Trace("添加OPC组成功.");

                // check the revised update rate
                if (revUpdateRate > reqUpdateRate)
                {
                    string strMsg = "The revised updaterate (" + revUpdateRate +
                        ") is higher than the requested update rate (" + reqUpdateRate +
                        ")!";
                    MessageBox.Show(strMsg, "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // set sync/async interfaces
                if (setSync)
                {
                    Trace("设置同步访问接口中...");
                    theGroupManager.setSyncInterface();
                    Trace("设置同步访问接口成功.");
                }
                if (setAsync)
                {
                    Trace("连接回调访问接口中...");
                    theGroupManager.connectCallback();
                    Trace("连接回调访问接口成功.");

                    Trace("设置异步访问接口...");
                    theGroupManager.setASyncInterface();
                    Trace("设置异步访问接口成功.");
                }
                else if (connectCallback)
                {
                    Trace("连接回调访问接口中...");
                    theGroupManager.connectCallback();
                    Trace("连接回调访问接口成功.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> 创建DB块的组-Items </summary>
        /// <param name="reqUpdateRateSettings"> DBblock-组的更新率 </param>
        /// <exception cref="Exception">forwards any exception (with short error description)</exception>
        private void buildItemForDBblock(int reqUpdateRateDBblock)
        {
            // **************************************
            // DBblock-item 创建DB块的items,获取数据
            // **************************************

            #region 准备ITEM项的属性-赋值
            NUM_DBblock_ITEMS = ITEM_DBblock.Length;

            int[] itmsClntHndl = new int[NUM_DBblock_ITEMS];
            int[] itmsSrvHndl = new int[NUM_DBblock_ITEMS];
            bool[] itmsActive = new bool[NUM_DBblock_ITEMS];
            short[] itmsType = new short[NUM_DBblock_ITEMS];
            IOPCItemControl[] correspondingObject = new IOPCItemControl[NUM_DBblock_ITEMS];

            int i;
            OPCItemExtender itm;

            string[] itmsID = new string[NUM_DBblock_ITEMS];
            for (i = 0; i < NUM_DBblock_ITEMS; i++)
            {
                itmsClntHndl[i] = i;
                itmsActive[i] = true;
                correspondingObject[i] = m_DBConnector;
                //itmsType[i] = Convert.ToInt16(VarEnum.VT_ARRAY | VarEnum.VT_UI1);// 

                if (i < Items_Bool_Num) // 32项 0-31
                {
                    itmsType[i] = Convert.ToInt16(VarEnum.VT_BOOL); // bool类型是11 VarEnum.VT_Bool
                }
                else if ((i >= Items_Bool_Num) && (i < Items_Bool_Num + Items_Int_Num)) // 32+1=33
                {
                    itmsType[i] = Convert.ToInt16(VarEnum.VT_I2); // bool类型是11 VarEnum.VT_Bool
                }
                else // 后续改成Byte判断
                {
                    itmsType[i] = Convert.ToInt16(VarEnum.VT_I2); //8192 | 17 = 8209
                    // Byte类型是VT_UI1 = 17, 采用或的方式构建一个数组加byte的类型：VarEnum.VT_ARRAY | VarEnum.VT_UI1； 
                    // string类型是8 VarEnum.VT_BSTR
                    // Indicates a short integer.VT_I2 = 2
                    // 直接定义的类型是uint8[] 数组型,但是还没有出现相对应的类型...
                }
                
                itmsID[i] = ITEM_DBblock[i];
            }
            // Item-IDs are given as described in opc-manual
            //itmsID[0] = ITEM_DBblock[0];
            #endregion

            try
            {
                // Now build the opc items for the DBblock group
                initOPCGroup(ref m_mgtGrpDBblock,
                    GRP_NAME_DBblock,
                    true,                   // grpActive
                    CLNT_HNDL_GRP_DBblock,  // grpClntHndl
                    reqUpdateRateDBblock,   // requpdaterate
                    false,                  // sync
                    true,                   // connectcallback
                    true);                 // Async  false

                // 事件添加初始化
                // receive messages from this class
                m_mgtGrpDBblock.OnOPCDataChanged +=
                    new OPCGroupManagement.DataChangedEventHandler(this.On_DBblock_DataChange);
                // 读opc完成
                m_mgtGrpDBblock.OnOPCReadCompleted +=
                    new OPCGroupManagement.ReadCompleteEventHandler(this.On_DBblock_ReadComplete);
                // 写opc完成
                m_mgtGrpDBblock.OnOPCWriteCompleted +=
                    new OPCGroupManagement.WriteCompleteEventHandler(this.On_DBblock_WriteComplete);

                // OPCGroupManagement类对象m_mgtGrpDBblock添加OPCITEMS
                Trace("向OPC组中添加子项: " + GRP_NAME_DBblock);
                m_mgtGrpDBblock.addOPCItems(itmsActive, // 是否激活
                                            itmsID,
                                            itmsType,
                                            itmsClntHndl,
                                            out itmsSrvHndl);
                Trace("添加OPC项成功.");
                // opcitems添加成功
                OPCItemsAd2CBX();
                // 给PLC发送-上位机准备ok信号 X2.0 序号(从0开始序号：16) 第17个item
                WriteAsyncToOPCOtherBoolVar(17, true);
                label_idx17_X20.BackColor = System.Drawing.Color.Green;



                // save the opc-item information for later usage
                m_itmExtDBblockList = new OPCItemExtenderList();
                itm = new OPCItemExtender(itmsActive[0],
                                            itmsID[0],
                                            itmsSrvHndl[0],
                                            itmsClntHndl[0],
                                            VarEnum.VT_BOOL,//VarEnum.VT_ARRAY | VarEnum.VT_UI1, 
                                            ref correspondingObject[0]);
                m_itmExtDBblockList.Add(itm);
            }
            catch (Exception ex)
            {
                // Just forward it
                throw ex;
            }
        }

        #region legencyInitial
        
        #endregion

        /// <summary>Adds the groups and items with further function calls</summary>
        /// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
        private void initOPCVariables()
        {
            try
            {
                // 创建DBblock group的Item项
                buildItemForDBblock(m_updRateDBblock);
                try
                { }
                catch (Exception ex)
                { throw new Exception("Error writing synchronous!\n\n" + ex.Message); }
            }
            catch (Exception ex)
            {
                // just forward it
                throw ex;
            }

        }
        #endregion

        #region private methods

        /// <summary> this method disconnects the application from the opcserver; furthermore all opc resources will be freed </summary>
        private void disconnectFromOPCServer()
        {
            if (m_mgtServer != null)
            {
                // Disconnect from OPC Server
                Trace("断开OPC服务器...");//Trace("Disconnecting OPC Server...");
                // NOTE: It is recommended to call dispose for the OPCServerManagement-object.
                // Because the class OPCServerManagement handles the freeing of the interfaces
                // this need not necessarily to be done. see code for destructor and dispose-methods
                // of the class OPCServerManagement.
                if (m_mgtServer != null)
                { m_mgtServer.Dispose();
                m_mgtServer = null;
                }
                // 还要清除已经插入的item
                if (m_mgtGrpDBblock != null)
                {
                    m_mgtGrpDBblock = null;
                    m_mgtGrpDBblock.Dispose();
                }
                // The groups and items have been released now
                Trace("OPC服务器已断开");//Trace("OPC Server is disconnected.");
            }
        }

        #region legencyCode
        /// <summary> sets the reset-flag (FillBuffer) in the plc </summary>
        /// <param name="reset"> the value, the reset-flag should have </param>
        /// <exception cref="Exception">forwards any exception (with short error description)</exception>
        private void setStartStopFlagForSimulation(bool reset)
        {
            // **************************************
            // set reset of simulation
            // **************************************
            int[] phServerHandle = new int[1];
            //phServerHandle[0] = m_itmExtSlowVisuList.getSrvHandle(ITEM_START_SIM);
            object[] phItemValue = new object[1];

            // here we don't read the item, because it is not connected with a
            // specific userinterface element
            phItemValue[0] = reset;

            int[] errors = null;

            try
            {
                // write it down
                Trace("Writing start/stop flag synchronous.");
                m_mgtGrpSlow.writeSync(phServerHandle, phItemValue, out errors);
                Trace("Writing synchronous successfull.");
            }
            catch (Exception ex)
            {
                // just forward
                throw ex;
            }
        }

        #endregion


        object Trace_Locker = new object();
        object Trace_Locker_All = new object();
        /// <summary> 添加信息到trace框(listbox) </summary>
        /// <param name="msg"> 要显示的字符串 </param>
        private void Trace(string msg)
        {
            //if (ShowTracingMenuItem.Checked || !ShowTracingMenuItem.Checked)
            //{
            if (msg == "开班基础码确认OK" || msg == "开班基础码确认NG,请确认后再生产")
            {
                // 确保这2条输出置顶,后续的显示都在这2条之后
            }
            lock(Trace_Locker_All)
            {
                if (msg == "清空")
                {
                    if (TraceListBox_Main != null)
                    {
                        TraceListBox_Main.Items.Clear();
                    }
                    else
                    {
                        TraceListBox_Main.Items.Insert(0, "TraceListBox_Main = null");
                    }
                }
                else
                {
                    msg = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + msg;
                    m_traceToolBox.addTrace(msg);
                    //TraceListBox_Main.Items.Add(msg);
                    TraceListBox_Main.Items.Insert(0, msg);  //Add(msg);
                    TraceListBox_Main.ScrollAlwaysVisible = true;
                    //this.TraceListBox_Main.Items[TraceListBox_Main.Items.Count - 1].EnsureVisible();
                    TraceListBox_Main.Update();

                    lock (Trace_Locker)  // 180308添加,测试是否还存在访问资源被占用的情况
                    {
                        // 输出Trace到log文件
                        TraceWrite2Log(msg);
                        //}
                    }
                }
                #region 之前写法,不知道为什么利用外部已经声明好的对象在这里没有成功(仅仅写入过一次，很可能还是资源占用的问题)
                //using(Stream fs = new FileStream(logPath + "\\日志文件" +"\\日志_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt", FileMode.Append,FileAccess.Write))
                //{ 
                //    using(StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                //    {
                //        sw.WriteLine(msg.ToString());
                //    }
                //}
                //if (sw != null) 不知道为什么不行
                //{
                //   // sw.WriteLine(msg.ToString());//msg
                //    sw.Write(msg.ToString());
                //}
                #endregion
            }
        }

        #endregion

        #region Eventhandlers

        

        
        private void UpdtRtsMenuItem_Click(object sender, System.EventArgs e)
        {
            UpdateRatesDlg dlg = new UpdateRatesDlg();

            //if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
                m_updRateDBblock = dlg.updateRateDBblock;

                btn_OPCCon.Enabled = true;
                ConnMenu.Enabled = true;
                UpdtRtsMenuItem.Enabled = false;
            //}
        }

        private void ShowTracingMenuItem_Click(object sender, System.EventArgs e)
        {
            // Disable/enable tracing-output
            ShowTracingMenuItem.Checked = !ShowTracingMenuItem.Checked;

            if (ShowTracingMenuItem.Checked)
                m_traceToolBox.Show();
            else
                m_traceToolBox.Hide();
        }

        private void DBRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            // switch the database to ACCESS
            if (DBRadioButton.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                XMLRadioButton.Checked = false;
                SQLDBRadioButton.Checked = false;
                m_DBConnector.activeDB = DBConnector.DBToSave.DBAccess;
                DelSingleDBMenuItem.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void XMLRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            // switch the database to XML
            if (XMLRadioButton.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                DBRadioButton.Checked = false;
                SQLDBRadioButton.Checked = false;
                m_DBConnector.activeDB = DBConnector.DBToSave.DBXML;
                DelSingleDBMenuItem.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void SQLDBRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            // switch the database to SQL SERVER
            if (SQLDBRadioButton.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                DBRadioButton.Checked = false;
                XMLRadioButton.Checked = false;
                m_DBConnector.activeDB = DBConnector.DBToSave.DBSQLServer;
                DelSingleDBMenuItem.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void ExitMenuItem_Click(object sender, System.EventArgs e)
        {
            //this.Close();
            //PersistentData_FormClosing(sender, (FormClosingEventArgs)e);
            //PersistentData_FormClosed(sender, (FormClosedEventArgs)e);

        }

        private void m_traceToolBox_OnCloseClicked(object source, EventArgs e)
        {
            ShowTracingMenuItem.Checked = false;
        }

        private void ShowRecMenuItem_Click(object sender, System.EventArgs e)
        {
            // read all data and show them with the "RecordsDialog"
            this.Cursor = Cursors.WaitCursor;
            RecordsDialog recDlg = new RecordsDialog(m_DBConnector.AccessDataSet, m_DBConnector.XMLDataSet, m_DBConnector.SQLDataSet);
            this.Cursor = Cursors.Default;
            recDlg.ShowDialog(this);
            recDlg.Dispose();
        }

        private void DelSingleDBMenuItem_Click(object sender, System.EventArgs e)
        {
            // clear the active database (deletes only the values!)
            this.Cursor = Cursors.WaitCursor;
            try
            {
                m_DBConnector.deleteDatatable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting the active datatable!\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DelRecMenuItem_Click(object sender, System.EventArgs e)
        {
            // clear all databases (deletes only the values!)
            this.Cursor = Cursors.WaitCursor;
            try
            {
                m_DBConnector.deleteAllDatatables();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting datatables!\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void StartSimButton_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // if the simulation is running, the database settings are not allowed to get changed
            if (StartSimButton.BackColor == System.Drawing.Color.Green)
            {
                XMLRadioButton.Enabled = false;
                DBRadioButton.Enabled = false;
                SQLDBRadioButton.Enabled = false;
                DelSingleDBMenuItem.Enabled = false;
                ShowRecMenuItem.Enabled = false;
                DelRecMenuItem.Enabled = false;
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                // wait until last writing to db is done
                if (m_waitHandle != null)
                    m_waitHandle.WaitOne();
                this.Cursor = Cursors.Default;

                XMLRadioButton.Enabled = m_XMLFileAvailable;
                DBRadioButton.Enabled = m_AccessAvailable;
                SQLDBRadioButton.Enabled = m_SQLAvailable;

                if (m_DBConnector.activeDB != DBConnector.DBToSave.DBNothing)
                    DelSingleDBMenuItem.Enabled = true;

                ShowRecMenuItem.Enabled = true;
                DelRecMenuItem.Enabled = true;
            }
        }

        private void InfoMenuItem_Click(object sender, System.EventArgs e)
        {
            AboutBox box = new AboutBox();

            box.ShowDialog();
        }
        #endregion

        #region Callback methods

        //实例码值：GN15-5019-A3B1707171030

        // delegate to be able to call "processDataChange" in an own thread
        private delegate void writingToDB(OPCItemExtenderList theList, OPCGroupManagement.OPCDataCallbackEventArgs e);
        /// <summary> processes the datachange event </summary>
        /// <param name="theList"> the itemextenderlist for which the datachange occured </param>
        /// <param name="e"> the parameters of the datachange event </param>
        private void processDataChange(OPCItemExtenderList theList, OPCGroupManagement.OPCDataCallbackEventArgs e)
        {
            string errorStr = "";

            // First: check for errors
            // just throw an exception if an error occured
            if (e.hrMastererror != 0 || e.hrMasterquality != 0)
            {
                // throw the first error
                errorStr = m_mgtServer.getErrorString(e.pErrors[0]);
                throw new Exception(errorStr + "\nQuality: " + m_mgtServer.getQualityText(e.pwQualities[0]));
            }

            OPCItemExtender itm;
            try
            {
                for (int i = 0; i < e.dwCount; i++)
                {
                    // save the value into the OPCItemExtender object
                    // NOTE: consequently, setting the value, will also fill the data into the database!
                    // --> this may take several seconds!
                    // --> when filling data into database it is recommended to call this method via a AsyncCallback (as delegate)
                    itm = (OPCItemExtender)(theList[e.phClientItems[i]]);
                    itm.actValue = e.pvValues[i];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        
        #region 异步Thread1：读ZESSI文件异步执行
        // 主界面label显示委托
        private delegate void UpdateMainFormLabel_Delegate();
        private void UpdateMainFormLabel()
        {
            label_Status4.BackColor = System.Drawing.Color.LightBlue;
            Thread.Sleep(50);
            label_Status4.BackColor = System.Drawing.Color.Gainsboro;
            Thread.Sleep(50);
        }

        XmlDocument xmlDoc = new XmlDocument();
        // 读excel的委托及事件--异步调用, 使用回调方式返回结果(将异步结果作为参数传递给回调函数)
        private delegate void delegate_readFromExcel(); // 定义读取excel的委托   需要添加事件变量-变量应该包括已经读到的码值

        bool[] returnValues = { false,false };
        bool zessi_CompareResults = false;
        bool m_bStopMark = false;  // 表明存在一个被终止打标的件
        public static string errorstr_pubstatic = "";
        private void On_ReadFromExcel() // object sender 要fill到委托中的函数(或者事件),其变量名称应该和委托相同
        {// 将excel数据复制到datagridview中

            #region 读xml并解析数据

            bool m_bIfZessiFileHavProblem = false;
            bool m_bZessiMissDetect = false;
            string errorstr = "";
            string xmlFileLoad = "C:\\[1]SimaticNetProj\\[2]蔡司数据\\GN15-5019-A3B 1801221710_20180122091952 - 副本.xml";
            zessi_CompareResults = false;

            zessi_DatFromZessiFile_Struct.AllMeasurePoint_str = new string[zessiFileParseOperate.MeasurePoint * 3];
            zessi_DatFromZessiFile_Struct.AllMeasurePoint = new double[zessiFileParseOperate.MeasurePoint * 3];
            zessi_DatFromZessiFile_Struct.AllMeasurePoint_Tol = new double[zessiFileParseOperate.MeasurePoint * 3 * 2];
            zessi_DatFromZessiFile_Struct.AllMeasurePoint_Tol_str = new string[zessiFileParseOperate.MeasurePoint * 3 * 2];
            zessi_DatFromZessiFile_Struct.AllTolerace_CompareResults = new bool[zessiFileParseOperate.MeasurePoint * 3 * 2];
            // 共享文件夹访问：
            // *****
            //
            //zessi_CompareResults = 
            try
            {
                returnValues = zessi_FileParse_Obj.On_ReadFromZessiFile(xmlFileLoad,
                                                                        dgvDatFromZessiFile,
                                                                        ref errorstr,
                                                                        ref zessi_DatFromZessiFile_Struct,
                                                                        ref m_bIfZessiFileHavProblem,
                                                                        ref m_bZessiMissDetect,
                                                                        ref zessi_CompareResults);// ref的要声明清楚
                zessi_CompareResults = returnValues[0];
                if (returnValues[1] == true)
                {
                    Trace("[异常] 读取文件出现异常.");
                    throw new Exception("[异常] 读取文件出现异常");
                }

                // 最好添加一个 zessi文件数量为0的标志位 以此来决定是否要发送读取完成以及公差比较信号
                CodeOriginal_FromFile = zessi_DatFromZessiFile_Struct.zessiMarkCode;//"GN15-5019-A3B1707171030";

                // 发送读取完成信号__在公差比较之前
                WriteAsyncToOPCOtherBoolVar(22, true); // X2.5 读取报告完成信号 从0开始序号21 第22个item
                label_idx22_X25.BackColor = System.Drawing.Color.Green;
            #endregion

                if (m_bIfZessiFileHavProblem || m_bZessiMissDetect)
                {
                    //throw new Exception(
                    Trace("[状态1] 读Zessi文件结束.Zessi文件存在问题:有点漏检\n" + errorstr);  //;请检查文件内容是否发生变化/n
                    //return;
                }
                if (!m_bIfZessiFileHavProblem || m_bIfZessiFileHavProblem)  // 无论蔡司文件是否有问题，都进行
                {
                    Trace("[状态1] 读Zessi文件结束.系统记录:\n" + errorstr);

                    #region 填充tbx
                    tbxDatDisp(zessi_DatFromZessiFile_Struct, Form_AdvancedFunc.B515_Standard_Tolerance);

                    #endregion

                    #region 测试
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
                    #endregion

                    #region 2.公差比较OK-NG对应操作
                    // 公差ok-ng对应操作
                    if (!zessi_CompareResults || m_bIfZessiFileHavProblem || m_bZessiMissDetect)
                    {//公差比较ng
                        //label_curPartMissDetect.Visible = true;
                        errorstr_pubstatic = errorstr;

                        WriteAsyncToOPCOtherBoolVar(28, true); // X3.3 公差比较NG信号 序号27 第28个项
                        label_idx28_X33.BackColor = System.Drawing.Color.Green;
                        // 弹出公差比较失败界面，进行操作
                        Form_ToleranceCompareNG F_TC = new Form_ToleranceCompareNG();
                        F_TC.Show();

                        while (!F_TC.IsDisposed)
                        {
                            Application.DoEvents();
                            //this.Enabled = false;
                        }
                        this.Enabled = true;
                    }
                    else
                    {// 公差比较ok
                        WriteAsyncToOPCOtherBoolVar(21, true); // X2.4 公差比较ok信号 序号20  第21个项
                        label_idx21_X24.BackColor = System.Drawing.Color.Green;
                        if (m_bStopMark) // 有终止打标的信号存在，说明上一个件没打标，当前的件继续沿用上一个码值
                        {
                            CodeSaveToDB = CodeOriginal_Standard;  // 此时的CodeSaveToDB已经是从DB50读取出来的基础码了
                        }
                        else // 没有的话直接在读到的码值处后三位数字+1
                        {
                            if (CodeOriginal_Standard.Length > 0) // 10
                            {
                                CodeSaveToDB = On_ReadFromXml_CodeAdd1(CodeOriginal_Standard);
                                // 表明码值已经+1
                                OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeHasAdded = true;
                                // 获取码值的byte信息
                                zessi_FileParse_Obj.GetCodeSaveToDBByte(CodeSaveToDB, CodeOriginal_Length, ref values_Data);
                            }
                        }

                        // 获取码值的byte信息
                        zessi_FileParse_Obj.GetCodeSaveToDBByte(CodeSaveToDB, CodeOriginal_Length, ref values_Data);
                    }
                    #endregion

                    // 判断是否在比较NG情况下继续打码
                    if (Form_ToleranceCompareNG.m_bContinueMark_UnderCompareNG)// || zessi_CompareResults)// 
                    {
                        WriteAsyncToOPCOtherBoolVar(28, false); // X3.3 公差比较NG信号 序号27 第28个项
                        label_idx28_X33.BackColor = System.Drawing.Color.Red;

                        if (m_bStopMark) // 有终止打标的信号存在，说明上一个件没打标，当前的件继续沿用上一个码值
                        {
                            CodeSaveToDB = CodeOriginal_Standard;  // 此时的CodeSaveToDB已经是从DB50读取出来的基础码了
                        }
                        else // 没有的话直接在读到的码值处后三位数字+1
                        {
                            try
                            {
                                //GN15-5019-A3B 1803051162
                                //GN15-5019-A3B1803051162
                                //GN15-5019-A3B18030511062
                                //GN15-5019-A3B18030511
                                if (CodeOriginal_Standard.Length > 0) // 10
                                {// .Substring(0, 21)

                                    CodeSaveToDB = On_ReadFromXml_CodeAdd1(CodeOriginal_Standard);
                                    // 在读zessi文件之前,就已经获得了基础码值,比较NG后,将基础码值给CodeSaveToDB(是要打并且存数据库的码)
                                    // 此时的码值必须非空

                                    // 表明码值已经+1
                                    OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeHasAdded = true;

                                    // 获取码值的byte信息
                                    zessi_FileParse_Obj.GetCodeSaveToDBByte(CodeSaveToDB, CodeOriginal_Length, ref values_Data);
                                }
                                else
                                {
                                    // 获取码值的byte信息
                                    zessi_FileParse_Obj.GetCodeSaveToDBByte(CodeSaveToDB, CodeOriginal_Length, ref values_Data);
                                    Trace("[状态1] 读取蔡司文件及OPC基础码值后进行码值处理阶段：码值长度小于10");
                                    MessageBox.Show("提示", "读取蔡司文件及OPC基础码值后进行码值处理阶段：码值长度小于10");
                                }
                            }
                            catch (Exception ex)
                            {
                                Trace("[状态1] 读取蔡司文件及OPC基础码值后进行码值处理阶段：码值长度或内容有异常" + ex.Message);
                                MessageBox.Show(ex.Message + "\n读取蔡司文件及OPC基础码值后进行码值处理阶段：码值长度或内容有异常", "提示");
                            }
                        }
                        // 复位
                    }
                    else
                    { }

                }
            }
            catch(Exception exx)
            {
                Trace("[异常] " + exx.Message);
                MessageBox.Show("[异常] \n\n" + exx.Message);
                // 异常处理函数
                P_ExceptionProcess(OPCItemProcessStatus_Struct);
            }
        }

        private string On_ReadFromXml_CodeAdd1(string CodeOriginal_Standard)
        {
            string CodeSaveToDB  = "";
            CodeSaveToDB = CodeOriginal_Standard; // 在读zessi文件之前,就已经获得了基础码值,比较NG后,将基础码值给CodeSaveToDB(是要打并且存数据库的码)
            string CodeSaveToDB_Last3Num = CodeSaveToDB.Substring(CodeSaveToDB.Length - 3, 3); // 此时的码值必须非空
            if ((Convert.ToInt16(CodeSaveToDB_Last3Num) + 1).ToString().Length == 1)
            {
                CodeSaveToDB = CodeSaveToDB.Substring(0, CodeSaveToDB.Length - 3) + "00" + (Convert.ToInt16(CodeSaveToDB_Last3Num) + 1).ToString();
            }
            else if ((Convert.ToInt16(CodeSaveToDB_Last3Num) + 1).ToString().Length == 2)
            {
                CodeSaveToDB = CodeSaveToDB.Substring(0, CodeSaveToDB.Length - 3) + "0" + (Convert.ToInt16(CodeSaveToDB_Last3Num) + 1).ToString();
            }
            else if ((Convert.ToInt16(CodeSaveToDB_Last3Num) + 1).ToString().Length == 3)
            {
                CodeSaveToDB = CodeSaveToDB.Substring(0, CodeSaveToDB.Length - 3) + (Convert.ToInt16(CodeSaveToDB_Last3Num) + 1).ToString();
            }
            return CodeSaveToDB;
        }
        private string On_ReadFromXml_CodeMinus1(string CodeOriginal_Standard)
        {
            string CodeSaveToDB = "";
            CodeSaveToDB = CodeOriginal_Standard; // 在读zessi文件之前,就已经获得了基础码值,比较NG后,将基础码值给CodeSaveToDB(是要打并且存数据库的码)
            string CodeSaveToDB_Last3Num = CodeSaveToDB.Substring(CodeSaveToDB.Length - 3, 3); // 此时的码值必须非空
            if ((Convert.ToInt16(CodeSaveToDB_Last3Num) - 1).ToString().Length == 1)
            {
                CodeSaveToDB = CodeSaveToDB.Substring(0, CodeSaveToDB.Length - 3) + "00" + (Convert.ToInt16(CodeSaveToDB_Last3Num) + 1).ToString();
            }
            else if ((Convert.ToInt16(CodeSaveToDB_Last3Num) - 1).ToString().Length == 2)
            {
                CodeSaveToDB = CodeSaveToDB.Substring(0, CodeSaveToDB.Length - 3) + "0" + (Convert.ToInt16(CodeSaveToDB_Last3Num) + 1).ToString();
            }
            else if ((Convert.ToInt16(CodeSaveToDB_Last3Num) - 1).ToString().Length == 3)
            {
                CodeSaveToDB = CodeSaveToDB.Substring(0, CodeSaveToDB.Length - 3) + (Convert.ToInt16(CodeSaveToDB_Last3Num) + 1).ToString();
            }
            return CodeSaveToDB;
        }

        #endregion

        public static string CallHistory_Date = "";
        static int CodeOriginal_Length = 40;
        static int ITEM_DBblock_Length = ITEM_DBblock.Length;
        public static string CodeOriginal_FromFile = ""; 
        public static string CodePLCSend1st = "";
        public static string CodePLCSend2nd = "";
        public static string CodeSaveToDB = "";
        public static string CodeOriginal_Standard = "";
        int m_StatusStep = 0; // 记录当前状态
        
        int[] DB8_40_UPDOWN = { ITEM_DBblock_Length - 40, ITEM_DBblock_Length - 1 };     // 45 84 85  序号：112-40->112-1 Value:112-40+1->112-1+1
        int[] DB50_40_UPDOWN = { ITEM_DBblock_Length - 80, ITEM_DBblock_Length - 41 }; // 5 44 85     //序号：112-80->112-41 Value:112-80+1->112-41+1    
        
        object locker = new object();
        object locker_status = new object();
        ManualResetEvent manualEventThread = new ManualResetEvent(true);
        StringBuilder MyStringBuilder = new StringBuilder(40); // 存储
        object[] values_ReadFromOPC = new object[ITEM_DBblock_Length]; //
        byte[] CodeSaveToDB_Byte = new byte[CodeOriginal_Length];  // 40个byte
        object[] values = new object[ITEM_DBblock_Length];
        object[] values_Data = new object[CodeOriginal_Length];
        int[] m_itmServerHandles_PC2PLCData = new int[CodeOriginal_Length];
        int[] m_itmServerHandles_PLC2PCData = new int[CodeOriginal_Length];
        bool m_StatusUI = false;

        #region 开机检测bool量
        bool m_bIfHvOriginalCodeFromPLC = false;
        bool m_bOriginalCodeOKSignal = false;
        bool m_bStartingUpSignal = false;
        #endregion

        #region [状态6] 延时清信号
        int m_Tick_Start = 0;
        int m_Tick_Diff = 0;

        #endregion

        private void On_DBblock_DataChange(object sender, OPCGroupManagement.OPCDataCallbackEventArgs e)
        {
            // NOTE: This is necessary, because the first datachange will be called from the OPCServer as soon as the opcitems are added to the opcgroup. The quality of these items is always QUALTIY_BAD.
            // --> wait until everything works fine.
            // 只要在没有连接OPC的情况下，不执行操作；防止在未连接OPC的状态下进入，误触发
            if (DisConnMenuItem.Enabled == true && ConnMenuItem.Enabled == false) { }
            else
                return; // 重要！在初始化添加item之后就会自动进入事件，然而此时还没有开始我们的正常逻辑
            if ((btn_OPCCon.Enabled == false) && (btn_OPCCon.Text == "Connect/Disconnect" || btn_OPCCon.Text == "Disconnect"))
                return;
            try
            {
                string ErrorStr = "";
                CheckIFRWOK(ErrorStr, e); // 检查是否error都为0 以及显示label


                #region 主界面状态显示部分
                try
                {
                    // X1.4 状态改变信号 序号12 第13项:由0-1或由1-0都去读INT4,把对应label变绿
                    if (CheckIFHvChangedStatusItemIdx_Ad_ItsValue(12, e) == 1 || 
                        CheckIFHvChangedStatusItemIdx_Ad_ItsValue(12, e) == 0)
                    {
                        m_StatusUI = true;
                        // INT4 序号32 第33项
                        int transactionID_ReadInt4 = 0;int[] INT4_item = { 33 };
                        m_mgtGrpDBblock.readAsync(INT4_item,ref transactionID_ReadInt4);
                        Thread.Sleep(200);  // 等一等，先进入readconplete事件
                        while (!m_bReadComplete_Flag); m_bReadComplete_Flag = false; // 阻断，直到读完得到新码值为止
                        m_StatusUI = false;
                        Disp_Label(m_bUIStatus_int,true);

                    }
                    // X1.5 状态故障信号 序号13 第14项:由0-1读INT4,把对应label变红;由1-0读INT4,把对应label变绿
                    if (CheckIFHvChangedStatusItemIdx_Ad_ItsValue(13, e) == 1) //由0->1读INT4,把对应label变红
                    {
                        m_StatusUI = true;
                        // INT4 序号32 第33项
                        int transactionID_ReadInt4 = 0; int[] INT4_item = { 33 };
                        m_mgtGrpDBblock.readAsync(INT4_item, ref transactionID_ReadInt4);
                        Thread.Sleep(200);  // 等一等，先进入readconplete事件
                        while (!m_bReadComplete_Flag) ; m_bReadComplete_Flag = false; // 阻断，直到读完得到新码值为止
                        m_StatusUI = false;
                        Disp_Label(m_bUIStatus_int, false);
                    }
                    else if (CheckIFHvChangedStatusItemIdx_Ad_ItsValue(13, e) == 0) //由1->0读INT4,把对应label变绿
                    {
                        m_StatusUI = true;
                        // INT4 序号32 第33项
                        int transactionID_ReadInt4 = 0; int[] INT4_item = { 33 };
                        m_mgtGrpDBblock.readAsync(INT4_item, ref transactionID_ReadInt4);
                        Thread.Sleep(200); while (!m_bReadComplete_Flag); 
                        m_bReadComplete_Flag = false; // 阻断，直到读完得到新码值为止
                        m_StatusUI = false;
                        Disp_Label(m_bUIStatus_int, true);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n主界面状态显示部分异常");
                }


                #endregion



                #region 状态机运行部分
                // 读进来的是变化的值！
                if (true) //(e.pErrors[0] == 0)&& (e.pErrors[1] == 0) && (e.pErrors[2] == 0) && (e.pErrors[3] == 0) && (e.pErrors[4] == 0)) // 均可成功读取  0 1 2 3 4
                {
                    // 状态机; 状态机是死的,等着变化的数据往上撞
                    try
                    {
                        // 检测到是因为busy信号变化进入的事件则直接退出
                        if (e.phClientItems.Length == 1 && e.phClientItems[0] == 18) // 此处的值对应的是其数组内从0开始的编号 从0开始序号18 第19个item
                            // 但是要写入的值是从1开始的 (CheckIFHvChangedItemIdx_Ad_ItsValue(18,e))
                            return;
                        // 每次进入数据变化事件 Busy信号都要对外输出为true
                        //WriteAsyncToOPCBusy(true);// 代表了X2.2 第18个opcitem-上位机工作Busy信号

                        ErrorStr = e.m_pvValues[0].ToString();
                        #region 开机检测
                        // 检测原始码ok信号 X0.0
                        if (CheckIFHvChangedItemIdx_Ad_ItsValue(0, e)) //e.m_pvValues[0].ToString() == "True" &&
                        { m_bOriginalCodeOKSignal = true; label_idx1_X00.BackColor = System.Drawing.Color.Green; }
                        // 开班检测ok信号 X0.1
                        if (CheckIFHvChangedItemIdx_Ad_ItsValue(1, e)) // 判断的这里没有关系，读取的对应的信号为从0开始的信号
                        { m_bStartingUpSignal = true; label_idx2_X01.BackColor = System.Drawing.Color.Green; }
                        if ((m_bStartingUpSignal && m_bOriginalCodeOKSignal) || m_bIfHvOriginalCodeFromPLC)
                        {
                            if (m_bIfHvOriginalCodeFromPLC)
                            { 
                                label_idx2_X01.BackColor = System.Drawing.Color.Green; 
                                label_idx1_X00.BackColor = System.Drawing.Color.Green; 
                            }
                            else // 第一次进入
                            { 
                                //读取基础码数据
                                int test=0;
                                m_mgtGrpDBblock.readAsync(m_mgtGrpDBblock.Get_m_itmServerHandles,ref test);  
                                Thread.Sleep(200);  // 等一等，先进入readconplete事件
                                while (!m_bReadComplete_Flag) ; // 阻断，直到读完得到新码值为止
                                m_bReadComplete_Flag = false;
                                CodeSaveToDB = CodeOriginal_Standard;  tbx_OriginalCode.Text = CodeSaveToDB;

                                //检查基础码码值是否正确
                                if (zessi_FileParse_Obj.CheckBaseCode(CodeSaveToDB)) 
                                { }
                                else
                                { throw new Exception("基础码判定有误,请检查输入的基础码是否正确"); }
                                
                                // 将基础码存入数据库
                                DBOperate_Obj.Insert_OpenMachineFlag_Dat(CodeSaveToDB, DateTime.Now);  
                                Trace("开班基础码确认OK,基础码为:" + CodeSaveToDB);
                                
                                label_idx2_X01.BackColor = System.Drawing.Color.Green; label_idx1_X00.BackColor = System.Drawing.Color.Green;
                                m_bIfHvOriginalCodeFromPLC = true;
                            }
                        }
                        else
                        { /*Trace("开班基础码确认NG,请确认后再生产"); */}
                        #endregion

                        if ((m_StatusStep == 0 && m_bIfHvOriginalCodeFromPLC))
                        {
                            // 先清除2文件夹中文件
                            zessi_FileParse_Obj.CheckZessiFileNumAdProcessOver_ForSignalClear(); 
                            m_StatusStep = 1;// 代表开机启动完成
                        }
                        if (m_StatusStep == 1 &&//(e.m_pvValues[0].ToString() == "True") && 
                             CheckIFHvChangedItemIdx_Ad_ItsValue(3, e)) //PLC发送的数据读取请求信号 -- 初始状态为0
                        {
                            #region 状态1
                            Trace("[状态1] 接收到数据读取请求信号");
                            label_curPartMissDetect.Visible = false;

                            if (e.m_phClientItems.Length > 1 && e.phClientItems[0] != 3) // 表明不是第一个元素发生变化
                            { /*return;*/}
                            label_idx4_X03.BackColor = System.Drawing.Color.Green;

                            // 为防止在X0.7信号为True阶段(状态6)没有清掉信号,在接收到X0.3后先清一遍
                            ClearOPCSignal_OnlyPC2PLC(true);


                            #region 1.第1步:去OPC B50-89中读取基础码值(这个基础码是上一个流程存储在DB50中的码值)
                            //只要m_bStopMark信号为false,则代表这个码值已经被成功打过,当前工件的码值可以在这个码值的基础上+1
                            //并保存
                            try
                            {
                                // 读取OPC中的基础码值：
                                if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
                                {
                                    int transactionID_Basic_OK = 0;
                                    m_mgtGrpDBblock.readAsync(m_mgtGrpDBblock.Get_m_itmServerHandles,
                                                                ref transactionID_Basic_OK);
                                }
                                Thread.Sleep(200);  // 等一等，先进入readconplete事件
                                while (!m_bReadComplete_Flag) ; // 阻断，直到读完得到新码值为止
                                m_bReadComplete_Flag = false;
                                CodeSaveToDB = CodeOriginal_Standard;
                                tbx_OriginalCode.Text = CodeSaveToDB;

                                Trace("[状态1] 读取当前基础码成功:" + CodeSaveToDB + ".");
                            }
                            catch (Exception ex)
                            {
                                Trace("[状态1] 读取DB50基础码阶段异常,异常信息: " + ex.Message);
                                MessageBox.Show("[状态1] 读取DB50基础码阶段异常,异常信息: \n\n" + ex.Message);
                            }
                            #endregion

                            #region 开启线程，异步写数据到数据库
                            // Now write the information to the database and visualize them;
                            // this will be done using a "worker" thread via IAsyncResult;

                            // 1. fill a delegate with the method "processDataChange"
                            //writingToDB wDB = new writingToDB(processDataChange);

                            // 2. inform the workerthread to signal the end with calling the method "DataChangeThreadEnd"
                            //AsyncCallback cb = new AsyncCallback(this.DataChangeThreadEnd);

                            // 3. start the "worker" thread
                            //Trace("Starting thread to save data to DB...");
                            //IAsyncResult ar = wDB.BeginInvoke(m_itmExtDBblockList,e,cb,null);

                            // 4. keep information, if the thread has ended
                            //m_waitHandle = ar.AsyncWaitHandle;
                            #endregion
                            #region 2.第2步:去访问Zessi文件夹获取偏差数据以及序列号,打标的码值以OPC的基准码为依据
                            try
                            {
                                WriteAsyncToOPCOtherBoolVar(20, true); // 告知PLC开始读取zessi文件信号 X2.3 从0开始序号19 第20个ITEM
                                label_idx20_X23.BackColor = System.Drawing.Color.Green;

                                #region 开启读excel线程，线程中包括  // 利用IAsyncResult 构建读excel(worker)线程完成获取码值及excel表中数据的操作
                                // 告知PLC开始读取zessi文件
                                delegate_readFromExcel d_rFExcel = On_ReadFromExcel;            // 1. 为前面的定义的代理readFromExcel添加(fill)一个执行事件
                                AsyncCallback cb = new AsyncCallback(this.DataChangeThreadEnd); // 2. 为读excel线程添加回调函数，调用DataChangeThreadEnd来表示结束
                                // 3. 启动线程
                                Trace("[状态1] 开始启动线程读取打码Excel中数据..."); //this.BeginInvoke(d_rFExcel); // 也是一种写法
                                //IAsyncResult ar_result = d_rFExcel.BeginInvoke(cb, d_rFExcel); //使用线程异步的执行委托所指向的方法,并获取异步操作的状态
                                IAsyncResult ar_result = d_rFExcel.BeginInvoke(null, null);
                                m_waitHandle = ar_result.AsyncWaitHandle;// 4. keep information, if the thread has ended
                                while (!ar_result.IsCompleted) ; // 异步执行结束之后再向下执行 像死了一样
                                // 告知PLC读取报告完成 EndInvoke可以获得被调用方法的返回值
                                
                                #endregion

                                lock (locker)
                                {

                                    if (Form_ToleranceCompareNG.m_bInputFindCodeSaveDB_NoMark) // 不打码-手动输入追溯码值
                                    {
                                        #region 发送X3.3 公差比较NG信号 的复位信号 {序号27 第28个项}

                                        WriteAsyncToOPCOtherBoolVar(28, false); // X3.3 公差比较NG信号 序号27 第28个项
                                        label_idx28_X33.BackColor = System.Drawing.Color.Red;

                                        #endregion

                                        #region // 不打码-手动输入追溯码值 并保存数据库; 发送X2.7 放弃打标入库OK信号
                                        //int CodeOriginal_int;
                                        CodeSaveToDB = Form_ToleranceCompareNG.str_ManualInput;

                                        Trace("[状态1] 不打码-手动输入追溯码值-" + CodeSaveToDB);
                                        Trace("[状态1] 开始存数据库...");

                                        // 添加存数据库程序 保存的码值即为CodeSaveToDB
                                        // 先将file读出来的数据结构体转换到最终要存db的结构体
                                        zessi_FileParse_Obj.TransferFileDatToSaveDBDat("NoRecord", DateTime.Now, 
                                                                                        zessi_CompareResults, "实测值", Form_AdvancedFunc.B515_Standard_Tolerance,
                                                                                        CodeSaveToDB,
                                                                                        zessi_DatFromZessiFile_Struct, ref zessi_DatFinalAdd2DB_Struct);
                                        DBOperate_Obj.Insert_Collect_Dat(zessi_DatFinalAdd2DB_Struct);
                                        //====

                                        Trace("[状态1] 放弃打标并保存数据到数据库.");

                                        WriteAsyncToOPCOtherBoolVar(24, true); // X2.7 放弃打标入库OK信号 从0开始序号23 第24个item
                                        label_idx24_X27.BackColor = System.Drawing.Color.Green;

                                        #endregion

                                        // 在step5中处理文件收尾工作
                                        m_StatusStep = 5;           // 回到状态1之前先到状态5,等pick-up存取数据ok信号;收到该信号后才能清信号(将放弃打标入库成功信号清掉),回到状态1;否则PLC不好判断
                                        m_bStopMark = true;         // 当前件没有打标，则该打标码顺延到下一个工件-直到该打标码顺利入库,再复位
                                        Form_ToleranceCompareNG.m_bInputFindCodeSaveDB_NoMark = false; // 复位
                                    }
                                    else if ((Form_ToleranceCompareNG.m_bContinueMark_UnderCompareNG == true) ||
                                             (zessi_CompareResults ) )  // 代表在NG状态下可以打标,且不输入手动追溯码
                                    {
                                        Form_ToleranceCompareNG.m_bContinueMark_UnderCompareNG = false;
                                        Trace("[状态1] 打标,且不输入手动追溯码.");

                                        Thread.Sleep(500);  // 出现不明错误,没有走到存数据的步骤,中间到了线程结束的回调函数处,
                                                            // 怀疑是多线程的错误，在这里延时500ms,先把callback执行过去再回来

                                        lock(locker_status)
                                        {
                                            #region 代表在NG状态下可以打标,且不输入手动追溯码
                                            int transactionID = 0;
                                            if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
                                            {
                                                for (int i = 0; i < CodeOriginal_Length; i++)
                                                {
                                                    m_itmServerHandles_PC2PLCData[i] = (ITEM_DBblock_Length - 80 + 1) + i;  // 0 -- 
                                                }
                                                m_mgtGrpDBblock.writeAsync(
                                                m_itmServerHandles_PC2PLCData,//m_mgtGrpDBblock.Get_m_itmServerHandles, 
                                                    values_Data,
                                                    ref transactionID);

                                                // 在这里出现过线程访问被占用的异常,导致X2.6信号没有发送
                                                Trace("[状态1] 向DB50存当前码值数据完成."); // 在这里会出现访问日志文件提示被其他进程占用的错误

                                            #region 发送打标内容生成OK信号 [X2.6序号22 第23个item]
                                            // 保持成功后-置位打标内容生成OK信号
                                            WriteAsyncToOPCOtherBoolVar(23, true); // X2.6 打标内容生成OK信号 序号22 第23个item
                                            label_idx23_X26.BackColor = System.Drawing.Color.Green;
                                            OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeGenerated = true; 
                                            #endregion

                                            tbx_OriginalCode.Text = CodeSaveToDB;// CodeSaveToDB;
                                            }
                                        }
                                        #endregion

                                        // 在读取并保存成功的状态下,状态变化为2 -- 并等待PLC发动码值的更新
                                        m_StatusStep = 2;
                                        
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Trace("[状态1] 异常信息: " + ex.Message);
                                // 异常处理函数
                                P_ExceptionProcess(OPCItemProcessStatus_Struct);
                                //== 出现异常后,要进入共享文件夹判断文件的数量 ==
                                MessageBox.Show("[状态1] 异常信息：\n" + ex.Message);
                            }
                            #endregion

                            #endregion
                        }

                        #region 状态5:等pick upX0.6信号,去清X2.7信号
                        if ( (m_StatusStep == 5) &&
                            (CheckIFHvChangedItemIdx_Ad_ItsValue(6, e))) // 等X0.6的pickup存取数据ok信号 序号为6 第7项
                        {
                            lock (locker)
                            {
                                label_idx7_X06.BackColor = System.Drawing.Color.Green;
                                zessi_FileParse_Obj.CheckZessiFileNumAdProcessOver(); // 状态机中的文件收尾工作

                                m_StatusStep = 1; // 回到状态1
                                m_bStopMark = false; // 手动输入追溯码阶段根本没有进行+1的操作,新的码值还没有生成,这里直接复位就好
                            }
                            #region 清信号 // 注意清信号
                            // 将这3个判别信号清掉
                            ClearDecisionSignal();
                            ClearOPCSignal_OnlyPC2PLC(true); //ClearOPCSignal(true,true); // 不清开机ok信号
                            #endregion
                        }
                        #endregion

                        //等待PLC的保存码值1成功信号 PLC保存到OPC中的码值后，将X1.0(序号:8)置位为True，进入该状态读取PLC保存的码值1
                        if ((m_StatusStep == 2) &&  
                                  CheckIFHvChangedItemIdx_Ad_ItsValue(8, e))
                        {
                            #region 状态2
                            try
                            {
                                if (e.m_phClientItems.Length > 1)// 表明不是第2个元素发生变化
                                { /*return;*/}
                                label_idx8_X10.BackColor = System.Drawing.Color.Green;
                                // 表明已经收到了第一次回传的码值，不管后面比较成功失败与否，只要在这个条件下出异常,就进去存数据库
                                OPCItemProcessStatus_Struct.m_OPCItemStatus_PLCHasSendBack1stMarkCode = true;
                                int transactionID_1st_OK = 0;

                                // 读取码值：
                                if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
                                {
                                    m_mgtGrpDBblock.readAsync(m_mgtGrpDBblock.Get_m_itmServerHandles,
                                                                ref transactionID_1st_OK);
                                }

                                Thread.Sleep(200);  // 等一等，先进入readconplete事件
                                while (!m_bReadComplete_Flag) ; // 阻断，直到读完得到新码值为止
                                m_bReadComplete_Flag = false;
                                tbx_PLC1stSendBKCode.Text = CodePLCSend1st;

                                

                                if (CodePLCSend1st == CodeSaveToDB)// 比较成功
                                {
                                    lock (locker)
                                    {
                                        Trace("[状态2] 接收到PLC第一次回传码值:" + CodePLCSend1st + ",码值比较成功");
                                        values_ReadFromOPC[2] = true; // X2.0 比较成功的item置为true
                                        // 向X3.0打标前打标内容核实OK信号 序号24 写入True 第25个item
                                        WriteAsyncToOPCOtherBoolVar(25, true);
                                        label_idx25_X30.BackColor = System.Drawing.Color.Green;
                                        m_StatusStep = 3; // 
                                    }
                                }
                                else // 打标前比较失败
                                {
                                    values_ReadFromOPC[2] = false;
                                    lock (locker)
                                    {
                                        Trace("[状态2] 接收到PLC第一次回传码值:" + CodePLCSend1st + ",打码前码值比较失败");
                                        
                                        //先发比较NG信号 向X3.1打标前打标内容核实NG信号 序号25 第26个item 应该为true
                                        WriteAsyncToOPCOtherBoolVar(26, true);
                                        label_idx26_X31.BackColor = System.Drawing.Color.Green;

                                        // 弹出对话框--第一次比较失败
                                        Form_FirstCodeCompare F_FCC_Dlg = new Form_FirstCodeCompare();
                                        F_FCC_Dlg.Show();
                                        while (!F_FCC_Dlg.IsDisposed)
                                        {
                                            Application.DoEvents();
                                            this.Enabled = false;
                                        }
                                        this.Enabled = true;

                                        if (Form_FirstCodeCompare.m_bContinueMark_Flag) //按了继续打码
                                        {
                                            if (Form_FirstCodeCompare.m_bContinueMarkWithPCCode_Flag)
                                            {
                                                WriteAsyncToOPCOtherBoolVar(31, true); //X3.6 核实NG,以PC码打标信号;序号30,第31项
                                                label_idx31_X36.BackColor = System.Drawing.Color.Green;
                                            }
                                            else if (Form_FirstCodeCompare.m_bContinueMarkWithPLCCode_Flag)
                                            {
                                                WriteAsyncToOPCOtherBoolVar(32, true);//X3.7 核实NG,以PLC码打标信号;序号31,第32项
                                                label_idx32_X37.BackColor = System.Drawing.Color.Green;
                                            }
                                            m_StatusStep = 3;  
                                        }
                                        else //放弃打码
                                        {
                                            // 向X3.2 终止打标信号 序号26 写入false  第27个item  应该为true
                                            WriteAsyncToOPCOtherBoolVar(27, true);
                                            label_idx27_X32.BackColor = System.Drawing.Color.Green;
                                            Trace("[状态2] 比较NG,终止打标.");
                                            
                                            m_bStopMark = true; // 当前件没有打标，则该打标码顺延到下一个工件-直到该打标码顺利入库,再复位

                                            
                                            zessi_FileParse_Obj.CheckZessiFileNumAdProcessOver(); // 状态机中的文件收尾工作
                                            Trace("[状态2] 文件收尾处理工作完成.");
                                            Trace("//===============");

                                            m_StatusStep = 6;   // 比较NG且放弃打码则先到状态6等X0.7信号,清掉X3.2信号---再回到初始状态1
                                            
                                            /*
                                             * step6中进行文件收尾处理工作
                                             */
                                        }
                                    }
                                }
                            }
                            catch (Exception e_state2)
                            {
                                // 异常处理函数
                                P_ExceptionProcess(OPCItemProcessStatus_Struct);
                                MessageBox.Show(e_state2.Message);
                            }
                            #endregion
                        }
                        
                        // 第一次比较成功，等待PLC第二次发送码值过来 PLC保存到OPC中的码值后，将X1.1(序号:9)置位为True，进入该状态读取PLC保存的码值2
                        if ((m_StatusStep == 3) &&
                                  CheckIFHvChangedItemIdx_Ad_ItsValue(9, e))
                        {
                            #region 状态3
                            try
                            {
                                Trace("[状态3] 接收到PLC二次回传的码值,进入状态3");
                                if (e.m_phClientItems.Length > 1) // 表明不是第2个元素发生变化
                                {   /*return;*/ }
                                label_idx9_X11.BackColor = System.Drawing.Color.Green;
                                int transactionID_2nd_OK = 0;

                                // 读取码值：
                                if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
                                {
                                    m_mgtGrpDBblock.readAsync(m_mgtGrpDBblock.Get_m_itmServerHandles,
                                                                ref transactionID_2nd_OK);

                                }

                                Thread.Sleep(200);  // 等一等，先进入readconplete事件
                                while (!m_bReadComplete_Flag) ; // 阻断，直到读完得到新码值为止
                                m_bReadComplete_Flag = false;

                                tbx_PLC2ndSendBKCode.Text = CodePLCSend2nd;

                                if (CodePLCSend2nd == CodeSaveToDB)// 比较成功 -- 第一次比较后确定了新的CodeSaveToDB,在第二次比较的时候就应该以PLC回传的码值和要入库打码的码值做比较
                                {
                                    lock (locker)
                                    {
                                        Trace("[状态3] 第二次码值比较成功，开始存数据库...");
                                        //开始写数据库
                                        //===
                                        /*
                                         1.添加写数据库函数
                                         */
                                        //===
                                        zessi_FileParse_Obj.TransferFileDatToSaveDBDat(CodeSaveToDB.Substring(10,3), DateTime.Now,
                                                                                       zessi_CompareResults,
                                                                                        "实测值", Form_AdvancedFunc.B515_Standard_Tolerance,
                                                                                        CodeSaveToDB,
                                                                                        zessi_DatFromZessiFile_Struct, ref zessi_DatFinalAdd2DB_Struct);
                                        DBOperate_Obj.Insert_Collect_Dat(zessi_DatFinalAdd2DB_Struct);


                                        //// 向X3.4 保存DB信号 序号28 写入false  第29个item
                                        //WriteAsyncToOPCOtherBoolVar(29, true);
                                        //label_idx29_X34.BackColor = System.Drawing.Color.Green;
                                        //Thread.Sleep(2000);
                                        //Trace("状态3:保存数据库成功");

                                        m_StatusStep = 4; // 
                                    }
                                }
                                else
                                {
                                    values_ReadFromOPC[4] = false;
                                    lock (locker)
                                    {
                                        Trace("[状态3] 第二次码值比较失败");
                                        // 弹出对话框--第2次比较失败
                                        Form_SecondCodeCompare F_FSC_Dlg = new Form_SecondCodeCompare();
                                        F_FSC_Dlg.Show();
                                        while (!F_FSC_Dlg.IsDisposed)
                                        {
                                            Application.DoEvents();
                                            this.Enabled = false;  
                                        }
                                        this.Enabled = true;

                                        Trace("[状态3] 开始保存数据到数据库...");
                                        //开始写数据库
                                        //===
                                        /*
                                         1.添加写数据库函数
                                         */
                                        //===
                                        zessi_FileParse_Obj.TransferFileDatToSaveDBDat(CodeSaveToDB.Substring(10,3), DateTime.Now,
                                                                                        zessi_CompareResults,
                                                                                        "实测值", Form_AdvancedFunc.B515_Standard_Tolerance,
                                                                                        CodeSaveToDB,
                                                                                        zessi_DatFromZessiFile_Struct, ref zessi_DatFinalAdd2DB_Struct);
                                        DBOperate_Obj.Insert_Collect_Dat(zessi_DatFinalAdd2DB_Struct);


                                        //// 向X3.4 保存DB信号 序号28 写入false  第29个item
                                        //WriteAsyncToOPCOtherBoolVar(29, true);
                                        //label_idx29_X34.BackColor = System.Drawing.Color.Green;
                                        //Thread.Sleep(2000);
                                        //Trace("状态3:保存数据库成功");

                                        m_StatusStep = 4; // 最后一次比较无论相同与否，最后都要存入数据库中

                                    }
                                }
                            }
                            catch (Exception e_state3)
                            {
                                // 异常处理函数
                                P_ExceptionProcess(OPCItemProcessStatus_Struct);
                                MessageBox.Show(e_state3.Message);
                            }
                            #endregion
                        }
                        #region 状态4
                        // 最后一步不用else if 直接进入状态即可
                        if (m_StatusStep == 4) // 保存所有数据到数据库
                        {
                            lock (locker)
                            {
                                m_bStopMark = false; // 当前件打标完成，取得的码值可以继续+1
                                try
                                {
                                    // 先处理文件，再发送存数据库ok信号
                                    int[] file_num_check = { 1, 1 };
                                    file_num_check = zessi_FileParse_Obj.CheckZessiFileNumAdProcessOver(); // 状态机中的文件收尾工作
                                    Trace("[状态4] 文件处理前[2]中数量为:" + file_num_check[0].ToString() + "; 处理后[2]中数量为:" + file_num_check[1].ToString());
                                    while (file_num_check[1] != 0)
                                    {
                                        Trace("[状态4] 文件收尾处理工作异常.开始重新清除...");
                                        file_num_check = zessi_FileParse_Obj.CheckZessiFileNumAdProcessOver();
                                        Trace("[状态4] 文件处理前[2]中数量为:" + file_num_check[0].ToString() + "; 处理后[2]中数量为:" + file_num_check[1].ToString());
                                    
                                    }
                                    if (file_num_check[1] == 0)
                                    {
                                        Trace("[状态4] 文件收尾处理工作完成.");
                                    }

                                    // 向X3.4 保存DB信号 序号28 写入false  第29个item
                                    WriteAsyncToOPCOtherBoolVar(29, true);
                                    label_idx29_X34.BackColor = System.Drawing.Color.Green;
                                    Thread.Sleep(2000);
                                    Trace("[状态3->4] 保存数据库成功.");


                                    // 进行周期收尾工作; 颜色变红 先进入6 然后重新回到状态1
                                    m_StatusStep = 6;  // 先进入状态6等X0.7,然后再清其他信号
                                    m_Tick_Start = Environment.TickCount;
                                    /*
                                     * 在step6中进行处理操作
                                     */
                                }
                                catch(Exception exx)
                                {
                                    Trace("[异常][状态4] " + exx.Message);
                                    throw new Exception("[状态4]");
                                }
                            }
                        }
                        else  // 打码信号为false
                        {
                            CheckIFRWOK(ErrorStr, e); // 检查是否error都为0 以及显示label
                        }
                        #endregion

                        #region 状态6
                        // 收到X0.7 序号7 第8项 0->1后, 才清信号(针对于第一次比较NG,然后放弃打标的情况;以及最后的入库成功情况)
                        if (m_StatusStep == 6)
                        {
                            lock (locker)
                            {
                                m_Tick_Diff = Environment.TickCount - m_Tick_Start;

                                if (m_Tick_Diff >= 5000) // 到了5s还是没有收到X0.7信号,就开始自己清
                                {
                                    label_idx8_X07.BackColor = System.Drawing.Color.Green;
                                    m_StatusStep = 1; // 回到状态1 
                                    // 状态6不能直接加m_StopMark的复位, 因为第一次比较NG放弃打标也会进入状态6

                                    #region 清信号
                                    // 将这3个判别信号清掉
                                    ClearDecisionSignal();
                                    ClearOPCSignal_OnlyPC2PLC(true); //ClearOPCSignal(true,true); // 不清开机ok信号
                                    Trace("[状态6] 自动清-流程结束-清信号完成.");
                                    Trace("//===============");
                                    Trace("清空");
                                    #endregion
                                }
                            }
                        }
                        if (m_StatusStep == 6 &&
                                  CheckIFHvChangedItemIdx_Ad_ItsValue(7, e)) // X0.7
                        {
                            lock (locker)
                            {
                                label_idx8_X07.BackColor = System.Drawing.Color.Green;
                                                                
                                m_StatusStep = 1; // 回到状态1 
                                // 状态6不能直接加m_StopMark的复位, 因为第一次比较NG放弃打标也会进入状态6

                                #region 清信号
                                // 将这3个判别信号清掉
                                ClearDecisionSignal();
                                ClearOPCSignal_OnlyPC2PLC(true); //ClearOPCSignal(true,true); // 不清开机ok信号
                                Trace("[状态6] 收到X0.7信号清-流程结束-清信号完成.");
                                Trace("//===============");
                                Trace("清空");
                                #endregion
                            }
                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        Trace("状态机中存在异常:" + ex.Message);
                        // 异常处理函数
                        P_ExceptionProcess(OPCItemProcessStatus_Struct);
                        MessageBox.Show("状态机中存在异常:/n" + ex.Message, "错误");
                    }
                   
                }
                #endregion
                
                // 每次离开数据变化事件时 Busy信号都要对外输出为false
                //WriteAsyncToOPCBusy(false);// 代表了X2.2 第18个opcitem-上位机工作Busy信号
                //Trace("OPC Group : " + GRP_NAME_DBblock + " 数据改变(触发)");// The opc server 触发一个数据改变事件
            }
            catch (Exception ex_ondata)
            {
                // 异常处理函数
                P_ExceptionProcess(OPCItemProcessStatus_Struct);
                MessageBox.Show(ex_ondata.Message);
            }
        }

        #region 异常处理函数 在每个发生异常的部分添加此函数
        // 在每个发生异常的部分添加此函数
        private void P_ExceptionProcess(OPCItemProcessStatus_Self OPCItemProcessStatus_Struct)
        {
            try
            {
                // 没有生成打标信号，码值也没有+1
                if (!OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeGenerated &&
                    !OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeHasAdded
                    )
                {
                    lock (locker_ExceptionProcess)
                    {
                        P_ExceptionProcess_Sub();
                    }
                }
                // 没有生成打标信号，但是码值已经+1了..............................
                else if (!OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeGenerated &&
                    OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeHasAdded
                    )
                {

                    lock (locker_ExceptionProcess)
                    {
                        // 0.已经加了的码值减1
                        CodeSaveToDB = On_ReadFromXml_CodeMinus1(CodeSaveToDB);
                        Trace("[异常处理] 已经收到了X1.0PLC 1次回传信号:保存数据库成功.");
                        P_ExceptionProcess_Sub();
                    }

                }
                // 已经收到了X1.0PLC 1次回传信号
                else if (OPCItemProcessStatus_Struct.m_OPCItemStatus_PLCHasSendBack1stMarkCode)
                {
                    lock (locker_ExceptionProcess)
                    {
                        P_ExceptionProcess_Sub();
                        // 0.将这个件存数据库
                        zessi_FileParse_Obj.TransferFileDatToSaveDBDat(CodeSaveToDB.Substring(10, 3), DateTime.Now,
                                                                                            zessi_CompareResults,
                                                                                            "实测值", Form_AdvancedFunc.B515_Standard_Tolerance,
                                                                                            CodeSaveToDB,
                                                                                            zessi_DatFromZessiFile_Struct, ref zessi_DatFinalAdd2DB_Struct);
                        DBOperate_Obj.Insert_Collect_Dat(zessi_DatFinalAdd2DB_Struct);
                        Trace("[异常处理] 已经收到了X1.0PLC 1次回传信号:保存数据库成功.");
                    }
                }
                // 将这3个判别信号清掉
                ClearDecisionSignal();
            }
            catch (Exception exx)
            {
                Trace("[异常] 异常处理函数异常!?!?,具体内容:" + exx.Message);
                MessageBox.Show("\n" + exx.Message);
            }
        }

        #endregion

        object locker_ExceptionProcess = new object();
        private void P_ExceptionProcess_Sub()
        {
            // 1.屏蔽X2.1信号，告知PLC当前软件有异常
            m_bCurrentExceptionHappened = true;
            Trace("[异常处理] 软件异常,中断X2.1信号发送");
            // 2.处理文件，将文件放到2文件夹中 // 文件收尾处理
            int file_num = zessi_FileParse_Obj.CheckZessiFileNumAdProcessOver_ForSignalClear(); // 状态机中的文件收尾工作
            Trace("[异常处理] 文件收尾处理工作完成(移送备份文件夹).此时共享文件夹存在xml文件数量为:" + file_num.ToString());   
            // 3.清PC侧信号
            ClearOPCSignal_OnlyPC2PLC(true);
            Trace("[异常处理] 清除PC侧信号完成");
            // 4.清状态，状态回到状态1
            m_StatusStep = 1;
            Trace("[异常处理] 回到状态1,等待PLC发送读取文件信号X0.3.");
 
        }
        // 将这3个判别信号清掉
        private void ClearDecisionSignal()
        {
            OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeGenerated = false;
            OPCItemProcessStatus_Struct.m_OPCItemStatus_MarkCodeHasAdded = false;
            OPCItemProcessStatus_Struct.m_OPCItemStatus_PLCHasSendBack1stMarkCode = false;
        }

        private void ClearOPCSignal(bool exceptOPCPrepareOK,bool exceptOpenMachineOKSig)
        {
            #region 清信号
            // 注意清信号
            try
            {
                int transactionID_ClearSig = 0;
                object[] values_Clear = new object[ITEM_DBblock.Length];
                for (int i = 0; i < ITEM_DBblock.Length; i++)
                {
                    if (i < 32)
                    {
                        if (exceptOPCPrepareOK)
                        {
                            if (i == 16) { values_Clear[i] = true; }
                            else 
                            { 
                                if (exceptOpenMachineOKSig)
                                {
                                    if (i == 0 || i == 1) { values_Clear[i] = true; }
                                    else { values_Clear[i] = false; }
                                }
                                else
                                { values_Clear[i] = false; }
                            }
                        }
                        else
                        {
                            if (exceptOpenMachineOKSig)
                            {
                                if (i == 0 || i == 1) { values_Clear[i] = true; }
                                else { values_Clear[i] = false; }
                            }
                            else
                            { values_Clear[i] = false; }
                        }
                    }
                    else
                    {
                        values_Clear[i] = 0;
                    }

                }
                if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
                {
                    m_mgtGrpDBblock.writeAsync(m_mgtGrpDBblock.Get_m_itmServerHandles,
                                                values_Clear,
                                                ref transactionID_ClearSig);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n清信号错误");
            }
            #endregion

        }
        private void ClearOPCSignal_Label(bool exceptOPCPrepareOK)
        {
            if (exceptOPCPrepareOK)
            {
                label_idx8_X07.BackColor = System.Drawing.Color.Red;
                label_idx9_X11.BackColor = System.Drawing.Color.Red;
                label_idx8_X10.BackColor = System.Drawing.Color.Red;
                label_idx7_X06.BackColor = System.Drawing.Color.Red;
                label_idx4_X03.BackColor = System.Drawing.Color.Red;
                label_idx2_X01.BackColor = System.Drawing.Color.Red;
                label_idx14_X15.BackColor = System.Drawing.Color.Red;
                label_idx13_X14.BackColor = System.Drawing.Color.Red;
                label_idx1_X00.BackColor = System.Drawing.Color.Red;

                label_idx31_X36.BackColor = System.Drawing.Color.Red;
                label_idx32_X37.BackColor = System.Drawing.Color.Red;
                label_idx29_X34.BackColor = System.Drawing.Color.Red;
                label_idx28_X33.BackColor = System.Drawing.Color.Red;
                label_idx27_X32.BackColor = System.Drawing.Color.Red;
                label_idx26_X31.BackColor = System.Drawing.Color.Red;
                label_idx25_X30.BackColor = System.Drawing.Color.Red;
                label_idx24_X27.BackColor = System.Drawing.Color.Red;
                label_idx23_X26.BackColor = System.Drawing.Color.Red;
                label_idx22_X25.BackColor = System.Drawing.Color.Red;
                label_idx21_X24.BackColor = System.Drawing.Color.Red;
                label_idx20_X23.BackColor = System.Drawing.Color.Red;
                label_idx18_X21.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                label_idx8_X07.BackColor = System.Drawing.Color.Red;
                label_idx9_X11.BackColor = System.Drawing.Color.Red;
                label_idx8_X10.BackColor = System.Drawing.Color.Red;
                label_idx7_X06.BackColor = System.Drawing.Color.Red;
                label_idx4_X03.BackColor = System.Drawing.Color.Red;
                label_idx2_X01.BackColor = System.Drawing.Color.Red;
                label_idx14_X15.BackColor = System.Drawing.Color.Red;
                label_idx13_X14.BackColor = System.Drawing.Color.Red;
                label_idx1_X00.BackColor = System.Drawing.Color.Red;


                label_idx31_X36.BackColor = System.Drawing.Color.Red;
                label_idx32_X37.BackColor = System.Drawing.Color.Red;
                label_idx29_X34.BackColor = System.Drawing.Color.Red;
                label_idx28_X33.BackColor = System.Drawing.Color.Red;
                label_idx27_X32.BackColor = System.Drawing.Color.Red;
                label_idx26_X31.BackColor = System.Drawing.Color.Red;
                label_idx25_X30.BackColor = System.Drawing.Color.Red;
                label_idx24_X27.BackColor = System.Drawing.Color.Red;
                label_idx23_X26.BackColor = System.Drawing.Color.Red;
                label_idx22_X25.BackColor = System.Drawing.Color.Red;
                label_idx21_X24.BackColor = System.Drawing.Color.Red;
                label_idx20_X23.BackColor = System.Drawing.Color.Red;
                label_idx18_X21.BackColor = System.Drawing.Color.Red;
                label_idx17_X20.BackColor = System.Drawing.Color.Red;
            }
        }
        private void ClearOPCSignal_OnlyPC2PLC(bool exceptOPCPrepareOK)
        {
            //X2.0-X3.7 序号16-31 第17-32个项 
            //除了X2.0上位机准备OK信号，即从17-31 第18-32个项
            #region 清PC->PLC侧信号
            // 注意清信号
            int[] ClearPC2PLCItems;// = new int[15]; // 15项
            object[] values_Clear;
            try
            {
                int transactionID_ClearSig = 0;
                if (exceptOPCPrepareOK)
                {
                    ClearPC2PLCItems = new int[15];
                    values_Clear = new object[ClearPC2PLCItems.Length];
                    for (int i = 0; i < ClearPC2PLCItems.Length; i++)
                    {

                        ClearPC2PLCItems[i] = 18 + i; // 读写是按第几项进行，非数据序号！
                        values_Clear[i] = false;
                    }
                    ClearOPCSignal_Label(exceptOPCPrepareOK);
                }
                else // 包括所有PC下发信号
                {
                    ClearPC2PLCItems = new int[16];
                    values_Clear = new object[ClearPC2PLCItems.Length];
                    for (int i = 0; i < ClearPC2PLCItems.Length; i++)
                    {

                        ClearPC2PLCItems[i] = 17 + i; // 读写是按第几项进行，非数据序号！
                        values_Clear[i] = false;
                    }
                    ClearOPCSignal_Label(false);
                }

                if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
                {
                    m_mgtGrpDBblock.writeAsync(ClearPC2PLCItems,
                                                values_Clear,
                                                ref transactionID_ClearSig);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n清信号错误");
            }
            #endregion

        }

        private void ReadAsyncFromOpcPosition(int[] m_itmServerHandles)
        {
            // 读取OPC中的基础码值：
            if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
            {
                int transactionID_Basic_OK = 0;
                m_mgtGrpDBblock.readAsync(m_itmServerHandles,
                                            ref transactionID_Basic_OK);
            }
            //Thread.Sleep(200);  // 等一等，先进入readconplete事件
            //while (!m_bReadComplete_Flag) ; // 阻断，直到读完得到新码值为止
            //m_bReadComplete_Flag = false;
 
        }
        // 向指定位置写入值
        private void WriteAsyncToOpcPosition(int[] m_itmServerHandles_Busy, object[] values_busy)
        {
            if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
            {
                int transactionID_Busy = 0;
                m_mgtGrpDBblock.writeAsync(m_itmServerHandles_Busy, values_busy, ref transactionID_Busy);
            }
        }
        // 向busy信号处写入true/false值
        private void WriteAsyncToOPCBusy(bool value)
        {
            if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
            {
                int[] m_itmServerHandles_Busy = { 19 };// 代表了X2.2 应该是第19个item  第18个opcitem-上位机工作Busy信号
                object[] values_busy = { value };
                WriteAsyncToOpcPosition(m_itmServerHandles_Busy, values_busy);
            }
        }
        // 向其他bool信号处写入true/false值
        private void WriteAsyncToOPCOtherBoolVar(int idx, bool value)
        {
            if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
            {
                int[] m_itmServerHandles_Busy = { idx };// 代表了X2.2 第18+1个opcitem-上位机工作Busy信号
                object[] values_busy = { value };
                WriteAsyncToOpcPosition(m_itmServerHandles_Busy, values_busy);
            }
        }


        // 检查当前改变量中是否有需要检测的索引值CheckedItemIdx
        private bool CheckIFHvChangedItemIdx_Ad_ItsValue(int CheckedItemIdx, OPCGroupManagement.OPCDataCallbackEventArgs e)
        {
            int chk_int = 0;
            bool checkedhv = false;
            bool checkedValue = false;
            for (int i = 0; i < e.phClientItems.Length; i++)
            {
                chk_int = e.phClientItems[i];
                if (chk_int == CheckedItemIdx) // 说明有相同的值 sh
                {
                    checkedhv = true;
                    if (e.pvValues[i].ToString() == "True")
                    {
                        checkedValue = true;
                    }
                    else
                    {
                        if (e.pvValues[i].ToString().Contains("1"))
                        {
                            checkedValue = true;
                        }
                        else
                        {
                            checkedValue = false;
                        }
                    }
                    continue;
                }
            }
            if (checkedhv && checkedValue) // 在变化的值中既有该索引，且值为true
                return true;
            else
                return false;
        }
        private int CheckIFHvChangedStatusItemIdx_Ad_ItsValue(int CheckedItemIdx, OPCGroupManagement.OPCDataCallbackEventArgs e)
        {
            int chk_int = 0;
            bool checkedhv = false;
            bool checkedValue = false;
            for (int i = 0; i < e.phClientItems.Length; i++)
            {
                chk_int = e.phClientItems[i];
                if (chk_int == CheckedItemIdx) // 说明有相同的值 sh
                {
                    checkedhv = true;
                    if (e.pvValues[i].ToString() == "True")
                    {
                        checkedValue = true;
                    }
                    else if (e.pvValues[i].ToString() == "False")
                    {
                        checkedValue = false;
                    }
                    continue;
                }
            }
            if (checkedhv && checkedValue) // 在变化的值中既有该索引，且值为true
                return 1;
            else if (checkedhv && !checkedValue) // 在变化的值中既有该索引，且值为false
                return 0;
            else
                return -1;
        }
        // 检查索引是否为true
        private bool CheckBoolItemState(int CheckedItemIdx, OPCGroupManagement.OPCDataCallbackEventArgs e)
        {
            object chk_bool;
            int chk_int;
            bool checkedbool = false;
            for (int i = 0; i < e.phClientItems.Length; i++)
            {
                chk_int = e.phClientItems[i];
                if (chk_int == CheckedItemIdx)
                {
                    chk_bool = e.pvValues[i];
                    if (chk_bool.ToString() == "True") // 说明有相同的值 sh
                    {
                        checkedbool = true;
                        continue;
                    }
                }

            }
            if (checkedbool)
                return true;
            else
                return false;
        }
        //
        private void CheckIFRWOK(string ErrorStr, OPCGroupManagement.OPCDataCallbackEventArgs e)
        {
            for (int i = 0; i < e.m_pErrors.Length; i++)
            {
                if (e.m_pErrors[i] != 0)
                {
                    ErrorStr = m_mgtServer.getErrorString(e.m_pErrors[i]);
                    System.Diagnostics.Debug.WriteLine(ErrorStr);
                    throw new Exception(ErrorStr);
                }
                //Disp_Label(i, e);
            }
        }
        //
        private void Disp_Label(int i, bool ColorStatus)
        {
            #region label的显示
            switch (i)
            {
                case 0:
                    label_Status1.BackColor = System.Drawing.Color.Goldenrod;
                    label_Status2.BackColor = System.Drawing.Color.Goldenrod;
                    label_Status3.BackColor = System.Drawing.Color.Goldenrod;
                    label_Status4.BackColor = System.Drawing.Color.Goldenrod;
                    label_Status5.BackColor = System.Drawing.Color.Goldenrod;
                    label_Status6.BackColor = System.Drawing.Color.Goldenrod;
                    label_Status7.BackColor = System.Drawing.Color.Goldenrod;
                    label_Status8.BackColor = System.Drawing.Color.Goldenrod;
                    break;
                case 1: // 零件放置到位
                    if (ColorStatus) label_Status1.BackColor = System.Drawing.Color.LimeGreen;
                    else label_Status1.BackColor = System.Drawing.Color.Red;
                    break;
                case 2: // 夹紧到位
                    if (ColorStatus) label_Status2.BackColor = System.Drawing.Color.LimeGreen;
                    else label_Status2.BackColor = System.Drawing.Color.Red;
                    break;
                case 3: // ZESSI检测并传输检测数据
                    if (ColorStatus) label_Status3.BackColor = System.Drawing.Color.LimeGreen;
                    else label_Status3.BackColor = System.Drawing.Color.Red;
                    break;
                case 4: // 接收ZESSI数据
                    if (ColorStatus) label_Status4.BackColor = System.Drawing.Color.LimeGreen;
                    else label_Status4.BackColor = System.Drawing.Color.Red;
                    break;
                case 5: // 结果判定并进行声光报警
                    if (ColorStatus) label_Status5.BackColor = System.Drawing.Color.LimeGreen;
                    else label_Status5.BackColor = System.Drawing.Color.Red;
                    break;
                case 6: // 零件被转移到打标工位
                    if (ColorStatus) label_Status6.BackColor = System.Drawing.Color.LimeGreen;
                    else label_Status6.BackColor = System.Drawing.Color.Red;
                    break;
                case 7: // 打标
                    if (ColorStatus) label_Status7.BackColor = System.Drawing.Color.LimeGreen;
                    else label_Status7.BackColor = System.Drawing.Color.Red;
                    break;
                case 8: // 入库
                    if (ColorStatus) label_Status8.BackColor = System.Drawing.Color.LimeGreen;
                    else label_Status8.BackColor = System.Drawing.Color.Red;
                    break;
                default:
                    break;
            }
            #endregion
        }

        private DateTime ToDateTime(OpcRcw.Da.FILETIME ft)
        {
            long highbuf = (long)ft.dwHighDateTime;
            long buffer = (highbuf << 32) + ft.dwLowDateTime;
            return DateTime.FromFileTimeUtc(buffer);
        }

        bool m_bReadComplete_Flag = false;
        string[] aa;
        int m_bUIStatus_int = 0;
        // 读opc完成事件，使用的事件传输类型是OPCDataCallbackEventArgs
        private void On_DBblock_ReadComplete(object sender, OPCGroupManagement.OPCDataCallbackEventArgs e)
        {
            int idx = OPCItem_Read_Idx;
            try
            {
                if (DisConnMenuItem.Enabled == true && ConnMenuItem.Enabled == false) { }
                else
                    return; // 重要！在初始化添加item之后就会自动进入事件，然而此时还没有开始我们的正常逻辑
                if ((btn_OPCCon.Enabled == false) && (btn_OPCCon.Text == "Connect/Disconnect" || btn_OPCCon.Text == "Disconnect"))
                    return;

                string ErrorStr = "";
                CheckIFRWOK(ErrorStr, e);  // datacallback的参数

                if (m_StatusUI && e.phClientItems.Length == 1) // 读取界面Status数字 -- 要和后面的状态区分开
                {
                    m_bUIStatus_int = Convert.ToInt16(e.pvValues[0]);
                    m_bReadComplete_Flag = true;
                }
                if (e.phClientItems.Length == 1 || e.phClientItems.Length < 100) // 说明不是在后续几个状态进行的读操作;后续的几个操作是全部读取
                { return; }                                                      // 后续的几个操作是全部读取

                if (true)
                {
                    if (m_StatusStep == 2)  // 记得改状态
                    {
                        MyStringBuilder.Remove(0, MyStringBuilder.Length);
                        aa = null;
                        #region 读plc第一次发送的数据
                        for (int i = 0; i < e.m_pvValues.Length; i++)
                        {
                            values_ReadFromOPC[i] = e.m_pvValues[i];
                            if (i >= DB8_40_UPDOWN[0] && i <= DB8_40_UPDOWN[1])
                                MyStringBuilder.Append(Convert.ToChar(e.m_pvValues[i]));
                            else
                            { }
                            if (MyStringBuilder.Length == (DB8_40_UPDOWN[1] - DB8_40_UPDOWN[0] + 1))
                            {
                                CodePLCSend1st = MyStringBuilder.ToString();
                            }
                        }
                        aa = CodePLCSend1st.Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries);
                        CodePLCSend1st = aa[0];
                        m_bReadComplete_Flag = true;
                        #endregion
                    }
                    else if (m_StatusStep == 3)
                    {
                        MyStringBuilder.Remove(0, MyStringBuilder.Length);
                        aa = null;
                        #region 读plc第2次发送的数据
                        for (int i = 0; i < e.m_pvValues.Length; i++)
                        {
                            values_ReadFromOPC[i] = e.m_pvValues[i];
                            if (i >= DB8_40_UPDOWN[0] && i <= DB8_40_UPDOWN[1])
                                MyStringBuilder.Append(Convert.ToChar(e.m_pvValues[i]));
                            else
                            { }
                            if (MyStringBuilder.Length == (DB8_40_UPDOWN[1] - DB8_40_UPDOWN[0] + 1))
                            {
                                CodePLCSend2nd = MyStringBuilder.ToString();
                            }
                        }

                        aa = CodePLCSend2nd.Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries);
                        CodePLCSend2nd = aa[0];
                        m_bReadComplete_Flag = true;
                        #endregion
                    }
                    else if (m_StatusStep == 1 || m_StatusStep == 0) // 表示在状态1中读取DB50的基础码值
                    {
                        MyStringBuilder.Remove(0, MyStringBuilder.Length);
                        aa = null;
                        #region 读DB50中的基础码值
                        for (int i = 0; i < e.m_pvValues.Length; i++)
                        {
                            values_ReadFromOPC[i] = e.m_pvValues[i];
                            // 读数据
                            if (i >= DB50_40_UPDOWN[0] && i <= DB50_40_UPDOWN[1])
                                MyStringBuilder.Append(Convert.ToChar(e.m_pvValues[i]));
                            else
                            { }
                            if (MyStringBuilder.Length == (DB50_40_UPDOWN[1] - DB50_40_UPDOWN[0] + 1))
                            {
                                CodeOriginal_Standard = MyStringBuilder.ToString();
                            }
                        }
                        // 码值的解析
                        aa = CodeOriginal_Standard.Split(new string[] {" ", "\0" }, StringSplitOptions.RemoveEmptyEntries);
                        CodeOriginal_Standard = aa[0];
                        m_bReadComplete_Flag = true;

                        if (m_StatusStep == 0)
                        {
                            // 读信号量(判断X0.0和X0.1是否为True,从而确定系统是否已经开机,省掉必须等发信号的步骤)
                            if (values_ReadFromOPC[0].ToString() == "True" && values_ReadFromOPC[1].ToString() == "True")
                            {
                                m_bIfHvOriginalCodeFromPLC = true;
                                Trace("开班基础码确认OK,基础码为:" + CodeOriginal_Standard);
                            }
                            else
                            {
                                m_bIfHvOriginalCodeFromPLC = false;
                                Trace("开班基础码确认NG,请在触摸屏输入基础码,并发送基础码OK、开班确认OK信号.");
                            }
                        }

                        #endregion
 
                    }
                    #region Test
                    // Value
                    //tbx_OPCItemValue.Text = String.Format("{0}", e.pvValues[idx]);
                    tbx_OPCItemValue.Text = e.pvValues[idx].ToString();
                    // Quality
                    tbx_OPCItemQuality.Text = m_mgtServer.getQualityText(e.pwQualities[idx]);
                    // Timestamp
                    DateTime dt = ToDateTime(e.pftTimeStamps[idx]);
                    tbx_OPCItemTimeStamp.Text = dt.ToString();
                    #endregion
                }
            }
            catch (Exception)
            {
                string strResult = "";
                strResult = m_mgtGrpDBblock.GetErrorString(e.pErrors[idx], "");
                // 异常处理函数
                P_ExceptionProcess(OPCItemProcessStatus_Struct);
                Trace("[异常] OnReadComlete");
                MessageBox.Show(strResult, "OnReadComlete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 写opc完成事件，使用的变量传输类型是OPCWriteCompletedEventArgs
        private void On_DBblock_WriteComplete(object sender, OPCGroupManagement.OPCWriteCompletedEventArgs e)
        {
            if (btn_OPCCon.Enabled == false)
                return;

            if (e.pErrors[0] == 0)
            {

            }
            else // 写入失败
            {

            }
        }

        private void m_DBConnector_OnDBblockOccured(object sender, DBConnector.DataToVisualize e)
        {
            Trace("The DBConnector object has filled the data into the database");

            // statistics
            // telegram size
            
            // mean send time
            //object timeDiff = m_itmExtFastVisuList.getValue(ITEM_TIMEDIFF);
            // after downloading the STEP7 programm, the first timediff will be 0
            // -> take a dummy value for the first one
            //if (Convert.ToInt32(timeDiff) == 0)
            //    timeDiff = (System.UInt32)250;
            //m_meanSendTime += (System.UInt32)timeDiff;
            m_numberTelegrams++;
            int meanSendTime = (int)(m_meanSendTime / m_numberTelegrams);
            
            // mean data rate (bytes per second)
            decimal telPerSecond = (decimal)(1000.0 / meanSendTime);
            m_meanDataRate = (int)(e.telHead.telLength * telPerSecond);
            
            // time for analyzing and converting
            decimal dec = (decimal)(e.timeSpanForConverting);
            
            // time for saving to DB
            dec = (decimal)(e.timeSpanForSaving);
            
            // telegram head data
           
            // measured data head
            decimal dec1 = System.Math.Round((decimal)e.measuredHead.headDataPressure, 2);
            decimal dec2 = System.Math.Round((decimal)e.measuredHead.headDataEnergy, 2);
            
            // measured data
            decimal dec3 = System.Math.Round((decimal)e.measuredData.dataTemperature, 2);
            decimal dec4 = System.Math.Round((decimal)e.measuredData.dataPressure, 2);
            decimal dec5 = System.Math.Round((decimal)e.measuredData.dataFlow, 2);
            decimal dec6 = System.Math.Round((decimal)e.measuredData.dataHumidity, 2);
            
            // write ackbit
            int[] srvHandle = new int[1];
            //srvHandle[0] = m_itmExtFastVisuList.getSrvHandle(ITEM_ACKBIT);
            object[] itmValue = new object[1];
            itmValue[0] = true;


            // Signal the plc that everything worked fine -> the next telegram can be sent.
            int[] errors;

            try
            {
                Trace("Writing acknowledge bit synchronous.");
                m_mgtGrpFast.writeSync(srvHandle, itmValue, out errors);
                Trace("Writing synchronous successfull.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nStop simulation and clear send bit!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DataChangeThreadEnd(IAsyncResult ar)
        {
            // the thread has now finished
            // check the result and "close" the thread

            object obj = ar.AsyncState;
            AsyncResult result = (AsyncResult)ar;

            //writingToDB wDB = (writingToDB)result.AsyncDelegate;

            try
            {
                //wDB.EndInvoke(ar);
                Trace("异步读取蔡司文件线程已结束");
                //Trace("Processing datachange on group :" + GRP_NAME_DBblock + " was successfull.");
            }
            catch (Exception ex)
            {
                Trace("异步线程回调函数异常:{Error writing to DB or OPC-error.}" + ex.Message);
                Trace(ex.Message);
                MessageBox.Show("Error writing to DB or OPC-error.\n\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void FastVisu_OnDataChange(object sender, OPCGroupManagement.OPCDataCallbackEventArgs e)
        {
            //Trace("Processing datachange on group :" + GRP_NAME_FVISU);
            //processDataChange(m_itmExtFastVisuList, e);
            //Trace("Processing datachange on group :" + GRP_NAME_FVISU + " was successfull.");
        }


        bool m_bOPCInitState = false;
        private void btn_OPCCon_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 先设置更新速率
                UpdtRtsMenuItem_Click(sender, null);

                if (btn_OPCCon.Text == "Connect/Disconnect" || btn_OPCCon.Text == "Disconnect")
                {
                    Trace("连接OPC服务器..");

                    if (m_ConRemoteOPC_Flag)
                    {
                        if (str_RemoteOPCServerIP == "")
                            return;
                        m_mgtServer = new OPCServerManagement();
                        m_mgtServer.connectOPCServer(PROG_ID, str_RemoteOPCServerIP);//带IP地址

                        label_OPCProcID.Text = PROG_ID;
                        btn_OPCCon.BackColor = System.Drawing.Color.Green;
                        btn_OPCCon.Text = "Connect";
                        Trace("OPC服务器已连接，ProcID:" + PROG_ID + "; IP:" + str_RemoteOPCServerIP);
                    }
                    else
                    {
                        // Connect to the server with "OPC.SimaticNet"
                        m_mgtServer = new OPCServerManagement();
                        m_mgtServer.connectOPCServer(PROG_ID);

                        label_OPCProcID.Text = PROG_ID;
                        btn_OPCCon.BackColor = System.Drawing.Color.Green;
                        btn_OPCCon.Text = "Connect";
                        Trace("OPC服务器已连接，ProcID:" + PROG_ID);

                        btn_ReadOPCItem.Enabled = true;
                        btn_WriteOPCItem.Enabled = true;
                    }
                    try
                    {
                        // now build the opcitemextender objects, -lists and add OPCGroups and OPCItems to the
                        // OPCServer object

                        initOPCVariables();

                        Thread.Sleep(200);// 等等那个ondatachanged操作，因为在初始化过后，它进入事件的事件不受程序控制，
                        //但是不会太久,等第一次进去过后，return,再走正常逻辑

                        StartSimButton.Enabled = true;
                        ClrSndBitMenuItem.Enabled = true;
                        UpdtRtsMenuItem.Enabled = false;

                        // 为了保证确实是在初始化opc组且添加item成功之后再进入ondatachanged事件
                        DisConnMenuItem.Enabled = true;
                        ConnMenuItem.Enabled = false;

                        m_bOPCInitState = true;
                        // OPCITEMS添加完成

                        // 加载成功后先清一遍信号
                        ClearOPCSignal_OnlyPC2PLC(true);


                        // 调用一次opc查询操作--判断开机码是否完成
                        if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null)
                        {
                            int transactionID_Basic_OK = 0;
                            m_mgtGrpDBblock.readAsync(m_mgtGrpDBblock.Get_m_itmServerHandles,
                                                        ref transactionID_Basic_OK);
                        }
                        Thread.Sleep(200);// 等等进入OnReadComplete事件
                        while (!m_bReadComplete_Flag) ; // 阻断，直到读完数据为止
                        m_bReadComplete_Flag = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        // now clean up already build structures
                        try
                        {
                            disconnectFromOPCServer();
                            btn_OPCCon.Text = "Disconnect";
                            btn_OPCCon.BackColor = System.Drawing.Color.Red;
                            m_bOPCInitState = false;
                            DisConnMenuItem.Enabled = false;
                            ConnMenuItem.Enabled = false;
                        }
                        catch (Exception ex2)
                        {
                            MessageBox.Show(ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        finally
                        {
                            m_itmExtDBblockList.Clear();
                        }
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
                else if (btn_OPCCon.Text == "Connect")
                {
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        Trace("断开OPC服务器...");

                        if (m_mgtServer != null)
                        {
                            m_mgtServer.Dispose();
                            //Marshal.ReleaseComObject(m_mgtServer);
                            m_mgtServer = null;
                            if (m_mgtGrpDBblock != null)
                            {
                                m_mgtGrpDBblock.Dispose();
                                //Marshal.ReleaseComObject(m_mgtServer);
                                m_mgtGrpDBblock = null;
                            }
                        }
                        if (m_itmExtDBblockList != null)
                        { m_itmExtDBblockList = null;
                          //m_itmExtDBblockList.Clear();
                        }
                        
                        Trace("OPC服务器已断开.");

                        btn_OPCCon.Text = "Disconnect";
                        btn_OPCCon.BackColor = System.Drawing.Color.Red;

                        btn_ReadOPCItem.Enabled = false;
                        btn_WriteOPCItem.Enabled = false;

                        DisConnMenuItem.Enabled = false;
                        ConnMenuItem.Enabled = false;

                        m_bOPCInitState = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message+"\n关闭OPC服务过程出错", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void FormClosed_CallBack()
        {
            // 关闭OPC 销毁对象
            if (btn_OPCCon.Text == "Connect")
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    Trace("断开OPC服务器...");

                    if (m_mgtServer != null)
                    {
                        m_mgtServer.Dispose();
                        //Marshal.ReleaseComObject(m_mgtServer);
                        m_mgtServer = null;
                        if (m_mgtGrpDBblock != null)
                        {
                            m_mgtGrpDBblock.Dispose();
                            //Marshal.ReleaseComObject(m_mgtServer);
                            m_mgtGrpDBblock = null;
                        }
                    }
                    if (m_itmExtDBblockList != null)
                    {
                        m_itmExtDBblockList = null;
                        //m_itmExtDBblockList.Clear();
                    }

                    Trace("OPC服务器已断开.");

                    btn_OPCCon.Text = "Disconnect";
                    btn_OPCCon.BackColor = System.Drawing.Color.Red;

                    btn_ReadOPCItem.Enabled = false;
                    btn_WriteOPCItem.Enabled = false;

                    DisConnMenuItem.Enabled = false;
                    ConnMenuItem.Enabled = false;

                    m_bOPCInitState = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n关闭OPC服务过程出错", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void PersistentData_FormClosed(object sender, FormClosedEventArgs e)
        {

            FormClosed_CallBack();

            if (m_DBConnector != null)
            {
                m_DBConnector.Dispose();
                m_mgtGrpFast = null;
            }
                //销毁OPC对象
            if (m_mgtServer != null)
            {
                m_mgtServer.Dispose();
                m_mgtServer = null;
            }
            if (m_mgtGrpDBblock != null)
            {
                //Marshal.ReleaseComObject(m_mgtGrpDBblock);  //对象的类型必须是 __ComObject 或是从 __ComObject 派生 才可用ReleaseComObject
                m_mgtGrpDBblock = null;
            }

            Application.Exit();
            this.Dispose();
        }

        private void PersistentData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btn_ClearTraceList_Click(object sender, EventArgs e)
        {
            if (Trace_Locker_All != null)
            {
                TraceListBox_Main.Items.Clear();
            }
            else
            { 
            }
        }

        #region 连接远程OPC
        private string str_RemoteOPCServerIP = "";
        private void btn_RecordServerIP_Click(object sender, EventArgs e)
        {
            str_RemoteOPCServerIP = tbx_IP1.Text + "." +
                                    tbx_IP2.Text + "." +
                                    tbx_IP3.Text + "." +
                                    tbx_IP4.Text;
        }
        bool m_ConRemoteOPC_Flag = false;

        private int m_Count = 0;
        private void rbt_ConRemoteOPC_Click(object sender, EventArgs e)
        {
            ++m_Count;  // 先自加 再运行其他的
            if (m_Count == 2)
            {
                m_Count = 0;
                rbt_ConRemoteOPC.Checked = !rbt_ConRemoteOPC.Checked;
            }
            if (rbt_ConRemoteOPC.Checked)
            {
                m_ConRemoteOPC_Flag = true;
                btn_RecordServerIP.Enabled = true;
                this.Cursor = Cursors.WaitCursor;
                m_ConRemoteOPC_Flag = true;
                this.Cursor = Cursors.Default;

                tbx_IP1.Focus();  // 光标转移
            }
            else
            {
                m_ConRemoteOPC_Flag = false;
                btn_RecordServerIP.Enabled = false;
                // rbt_ConRemoteOPC.Checked = !rbt_ConRemoteOPC.Checked;
            }
        }
        #endregion

        #region 测试OPC读写

        private void btn_ReadOPCItem_Click(object sender, EventArgs e)
        {
            //int nCancelid = 0;
            int transactionID = 0;
            IntPtr pErrors = IntPtr.Zero;

            if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null) // 异步读写对象不为空 利用属性获取
            {
                try
                {
                    // 执行读函数  // 读的是我的序号cbx_TriggerSignal.SelectedIndex
                    m_mgtGrpDBblock.readAsync(m_mgtGrpDBblock.Get_m_itmServerHandles, ref transactionID); // 异步读
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OPC读数据失败：\n" + ex.Message);
                }
            }
        }

        private void btn_WriteOPCItem_Click(object sender, EventArgs e)
        {
            //int nCancelid = 0;
            int transactionID = 0;
            IntPtr pErrors = IntPtr.Zero;

            tbx_OPCItemTimeStamp.Text = DateTime.Now.ToString();


            object[] values = new object[3];
            values[0] = tbx_OPCItemValue.Text;
            values[1] = tbx_OPCItemQuality.Text;
            values[2] = tbx_OPCItemTimeStamp.Text;

            if (m_mgtGrpDBblock.Get_IOPCAsyncIO2_obj != null) // 异步读写对象不为空 利用属性获取
            {
                try
                {
                    // 执行读函数
                    m_mgtGrpDBblock.writeAsync(m_mgtGrpDBblock.Get_m_itmServerHandles,
                                                values,
                                                ref transactionID); // 异步读
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OPC写入数据失败：\n" + ex.Message);
                }
            }

        }





        #endregion

        #region 主界面按键操作
        object locker_Btn = new object();
        private void btn_login_Click(object sender, EventArgs e)
        {
            btn_AdvancedFunc.Enabled = false;
            Form_AdvancedFunc_Login.m_bAdminLogin = false;

            Form_AdvancedFunc_Login F_Ad_Login = new Form_AdvancedFunc_Login();
            F_Ad_Login.Show();

            while (!F_Ad_Login.IsDisposed)
            {
                Application.DoEvents();
                this.Enabled = false;  // 主界面使能禁止 不可使用

            }
            this.Enabled = true;

            if (Form_AdvancedFunc_Login.m_bAdminLogin)
            {
                lock (locker_Btn)
                {
                    btn_AdvancedFunc.Enabled = true;
                    Form_AdvancedFunc_Login.m_bAdminLogin = false;

                    Trace("登录成功");

                    this.tabPage4.Parent = this.tabControl2;
                    tbx_username.Text = DBOperate_Obj.Query_LogIn_Dat(false)[0];
                    tbx_password.Text = DBOperate_Obj.Query_LogIn_Dat(false)[1];
                    // 调试权限
                    tbx_DebugUsername.Text = DBOperate_Obj.Query_LogIn_Dat(true)[0];
                    tbx_DebugPassword.Text = DBOperate_Obj.Query_LogIn_Dat(true)[1];
                }
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            btn_AdvancedFunc.Enabled = false;
            this.tabPage4.Parent = null; 
        }

        private void btn_AdvancedFunc_Click(object sender, EventArgs e)
        {
            Form_AdvancedFunc F_AF = new Form_AdvancedFunc();
            F_AF.Show();

            while (!F_AF.IsDisposed)
            {
                Application.DoEvents();
                //this.Enabled = false;
            }
            this.Enabled = true;

            btn_logout_Click(sender, e);

        }
        private void btn_InquireDataInterface_Click(object sender, EventArgs e)
        {
            Form_DataInterface F_dif = new Form_DataInterface();
            F_dif.Show();

            while (!F_dif.IsDisposed)
            {
                Application.DoEvents();
                //this.Enabled = false; // 会否影响其他操作
            }
            this.Enabled = true;
        }
        private void btn_DataInterface_Click(object sender, EventArgs e)
        {
            Form_RealTimeDataInterface F_FTDI = new Form_RealTimeDataInterface();
            F_FTDI.Show();

            while (!F_FTDI.IsDisposed)
            {
                Application.DoEvents();
                //this.Enabled = false;
            }
            this.Enabled = true;
        }
        #endregion

        // 获取当前程序目录
        string logPath = Path.GetDirectoryName(Application.ExecutablePath);
        string txtlogPath = ""; string siglogPath = "";
        System.IO.StreamWriter sw = null;
        FileStream fs = null;
        private void DisposeStreamWriter()
        {
            if (sw != null)
            {
                sw.Close();
                sw.Dispose();
            }
            if (fs != null)
            {
                fs.Close();
                fs.Dispose();
            }
        }
        #region 写日志
        private void TraceWrite2Log(string msg)
        {
            try // 在写log文件的时候进行判断，如果过了一天，发现没有当前的log，再生成也不迟
            {
                siglogPath = logPath + "\\日志文件" + "\\日志_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
                
                if (File.Exists(siglogPath)) // 有当日文件
                {
                    using (Stream fs = new FileStream(logPath + "\\日志文件" + "\\日志_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                        { sw.WriteLine(msg.ToString());}
                    }
                }
                else  // 无当日文件  + 
                {
                    using (Stream fs = new FileStream(logPath + "\\日志文件" + "\\日志_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                        { sw.WriteLine(msg.ToString()); }
                    }
                }
                
            }
            catch (Exception ex)
            { throw ex; }
        }

        #region P_CreateNewlogFile() //为什么用公用的sw fs在Trace中调用不行
        private void P_CreateNewlogFile()
        {
            try
            {
                txtlogPath = logPath + "\\日志文件";
                siglogPath = logPath + "\\日志文件" +"\\日志_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
                if (sw == null)
                {
                    if (File.Exists(siglogPath)) // 有当日文件
                    {
                        //fs = new FileStream(txtlogPath + "\\日志_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt", FileMode.Append, FileAccess.Write);
                        //sw = new StreamWriter(fs);//sw = System.IO.File.AppendText(txtlogPath + "\\日志_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");//new StreamWriter(fs);
                        //Thread.Sleep(100);
                        //sw.WriteLine("wtf");
                        //Thread.Sleep(100);
                    }
                    else  // 无当日文件  + 
                    {
                        //fs = new FileStream(txtlogPath + "\\日志_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt", FileMode.Create, FileAccess.Write);
                        //sw = new StreamWriter(fs);
                        //Thread.Sleep(100);
                        //sw.WriteLine("wtf");
                        //Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #endregion

        bool m_bCurrentExceptionHappened = false;
        private void timer_SendOK_Tick(object sender, EventArgs e)
        {
            try
            {
                txtlogPath = logPath + "//日志文件";
                if (m_bOPCInitState && !m_bCurrentExceptionHappened)
                {
                    // 不初始化opc，则无法正常读写，未实例化
                    //每1000ms进来一次，给PLC发送上位机工作ok信号
                   WriteAsyncToOPCOtherBoolVar(18, true);  // X2.1 上位机工作OK信号 从0开始序号17 第18个item
                   label_idx18_X21.BackColor = System.Drawing.Color.Green;

                }

                // 开启异步线程，检查当前时间
                if ((DateTime.Now.ToString("HH:mm") == "06:00") || (DateTime.Now.ToString("HH:mm") == "03:00"))
                {
                    // 新建日志文件
                    //fs = new FileStream(txtlogPath + "//日志_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                    // = System.IO.File.AppendText(txtlogPath + "//日志_" + DateTime.Now.ToString("yyyy-MM-dd") );
                    //sw = new StreamWriter(fs);
                    //m_StatusStep = 0; // 等待传输基础码
                    // 自动查询前一天的数据，赋值到dgvDatFromZessiFile中
                    // 开启异步线程,生成报表

                   int filenum = zessi_FileParse_Obj.CheckZessiFileNumAdProcessOver_ForSignalClear();
                   Trace("[处理凌晨生成文件] 处理过后[B515_MCA]文件夹中文件数量为:" + filenum.ToString() + ".");
                }
            }
            catch (Exception ex)
            {
                Trace("TimerSendOK事件异常：\n" + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        #region 清除信号
        static public bool m_bClearSignalRoot = false;
        private void btn_ClearSignal_Click(object sender, EventArgs e)
        {
            m_bClearSignalRoot = true;// 仅在执行的这一刻有效 进入置位

            // 要先弹出来密码界面
            Form_AdvancedFunc_Login.m_bAdminLogin = false;

            Form_AdvancedFunc_Login F_Ad_Login = new Form_AdvancedFunc_Login();
            F_Ad_Login.Show();

            while (!F_Ad_Login.IsDisposed)
            {
                Application.DoEvents();
                this.Enabled = false;  // 主界面使能禁止 不可使用
            }
            this.Enabled = true;

            if (Form_AdvancedFunc_Login.m_bAdminLogin)
            {
                lock (locker_Btn)
                {
                    Form_AdvancedFunc_Login.m_bAdminLogin = false;

                    Trace("获取清信号权限成功");

                    ClearOPCSignal_OnlyPC2PLC(true);// ClearOPCSignal(true, false);
                    m_StatusStep = 1;               // 180306 使状态复位 回到状态1

                    // 文件收尾处理
                    int file_num = zessi_FileParse_Obj.CheckZessiFileNumAdProcessOver_ForSignalClear(); // 状态机中的文件收尾工作
                    Trace("文件收尾处理工作完成(移送备份文件夹).此时共享文件夹存在xml文件数量为:" + file_num.ToString());
                }
            }
            m_bClearSignalRoot = false; // 仅在执行的这一刻有效 退出复位
        }

        // 异常手动复位
        private void btn_Onlyclearsig_Click(object sender, EventArgs e)
        {
            m_bClearSignalRoot = true;// 仅在执行的这一刻有效 进入置位

            // 要先弹出来密码界面
            Form_AdvancedFunc_Login.m_bAdminLogin = false;

            Form_AdvancedFunc_Login F_Ad_Login = new Form_AdvancedFunc_Login();
            F_Ad_Login.Show();

            while (!F_Ad_Login.IsDisposed)
            {
                Application.DoEvents();
                this.Enabled = false;  // 主界面使能禁止 不可使用
            }
            this.Enabled = true;

            if (Form_AdvancedFunc_Login.m_bAdminLogin)
            {
                lock (locker_Btn)
                {
                    Form_AdvancedFunc_Login.m_bAdminLogin = false;

                    Trace("获取手动复位权限成功");

                    m_bCurrentExceptionHappened = false;

                    // 文件收尾处理
                    int file_num = zessi_FileParse_Obj.CheckZessiFileNumAdProcessOver_ForSignalClear(); // 状态机中的文件收尾工作
                    Trace("文件收尾处理工作完成(移送备份文件夹).此时共享文件夹存在xml文件数量为:" + file_num.ToString());
                }
            }
            m_bClearSignalRoot = false; // 仅在执行的这一刻有效 退出复位
            
        }
        #endregion

        private void grp_PTA3D_MouseHover(object sender, EventArgs e)
        {
        }


        //界面数据更新
        List<TextBox> DatTextBox_List = null;
        List<TextBox> DatTextBox_List_sub4 = null;
        List<GroupBox> GroupBox_List = null;
        List<GroupBox> GroupBox_List_sub4 = null;
        private void tbxDatDisp(ZessiDatFromZessiFile zessi_DatFromZessiFile_Struct_local, double[] CurrentStandardTolerance)
        {
            #region tbx_list-传参用
            DatTextBox_List = new List<TextBox> { 
                tbx_PTGLH_1_X, tbx_PTGLH_1_Y, tbx_PTGLH_1_Z,
                tbx_PTGRH_2_X, tbx_PTGRH_2_Y, tbx_PTGRH_2_Z,
                tbx_PTC_3_X,tbx_PTC_3_Y,tbx_PTC_3_Z,
                tbx_PTB_4_X,tbx_PTB_4_Y,tbx_PTB_4_Z,
                
                tbx_PTA22_5_X,tbx_PTA22_5_Y,tbx_PTA22_5_Z,
                tbx_PTA12_6_X,tbx_PTA12_6_Y,tbx_PTA12_6_Z,
                tbx_PTHRHG_7_X,tbx_PTHRHG_7_Y,tbx_PTHRHG_7_Z,
                tbx_PTHLHG_8_X,tbx_PTHLHG_8_Y,tbx_PTHLHG_8_Z,
                tbx_PTILH_9_X,tbx_PTILH_9_Y,tbx_PTILH_9_Z,
                tbx_PTIRH_10_X,tbx_PTIRH_10_Y,tbx_PTIRH_10_Z,
                tbx_PTFRHF_11_X,tbx_PTFRHF_11_Y,tbx_PTFRHF_11_Z,
                tbx_PTFLHF_12_X,tbx_PTFLHF_12_Y,tbx_PTFLHF_12_Z,
                tbx_PTJLH_13_X,tbx_PTJLH_13_Y,tbx_PTJLH_13_Z,
            
                tbx_PTA21_14_X,tbx_PTA21_14_Y,tbx_PTA21_14_Z,//39 40 41  //15 16 17
                tbx_PTA11_15_X,tbx_PTA11_15_Y,tbx_PTA11_15_Z,//42 43 44  //12 13 14 
                tbx_PTA3D_16_X,tbx_PTA3D_16_Y,tbx_PTA3D_16_Z,//45 46 47  18 19 20
                
                                 
                tbx_PTJRH_17_X,tbx_PTJRH_17_Y,tbx_PTJRH_17_Z,
                tbx_PTA4D_18_X,tbx_PTA4D_18_Y,tbx_PTA4D_18_Z,//51 52 53  24 25 26
                tbx_PTDLH_19_X,tbx_PTDLH_19_Y,tbx_PTDLH_19_Z,
                tbx_PTDRH_20_X,tbx_PTDRH_20_Y,tbx_PTDRH_20_Z,
                tbx_PTELH_21_X,tbx_PTELH_21_Y,tbx_PTELH_21_Z,
                tbx_PTNLH_22_X,tbx_PTNLH_22_Y,tbx_PTNLH_22_Z,
                tbx_PTPLH_23_X,tbx_PTPLH_23_Y,tbx_PTPLH_23_Z,
                tbx_PTPRH_24_X,tbx_PTPRH_24_Y,tbx_PTPRH_24_Z,
                tbx_PTERH_25_X,tbx_PTERH_25_Y,tbx_PTERH_25_Z
                };
            DatTextBox_List_sub4 = new List<TextBox> {
                                                    tbx_PTA11_X, tbx_PTA11_Y, tbx_PTA11_Z,
                                                    tbx_PTA21_X, tbx_PTA21_Y, tbx_PTA21_Z,
                                                    tbx_PTA3D_X, tbx_PTA3D_Y, tbx_PTA3D_Z,
                                                    tbx_PTA4D_X, tbx_PTA4D_Y, tbx_PTA4D_Z
            };

            GroupBox_List = new List<GroupBox>
            {
                grp_PTGLH_1,grp_PTGRH_2,grp_PTC_3,grp_PTB_4,
                grp_PTA22_5,grp_PTA12_6,grp_PTHRHG_7,grp_PTHLHG_8,
                grp_PTILH_9,grp_PTIRH_10,grp_PTFRHF_11,grp_PTFLHF_12,
                grp_PTJLH_13, grp_PTA21_14,grp_PTA11_15,grp_PTA3D_16,
                grp_PTJRH_17,grp_PTA4D_18,grp_PTDLH_19,grp_PTDRH_20,
                grp_PTELH_21,grp_PTNLH_22,grp_PTPLH_23,grp_PTPRH_24,
                grp_PTERH_25
            };
            GroupBox_List_sub4 = new List<GroupBox>
            {
                grp_PTA11,grp_PTA21,grp_PTA3D,grp_PTA4D 
            };

            #endregion
            
            commonUse_Obj.TBX_Dat_Disp(DatTextBox_List, DatTextBox_List_sub4, 
                                        GroupBox_List, GroupBox_List_sub4,
                                CurrentStandardTolerance,zessi_DatFromZessiFile_Struct_local);
            // 比较并标红
        }
        
        // groupbox界面tooltip进行提示
        private void grp_ToleranceDisp_MouseHover(object sender, EventArgs e)
        {
            commonUse_Obj.GroupBox_ToolTip_Disp((GroupBox)sender, toolTip1, null);
        }

        private void PersistentData_Load(object sender, EventArgs e)
        {

        }

        private void btn_ConfirmChangeUsrNamePwd_Click_1(object sender, EventArgs e)
        {
            DBOperate_Obj.Update_LogIn_Dat(tbx_username.Text.Trim().ToString(), tbx_password.Text.Trim().ToString(), 1);
            Trace("更新用户名-密码成功.");
            MessageBox.Show("更新用户名-密码成功.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBOperate_Obj.Update_LogIn_Dat(tbx_DebugUsername.Text.Trim().ToString(), tbx_DebugPassword.Text.Trim().ToString(), 2);
            Trace("更新调试用户名-密码权限成功.");
            MessageBox.Show("更新调试用户名-密码权限成功.");
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btn_AdvancedFunc.Enabled == true)
            {
                if (tabControl2.SelectedIndex != 5)
                {
                    btn_logout_Click(sender, e);
                }
            }
            else
            { }
        }



    }

    



}

        