using System;
using System.Runtime.InteropServices;
using Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB;
using DataConversion;
using Siemens.Automation.ServiceSupport.Applications.OPC.IOPCConnector;
using Siemens.ATS14.ToolBox.Performance;
using System.Collections;

using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB
{

	/// <summary> this class takes the data from the datachange-event (byte-array),
	/// converts the data into C#-datatypes and stores it into a persistent database </summary>
	public class DBConnector: IOPCItemControl, IDisposable
	{
        

		/// <summary> this enum defines the type of database this connector provides </summary>
		public enum DBToSave{ DBNothing = 0, DBAccess, DBSQLServer, DBXML }

		#region fields
		#region database

		private Hashtable		m_dataBases = new Hashtable(4);
		private DBToSave		m_ActiveDB = DBToSave.DBNothing;
		#endregion
		
		#region disposable pattern
		private bool m_disposed				= false;
		#endregion
		#endregion

		#region construction
        public static bool m_bDataBaseOpen = false;
        // str_sqlcon
        string sqlCon_RC = "";
        public SqlConnection m_Sqlconn = null;
        public SqlCommand m_SqlCommand = null;
        public DBConnector(string dbName)  // 在构造函数中配置连接
        {
            try
            {
                // Data Source=USER-20171220HP;Initial Catalog=RC_DB;Integrated Security=True;MultipleActiveResultSets=True
                // USER-20180129LH
                string sqlCon = "Data Source=USER-20180129LH;Initial Catalog=" + dbName + ";Integrated Security=True;Connect Timeout=5;MultipleActiveResultSets=True";  // windows身份验证登录：USER-20171220HP；USER-20171220HP\Administrator
                m_Sqlconn = new SqlConnection(sqlCon);//m_Sqlconn.ConnectionString = sqlCon;
                m_SqlCommand = new SqlCommand();
                m_SqlCommand.Connection = m_Sqlconn;

                #region
                // 开启数据库--不建议的操作
                if (m_Sqlconn.State == ConnectionState.Closed)
                {
                    //m_Sqlconn.ConnectionTimeout = 5;// 只读不能修改 默认是15s
                    m_Sqlconn.Open();
                    
                    //if (m_Sqlconn.State == ConnectionState.Open)
                    //{
                    //    m_bDataBaseOpen = true;
                    //}
                }
                #endregion
            }
            catch (Exception) //ex
            {
                m_Sqlconn = null; m_bDataBaseOpen = false;
                //180711 throw( new Exception("数据库初始化错误,请检查数据库服务是否打开.\n"));
                //MessageBox.Show(ex.Message + "数据库初始化错误"); + ex.Message
            }
            finally
            {
                m_Sqlconn.Close();
            }
        }

        public SqlConnection CurrentSqlCon
        {
            get { return m_Sqlconn; }
        }
        public SqlCommand CurrentSqlCmd
        {
            get { return m_SqlCommand; }
        }

		#endregion

		#region destructor; disposable-pattern

		/// <summary> Use C# destructor syntax for finalization code.
		/// This destructor will run only if the Dispose method
		/// does not get called.
		/// It gives your base class the opportunity to finalize.
		/// Do not provide destructors in types derived from this class.   
		/// </summary>
		~DBConnector()
		{
            //if(m_Sqlconn != null)
            //{
            //    m_Sqlconn.Dispose();
            //}
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose(false) is optimal in terms of
			// readability and maintainability.
			Dispose(false);
		}

		#region IDisposable Member

		public void Dispose()
		{
			Dispose(true);
			// Take yourself off the Finalization queue 
			// to prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		#endregion

		/// <summary> Dispose(bool disposing) executes in two distinct scenarios.
		/// If disposing equals true, the method has been called directly
		/// or indirectly by a user's code.
		/// If disposing equals false, the method has been called by the 
		/// runtime from inside the finalizer and you should not reference 
		/// other objects. /// </summary>
		/// <param name="disposing"> determines whether it is called from the finalizer or
		/// from the user. </param>
		protected virtual void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if(!m_disposed)
			{
				// Don't dispose managed ressources in here; GC will take care
				try
				{
					for(int i=0; i<m_dataBases.Count;i++)
					{
						if(m_dataBases[i] != null)
							((IDisposable)(m_dataBases[i])).Dispose();
					}
					m_dataBases.Clear();
				}
				catch(Exception ex)
				{
					throw ex;
				}
				
				// Note that this is not thread safe.
				// Another thread could start disposing the object
				// after the managed resources are disposed,
				// but before the disposed flag is set to true.
				// If thread safety is necessary, it must be
				// implemented by the client.
			}
			m_disposed = true;
		}
		#endregion

    
        // 通过Transact-SQL语句提交数据
        public int ExecDataBySql(string strSql)
        {
            int intReturnValue;

            m_SqlCommand.CommandType = CommandType.Text;
            m_SqlCommand.CommandText = strSql;

            try
            {
                if (m_Sqlconn.State == ConnectionState.Closed)
                {
                    m_Sqlconn.Open();
                }

                intReturnValue = m_SqlCommand.ExecuteNonQuery(); // 返回被影响的行数
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                m_Sqlconn.Close();//连接关闭，但不释放掉该对象所占的内存单元
            }

            return intReturnValue;
        }


        /// <summary>
        /// 通过存储过程提交数据
        /// </summary>
        /// <param name="Proc_Name"></param>
        /// <param name="inputParameters"></param>
        /// <returns></returns>
        public int ExecDataByStoredProc(string Proc_Name, SqlParameter[] inputParameters) // 存储过程名字;存储过程参数
        {
            //SqlDataAdapter sda = null;
            int intReturnValue = -1;
            try
            {
                if (m_Sqlconn.State == ConnectionState.Closed)
                    m_Sqlconn.Open();

                m_SqlCommand.Connection = m_Sqlconn;
                m_SqlCommand.CommandType = CommandType.StoredProcedure; // sql cmd类型用存储过程
                m_SqlCommand.CommandText = Proc_Name; // sql cmd名称即为存储过程名字

                // 先清,再为m_SqlCommand添加参数
                m_SqlCommand.Parameters.Clear();
                //foreach (SqlParameter param in inputParameters)
                //{
                //    param.Direction = ParameterDirection.Input;
                //    m_SqlCommand.Parameters.Add(param);
                //}
                m_SqlCommand.Parameters.AddRange(inputParameters);

                intReturnValue= m_SqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            { throw ex; } // 向上一层抛出异常
            finally
            {
                m_Sqlconn.Close();
            }
            return intReturnValue;
        }



        // 通过存储过程得到datatable实例
        public DataTable GetDataTable(string Proc_Name, SqlParameter[] inputParameters) // 存储过程名字;存储过程参数
        {
            DataTable dt = new DataTable(); // 填充过后返回dt
            //SqlDataAdapter sda = null;
            try
            {
                m_SqlCommand.Connection = m_Sqlconn;
                m_SqlCommand.CommandType = CommandType.StoredProcedure; // sql cmd类型用存储过程
                m_SqlCommand.CommandText = Proc_Name; // sql cmd名称即为存储过程名字

                if (m_Sqlconn.State == ConnectionState.Closed)
                    m_Sqlconn.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(m_SqlCommand)) // using块结束后会清除sda
                {
                    // 先清,再为m_SqlCommand添加参数
                    m_SqlCommand.Parameters.Clear();
                    foreach (SqlParameter param in inputParameters)
                    {
                        param.Direction = ParameterDirection.Input;
                        m_SqlCommand.Parameters.Add(param);
                    }

                    sda.Fill(dt);
                }
            }
            catch (Exception ex)
            { throw ex; } // 向上一层抛出异常
            finally
            {
                m_Sqlconn.Close();
            }
            return dt;
        }

        public DataTable GetDataTable(string sqlCmd, string tableName)
        {
            DataTable dt = null;//new DataTable(); // 填充过后返回dt

            try
            {
                if (m_Sqlconn.State == ConnectionState.Closed)
                    m_Sqlconn.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(sqlCmd, m_Sqlconn))
                {
                    dt = new DataTable(tableName);
                    sda.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_Sqlconn.Close();
            }
            return dt;
        }

        












		#region Delegates and Events
		
		public delegate void BRCVOccuredEventHandler(object source, DataToVisualize e);
		/// <summary> event will be fired as soon as all data are converted and saved into a DB </summary>
		public event BRCVOccuredEventHandler OnBRCVOccured;

		/// <summary> class for eventhandler-parameter </summary>
		public class DataToVisualize : EventArgs
		{
			#region Fields
			private TelegramHead m_telHead;
			private MeasuredHead m_measuredHead;
			private MeasuredData m_measuredData;
			private double	m_convTimeSpan;
			private double	m_saveTimeSpan;
			#endregion

			#region Properties
			public TelegramHead telHead
			{
				get	{ return m_telHead; }
			}
			public MeasuredHead measuredHead
			{
				get	{ return m_measuredHead; }
			}
			public MeasuredData measuredData
			{
				get	{ return m_measuredData; }
			}
			public double timeSpanForConverting
			{
				get	{ return m_convTimeSpan; }
			}
			public double timeSpanForSaving
			{
				get	{ return m_saveTimeSpan; }
			}
			#endregion

			#region Construction
			internal DataToVisualize(TelegramHead telHead, MeasuredHead measuredHead, MeasuredData measuredData,
				double convTime, double saveTime)
			{
				m_telHead = telHead;
				m_measuredHead = measuredHead;
				m_measuredData = measuredData;
				m_convTimeSpan = convTime;
				m_saveTimeSpan = saveTime;
			}

			#endregion
			
		}
		#endregion

		#region properties

		public System.Data.DataSet AccessDataSet
		{
			get
			{
				if(!m_disposed)
				{
					int idx = (int)DBToSave.DBAccess;
					if(m_dataBases[idx] != null)
						return ((DBInterface)(m_dataBases[idx])).getDataSet();
					else return null;
				}
				else
					throw new Exception("Object has been disposed!");
			}
		}

		public System.Data.DataSet XMLDataSet
		{
			get
			{
				if(!m_disposed)
				{
					int idx = (int)DBToSave.DBXML;
					if(m_dataBases[idx] != null)
						return ((DBInterface)(m_dataBases[idx])).getDataSet();
					else return null;
				}
				else
					throw new Exception("Object has been disposed!");
			}
		}

		public System.Data.DataSet SQLDataSet
		{
			get
			{
				if(!m_disposed)
				{
					int idx = (int)DBToSave.DBSQLServer;
					if(m_dataBases[idx] != null)
						return ((DBInterface)(m_dataBases[idx])).getDataSet();
					else return null;
				}
				else
					throw new Exception("Object has been disposed!");
			}
		}

		/// <summary> with this property the database will be changed; furthermore the actual data will be read </summary>
		public DBToSave activeDB
		{
			set
			{
				if(value == DBToSave.DBNothing)
				{
					m_ActiveDB = value;
					return;
				}

				int idx = (int)value;
				DBInterface theDB = (DBInterface)(m_dataBases[idx]);
				if(theDB == null) throw new Exception("Database not found!");
				else
				{
					theDB.Fill();
					m_ActiveDB = value;
				}
			}
			get { return m_ActiveDB; }
		}
		#endregion

		#region private methods

		/// <summary> converts a part (telegram head) of the byte array into C# types for further usage </summary>
		/// <param name="theData"> the byte array </param>
		/// <param name="length"> the length of the telegram </param>
		/// <param name="offset"> offset needed to convert the data </param>
		/// <returns> object of type "TelegramHead" for further usage (displaying on the UI) </returns>
		private TelegramHead readOutTelegramHead(S7DataConverter theData, int length, ref int offset)
		{
			// read telegram-header
			UInt16 telID = 0;
			UInt16 telStatus = 0;
			UInt32 telNumber = 0;
			DateTime telSendTime = new DateTime(2004,12,3);
			Int16 telNumberRecords = 0;

			offset = theData.GetValue(offset,S7Types.WORD,ref telID);
			offset = theData.GetValue(offset,S7Types.WORD,ref telStatus);
			offset = theData.GetValue(offset,S7Types.DWORD,ref telNumber);
			offset = theData.GetValue(offset,S7Types.DATE_AND_TIME,ref telSendTime);
			offset = theData.GetValue(offset,S7Types.INT,ref telNumberRecords);

			return new TelegramHead(length,telID,telStatus,telNumber,telSendTime, telNumberRecords);
		}

		/// <summary>  converts a part (measured data) of the byte array into C# types for further usage  </summary>
		/// <param name="theData"> the byte array </param>
		/// <param name="telNumberRecords"> the number of records (delivered via the telegram head) </param>
		/// <param name="offset">  offset needed to convert the data  </param>
		/// <param name="measHeadVisu"> the head data for visualization </param>
		/// <param name="measDataVisu">  the measured data for visualization </param>
		/// <remarks> NOTE: this method also fills the data into the meant (transient) DB; the insertion of the data
		/// into the persistent storage has to be done in calling function! </remarks>
		private void readOutData(S7DataConverter theData,int telNumberRecords, ref int offset,
			out MeasuredHead measHeadVisu, out MeasuredData measDataVisu)
		{
			Int16 headDataH_ID = 0;
			DateTime headDataMeasureTime = new DateTime(2004,12,3);
			float headDataPressure = 0;
			float headDataEnergy = 0;
			string headDataDescription = "";
			
			Int16 dataMeasurepointIndex = 0;
			float dataTemperature = 0;
			float dataPressure = 0;
			float dataFlow = 0;
			float dataHumidity = 0;
			string dataDescription = "";

			MeasuredHead measHead = null;
			MeasuredData measData = null;
			measHeadVisu = null;
			measDataVisu = null;

			for(int i=0; i<telNumberRecords;i++)
			{
				// read data-header
				offset = theData.GetValue(offset,S7Types.INT,ref headDataH_ID);
				offset = theData.GetValue(offset,S7Types.DATE_AND_TIME,ref headDataMeasureTime);
				offset = theData.GetValue(offset,S7Types.REAL,ref headDataPressure);
				offset = theData.GetValue(offset,S7Types.REAL,ref headDataEnergy);
				offset = theData.GetValue(offset,S7Types.STRING,ref headDataDescription);
				// adjust offset manually! The string has 10 characters.
				offset += 10;

				System.Int32 h_idx = 0;
				measHead = new MeasuredHead(headDataH_ID,headDataMeasureTime,
					headDataPressure, headDataEnergy,
					headDataDescription);

				// Now add the row
				h_idx = addRowsHeater(measHead);
				
				// read measured data
				// there are 8 measurementpoints
				for(int j=0; j<8;j++)
				{
					offset = theData.GetValue(offset,S7Types.INT,ref dataMeasurepointIndex);
					offset = theData.GetValue(offset,S7Types.REAL,ref dataTemperature);
					offset = theData.GetValue(offset,S7Types.REAL,ref dataPressure);
					offset = theData.GetValue(offset,S7Types.REAL,ref dataFlow);
					offset = theData.GetValue(offset,S7Types.REAL,ref dataHumidity);
					offset = theData.GetValue(offset,S7Types.STRING,ref dataDescription);
					// adjust offset manually! The string has 10 characters.
					offset += 10;

					measData = new MeasuredData(dataMeasurepointIndex,
						dataTemperature,dataPressure,dataFlow,
						dataHumidity,dataDescription);

					// all data is collected -> add the row
					addRowsMeasure(h_idx, measData);
					
					if(i==0 && j==0)
					{
						measHeadVisu = measHead.Clone();
						measDataVisu = measData.Clone();
					}
				}
			}
		}

		/// <summary> adds a row to the transient heater table of the meant database </summary>
		/// <param name="measHead"> the data of the head </param>
		/// <returns> the index of the new row to use it for filling the foreign key </returns>
		private System.Int32 addRowsHeater(MeasuredHead measHead)
		{
			int idx = (int)m_ActiveDB;
			if(idx == 0)
				return -1;
			else
				return ((DBInterface)(m_dataBases[idx])).addRowToHeaterTable(measHead);
		}

		/// <summary> adds a row to the transient measure table of the meant database </summary>
		/// <param name="h_idx"> the foreign key to insert </param>
		/// <param name="measData"> the data </param>
		private void addRowsMeasure(System.Int32 h_idx, MeasuredData measData)
		{
			int idx = (int)m_ActiveDB;
			if(idx == 0)
				return;
			else
				((DBInterface)(m_dataBases[idx])).addRowToMeasureTable(h_idx,measData);
		}

		#endregion

		#region public methods

		/// <summary> connects to the xml file </summary>
		/// <param name="xmlDBFilePath"> the fully specified path of the xml file </param>
		/// <exception cref="Exception"> forwards any thrown exception </exception>
		public void connectXMLDB(string xmlDBFilePath)
		{
			int idx = (int)DBToSave.DBXML;
			try
			{
				//m_dataBases.Add(idx, new XMLDBMapper(xmlDBFilePath));
			}
			catch(Exception ex)
			{
				m_dataBases[idx] = null;
				throw ex;
			}
		}

		/// <summary> connects to the access db </summary>
		/// <param name="xmlDBFilePath"> the fully specified path of the access file </param>
		/// <exception cref="Exception"> forwards any thrown exception </exception>
		public void connectAccessDB(string accessDBFilePath)
		{
			int idx = (int)DBToSave.DBAccess;
			try
			{
				m_dataBases.Add(idx, new AccessDB(accessDBFilePath));
			}
			catch(Exception ex)
			{
				m_dataBases[idx] = null;
				throw ex;
			}
		}

		/// <summary> connects to the sql db </summary>
		/// <exception cref="Exception"> forwards any thrown exception </exception>
		public void connectSQLDB()
		{
			int idx = (int)DBToSave.DBSQLServer;
			try
			{
				m_dataBases.Add(idx, new SQLServerDB("HeatCircuit"));
			}
			catch(Exception ex)
			{
				m_dataBases[idx] = null;
				throw ex;
			}
		}


		/// <summary> deletes all connected datatables </summary>
		/// <exception cref="Exception"> forwards any thrown exception </exception>
		public void deleteAllDatatables()
		{
			if(!m_disposed)
			{
				try
				{
					if(m_dataBases[(int)DBToSave.DBAccess] != null)
						((DBInterface)(m_dataBases[(int)DBToSave.DBAccess])).deleteAllTables();
					if(m_dataBases[DBToSave.DBSQLServer] != null)
						((DBInterface)(m_dataBases[(int)DBToSave.DBSQLServer])).deleteAllTables();
					if(m_dataBases[(int)DBToSave.DBXML] != null)
						((DBInterface)(m_dataBases[(int)DBToSave.DBXML])).deleteAllTables();
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
			else
				throw new Exception("Object has been disposed!");
			
		}

		/// <summary> clears the tables of the active database </summary>
		/// <exception cref="Exception"> forwards any thrown exception </exception>
		public void deleteDatatable()
		{
			if(!m_disposed)
			{
				int idx = (int)m_ActiveDB;
				try
				{
					((DBInterface)(m_dataBases[idx])).deleteAllTables();
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
			else
				throw new Exception("Object has been disposed!");
		}
		#endregion

		#region IOPCItemControl Member

		/// <summary>  sets the value delivered from ondatachange </summary>
		/// <param name="theValue"> the value (must be byte array) </param>
		/// <exception cref="InvalidCastException"> if the value cannot be converted into a byte array </exception>
		/// <remarks> NOTES:
		/// - this methods measures the time for converting and saving to DB
		/// - it is recommended to call this method asynchronously, because saving to DB may take several seconds </remarks>
		public void setOPCParam(object theValue)
		{
			TelegramHead telHead = null;
			MeasuredHead measHeadVisu = null;
			MeasuredData measDataVisu = null;

			// Take time for converting
			StopWatch convStopWatch = new StopWatch(TimerType.PerformanceCounter);
			convStopWatch.Scale = 1000.0;
			convStopWatch.Start();

			// Take time for saving
			StopWatch saveStopWatch = new StopWatch(TimerType.PerformanceCounter);
			saveStopWatch.Scale = 1000.0;

			// Convert data
			S7DataConverter theData;
			byte[] data = null;
			try
			{
				data = (byte[])theValue;
				theData = new S7DataConverter(data,true,true);
			}
			catch(InvalidCastException ex)
			{
				// forward the exception
				throw ex;
			}

			int offset = 0;
			
			// read telegram-header
			telHead = readOutTelegramHead(theData,data.Length,ref offset);
			
			// read out data
			readOutData(theData, telHead.telNumberRecords, ref offset,out measHeadVisu, out measDataVisu);
			convStopWatch .Stop();
			
			// Now save update the database
			try
			{
				saveStopWatch.Start();
				int idx = (int)m_ActiveDB;
				if(idx != 0) ((DBInterface)(m_dataBases[idx])).Insert();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				saveStopWatch.Stop();
				
				// Fill the data for visualization
				DataToVisualize dataToVisu = new DataToVisualize(telHead,measHeadVisu,measDataVisu,
												convStopWatch.Time,saveStopWatch.Time);
				
				// fire the event, that saving is completed
				OnBRCVOccured(this,dataToVisu);
			}
		}

		#endregion
	}
	
}
