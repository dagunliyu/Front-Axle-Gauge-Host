using System;
using OpcRcw.Da;
using OpcRcw.Comn;
using System.Runtime.InteropServices;
using System.Resources;
using Siemens.Automation.ServiceSupport.Applications.OPC.IOPCConnector;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> this class expands the data of an OPCItemStateMgt-object with
	/// specific data to provide a maximum flexibility and performance </summary>
	public class OPCItemExtender
	{
		#region fields
		private string				m_itemID				= "";
		private int					m_srvHndl				= 0;
		private	int					m_clntHndl				= 0;
		private bool				m_IsActive				= false;
		private VarEnum				m_itemType;

		private object				m_itmValue;

		/// <summary> With this field it is possible to provide multiple functionality
		/// for updating specific controls of the userinterface with the opc-item (polymorphy) </summary>
		private IOPCItemControl		m_correspondingObject;
		
		#endregion

		#region construction
		/// <summary> creates a instance of this class </summary>
		/// <param name="itmIsActive"> determines whether the item is active </param>
		/// <param name="itmID"> the item id of the item </param>
		/// <param name="itmSrvHndl"> the server handle (provided from the opc server)</param>
		/// <param name="itmClntHndl"> the client handle of the item </param>
		/// <param name="itemType"> the datatype of the item </param>
		/// <param name="correspondingObject"> the corresponding object of this opc item; </param>
		public OPCItemExtender(bool itmIsActive, string itmID, int itmSrvHndl, int itmClntHndl, 
								VarEnum itemType, ref IOPCItemControl correspondingObject)
		{
			m_itemID = itmID;
			m_IsActive = itmIsActive;
			m_srvHndl = itmSrvHndl;
			m_clntHndl = itmClntHndl;
			m_itemType = itemType;
			m_correspondingObject = correspondingObject;
		}
		#endregion

		#region Properties

		public string itemID
		{
			get	{ return m_itemID;	}
			set	{ m_itemID = value; }
		}
		public int serverHndl
		{
			get	{ return m_srvHndl;	}
			set	{ m_srvHndl = value; }
		}
		public int clientHndl
		{
			get	{ return m_clntHndl;	}
			set	{ m_clntHndl = value; }
		}
		public bool isActive
		{
			get	{ return m_IsActive;	}
			set	{ m_IsActive = value; }
		}

		public object actValue
		{
			// only returns the last written value! (not of the value of the control, which might be changed)
			// this is necessary to have the chance to have consistent data
			get{ return m_itmValue; }
			set
			{
				m_itmValue = value;
				if(m_correspondingObject != null)
					m_correspondingObject.setOPCParam(value);
			}
		}
		public IOPCItemControl correspondingObject
		{
			get	{ return m_correspondingObject;	}
			set	{ m_correspondingObject = value; }
		}
		#endregion
	}
}
