using System;
using Siemens.Automation.ServiceSupport.Applications.OPC.IOPCConnector;
using System.Windows.Forms;
//using Siemens.Automation.ServiceSupport.Applications.OPC.

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> the text of the label of this class is the string delivered from ondatachange </summary>
	public class TextLabelOPCConnector : IOPCItemControl
	{
		private Label m_Label;
		
		public TextLabelOPCConnector(Label theLabel)
		{
			m_Label = theLabel;
		}

		#region IOPCItemControl Member
		/// <summary> writes the given value (object) into the controls</summary>
		/// <param name="theValue"> the value (must be string) </param>
		/// <exception cref="InvalidCastException"> if the value cannot be converted into a string </exception>
		public void setOPCParam(object theValue)
		{
			try
			{
				m_Label.Text = Convert.ToString(theValue);
			}
			catch(InvalidCastException ex)
			{
				throw ex;
			}
		}

		#endregion
	}
}
