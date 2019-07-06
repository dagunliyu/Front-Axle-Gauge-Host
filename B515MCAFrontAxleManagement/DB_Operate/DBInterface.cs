using System;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB
{
	/// <summary> this interface defines the methods a *DB class has to implement;
	/// otherwise the DBConnector class cannot work properly. </summary>
	public interface DBInterface : IDisposable
	{
		void Fill();
		System.Int32 addRowToHeaterTable(MeasuredHead measHead);
		void addRowToMeasureTable(System.Int32 h_idx,MeasuredData measData);
		void Insert();
		void deleteAllTables();
		System.Data.DataSet getDataSet();
	}
}
