using System;
using OpcRcw.Da;
using OpcRcw.Comn;
using System.Resources;
using System.Runtime.InteropServices;
using System.Collections;

using System.Windows.Forms;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> This class provides connecting and disconnecting from the OPC-Server.
	/// It implements the IDisposble-Interface.</summary>
	public class OPCServerManagement : IDisposable
	{
		#region fields
		#region Required OPC Interfaces
		/// <summary> only the opc-server and the common-interface is needed in here;
		/// the rest of it is encapsulated in the class OPCManagement </summary>
		private IOPCServer					m_OPCServer			= null;
		private IOPCCommon					m_OPCCommon			= null;
		
        // 异步读对象
        private IOPCAsyncIO2 m_IOPCAsyncIO2 = null;
        // 管理OPC Group对象
        private IOPCGroupStateMgt m_IOPCGroupStateMgt = null;
        // 异步事件点
        IConnectionPointContainer pIConnectionPointContainer = null;
        IConnectionPoint pIConnectionPoint = null;
        // OPCServer语言码 - 英语
        internal const int LOCALE_ID = 0x407;// 只有当前工程能访问

        // OPCGroup对象-自测试
        Object OPCGroup_Test = null;
        // Item句柄数组
        int[] OPCItemServerHandle;
        // OPCGroup句柄
        int pSvrGroupHandle = 0; 

        // Client's sink
        Int32 dwCookie = 0;

        #endregion

        #region 获取属性
        public IOPCAsyncIO2 IOPCAsyncIO2_obj
        {
            get { return this.m_IOPCAsyncIO2; }
        }

        #endregion

        private int m_LocaleID									= 0;

		/// <summary> this list is needed to dispose the opcgroups before freeing the OPCServer! </summary>
		ArrayList							m_grpManagerList	= new ArrayList();

		/// <summary> determines whether this object has already been disposed.
		/// all public operations made after disposing the object (except re-connecting)
		/// are forbidden. </summary>
		bool								m_disposed			= false;

		/// <summary> determines whether this object has already been connected.
		/// this object is only allowed to connect exactly one time! </summary>
		bool								m_connected			= false;

		#endregion

		#region constructor
		public OPCServerManagement()
		{
			m_OPCServer = null;
		}
		#endregion

		#region OPC-Connect
		/// <summary>Establishes the connection to the S韒aticNET OPC Server.</summary>
		/// <param name="localeID"> The locale ID returned from IOPCCommon the OPC-Server </param>
		/// <exception cref="InvalidOperationException"> if the opcserver is already connected </exception>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		public void connectOPCServer(string progID)
		{
			if(!m_connected)
			{
				Type typeofOPCserver = null;

				try
				{
					//Get the Type from ProgID.Text
					typeofOPCserver = Type.GetTypeFromProgID(progID);

					string strDummy = "Couldn't find a server with the ProgID '" + progID + "'!";
					if (typeofOPCserver == null) throw new Exception(strDummy);

					// Must be freed with Marshal.ReleaseComObject(myOPCServer)
					m_OPCServer = (IOPCServer) Activator.CreateInstance(typeofOPCserver);
					if (m_OPCServer == null)
					{
						strDummy = "CreateInstance failed - server not connected!";
						throw new Exception(strDummy);
					}

					// Don't free it - we don't AddRef it!!!
					m_OPCCommon = (IOPCCommon) m_OPCServer;
					if (m_OPCCommon == null)
					{
						strDummy = "No 'IOPCCommon' Interface found!";
						throw new Exception(strDummy);
					}
				
					m_OPCCommon.GetLocaleID(out m_LocaleID);
					m_connected	= true;
				}
				catch (Exception)
				{
					throw;
				}
			}
			else
				throw new InvalidOperationException("Already connected to OPC-Server!");
		}

        // 利用IP地址连接OPC服务器
        public void connectOPCServer(string progID, string IP)
        {
            if (!m_connected)
            {
                Type typeofOPCserver = null;

                try
                {
                    //Get the Type from ProgID.Text
                    typeofOPCserver = Type.GetTypeFromProgID(progID, IP);

                    string strDummy = "Couldn't find a server with the ProgID '" + progID + "' with IP: " + IP + "!";
                    if (typeofOPCserver == null) throw new Exception(strDummy);

                    // Must be freed with Marshal.ReleaseComObject(myOPCServer)
                    m_OPCServer = (IOPCServer)Activator.CreateInstance(typeofOPCserver);// 创建OPCServer的实例
                    if (m_OPCServer == null)
                    {
                        strDummy = "CreateInstance failed - server not connected!";
                        throw new Exception(strDummy);
                    }

                    // Don't free it - we don't AddRef it!!!
                    m_OPCCommon = (IOPCCommon)m_OPCServer;
                    if (m_OPCCommon == null)
                    {
                        strDummy = "No 'IOPCCommon' Interface found!";
                        throw new Exception(strDummy);
                    }

                    m_OPCCommon.GetLocaleID(out m_LocaleID);
                    m_connected = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                throw new InvalidOperationException("Already connected to OPC-Server!");
        }

		#endregion

		#region destructor, disposable-pattern

		/// <summary> Use C# destructor syntax for finalization code.
		/// This destructor will run only if the Dispose method
		/// does not get called.
		/// It gives your base class the opportunity to finalize.
		/// Do not provide destructors in types derived from this class.   
		/// </summary>
		~OPCServerManagement()
		{
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
				IntPtr pErrors = IntPtr.Zero;

				if (m_OPCServer != null)
				{
					try
					{
						// Do NOT forget to call dispose for the management-objects!
						// in all properbility you will get an exception otherwise!
						foreach(OPCGroupManagement theManager in m_grpManagerList)
						{
							theManager.Dispose();
						}
						m_grpManagerList.Clear();
					}
					catch (Exception)
					{
						throw;
					}
					finally
					{
						// MUST NOT be freed because we got it with
						//myOPCCommon = (IOPCCommon) myOPCServer;

						// We must free it here, because we got it with
						// myOPCServer = (IOPCServer) Activator.CreateInstance(typeofOPCserver);
						if(m_OPCServer != null)
						{
							Marshal.ReleaseComObject(m_OPCServer);
							m_OPCServer = null;
						}
					}
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



        public void IOPCAsyncIO2_Read(int nCancelid, IntPtr pErrors)
        {
            //int dwCount,int[] phServer,
            //int dwTransactionID, out int pdwCancelID, 
            //out IntPtr ppErrors
            try
            {
                m_IOPCAsyncIO2.Read(4, OPCItemServerHandle, 2,
                                    out nCancelid, out pErrors);
                int[] errors = new int[4];

                Marshal.Copy(pErrors, errors, 0, 4); // 从非托管的内存指针到托管的整形数组
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




		#region for this sample used methods

		/// <summary> encapsulates the addGroup-method of the opcserver by using 
		/// the OPCGroupManagement-class </summary>
		/// <param name="grpName"> the group name </param>
		/// <param name="grpActive"> determines whether the group should be active </param>
		/// <param name="grpClntHndl"> the clienthandle of the group </param>
		/// <param name="reqUpdateRate"> the requested update rate for the group </param>
		/// <param name="revUpdateRate"> the revised update rate of the group </param>
		/// <returns> the manager object for the group </returns>
		/// <exception cref="NullReferenceException"> if the object is disposed </exception>
		/// <exception cref="Exception"> forwarded exception from the opcserver </exception>	
		public OPCGroupManagement addGroup(string grpName,bool grpActive,int grpClntHndl,
											int reqUpdateRate,
											out int revUpdateRate)
		{
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");

			try
			{
				// let the manager create a opc-group
				OPCGroupManagement theManager = new OPCGroupManagement(
																m_OPCServer,
																grpName,
																grpActive,
																grpClntHndl,
																reqUpdateRate);

				revUpdateRate = theManager.addOPCGroup(m_OPCCommon);

				// add this group to the list in order to dispose all groups on finalization
				m_grpManagerList.Add(theManager);

				return theManager;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary> encapsulates the method "GetErrorString" </summary>
		/// <param name="error"> the error-code returned from an opc-operation </param>
		/// <returns> the error-string with the current locale-id </returns>
		/// <exception cref="NullReferenceException"> if the object is disposed </exception>
		/// <exception cref="Exception"> forwarded exception from the opcserver </exception>	
		public string getErrorString(int error)
		{
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");

			try
			{
				string errorString;

				m_OPCServer.GetErrorString(error,m_LocaleID,out errorString);
			
				return errorString;
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>changes the error code given from the OPC server into a string</summary>
		/// <param name="qnr">error number</param>
		/// <returns>error string</returns>
		public string getQualityText(int qnr)
		{
			const int OPC_QUALITY_BAD					= 0x0;
			const int OPC_QUALITY_UNCERTAIN				= 0x40;
			const int OPC_QUALITY_GOOD					= 0xC0;
			const int OPC_QUALITY_CONFIG_ERROR			= 0x4;
			const int OPC_QUALITY_NOT_CONNECTED			= 0x8;
			const int OPC_QUALITY_DEVICE_FAILURE		= 0xC;
			const int OPC_QUALITY_SENSOR_FAILURE		= 0x10;
			const int OPC_QUALITY_LAST_KNOWN			= 0x14;
			const int OPC_QUALITY_COMM_FAILURE			= 0x18;
			const int OPC_QUALITY_OUT_OF_SERVICE		= 0x1C;
			const int OPC_QUALITY_LAST_USABLE			= 0x44;
			const int OPC_QUALITY_SENSOR_CAL			= 0x50;
			const int OPC_QUALITY_EGU_EXCEEDED			= 0x54;
			const int OPC_QUALITY_SUB_NORMAL			= 0x58;
			const int OPC_QUALITY_LOCAL_OVERRIDE		= 0xD8;
	
			string qstr = "";
	
			switch (qnr)
			{
				case OPC_QUALITY_BAD:
					qstr = "BAD";
					break;
				case OPC_QUALITY_UNCERTAIN:
					qstr = "UNCERTAIN";
					break;
				case OPC_QUALITY_GOOD:
					qstr = "GOOD";
					break;
				case OPC_QUALITY_CONFIG_ERROR:
					qstr = "OPC_QUALITY_CONFIG_ERROR";
					break;
				case OPC_QUALITY_NOT_CONNECTED:
					qstr = "NOT_CONNECTED";
					break;
				case OPC_QUALITY_DEVICE_FAILURE:
					qstr = "DEVICE_FAILURE";
					break;
				case OPC_QUALITY_SENSOR_FAILURE:
					qstr = "SENSOR_FAILURE";
					break;
				case OPC_QUALITY_LAST_KNOWN:
					qstr = "LAST_KNOWN";
					break;
				case OPC_QUALITY_COMM_FAILURE:
					qstr = "COMM_FAILURE";
					break;
				case OPC_QUALITY_OUT_OF_SERVICE:
					qstr = "OUT_OF_SERVICE";
					break;
				case OPC_QUALITY_LAST_USABLE:
					qstr = "LAST_USABLE";
					break;
				case OPC_QUALITY_SENSOR_CAL:
					qstr = "SENSOR_CAL";
					break;
				case OPC_QUALITY_EGU_EXCEEDED:
					qstr = "EGU_EXCEEDED";
					break;
				case OPC_QUALITY_SUB_NORMAL:
					qstr = "SUB_NORMAL";
					break;
				case OPC_QUALITY_LOCAL_OVERRIDE:
					qstr = "LOCAL_OVERRIDE";
					break;
				default:
					qstr = "UNKNOWN ERROR";
					break;
			}
			return qstr;
		}

		#endregion

	}

    public struct OPCItemProcessStatus_Self
    {
        public bool m_OPCItemStatus_MarkCodeHasAdded;

        public bool m_OPCItemStatus_MarkCodeGenerated; // X2.6 打标内容生成

        public bool m_OPCItemStatus_PLCHasSendBack1stMarkCode; // X1.0 PLC一次回传信号
    }

}
