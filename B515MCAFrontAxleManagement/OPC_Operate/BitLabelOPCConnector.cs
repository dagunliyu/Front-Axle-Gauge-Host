using System;
using Siemens.Automation.ServiceSupport.Applications.OPC.IOPCConnector;
using System.Windows.Forms;


namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement
{
	/// <summary> the label of this class turns red, if the value is true, otherwise green </summary>
	public class BitLabelOPCConnector : IOPCItemControl
	{
		private Label m_Label;
		
		public BitLabelOPCConnector(Label theLabel)
		{
			m_Label = theLabel;
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
					m_Label.BackColor = System.Drawing.Color.Red;
				else
					m_Label.BackColor = System.Drawing.Color.Green;
			}
			catch(InvalidCastException ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}
