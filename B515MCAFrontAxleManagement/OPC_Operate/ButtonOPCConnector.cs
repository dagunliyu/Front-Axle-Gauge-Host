using System;
using Siemens.Automation.ServiceSupport.Applications.OPC.IOPCConnector;
using System.Windows.Forms;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> the button of this class turns green, if the value is true, otherwise red </summary>
	public class ButtonOPCConnector : IOPCItemControl
	{
		Button m_button;
		public ButtonOPCConnector(Button theButton)
		{
			m_button = theButton;
		}

		#region IOPCItemControl Member

		/// <summary> sets the value delivered from ondatachange </summary>
		/// <param name="theValue"> the value (must be boolean) </param>
		/// <exception cref="InvalidCastException"> if the value cannot be converted into a boolean </exception>
		public void setOPCParam(object theValue)
		{
			try
			{
				bool status = Convert.ToBoolean(theValue);
				if(status)
					m_button.BackColor = System.Drawing.Color.Green;
				else
					m_button.BackColor = System.Drawing.Color.Red;
			}
			catch(InvalidCastException ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}
