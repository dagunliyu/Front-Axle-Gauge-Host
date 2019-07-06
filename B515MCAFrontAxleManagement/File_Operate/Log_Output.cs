using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
    class Log_Output
    {


        //private static string[] ITEM_DBblock = { 
        ////PLC->PC 0.0-1.7
        //"S7:[S7_Connection_1]DB1001,X0.0", //0  Value:1    PLC->PC 原始码OK信号
        //"S7:[S7_Connection_1]DB1001,X0.1", //1  Value:2    PLC->PC 开班检测系统确认OK信号
        //"S7:[S7_Connection_1]DB1001,X0.2", //2  Value:3    PLC->PC MCC检测完成信号
        //"S7:[S7_Connection_1]DB1001,X0.3", //3  Value:4    PLC->PC 数据读取请求信号
        //"S7:[S7_Connection_1]DB1001,X0.4", //4  Value:5    PLC->PC
        //"S7:[S7_Connection_1]DB1001,X0.5", //5  Value:6    PLC->PC 
        //"S7:[S7_Connection_1]DB1001,X0.6", //6  Value:7    PLC->PC pick-up存取数据ok
        //"S7:[S7_Connection_1]DB1001,X0.7", //7  Value:8    PLC->PC 
        //"S7:[S7_Connection_1]DB1001,X1.0", //8  Value:9    PLC->PC PLC第1次回传码值OK信号
        //"S7:[S7_Connection_1]DB1001,X1.1", //9  Value:10   PLC->PC PLC第2次回传码值OK信号
        //"S7:[S7_Connection_1]DB1001,X1.2", //10 Value:11   PLC->PC
        //"S7:[S7_Connection_1]DB1001,X1.3", //11 Value:12   PLC->PC
        //"S7:[S7_Connection_1]DB1001,X1.4", //12 Value:13   PLC->PC 状态改变信号:由0-1或由1-0都去读INT4和INT5,把对应label变绿
        //"S7:[S7_Connection_1]DB1001,X1.5", //13 Value:14   PLC->PC 状态故障信号:由0-1读INT4-5,把对应label变红;由1-0读INT4-5,把对应label变绿
        //"S7:[S7_Connection_1]DB1001,X1.6", //14 Value:15   PLC->PC
        //"S7:[S7_Connection_1]DB1001,X1.7", //15 Value:16   PLC->PC
        ////PC->PLC 2.0-3.7                     
        //"S7:[S7_Connection_1]DB1001,X2.0", //16 Value:17   PC->PLC 上位机准备OK信号-开机完成且OPC_items插入完毕后
        //"S7:[S7_Connection_1]DB1001,X2.1", //17 Value:18   PC->PLC 上位机准备OK信号-每隔500ms向PLC写一个true
        //"S7:[S7_Connection_1]DB1001,X2.2", //18 Value:19   PC->PLC 上位机工作Busy信号，每次进入On_DataChanged()后，对opc变化的量进行处理时,为true,执行结束后变为false
        //"S7:[S7_Connection_1]DB1001,X2.3", //19 Value:20   PC->PLC 数据读取开始信号 PC收到读信号
        //"S7:[S7_Connection_1]DB1001,X2.4", //20 Value:21   PC->PLC 公差比较OK信号(和NG信号分开)
        //"S7:[S7_Connection_1]DB1001,X2.5", //21 Value:22   PC->PLC 读取报告完成信号
        //"S7:[S7_Connection_1]DB1001,X2.6", //22 Value:23   PC->PLC 打标内容生成OK信号 公差比较OK，生成打标码且已经保存到B50.40中
        //"S7:[S7_Connection_1]DB1001,X2.7", //23 Value:24   PC->PLC 放弃打标入库成功信号  公差比较NG，且手动输入追溯码值存数据库
        //"S7:[S7_Connection_1]DB1001,X3.0", //24 Value:25   PC->PLC 打标前打标内容核实OK信号
        //"S7:[S7_Connection_1]DB1001,X3.1", //25 Value:26   PC->PLC 打标前打标内容核实NG信号
        //"S7:[S7_Connection_1]DB1001,X3.2", //26 Value:27   PC->PLC 终止打标信号
        //"S7:[S7_Connection_1]DB1001,X3.3", //27 Value:28   PC->PLC 
        //"S7:[S7_Connection_1]DB1001,X3.4", //28 Value:29   PC->PLC 
        //"S7:[S7_Connection_1]DB1001,X3.5", //29 Value:30   PC->PLC
        //"S7:[S7_Connection_1]DB1001,X3.6", //30 Value:31   PC->PLC
        //"S7:[S7_Connection_1]DB1001,X3.7", //31 Value:32   PC->PLC
        //"S7:[S7_Connection_1]DB1001,INT4", //32 Value:33   PLC->PC 2Byte-保存数字,数字即为当前需要操作的状态label
        //// 5-44 (length-80)->(length-41)    原始码值保存地址(以Byte保存)(用24，预留16-每个码值一共40Byte) PC->PLC
        ////序号：length-80->length-41 Value:length-80+1->length-41+1
        //"S7:[S7_Connection_1]DB1001,B50",/*5*/"S7:[S7_Connection_1]DB1001,B51","S7:[S7_Connection_1]DB1001,B52","S7:[S7_Connection_1]DB1001,B53","S7:[S7_Connection_1]DB1001,B54","S7:[S7_Connection_1]DB1001,B55","S7:[S7_Connection_1]DB1001,B56","S7:[S7_Connection_1]DB1001,B57","S7:[S7_Connection_1]DB1001,B58","S7:[S7_Connection_1]DB1001,B59",
        //"S7:[S7_Connection_1]DB1001,B60",/*15*/"S7:[S7_Connection_1]DB1001,B61","S7:[S7_Connection_1]DB1001,B62","S7:[S7_Connection_1]DB1001,B63","S7:[S7_Connection_1]DB1001,B64","S7:[S7_Connection_1]DB1001,B65","S7:[S7_Connection_1]DB1001,B66","S7:[S7_Connection_1]DB1001,B67","S7:[S7_Connection_1]DB1001,B68","S7:[S7_Connection_1]DB1001,B69",
        //"S7:[S7_Connection_1]DB1001,B70",/*25*/"S7:[S7_Connection_1]DB1001,B71","S7:[S7_Connection_1]DB1001,B72","S7:[S7_Connection_1]DB1001,B73","S7:[S7_Connection_1]DB1001,B74","S7:[S7_Connection_1]DB1001,B75","S7:[S7_Connection_1]DB1001,B76","S7:[S7_Connection_1]DB1001,B77","S7:[S7_Connection_1]DB1001,B78","S7:[S7_Connection_1]DB1001,B79",
        //"S7:[S7_Connection_1]DB1001,B80",/*35*/"S7:[S7_Connection_1]DB1001,B81","S7:[S7_Connection_1]DB1001,B82","S7:[S7_Connection_1]DB1001,B83","S7:[S7_Connection_1]DB1001,B84","S7:[S7_Connection_1]DB1001,B85","S7:[S7_Connection_1]DB1001,B86","S7:[S7_Connection_1]DB1001,B87","S7:[S7_Connection_1]DB1001,B88","S7:[S7_Connection_1]DB1001,B89",
        ////45-84 (length-40)->(length-1)
        ////序号：length-40->length-1 Value:length-40+1->length-1+1
        //"S7:[S7_Connection_1]DB1001,B8",/*45*/"S7:[S7_Connection_1]DB1001,B9","S7:[S7_Connection_1]DB1001,B10","S7:[S7_Connection_1]DB1001,B11","S7:[S7_Connection_1]DB1001,B12","S7:[S7_Connection_1]DB1001,B13","S7:[S7_Connection_1]DB1001,B14","S7:[S7_Connection_1]DB1001,B15","S7:[S7_Connection_1]DB1001,B16","S7:[S7_Connection_1]DB1001,B17",
        //"S7:[S7_Connection_1]DB1001,B18",/*55*/"S7:[S7_Connection_1]DB1001,B19","S7:[S7_Connection_1]DB1001,B20","S7:[S7_Connection_1]DB1001,B21","S7:[S7_Connection_1]DB1001,B22","S7:[S7_Connection_1]DB1001,B23","S7:[S7_Connection_1]DB1001,B24","S7:[S7_Connection_1]DB1001,B25","S7:[S7_Connection_1]DB1001,B26","S7:[S7_Connection_1]DB1001,B27",
        //"S7:[S7_Connection_1]DB1001,B28",/*65*/"S7:[S7_Connection_1]DB1001,B29","S7:[S7_Connection_1]DB1001,B30","S7:[S7_Connection_1]DB1001,B31","S7:[S7_Connection_1]DB1001,B32","S7:[S7_Connection_1]DB1001,B33","S7:[S7_Connection_1]DB1001,B34","S7:[S7_Connection_1]DB1001,B35","S7:[S7_Connection_1]DB1001,B36","S7:[S7_Connection_1]DB1001,B37",
        //"S7:[S7_Connection_1]DB1001,B38",/*75*/"S7:[S7_Connection_1]DB1001,B39","S7:[S7_Connection_1]DB1001,B40","S7:[S7_Connection_1]DB1001,B41","S7:[S7_Connection_1]DB1001,B42","S7:[S7_Connection_1]DB1001,B43","S7:[S7_Connection_1]DB1001,B44","S7:[S7_Connection_1]DB1001,B45","S7:[S7_Connection_1]DB1001,B46","S7:[S7_Connection_1]DB1001,B47"
        //};// PLC发送的码值保存地址(以Byte发送) PLC->PC 用数据类型或的方式
        //// uint8[]的类型
        //private string[] ITEM_DBblock_Trigger = { 
        ////PLC->PC 0.0-1.7
        //"S7:[S7_Connection_1]DB1001,X0.0", //0  Value:1    PLC->PC 原始码OK信号
        //"S7:[S7_Connection_1]DB1001,X0.1", //1  Value:2    PLC->PC 开班检测系统确认OK信号
        //"S7:[S7_Connection_1]DB1001,X0.2", //2  Value:3    PLC->PC MCC检测完成信号
        //"S7:[S7_Connection_1]DB1001,X0.3", //3  Value:4    PLC->PC 数据读取请求信号
        //"S7:[S7_Connection_1]DB1001,X0.4", //4  Value:5    PLC->PC
        //"S7:[S7_Connection_1]DB1001,X0.5", //5  Value:6    PLC->PC 
        //"S7:[S7_Connection_1]DB1001,X0.6", //6  Value:7    PLC->PC 
        //"S7:[S7_Connection_1]DB1001,X0.7", //7  Value:8    PLC->PC 
        //"S7:[S7_Connection_1]DB1001,X1.0", //8  Value:9    PLC->PC PLC第1次回传码值OK信号
        //"S7:[S7_Connection_1]DB1001,X1.1", //9  Value:10   PLC->PC PLC第2次回传码值OK信号
        //"S7:[S7_Connection_1]DB1001,X1.2", //10 Value:11   PLC->PC
        //"S7:[S7_Connection_1]DB1001,X1.3", //11 Value:12   PLC->PC
        //"S7:[S7_Connection_1]DB1001,X1.4", //12 Value:13   PLC->PC 状态改变信号:由0-1或由1-0都去读B4
        //"S7:[S7_Connection_1]DB1001,X1.5", //13 Value:14   PLC->PC 状态故障信号
        //"S7:[S7_Connection_1]DB1001,X1.6", //14 Value:15   PLC->PC
        //"S7:[S7_Connection_1]DB1001,X1.7", //15 Value:16   PLC->PC
        ////PC->PLC 2.0-3.7                     
        //"S7:[S7_Connection_1]DB1001,X2.0", //16 Value:17   PC->PLC 上位机准备OK信号-开机完成且OPC_items插入完毕后
        //"S7:[S7_Connection_1]DB1001,X2.1", //17 Value:18   PC->PLC 上位机准备OK信号-每隔500ms向PLC写一个true
        //"S7:[S7_Connection_1]DB1001,X2.2", //18 Value:19   PC->PLC 上位机工作Busy信号，每次进入On_DataChanged()后，对opc变化的量进行处理时,为true,执行结束后变为false
        //"S7:[S7_Connection_1]DB1001,X2.3", //19 Value:20   PC->PLC 数据读取开始信号 PC收到读信号
        //"S7:[S7_Connection_1]DB1001,X2.4", //20 Value:21   PC->PLC 公差比较OK信号(和NG信号分开)
        //"S7:[S7_Connection_1]DB1001,X2.5", //21 Value:22   PC->PLC 读取报告完成信号
        //"S7:[S7_Connection_1]DB1001,X2.6", //22 Value:23   PC->PLC 打标内容生成OK信号 公差比较OK，生成打标码且已经保存到B50.40中
        //"S7:[S7_Connection_1]DB1001,X2.7", //23 Value:24   PC->PLC 放弃打标入库成功信号  公差比较NG，且手动输入追溯码值存数据库
        //"S7:[S7_Connection_1]DB1001,X3.0", //24 Value:25   PC->PLC 打标前打标内容核实OK信号
        //"S7:[S7_Connection_1]DB1001,X3.1", //25 Value:26   PC->PLC 打标前打标内容核实NG信号
        //"S7:[S7_Connection_1]DB1001,X3.2", //26 Value:27   PC->PLC 终止打标信号
        //"S7:[S7_Connection_1]DB1001,X3.3", //27 Value:28   PC->PLC 
        //"S7:[S7_Connection_1]DB1001,X3.4", //28 Value:29   PC->PLC 
        //"S7:[S7_Connection_1]DB1001,X3.5", //29 Value:30   PC->PLC
        //"S7:[S7_Connection_1]DB1001,X3.6", //30 Value:31   PC->PLC
        //"S7:[S7_Connection_1]DB1001,X3.7", //31 Value:32   PC->PLC
        //"S7:[S7_Connection_1]DB1001,INT4", //32 Value:33   PLC->PC
        //"S7:[S7_Connection_1]DB1001,INT5" //33 Value:34   PLC->PC
        //                                        };
        ////"S7:[S7_Connection_1]DB1001,B50.40",
        ////"S7:[S7_Connection_1]DB1001,B8.40"                                        
        //private string[] ITEM_DBblock_Mark = { 
        //                                  "S7:[S7_Connection_1]DB1001,B50","S7:[S7_Connection_1]DB1001,B51","S7:[S7_Connection_1]DB1001,B52","S7:[S7_Connection_1]DB1001,B53","S7:[S7_Connection_1]DB1001,B54","S7:[S7_Connection_1]DB1001,B55","S7:[S7_Connection_1]DB1001,B56","S7:[S7_Connection_1]DB1001,B57","S7:[S7_Connection_1]DB1001,B58","S7:[S7_Connection_1]DB1001,B59",
        //                                  "S7:[S7_Connection_1]DB1001,B60","S7:[S7_Connection_1]DB1001,B61","S7:[S7_Connection_1]DB1001,B62","S7:[S7_Connection_1]DB1001,B63","S7:[S7_Connection_1]DB1001,B64","S7:[S7_Connection_1]DB1001,B65","S7:[S7_Connection_1]DB1001,B66","S7:[S7_Connection_1]DB1001,B67","S7:[S7_Connection_1]DB1001,B68","S7:[S7_Connection_1]DB1001,B69",
        //                                  "S7:[S7_Connection_1]DB1001,B70","S7:[S7_Connection_1]DB1001,B71","S7:[S7_Connection_1]DB1001,B72","S7:[S7_Connection_1]DB1001,B73","S7:[S7_Connection_1]DB1001,B74","S7:[S7_Connection_1]DB1001,B75","S7:[S7_Connection_1]DB1001,B76","S7:[S7_Connection_1]DB1001,B77","S7:[S7_Connection_1]DB1001,B78","S7:[S7_Connection_1]DB1001,B79",
        //                                  "S7:[S7_Connection_1]DB1001,B80","S7:[S7_Connection_1]DB1001,B81","S7:[S7_Connection_1]DB1001,B82","S7:[S7_Connection_1]DB1001,B83","S7:[S7_Connection_1]DB1001,B84","S7:[S7_Connection_1]DB1001,B85","S7:[S7_Connection_1]DB1001,B86","S7:[S7_Connection_1]DB1001,B87","S7:[S7_Connection_1]DB1001,B88","S7:[S7_Connection_1]DB1001,B89",
        //                                  //
        //                                  "S7:[S7_Connection_1]DB1001,B8","S7:[S7_Connection_1]DB1001,B9","S7:[S7_Connection_1]DB1001,B10","S7:[S7_Connection_1]DB1001,B11","S7:[S7_Connection_1]DB1001,B12","S7:[S7_Connection_1]DB1001,B13","S7:[S7_Connection_1]DB1001,B14","S7:[S7_Connection_1]DB1001,B15","S7:[S7_Connection_1]DB1001,B16","S7:[S7_Connection_1]DB1001,B17",
        //                                  "S7:[S7_Connection_1]DB1001,B18","S7:[S7_Connection_1]DB1001,B19","S7:[S7_Connection_1]DB1001,B20","S7:[S7_Connection_1]DB1001,B21","S7:[S7_Connection_1]DB1001,B22","S7:[S7_Connection_1]DB1001,B23","S7:[S7_Connection_1]DB1001,B24","S7:[S7_Connection_1]DB1001,B25","S7:[S7_Connection_1]DB1001,B26","S7:[S7_Connection_1]DB1001,B27",
        //                                  "S7:[S7_Connection_1]DB1001,B28","S7:[S7_Connection_1]DB1001,B29","S7:[S7_Connection_1]DB1001,B30","S7:[S7_Connection_1]DB1001,B31","S7:[S7_Connection_1]DB1001,B32","S7:[S7_Connection_1]DB1001,B33","S7:[S7_Connection_1]DB1001,B34","S7:[S7_Connection_1]DB1001,B35","S7:[S7_Connection_1]DB1001,B36","S7:[S7_Connection_1]DB1001,B37",
        //                                  "S7:[S7_Connection_1]DB1001,B38","S7:[S7_Connection_1]DB1001,B39","S7:[S7_Connection_1]DB1001,B40","S7:[S7_Connection_1]DB1001,B41","S7:[S7_Connection_1]DB1001,B42","S7:[S7_Connection_1]DB1001,B43","S7:[S7_Connection_1]DB1001,B44","S7:[S7_Connection_1]DB1001,B45","S7:[S7_Connection_1]DB1001,B46","S7:[S7_Connection_1]DB1001,B47"   
        //                                     };

    }
}
