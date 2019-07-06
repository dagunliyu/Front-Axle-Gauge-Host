using System;
using OpcRcw.Da;
using OpcRcw.Comn;
using System.Runtime.InteropServices;


namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> this class encapsulates all necessary operations for this automatoin task
	/// to read and write opc-items via the OPC DA interface </summary>
	public class OPCGroupManagement : IDisposable, IOPCDataCallback
	{
		#region fields
        // 管理OPC Group对象
		private IOPCGroupStateMgt			m_OPCGroupStateMgt				= null;

		/// <summary> These interfaces are necessary to have read/write access to process variables </summary>
		private IOPCSyncIO					m_OPCSyncIO						= null;
        private IOPCAsyncIO2 m_OPCAsyncIO2 = null;  // 异步读对象

        // 异步事件点
        private IConnectionPoint			m_OPCCP							= null;
		private IConnectionPointContainer	m_OPCCPContainer				= null;
		private int							m_cookie_CP						= 0;
		
		/// <summary> necessary for messages from the opc-server </summary>
		private int							m_LocaleID						= 0;

		/// <summary> properties of an OPCGroupStateMgt, which are partly necessary for further use </summary>
		private string						m_grpName						= "";
		private int							m_revUpdateRate					= 0;
		private int							m_reqUpdateRate					= 0;
		private int							m_grpSrvHndl					= 0;
		private int							m_grpClntHndl					= 0;
		private bool						m_isActive						= false;

		/// <summary> just the OPCItemMgt-object </summary>
		private IOPCItemMgt					m_OPCItem						= null;
        private int[] m_itmServerHandles;  // Item句柄数组


		/// <summary> the OPC server-object, which has to be passed by, is 
		/// necessary to get error-texts or similar from the opc-server. </summary>
		private IOPCServer					m_OPCServer						= null;

		/// <summary> to control, whether this object is disposed </summary>
		private bool						m_disposed						= false;
		#endregion

		#region properties
		public int revisedUpdateRate
		{
			get{ return m_revUpdateRate; }
		}
		public int srvHandle
		{
			get{ return m_grpSrvHndl; }
		}
        public IOPCAsyncIO2 Get_IOPCAsyncIO2_obj
        {
            get { return m_OPCAsyncIO2; }
        }
        public int[] Get_m_itmServerHandles
        {
            get { return m_itmServerHandles; }
        }
		#endregion

		#region construction
		/// <summary> creates an instance of this class </summary>
		/// <param name="theServer"> the OPCServer; must not be null! </param>
		/// <param name="grpName"> the group name</param>
		/// <param name="isActive"> determines whether the group is active </param>
		/// <param name="grpClntHndl"> the group client handle </param>
		/// <param name="reqUpdateRate"> the requested update rate </param>
		/// <exception cref="ArgumentNullException"> if "theServer" or "resMan" is null, then an exception
		/// will be thrown </exception>
		public OPCGroupManagement(IOPCServer theServer, string grpName, bool isActive, int grpClntHndl, int reqUpdateRate)
		{
			if(theServer == null) throw new ArgumentNullException("theServer");

			m_grpName = grpName;
			m_grpClntHndl = grpClntHndl;
			m_reqUpdateRate = reqUpdateRate;
			m_isActive = isActive;
			m_OPCServer = theServer;
		}

		#endregion

		#region destructor; disposable-pattern

		/// <summary> Use C# destructor syntax for finalization code.
		/// This destructor will run only if the Dispose method
		/// does not get called.
		/// It gives your base class the opportunity to finalize.
		/// Do not provide destructors in types derived from this class.   
		/// </summary>
		~OPCGroupManagement()
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
				// Don't dispose managed ressources in here; GC will take care
				try
				{
					// Release unmanaged resources.
					removeAllItemsAndGroup();
				}
				catch(Exception)
				{
					// user called dispose -> a try-catch is still there
					if(disposing)
						throw;
					else
					{
						// do handle the Exception
						throw new NullReferenceException("m_OPCServer");
					}
				}
				finally
				{
					freeGroup();
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

		#region Delegates and Events
		
		public delegate void DataChangedEventHandler(object source, OPCDataCallbackEventArgs e);
		/// <summary>event will be fired, if OPC calls datachange</summary>
		public event DataChangedEventHandler OnOPCDataChanged;

		public delegate void ReadCompleteEventHandler(object source, OPCDataCallbackEventArgs e);
		/// <summary>eventwill be fired, if OPC calls onreadcomplete</summary>
		public event ReadCompleteEventHandler OnOPCReadCompleted;

		public delegate void WriteCompleteEventHandler(object source, OPCWriteCompletedEventArgs e);
		/// <summary>eventwill be fired, if OPC calls onwritecomplete</summary>
		public event WriteCompleteEventHandler OnOPCWriteCompleted;


		// class for eventhandler-parameter
		public class OPCDataCallbackEventArgs : EventArgs
		{
			#region Fields
			internal int m_dwTransid;
			internal int m_hGroup;
			internal int m_hrMasterquality;
			internal int m_hrMastererror;
			internal int m_dwCount;
			internal int[] m_phClientItems;
			internal object[] m_pvValues; // 值
			internal short[] m_pwQualities; // 质量码
			internal OpcRcw.Da.FILETIME[] m_pftTimeStamps; // 时间戳
			internal int[] m_pErrors;
			#endregion

			#region Properties
			public int Transid
			{
				get	{ return m_dwTransid; }
			}
			public int hGroup
			{
				get	{ return m_hGroup; }
			}
			public int hrMasterquality
			{
				get	{ return m_hrMasterquality; }
			}
			public int hrMastererror
			{
				get	{ return m_hrMastererror; }
			}
			public int dwCount
			{
				get	{ return m_dwCount; }
			}
			public int[] phClientItems
			{
				get	{ return m_phClientItems; }
			}
			public object[] pvValues
			{
				get	{ return m_pvValues; }
			}
			public short[] pwQualities
			{
				get	{ return m_pwQualities; }
			}
			public OpcRcw.Da.FILETIME[] pftTimeStamps
			{
				get	{ return m_pftTimeStamps; }
			}
			public int[] pErrors
			{
				get	{ return m_pErrors; }
			}
			#endregion

			#region Construction
			
			internal OPCDataCallbackEventArgs(int dwTransid, int hGroup, int hrMasterquality, int hrMastererror, int dwCount,
				int[] phClientItems, object[] pvValues, short[] pwQualities,
				OpcRcw.Da.FILETIME[] pftTimeStamps, int[] pErrors)
			{
				m_dwTransid			= dwTransid;
				m_hGroup			= hGroup;
				m_hrMasterquality	= hrMasterquality;
				m_hrMastererror		= hrMastererror;
				m_dwCount			= dwCount;
				m_phClientItems		= (int[])(phClientItems.Clone());
				m_pvValues			= (object[])(pvValues.Clone());
				m_pwQualities		= (short[])(pwQualities.Clone());
				m_pftTimeStamps		= (OpcRcw.Da.FILETIME[])(pftTimeStamps.Clone());
				m_pErrors			= (int[])(pErrors);
			}

			#endregion
			
		}

		public class OPCWriteCompletedEventArgs : EventArgs
		{
			#region Fields
			internal int m_dwTransid;
			internal int m_hGroup;
			internal int m_hrMastererror;
			internal int m_dwCount;
			internal int[] m_phClienthandles;
			internal int[] m_pErrors;
			#endregion

			#region Properties
			public int Transid
			{
				get	{ return m_dwTransid; }
			}
			public int hGroup
			{
				get	{ return m_hGroup; }
			}
			public int hrMastererror
			{
				get	{ return m_hrMastererror; }
			}
			public int dwCount
			{
				get	{ return m_dwCount; }
			}
			public int[] phClienthandles
			{
				get	{ return m_phClienthandles; }
			}
			public int[] pErrors
			{
				get	{ return m_pErrors; }
			}
			#endregion
			
			#region Construction
			internal OPCWriteCompletedEventArgs(int dwTransid, int hGroup, int hrMastererror, int dwCount,
				int[] pClienthandles, int[] pErrors)
			{
				m_dwTransid			= dwTransid;
				m_hGroup			= hGroup;
				m_hrMastererror		= hrMastererror;
				m_dwCount			= dwCount;
				m_phClienthandles	= (int[])(pClienthandles.Clone());
				m_pErrors			= (int[])(pErrors);
			}
			#endregion
		}
		#endregion

		#region add groups and items

		/// <summary>adds a IOPCGroupStateMgt-object to the OPC-server</summary>
		/// <param name="commonInterface">the common interface</param>
		/// <returns> the revisedUpdateRate for further use </returns>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		public int addOPCGroup(IOPCCommon commonInterface)
		{
			// initialize arguments.
			Guid	iid					= Guid.Empty;
			object	objGroup			= null;
			IntPtr	pTimeBias			= IntPtr.Zero;
			IntPtr	pDeadband			= IntPtr.Zero;
			
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");
		
			m_revUpdateRate = 0;

			try
			{
				// get the default locale for the server.
				commonInterface.GetLocaleID(out m_LocaleID);
				iid   = typeof(IOPCGroupStateMgt).GUID;
				if (iid == Guid.Empty)
				{
					throw new Exception("Could not get the interface 'IOPCGroupStateMgt'!");
				}

				// Add a group object "m_OPCGroup" and query for interface IOPCItemMgt
				// Parameter as following:
				// [in] active, so do OnDataChange callback
				// [in] Request this Update Rate from Server
				// [in] Client Handle, not necessary in this sample
				// [in] No time interval to system UTC time
				// [in] No Deadband, so all data changes are reported
				// [in] Server uses english language to for text values
				// [out] Server handle to identify this group in later calls
				// [out] The answer from Server to the requested Update Rate
				// [in] requested interface type of the group object
				// [out] pointer to the requested interface
				m_OPCServer.AddGroup(	m_grpName,
					Convert.ToInt32(m_isActive),
					m_reqUpdateRate,
					m_grpClntHndl,
					pTimeBias,
					pDeadband,
					m_LocaleID,
					out m_grpSrvHndl,
					out m_revUpdateRate,
					ref iid,
					out objGroup );

				if (objGroup == null)
				{
					string strMsg = "Couldn't add the group '"
						+ m_grpName + "'  to the server!";
					throw new Exception(strMsg);
				}

				// Get our reference from the created group
				m_OPCGroupStateMgt = (IOPCGroupStateMgt) objGroup;
				if (m_OPCGroupStateMgt == null)
				{
					throw new Exception("Could not get the interface 'IOPCGroupStateMgt'!");
				}

			}
			catch (Exception)
			{
				throw;
			}
			return m_revUpdateRate;
		}


		/// <summary> adds an IOPCItemStateMgt-object to given group </summary>
		/// <param name="itemsActive"> determines which item is active </param>
		/// <param name="itemIDs"> determines the itemIDs </param>
		/// <param name="itemTypes"> the datatypes of the items </param>
		/// <param name="itmClntHndls"> the clientHandles of the items </param>
		/// <param name="itmSrvHndls"> the serverHandles given from the opc-server </param>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		public void addOPCItems(bool[] itemsActive,  // 激活
                                string[] itemIDs,    // item id
                                short[] itemTypes,   // item的数据类型
							    int[] itmClntHndls,  // item的client句柄数组
                                out int[] itmSrvHndls) // item的server句柄数组
		{
			// initialize arguments.
			Guid iid = Guid.Empty;
			//number of elements we can add
			int numberOfItems = itmClntHndls.Length;
			OPCITEMDEF[] Itemdefs = new OPCITEMDEF[numberOfItems];
			IntPtr ppResults = IntPtr.Zero;
			IntPtr ppErrors = IntPtr.Zero;
			string strDummy = "";

			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");
			
			// Init item server handles
			itmSrvHndls = new int[numberOfItems];
		
			try
			{
				// Get Item interface
				m_OPCItem = (IOPCItemMgt) m_OPCGroupStateMgt;
				if (m_OPCItem == null)
				{
					strDummy = "Could not get the interface 'IOPCItemMgt'!";
					throw new Exception(strDummy);
				}
		
				// Now add the items
				for(int i=0; i < numberOfItems; i++)
				{
					// Accesspath not needed
					Itemdefs[i].szAccessPath = "";
					// AddItem Active, so OnDataChange will come in an active group for this item 
					Itemdefs[i].bActive = Convert.ToInt32(itemsActive[i]);
					// We want to get the items as string
					Itemdefs[i].vtRequestedDataType = itemTypes[i];
					// "BinaryLargeOBject" not needed by SimaticNet OPC Server
					Itemdefs[i].dwBlobSize = 0;
					// no blob
					Itemdefs[i].pBlob = IntPtr.Zero;

					Itemdefs[i].hClient = itmClntHndls[i];
					Itemdefs[i].szItemID = itemIDs[i];
				}
			
				// Adding items to the Group
				m_OPCItem.AddItems(	numberOfItems,
					Itemdefs,
					out ppResults,
					out ppErrors);

				if (ppResults == IntPtr.Zero)
				{
					strDummy = "The server did not return a result array.";
					throw new Exception(strDummy);
				}
				if (ppErrors == IntPtr.Zero)
				{
					strDummy = "The server did not return an arror array.";
					throw new Exception(strDummy);
				}

				//Evaluate return ErrorCodes to exclude possible Errors
				int[] errors = new int[numberOfItems];
				Marshal.Copy(ppErrors, errors, 0, numberOfItems);

				OPCITEMRESULT[] result = new OPCITEMRESULT[numberOfItems];
				IntPtr pos = ppResults;

				for(int dwCount = 0; dwCount < numberOfItems; dwCount++)
				{
					try
					{
						if (errors[dwCount] != 0)
						{
							strDummy = "At least one item is not added.";
							throw new Exception(strDummy); // 抛出异常
                            
						}
						// Item was added succesfully
						result[dwCount] = (OPCITEMRESULT) Marshal.PtrToStructure(pos, typeof(OPCITEMRESULT));
						itmSrvHndls[dwCount] = result[dwCount].hServer;
					}
					catch (Exception)
					{
						// Item was not added
						throw;
					}

					pos = (IntPtr)(pos.ToInt32() + Marshal.SizeOf(typeof(OPCITEMRESULT)));
				}

				m_itmServerHandles = new int[numberOfItems];
				m_itmServerHandles = itmSrvHndls;

				// Free allocated COM-ressouces
				Marshal.FreeCoTaskMem(ppResults);
				Marshal.FreeCoTaskMem(ppErrors);
			}
			catch (Exception)
			{
				// Free allocated COM-ressouces
				Marshal.FreeCoTaskMem(ppResults);
				Marshal.FreeCoTaskMem(ppErrors);

				throw;
			}
		}
		
        
        
        
        
        #endregion

		#region activation of sync, async interfaces and the callback
		/// <summary> activates the sync-interface </summary>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		public void setSyncInterface()
		{
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");
			
			try
			{
				if (m_OPCGroupStateMgt != null)
				{
					// Query interface for Sync calls on group object
					// Take care:	IOPCSyncIO is for DA 1.x AND DA 2.x
					//				IOPCSyncIO2 is for DA 3.x
					m_OPCSyncIO = (IOPCSyncIO) m_OPCGroupStateMgt;
					if(m_OPCSyncIO == null)
						throw new Exception("Could not get the interface 'IOPCSyncIO2'!");
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary> activates the async-interface </summary>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		public void setASyncInterface()
		{
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");
			
			try
			{
				if(m_OPCGroupStateMgt != null)
				{
					// Query interface for Async calls on group object
					// Take care:	IOPCAsyncIO is only for DA 1.x
					//				IOPCAsyncIO2 is for DA 2.x
					//				IOPCAsyncIO3 is for DA 3.x
					m_OPCAsyncIO2 = (IOPCAsyncIO2) m_OPCGroupStateMgt;
					if(m_OPCAsyncIO2 == null)
						throw new Exception("Could not get the interface 'IOPCAsyncIO2'!");

					if(m_cookie_CP == 0)
						connectCallback();
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary> Connects the group to the connectionpointcontainer of IOPCGroupStateMgt </summary>
		public void connectCallback()
		{
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");
			
			try
			{
				// Allow connection only one time!
				if(m_OPCGroupStateMgt != null && m_cookie_CP == 0)
				{
					// Query interface for callbacks on group object
					m_OPCCPContainer = (IConnectionPointContainer) m_OPCGroupStateMgt;
					if(m_OPCCPContainer == null)
						throw new Exception("Could not get the interface 'IConnectionPointCointainer'!");

					// Establish Callback for all async operations
					Guid iid = typeof(IOPCDataCallback).GUID;
					m_OPCCPContainer.FindConnectionPoint( ref iid, out m_OPCCP);

					if(m_OPCCP == null)
						throw new Exception("Could not get a connection point!");
					// Creates a connection between the OPC servers's connection point and 
					// this client's sink (the callback object). 
					m_OPCCP.Advise(this, out m_cookie_CP);

					if (m_cookie_CP == 0)
					{
						string strDummy = "'Advise' for the sink interface failed!";
						throw new Exception(strDummy);
					}
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion

		#region changing the group state
		/// <summary>This function provides the switching of the active state of the given group.</summary>
		/// <param name="reqUpdateRate">requested update rate</param>
		/// <param name="activateGroup">de/activate the group</param>
		/// <param name="revUpdateRate">revised update rate</param>
		/// <exception cref="Exception">forwards any exception (with short error description)</exception>
		/// <remarks>In order to minimize the traffic on the net and the demands on the opc server it
		/// is recommended to switch off the active groups if they are not necessary.</remarks>
		public void changeGroupState(int reqUpdateRate,	bool activateGroup, out int revUpdateRate)
		{
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");
			
			int Active = 0;
			int Loc = 0x409;
			revUpdateRate = 0;

			IntPtr pTimeBias = IntPtr.Zero;
			IntPtr pDeadband = IntPtr.Zero;

			// Access unmanaged memory
			GCHandle hRequestedUpdateRate = GCHandle.Alloc(reqUpdateRate, GCHandleType.Pinned);
			GCHandle hLoc = GCHandle.Alloc(Loc, GCHandleType.Pinned);
			GCHandle hActive = GCHandle.Alloc(Active, GCHandleType.Pinned);
			GCHandle hServerGroup = GCHandle.Alloc(m_grpSrvHndl, GCHandleType.Pinned);

			try
			{
				// Set state
				if (activateGroup)
					hActive.Target = 1;
				else
					hActive.Target = 0;

				// Because we have to allocate unmanaged memory, we have to pinn them. Otherwise .Net's
				// garbage collector might free the memory and the opc server will do something randomly.
				m_OPCGroupStateMgt.SetState(hRequestedUpdateRate.AddrOfPinnedObject(),
					out revUpdateRate,
					hActive.AddrOfPinnedObject(),
					// if the parameter is used, it must be pinned!!!
					pTimeBias,
					// if the parameter is used, it must be pinned!!!
					pDeadband,
					hLoc.AddrOfPinnedObject(),
					hServerGroup.AddrOfPinnedObject());
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				// now free the unmanaged memory
				if(hActive.IsAllocated) {hActive.Free();}
				if(hLoc.IsAllocated) {hLoc.Free();}
				if(hRequestedUpdateRate.IsAllocated) {hRequestedUpdateRate.Free();}
				if(hServerGroup.IsAllocated) {hServerGroup.Free();}
			}
		}
		#endregion

		#region reading and writing
		/// <summary>Reads values via an 'asynchronous read'</summary>
		/// <param name="asyncIO2Interface">async interface to read values from</param>
		/// <param name="itemServerHandles">the corresponding item server handles</param>
		/// <param name="transactionID">transaction id for tracing</param>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		public void readAsync(int[] itemServerHandles,ref int transactionID)
		{
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");
			
			IntPtr pErrors = IntPtr.Zero;
			int numItems = itemServerHandles.Length;
			int[] errors = new int[numItems];
			string ErrorStr = "";

			try
			{
				// We don't use the CancelID in this programm
				int CancelID = 0;

				transactionID++;

				m_OPCAsyncIO2.Read(	numItems,
					itemServerHandles,
					transactionID,
					out CancelID,
					out pErrors);

				//check if an error occured
				Marshal.Copy(pErrors, errors, 0, numItems);

				for(int xCount = 0; xCount < numItems; xCount++)
				{
					// Data has been received
					if(errors[xCount] != 0)
					{
						m_OPCServer.GetErrorString(errors[xCount], m_LocaleID, out ErrorStr);
						System.Diagnostics.Debug.WriteLine(ErrorStr);
						throw new Exception(ErrorStr);
					}
				}
				// Free allocated COM-ressouces
				Marshal.FreeCoTaskMem(pErrors);
			}
			catch (Exception)
			{
				Marshal.FreeCoTaskMem(pErrors);
				throw;
			}
		}


		/// <summary>Reads opc-items via an synchronous read</summary>
		/// <param name="OPCDataSrc">determines the source, the data have to be read (cache or device)</param>
		/// <param name="itemServerHandles">the serverhandles of the opc-items to be read</param>
		/// <param name="theGroup">the group in which the opc-items are hosted</param>
		/// <returns> itemstates, returned form the opc-server </returns>
		public OPCITEMSTATE[] readSync(OpcRcw.Da.OPCDATASOURCE OPCDataSrc,
										int[] itemServerHandles, out int[] errors)
		{
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");

			IntPtr pItemStates = IntPtr.Zero;
			IntPtr pErrors = IntPtr.Zero;
			int NumItems = itemServerHandles.Length;
			OPCITEMSTATE[] result = new OPCITEMSTATE[NumItems];

			string ErrorStr = "";

			try
			{
				// Usually reading from CACHE is more efficient
				// AND the underlying device has to do less communication work.
				// BUT, you must have an active ITEM and GROUP, so that the opc server
				// will update the data by itself; otherwise you'll get quality OUT_OF_SERVICE
				m_OPCSyncIO.Read(	OPCDataSrc,	//OPCDATASOURCE.OPC_DS_DEVICE or OPCDATASOURCE.OPC_DS_CACHE,
					NumItems,
					itemServerHandles,
					out pItemStates,
					out pErrors);

				if (pItemStates == IntPtr.Zero) throw new ArgumentNullException("EXT_OPC_RDSYNC_ITEMSTATES");
				if (pErrors == IntPtr.Zero) throw new ArgumentNullException("EXT_OPC_RDSYNC_ERRORS");

				//Evaluate return ErrorCodes to exclude possible Errors
				errors = new int[NumItems];
				Marshal.Copy(pErrors, errors, 0, NumItems);

				IntPtr pos = pItemStates;

				// Now get the read values and check errors
				for(int dwCount = 0; dwCount < NumItems; dwCount++)
				{
					result[dwCount] = (OPCITEMSTATE) Marshal.PtrToStructure(pos, typeof(OPCITEMSTATE));

					if (errors[dwCount] != 0)
					{
						m_OPCServer.GetErrorString(errors[dwCount], m_LocaleID, out ErrorStr);
						System.Diagnostics.Debug.WriteLine(ErrorStr);
					}

					pos = (IntPtr)(pos.ToInt32() + Marshal.SizeOf(typeof(OPCITEMSTATE)));
				}

				if(ErrorStr != "")
				{
					throw new Exception(ErrorStr);
				}

				// Free allocated COM-ressouces
				Marshal.FreeCoTaskMem(pItemStates);
				Marshal.FreeCoTaskMem(pErrors);
			}
			catch (Exception)
			{
				// Free allocated COM-ressouces
				Marshal.FreeCoTaskMem(pItemStates);
				Marshal.FreeCoTaskMem(pErrors);

				// just forward it
				throw;
			}
			return result;

		}

		/// <summary>Writes values via an 'sychronous write'</summary>
		/// <param name="syncIOInterface">sync interface to write values to</param>
		/// <param name="itemServerHandles">the corresponding item server handles</param>
		/// <param name="pItemValues">values to write</param>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		public void writeSync(int[] itemServerHandles, object[] pItemValues, out int[] errors)
		{
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");

			IntPtr pErrors = IntPtr.Zero;
			int numItems = itemServerHandles.Length;
			string ErrorStr = "";
			
			try
			{
				// sync write will block the user interface! so the ui is not responsive.
				// therefore it should only be used if the user is not allowed to control the ui.
				m_OPCSyncIO.Write(	numItems,
									itemServerHandles,
									pItemValues,
									out pErrors);

				errors = new int[numItems];
				//check if an error occured
				Marshal.Copy(pErrors, errors, 0, numItems);

				for(int xCount = 0; xCount < numItems; xCount++)
				{
					// Data has been received
					if(errors[xCount] != 0)
					{
						// Errors occured - raise Exception
						m_OPCServer.GetErrorString(errors[xCount], m_LocaleID, out ErrorStr);
						System.Diagnostics.Debug.WriteLine(ErrorStr);
					}
				}

				if(ErrorStr != "")
					throw new Exception(ErrorStr);

				// Free allocated COM-ressouces
				Marshal.FreeCoTaskMem(pErrors);
			}
			catch (Exception)
			{
				// Free allocated COM-ressouces
				Marshal.FreeCoTaskMem(pErrors);
				throw;
			}
		}

		/// <summary>Writes the opc-items via an 'asynchronous write'</summary>
		/// <param name="asyncIO2Interface">async interface to write values to</param>
		/// <param name="itemServerHandles">the corresponding item server handles</param>
		/// <param name="itemValues">the values to write</param>
		/// <param name="transactionID">transaction id for tracing</param>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		public void writeAsync(int[] itemServerHandles,object[] itemValues,
								ref int transactionID)
		{
			if(m_disposed)
				throw new NullReferenceException("This object has been disposed!");

			IntPtr pErrors = IntPtr.Zero;
			int numItems = itemServerHandles.Length;
			int[] errors = new int[numItems];
			string ErrorStr = "";

			try
			{
				// We never use dwCancelID anymore
				int dwCancelID = 0;

				transactionID++;

				m_OPCAsyncIO2.Write(	numItems,
										itemServerHandles,
										itemValues,
										transactionID,
										out dwCancelID,
										out pErrors);

				//check if an error occured
				Marshal.Copy(pErrors, errors, 0, numItems);

				for(int xCount = 0; xCount < numItems; xCount++)
				{
					// Data has been received
					if(errors[xCount] != 0)
					{
						// Errors occured - raise Exception
						m_OPCServer.GetErrorString(errors[xCount], m_LocaleID, out ErrorStr);
						System.Diagnostics.Debug.WriteLine(ErrorStr);
						throw new Exception(ErrorStr);
					}
				}
				// Free allocated COM-ressouces
				Marshal.FreeCoTaskMem(pErrors);
			}
			catch (Exception)
			{
				// Free allocated COM-ressouces
				Marshal.FreeCoTaskMem(pErrors);

				throw;
			}
		}
		#endregion

		#region freeing of the com-interfaces
		/// <summary> frees the IOPCItemStateMgt-interfaces and then the IOPCGroupStateMgt-interface </summary>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		private void removeAllItemsAndGroup()
		{		
			try
			{
				disconnectCallback();
				removeItems();
				removeGroup();
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary> releases the IOPCGroupStateMgt-object </summary>
		private void freeGroup()
		{
			// *****************************************************
			// Release all interfaces
			// *****************************************************
			// MUST NOT be freed because we got it with
			//myOPCAsyncIO2 = (IOPCAsyncIO2) myOPCGroup;

			// MUST NOT be freed because we got it with
			//myOPCSyncIO = (IOPCSyncIO) myOPCGroup;

			// MUST NOT be freed because we got it with
			//myOPCItem = (IOPCItemMgt) myOPCGroup;

			// MUST NOT be freed because we got it with
			//myOPCConnectionPointContainer = (IConnectionPointContainer) myOPCGroup;
			
			// We must free it here, because we got it with
			//myOPCServer.AddGroup(	TextBox_AddGroup.Text,
			//						Convert.ToInt32(false),
			//						RequestedUpdateRate,
			//						1,
			//						pTimeBias,
			//						pDeadband,
			//						m_LocaleID,
			//						out m_Handle_myOPCGroup,
			//						out RevisedUpdateRate,
			//						ref  iid,
			//						out objGroup );
			//myOPCGroup = (IOPCGroupStateMgt) objGroup;
			// It doesn't matter you can eather free objGroup OR myOPCGroup 
			// but, NEVER both.
			Marshal.ReleaseComObject(m_OPCGroupStateMgt);
			m_OPCGroupStateMgt = null;
		}

		/// <summary>  </summary>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		private void removeGroup()
		{
			if(m_grpSrvHndl != 0)
			{
				if(m_OPCServer != null)
				{
					try
					{
						m_OPCServer.RemoveGroup(m_grpSrvHndl, Convert.ToInt32(true));
						m_grpSrvHndl = 0;
					}
					catch(Exception)
					{
						throw;
					}
				}
			}
		}

		/// <summary> removes all OPC-Items from this group </summary>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		private void removeItems()
		{
			IntPtr pErrors = IntPtr.Zero;
			
			if (m_itmServerHandles != null)
			{
				try
				{
					int[] pItems;
					int[] errors;
					int numberOfItems = m_itmServerHandles.Length;
					// Removes items
					pItems = new int[numberOfItems];
					errors = new int[numberOfItems];

					pItems = (int[])(m_itmServerHandles.Clone());

					m_OPCItem.RemoveItems(numberOfItems, pItems, out pErrors);

					//Evaluating Return ErrorCodes to exclude possible Errors
					Marshal.Copy(pErrors, errors, 0, numberOfItems);

					for(int xCount = 0; xCount < numberOfItems; xCount++)
					{
						// Data has been received
						if(errors[xCount] != 0)
						{
							throw new Exception("At least one item is not removed.");
						}
					}

					// Delete item server handles
					for(int idx=0; idx < numberOfItems; idx++)
					{
						m_itmServerHandles[idx] = 0;
					}

					Marshal.FreeCoTaskMem(pErrors);
				}
				catch(Exception)
				{
					throw;
				}
			}

		}

		/// <summary>Disconnects from callback</summary>
		/// <exception cref="Exception">throws and forwards any exception (with short error description)</exception>
		private void disconnectCallback()
		{
			try
			{
				// disconnect from callback
				if(m_cookie_CP != 0 && m_OPCCP != null)
				{
					m_OPCCP.Unadvise(m_cookie_CP);
					m_cookie_CP = 0;

					Marshal.ReleaseComObject(m_OPCCP);
					m_OPCCP = null;
				}
			}
			catch(Exception)
			{
				throw;
			}
		}
		#endregion

		#region Not used OPC functionality

		#region OPCGetStatus
//		private void OPCGetStatus()
//		{
//			IntPtr myStatus = IntPtr.Zero;
//			OPCSERVERSTATUS state;
//			ListViewItem myItem;
//			DateTime dt;
//	
//			try
//			{
//				m_OPCServer.GetStatus(out myStatus);
//				state = (OPCSERVERSTATUS) Marshal.PtrToStructure(myStatus, typeof(OPCSERVERSTATUS));
//
//				Marshal.FreeCoTaskMem(myStatus);
//			}
//			catch (Exception ex)
//			{
//			}
//		}
		#endregion
		#endregion

		#region IOPCDataCallback Member

		public void OnCancelComplete(int dwTransid, int hGroup)
		{
			// will not be implemented
		}

		public void OnReadComplete(int dwTransid, int hGroup, int hrMasterquality, int hrMastererror, int dwCount, int[] phClientItems, object[] pvValues, short[] pwQualities, OpcRcw.Da.FILETIME[] pftTimeStamps, int[] pErrors)
		{
			OnOPCReadCompleted(this,
				new OPCDataCallbackEventArgs(dwTransid, hGroup, hrMasterquality, hrMastererror, dwCount, phClientItems,
				pvValues, pwQualities, pftTimeStamps, pErrors));
		}

		public void OnDataChange(int dwTransid, int hGroup, int hrMasterquality, int hrMastererror, int dwCount, int[] phClientItems, object[] pvValues, short[] pwQualities, OpcRcw.Da.FILETIME[] pftTimeStamps, int[] pErrors)
		{
			OnOPCDataChanged(this,
				new OPCDataCallbackEventArgs(dwTransid, hGroup, hrMasterquality, hrMastererror, dwCount, phClientItems,
				pvValues, pwQualities, pftTimeStamps, pErrors));
		}

		public void OnWriteComplete(int dwTransid, int hGroup, int hrMastererr, int dwCount, int[] pClienthandles, int[] pErrors)
		{
			OnOPCWriteCompleted(this,
				new OPCWriteCompletedEventArgs(dwTransid, hGroup, hrMastererr, dwCount, pClienthandles, pErrors));
		}

		#endregion

        public string GetErrorString(int pError, string strResult)
        {
            m_OPCServer.GetErrorString(pError, m_LocaleID, out strResult);
            return strResult;
        }


	}
}