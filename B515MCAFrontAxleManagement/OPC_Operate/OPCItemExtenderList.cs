using System;
using System.Collections;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> this list, which is derived vom a .NET-arraylist, only accepts
	/// OPCItemExtender-objects; Exception are going to be thrown otherwise;
	/// to use this class as "global" OPCItemExternList some more work needs to done
	/// to provide a secure type list </summary>
	public class OPCItemExtenderList : ArrayList
	{
		#region fields
		/// <summary> with this hashtable a "indexed" access via a given ITEM-ID is possible!
		/// So the search of a specific ITEM-ID is possible in O(1)-complexity (maximum performance)!
		/// s. also the "get" and "set" methods</summary>
		private Hashtable m_itemIDItemMapping = new Hashtable();

		#endregion

		#region construction
		public OPCItemExtenderList()
		{
		}
		#endregion

		#region add methods
		/// <summary> adds a new OPCItemExtender to the list </summary>
		/// <param name="theItem"> the OPCItemExtender-object </param>
		/// <returns> the index of the added item in this list </returns>
		/// <exception cref="ArgumentException"> the itemID of the OPCItemExtender has to be different from
		/// all other itemID in this list! </exception>
		public int Add(OPCItemExtender theItem)
		{
			if(m_itemIDItemMapping.ContainsKey(theItem.itemID))
			{
				string str = "The opc-item already exists in this list." + "\n" +
				"In order to use the opc-item in a multiple way, you have to insert it into another opc-item-list.";
				throw new ArgumentException(str);
			}

			m_itemIDItemMapping.Add(theItem.itemID,theItem);
			
			return base.Add (theItem);
		}

		/// <summary> not supported with this list </summary>
		/// <param name="value"> a object </param>
		/// <returns> nothing </returns>
		/// <exception cref="ArgumentException"> no "native" objects can be added </exception>
		public override int Add(object value)
		{
			// adding a normal object is not possible!!!
			string strMsg = "This list does only provide adding OPCItemExtender objects!";
			throw new ArgumentException(strMsg);
		}

		/// <summary> not supported with this list </summary>
		/// <param name="c"> an ICollection </param>
		/// <exception cref="ArgumentException"> no collections can be added </exception>
		public override void AddRange(ICollection c)
		{
			// adding a normal object is not possible!!!
			string strMsg = "This list does only provide adding OPCItemExtender objects!";
			throw new ArgumentException(strMsg);
		}
		#endregion

		#region insert methods
		/// <summary> not supported with this list </summary>
		/// <param name="index"> the index </param>
		/// <param name="value"> a object </param>
		/// <exception cref="ArgumentException"> no "native" objects can be inserted </exception>
		public override void Insert(int index, object value)
		{
			// adding a normal object is not possible!!!
			string strMsg = "This list does only provide adding OPCItemExtender objects!";
			throw new ArgumentException(strMsg);
		}

		/// <summary> not supported with this list </summary>
		/// <param name="index"> the index </param>
		/// <param name="c"> an ICollection </param>
		/// <exception cref="ArgumentException"> no collections can be inserted </exception>
		public override void InsertRange(int index, ICollection c)
		{
			// adding a normal object is not possible!!!
			string strMsg = "This list does only provide adding OPCItemExtender objects!";
			throw new ArgumentException(strMsg);
		}
		#endregion

		#region remove methods
		
		/// <summary>  removes the OPCItemExtender-object;
		/// NOTE: the caller must ensure that the clienthandles are going to be reset
		/// to the new values! otherwise the callback-mechanism will fail! </summary>
		/// <param name="theItem"> the OPCItemExtender-object </param>
		public void Remove(OPCItemExtender theItem)
		{
			int idx = theItem.clientHndl;
			m_itemIDItemMapping.Remove(theItem.itemID);
			base.Remove(theItem);

			// renew clienthandles of all 'following' items, because the array-index has now changed
			for(int i=idx; i<this.Count;i++)
			{
				((OPCItemExtender)this[i]).clientHndl = i;
			}
		}
		
		/// <summary> not supported with this list </summary>
		/// <param name="obj"> a "native" object </param>
		/// <exception cref="ArgumentException"> no "native" objects can be removed </exception>
		public override void Remove(object obj)
		{
			// removing a normal object is not possible!!!
			string strMsg = "This list does only provide adding OPCItemExtender objects!";
			throw new ArgumentException(strMsg);
		}

		/// <summary> removes the OPCItemExtender-object;
		/// NOTE: the caller must ensure that the clienthandles are going to be reset
		/// to the new values! otherwise the callback-mechanism will fail! </summary>
		/// <param name="index"> the index </param>
		public override void RemoveAt(int index)
		{
			OPCItemExtender itm = (OPCItemExtender)this[index];
			m_itemIDItemMapping.Remove(itm.itemID);
			base.RemoveAt(index);

			// renew clienthandles of all 'following' items, because the array-index has now changed
			for(int i=index; i<this.Count;i++)
			{
				((OPCItemExtender)this[i]).clientHndl = i;
			}
		}

		/// <summary>  removes the OPCItemExtender-object;
		/// NOTE: the caller must ensure that the clienthandles are going to be reset
		/// to the new values! otherwise the callback-mechanism will fail! </summary>
		/// <param name="index"> index to start from </param>
		/// <param name="count"> number of items to invalidate </param>
		public override void RemoveRange(int index, int count)
		{
			OPCItemExtender itm;
			
			// remove the items from the hashtable
			for(int i=index; i<count;i++)
			{
				itm = (OPCItemExtender)this[i];
				m_itemIDItemMapping.Remove(itm.itemID);
			}

			base.RemoveRange(index,count);

			// renew clienthandles of all 'following' items, because the array-index has now changed
			for(int i=index;i<this.Count;i++)
			{
				((OPCItemExtender)this[i]).clientHndl = i;
			}
		}

		/// <summary> clears the list </summary>
		public override void Clear()
		{
			m_itemIDItemMapping.Clear();
			base.Clear ();
		}

		#endregion

		#region get and set methods (no properties!)
		public OPCItemExtender getExtenderItem(string itemID)
		{
			return (OPCItemExtender)m_itemIDItemMapping[itemID];
		}
		public void setValue(string itemID, object theValue)
		{
			OPCItemExtender itm = (OPCItemExtender)m_itemIDItemMapping[itemID];
			itm.actValue = theValue;
		}

		public object getValue(string itemID)
		{
			OPCItemExtender itm = (OPCItemExtender)m_itemIDItemMapping[itemID];
			return itm.actValue;
		}

		public int getSrvHandle(string itemID)
		{
			OPCItemExtender itm = (OPCItemExtender)m_itemIDItemMapping[itemID];
			return itm.serverHndl;
		}

		public int[] getAllSrvHandles()
		{
			// the returned srvhandles have to be in correct order!
			// otherwise the the caller could assign the values to wrong controls;
			// with hashtables the ordering might be different to the creation ordering
			// -> we have to copy the serverhandles manually.
			int[] arr = new int[this.Count];
			for (int i=0;i<this.Count;i++)
			{
				arr[i] = ((OPCItemExtender)(this[i])).serverHndl;
			}

			return arr;
		}
		#endregion
	}

}

