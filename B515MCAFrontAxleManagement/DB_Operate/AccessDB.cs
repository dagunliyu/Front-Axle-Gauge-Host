using System;
using System.Data.OleDb;
using System.Data;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB
{
	/// <summary> This class handles the access to an ACCESS database </summary>
	public class AccessDB: IDisposable, DBInterface
	{
		#region fields
		
		#region for database
		private OleDbConnection m_Connection;
		private DataTable m_dtHeater;
		private OleDbDataAdapter m_daHeater;
		private DataTable m_dtMeasure;
		private OleDbDataAdapter m_daMeasure;
		#endregion

		#region disposable-pattern
		bool m_disposed = false;
		#endregion
		#endregion

		#region construction

		/// <summary> Creates as AccessDB object </summary>
		/// <param name="dbPath"> The fully specified path of the access database file (*.mdb) </param>
		/// <exception cref="Exception"> forwards any thrown exception </exception>
		public AccessDB(string dbPath)
		{
			// connect to database
			string connectionString;
			try
			{
				// Connect to database
				connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;"+
					"data source = " + dbPath + ";";
				m_Connection = new OleDbConnection(connectionString);
				m_Connection.Open();

				// init datatables
				initDatabaseHeater();
				initDatabaseMeasure();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				m_Connection.Close();
			}
		}

		#endregion

		#region destructor; disposable-pattern

		/// <summary> Use C# destructor syntax for finalization code.
		/// This destructor will run only if the Dispose method
		/// does not get called.
		/// It gives your base class the opportunity to finalize.
		/// Do not provide destructors in types derived from this class.   
		/// </summary>
		~AccessDB()
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
					if(disposing && m_Connection != null)
						if (m_Connection.State != ConnectionState.Closed)
							m_Connection.Close();
				}
				catch(OleDbException ex)
				{
					// user called dispose -> a try-catch is still there
					if(disposing)
						throw;
					else
					{
						// do handle the Exception
						throw new Exception(ex.Message);
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

		#region private methods for oleDB provider
		
		/// <summary> this method initializes the transient heater table of the dataadapter;
		/// furthermore the select and insert commands are defined </summary>
		private void initDatabaseHeater()
		{
			// initialize ado.net objects for the table heater
			// NOTE: here 2 adapters are used for 2 tables; this is
			// not the only way; you can use one adapter with one dataset for this
			// application (2 tables), but the you have to switch the "select" and the
			// "insert"-commands for the adapter, because 'Update' and 'Fill' methods are
			// working with tables (except the dataset has only one table).
			m_dtHeater = new DataTable();
			// Select only one datarow
			m_daHeater = new OleDbDataAdapter(
								"SELECT * FROM Heater WHERE H_idx=(SELECT MAX(H_idx) FROM Heater)"
								,m_Connection);
		
			// ole db provider doesn't support parameter names;
			// identification is realized via the position of the parameters
			m_daHeater.InsertCommand = new OleDbCommand(
							"INSERT INTO Heater (H_idx , H_id , M_time , Pval , Qval , H_desc) " +
							"VALUES ( ?, ? , ? , ? , ? , ? )"
							,m_Connection);
			// Fill the parameters
			m_daHeater.InsertCommand.Parameters.Add("H_idx",OleDbType.Integer,4,"H_idx");
			m_daHeater.InsertCommand.Parameters.Add("H_id",OleDbType.SmallInt,4,"H_id");
			m_daHeater.InsertCommand.Parameters.Add("M_time",OleDbType.DBTimeStamp,50,"M_time");
			m_daHeater.InsertCommand.Parameters.Add("Pval",OleDbType.Single,20,"Pval");
			m_daHeater.InsertCommand.Parameters.Add("Qval",OleDbType.Single,20,"Qval");
			m_daHeater.InsertCommand.Parameters.Add("H_desc",OleDbType.VarWChar,10,"H_desc");
			
			// Fill the Datatable object
			m_daHeater.Fill(m_dtHeater);

			// set the autoincrementation of this column
			m_dtHeater.Columns["H_idx"].AutoIncrement = true;
			m_dtHeater.Columns["H_idx"].AutoIncrementStep = 1;
			object maxIndex = m_dtHeater.Compute("MAX(H_idx)","");
			if(maxIndex is System.Int32)
				m_dtHeater.Columns["H_idx"].AutoIncrementSeed = (System.Int32)(maxIndex)+1;

			m_dtHeater.AcceptChanges();
		}

		/// <summary>  this method initializes the transient measure table of the dataadapter;
		/// furthermore the select and insert commands are defined  </summary>
		private void initDatabaseMeasure()
		{
			// NOTE: see initDatabaseHeater()
			m_dtMeasure = new DataTable();
			m_daMeasure = new OleDbDataAdapter(
						"SELECT * FROM Measure WHERE M_idx=(SELECT MAX(M_idx) FROM Measure)",
						m_Connection);
			
			// ole db provider doesn't support parameter names;
			// identification is realized via the position of the parameters
			m_daMeasure.InsertCommand = new OleDbCommand(
				"INSERT INTO Measure (M_idx, H_idx , M_id , Temp , Pressure , Flow , Humidity , M_desc) " +
				"VALUES ( ?, ? , ? , ? , ? , ? , ? , ? )"
				,m_Connection);
			// Fill the parameters
			m_daMeasure.InsertCommand.Parameters.Add("M_idx",OleDbType.Integer,4,"M_idx");
			m_daMeasure.InsertCommand.Parameters.Add("H_idx",OleDbType.Integer,4,"H_idx");
			m_daMeasure.InsertCommand.Parameters.Add("M_id",OleDbType.SmallInt,4,"M_id");
			m_daMeasure.InsertCommand.Parameters.Add("Temp",OleDbType.Single,20,"Temp");
			m_daMeasure.InsertCommand.Parameters.Add("Pressure",OleDbType.Single,20,"Pressure");
			m_daMeasure.InsertCommand.Parameters.Add("Flow",OleDbType.Single,20,"Flow");
			m_daMeasure.InsertCommand.Parameters.Add("Humidity",OleDbType.Single,20,"Humidity");
			m_daMeasure.InsertCommand.Parameters.Add("M_desc",OleDbType.VarWChar,10,"M_desc");
			
			// Fill the Datatable object
			m_daMeasure.Fill(m_dtMeasure);
			
			// set the autoincrementation of this column
			m_dtMeasure.Columns["M_idx"].AutoIncrement = true;
			m_dtMeasure.Columns["M_idx"].AutoIncrementStep = 1;
			object maxIndex = m_dtMeasure.Compute("MAX(M_idx)","");
			if(maxIndex is System.Int32)
				m_dtMeasure.Columns["M_idx"].AutoIncrementSeed = (System.Int32)(maxIndex)+1;

			m_dtMeasure.AcceptChanges();
		}

		#endregion

		#region implemented methods for DBInterface

		/// <summary> fills up the dataadapter object with the data stored in the database </summary>
		public void Fill()
		{
			if(!m_disposed)
			{
				try
				{
					if(m_Connection.State != System.Data.ConnectionState.Open) 
						m_Connection.Open();

					m_daHeater.Fill(m_dtHeater);
					m_daMeasure.Fill(m_dtMeasure);
				}
				catch(Exception ex)
				{
					throw ex;
				}
				finally
				{
					m_Connection.Close();
				}
			}
			else
				throw new Exception("Object has been disposed!");
		}

		/// <summary> adds a new row to the transient datatable "heater" </summary>
		/// <param name="measHead"> the converted data delivered from the datachange event </param>
		/// <returns> the index of the added row; for usage to fill the foreign key of the measure table </returns>
		public System.Int32 addRowToHeaterTable(MeasuredHead measHead)
		{
			if(!m_disposed)
			{
				DataRow row = m_dtHeater.NewRow();
				row["H_id"] = measHead.headDataH_ID;
				row["M_time"] = measHead.headDataMeasureTime.ToLongTimeString();
				row["Pval"] = System.Math.Round((decimal)(measHead.headDataPressure),4);
				row["QVal"] = System.Math.Round((decimal)(measHead.headDataEnergy),4);
				row["H_desc"] = measHead.headDataDescription;
				m_dtHeater.Rows.Add(row);

				// Compute the maximum auto-index (primary key) from the new row
				object obj = m_dtHeater.Compute("MAX(H_idx)","");
				return (System.Int32)obj;
			}
			else
				throw new Exception("Object has been disposed!");
		}

		/// <summary> adds a new row to the transient datatable "measure" </summary>
		/// <param name="h_idx"> the foreign key (must be the PK of the "heater" table) </param>
		/// <param name="measData"> the converted data delivered from the datachange event </param>
		public void addRowToMeasureTable(System.Int32 h_idx, MeasuredData measData)
		{
			if(!m_disposed)
			{
				DataRow row = m_dtMeasure.NewRow();
				row["H_idx"] = h_idx;
				row["M_id"] = measData.dataMeasurepointIndex;
				row["Temp"] = System.Math.Round((decimal)(measData.dataTemperature),4);
				row["Pressure"] = System.Math.Round((decimal)(measData.dataPressure),4);
				row["Flow"] = System.Math.Round((decimal)(measData.dataFlow),4);
				row["Humidity"] = System.Math.Round((decimal)(measData.dataHumidity),4);
				row["M_desc"] = measData.dataDescription;
				m_dtMeasure.Rows.Add(row);
			}
			else
				throw new Exception("Object has been disposed!");
		}

		/// <summary> updates the persistent storage by calling dataadapter.Update(table) </summary>
		/// <exception cref="Exception"> forwards any thrown exception </exception>
		public void Insert()
		{
			if(!m_disposed)
			{
				// use a transaction to ensure, that one complete heater and measure data telegram is to be stored
				OleDbTransaction trans = null;
				try
				{
					if(m_Connection.State != System.Data.ConnectionState.Open) 
						m_Connection.Open();

					trans = m_Connection.BeginTransaction();

					m_daHeater.InsertCommand.Transaction = trans;
					m_daMeasure.InsertCommand.Transaction = trans;

					m_daHeater.Update(m_dtHeater);
					m_daMeasure.Update(m_dtMeasure);

					trans.Commit();
				}
				catch(Exception ex)
				{
					if(trans!=null)
						trans.Rollback();
					
					throw ex;
				}
				finally
				{
					// flush the transient tables to increase performance;
					// otherwise the time for adding rows would increase and transient
					// memory storage would be wasted.

					// NOTE: Clearing this tables will NOT delete the data in the persistent tables!
					// To do so, see "deleteAllTables".
					m_dtMeasure.Clear();
					m_dtHeater.Clear();

					m_Connection.Close();
				}
			}
			else
				throw new Exception("Object has been disposed!");
		}

		/// <summary> deletes all data as well the transient as the persistent storage </summary>
		/// <exception cref="Exception"> forwards any thrown exception </exception>
		public void deleteAllTables()
		{
			if(!m_disposed)
			{
				try
				{
					if(m_Connection.State != System.Data.ConnectionState.Open) 
						m_Connection.Open();

					// create a "DELETE" command object for Measure table
					OleDbCommand cmd = new OleDbCommand("DELETE * FROM Measure",m_Connection);

					// delete it
					cmd.ExecuteNonQuery();
					
					// create a "DELETE" command object for Heater table
					cmd = new OleDbCommand("DELETE * FROM Heater",m_Connection);

					// delete it
					cmd.ExecuteNonQuery();

					// to delete one single dataset do the following
					// 1. mark the row to delete (do NOT clear/remove it, because the table will
					// then remove the row from its row-collection and then the dataadapter
					// has now chance to find out, which row is to be deleted in the persistent db
//					DataRow row = m_dtHeater.Rows(theRowToDelete)
//					row.Delete();
					// 2. execute it
//					m_daHeater.Update(m_dtHeater);
					
				}
				catch(Exception ex)
				{
					throw ex;
				}
				finally
				{
					// flush the transient tables to increase performance;
					// otherwise the time for adding rows would increase and transient
					// memory storage would be wasted.
					m_dtMeasure.Clear();
					m_dtHeater.Clear();

					m_Connection.Close();
				}
			}
			else
				throw new Exception("Object has been disposed!");
		}

		/// <summary> method returns a complete dataset (-> both tables) read out of the persistent storage </summary>
		/// <returns> the dataset object </returns>
		public DataSet getDataSet()
		{
			try
			{
				if(m_Connection.State != System.Data.ConnectionState.Open) 
					m_Connection.Open();

				DataSet ds = new DataSet("DataSetClone");
				OleDbDataAdapter daH = new OleDbDataAdapter("SELECT * FROM Heater",m_Connection);
				OleDbDataAdapter daM = new OleDbDataAdapter("SELECT * FROM Measure",m_Connection);

				DataTable dtH = ds.Tables.Add("Heater");
				DataTable dtM = ds.Tables.Add("Measure");
			
				daH.Fill(dtH);
				daM.Fill(dtM);
			
				return ds;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				m_Connection.Close();
			}
		}

		#endregion
	}
}
