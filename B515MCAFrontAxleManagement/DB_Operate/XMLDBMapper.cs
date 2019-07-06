using System;
using System.Data.OleDb;
using System.Data;
using System.Xml;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB
{
	/// <summary> This class handles the access to an XML file as database </summary>
	public class XMLDBMapper : DBInterface
	{
        // 20171221
        // 这里采用的接口实现的方法，因此全部屏蔽的话相当于"继承"的接口有很多方法都没有实现，会报错
        // 屏蔽的原因是因为找不到XMLDB这个类，应该是Oracle数据库中的Oracle XMLDB
        // 验证的话可能需要安装Oracle或者添加相应的组件
        // 系统默认的引用中都没有包含这个类  (也许有包含的额)

        #region constants
        private const int RECORDS_PER_HEATER = 8;
        private const int RECORDS_TO_SAVE = 1000;
        #endregion

        #region fields
        //private XMLDB m_XMLDB;  // 找不到XMLDB类  lhc171221
        private string m_fileName;
        private bool m_disposed = false;
        //private System.Int32 m_telNumRecords = System.Int32.MaxValue;
        #endregion

        #region construction, destruction

        /// <summary> builds a xml mapper object </summary>
        /// <param name="filePath"> the path of the xml-file </param>
        public XMLDBMapper(string filePath)
        {
            //m_XMLDB = new XMLDB();  // 找不到XMLDB类  lhc171221

            // read the data
            //m_XMLDB.ReadXml(filePath, System.Data.XmlReadMode.DiffGram); // 找不到XMLDB类  lhc171221
            m_fileName = filePath;
            //m_XMLDB.AcceptChanges();// 找不到XMLDB类  lhc171221

            // if there are to much datasets, then clear them
            adjustMaximumSize();
        }

        #endregion

        #region private methods
        /// <summary> this method removes rows of the datatables until the number of them as been decreased to 
        /// a specified value </summary>
        private void adjustMaximumSize()
        {
            // 找不到XMLDB类  lhc171221
            // delete rows in front to reduce the size of the records to save
            //while (m_XMLDB.Heater.Rows.Count >= RECORDS_TO_SAVE + 1)
            //    m_XMLDB.Heater.Rows.RemoveAt(0);

            //while (m_XMLDB.Measure.Rows.Count >= (RECORDS_TO_SAVE + 1) * RECORDS_PER_HEATER)
            //    m_XMLDB.Measure.Rows.RemoveAt(0);
        }
        #endregion

        #region destructor; disposable-pattern

        /// <summary> Use C# destructor syntax for finalization code.
        /// This destructor will run only if the Dispose method
        /// does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide destructors in types derived from this class.   
        /// </summary>
        ~XMLDBMapper()
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
            if (!m_disposed)
            {
                // Don't dispose managed ressources in here; GC will take care
                m_disposed = true;
            }
        }
        #endregion

        #region implemented methods for DBInterface

        /// <summary> fills up the dataadapter object with the data stored in the database </summary>
        public void Fill()
        {
            // 找不到XMLDB类  lhc171221
            // m_XMLDB.ReadXml(m_fileName, System.Data.XmlReadMode.Auto);
            //m_XMLDB.AcceptChanges();

            // remove rows 
            adjustMaximumSize();
        }

        /// <summary> adds a new row to the transient datatable "heater" </summary>
        /// <param name="measHead"> the converted data delivered from the datachange event </param>
        /// <returns> the index of the added row; for usage to fill the foreign key of the measure table </returns>
        // 找不到XMLDB类  lhc171221
        // addRowToHeaterTable是接口中的方法，需要实现
        public System.Int32 addRowToHeaterTable(MeasuredHead measHead)
        {
        //    // remove the first row, if there are more than RECORDS_TO_SAVE datasets
        //    if (m_XMLDB.Heater.Rows.Count >= RECORDS_TO_SAVE + 1)
        //    {
        //        m_XMLDB.Heater.Rows.RemoveAt(0);
        //    }

        //    DataRow row = m_XMLDB.Heater.NewRow();
        //    row["H_id"] = measHead.headDataH_ID;
        //    row["M_time"] = measHead.headDataMeasureTime.ToLongTimeString();
        //    row["Pval"] = measHead.headDataPressure;
        //    row["QVal"] = measHead.headDataEnergy;
        //    row["H_desc"] = measHead.headDataDescription;
        //    m_XMLDB.Heater.Rows.Add(row);

        //    // Compute the maximum auto-index (primary key) from the new row
        //    object obj = m_XMLDB.Heater.Compute("MAX(H_idx)", "");
        //    return (System.Int32)obj;
            return 0;
        }

        /// <summary> adds a new row to the transient datatable "measure" </summary>
        /// <param name="h_idx"> the foreign key (must be the PK of the "heater" table) </param>
        /// <param name="measData"> the converted data delivered from the datachange event </param>
        public void addRowToMeasureTable(System.Int32 h_idx, MeasuredData measData)
        {
            // remove the first row, if there are more than 5 datasets

            // 找不到XMLDB类  lhc171221
            //if (m_XMLDB.Measure.Rows.Count >= (RECORDS_TO_SAVE + 1) * RECORDS_PER_HEATER)
            //{
            //    m_XMLDB.Measure.Rows.RemoveAt(0);
            //}
            //DataRow row = m_XMLDB.Measure.NewRow();
            //row["H_idx"] = h_idx;
            //row["M_id"] = measData.dataMeasurepointIndex;
            //row["Temp"] = measData.dataTemperature;
            //row["Pressure"] = measData.dataPressure;
            //row["Flow"] = measData.dataFlow;
            //row["Humidity"] = measData.dataHumidity;
            //row["M_desc"] = measData.dataDescription;
            //m_XMLDB.Measure.Rows.Add(row);
        }

        /// <summary> updates the persistent storage by calling dataset.WriteXML(..) </summary>
        /// <exception cref="Exception"> forwards any thrown exception </exception
        
        public void Insert()
        {
            try
            {
                //m_XMLDB.WriteXml(m_fileName, System.Data.XmlWriteMode.DiffGram);  // 找不到XMLDB类  lhc171221
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary> deletes all data as well the transient as the persistent storage </summary>
        /// <exception cref="Exception"> forwards any thrown exception </exception>
        public void deleteAllTables()
        {
            try
            {
                /*
                  // 找不到XMLDB类  lhc171221m_XMLDB.Heater.Clear();
                m_XMLDB.Measure.Clear();
                m_XMLDB.WriteXml(m_fileName, System.Data.XmlWriteMode.DiffGram);
            */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> method returns a complete dataset (-> both tables) read out of the persistent storage </summary>
        /// <returns> the dataset object </returns>
        public DataSet getDataSet()
        {
            //XMLDB xmlDB = new XMLDB();
            //xmlDB.ReadXml(m_fileName, System.Data.XmlReadMode.DiffGram);
            //return xmlDB;
            DataSet xmlDB_substitude = new DataSet();
            return xmlDB_substitude;   // lhc171221
        }
        #endregion
	}
}
