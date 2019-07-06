using System;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.IOPCConnector
{
	/// <summary> This interface defines the method which is to be called to update *Connector classes with
	/// data delivered from datachange-events </summary>
	public interface IOPCItemControl
	{
		void setOPCParam(object theValue);
	}
}
