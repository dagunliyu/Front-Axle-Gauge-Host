using System;

namespace Siemens.Automation.ServiceSupport.Applications.OPC.B515MCAFrontAxleManagement.DB
{
	// This file contains all relevant structures used in the plc
	#region class for telegram head
	public class TelegramHead
	{
		#region Fields
		private int m_telLength;
		private ushort m_telID;
		private ushort m_telStatus;
		private uint m_telNumber;
		private DateTime m_telSendTime;
		private short m_telNumberRecords;
		#endregion

		#region Properties
		public int telLength
		{
			get { return m_telLength; }
		}
		public ushort telID
		{
			get	{ return m_telID; }
		}
		public ushort telStatus
		{
			get	{ return m_telStatus; }
		}
		public uint telNumber
		{
			get	{ return m_telNumber; }
		}
		public DateTime telSendTime
		{
			get	{ return m_telSendTime; }
		}
		public short telNumberRecords
		{
			get	{ return m_telNumberRecords; }
		}
		#endregion

		#region construction
		public TelegramHead(int telLength, ushort telID,ushort telStatus, uint telNumber,
			DateTime telSendTime, short telNumberRecords)
		{
			m_telLength = telLength;
			m_telID = telID;
			m_telStatus = telStatus;
			m_telNumber = telNumber;
			m_telSendTime = telSendTime;
			m_telNumberRecords = telNumberRecords;
		}
		#endregion
	}
	#endregion

	#region class for measure data head
	public class MeasuredHead
	{
		#region Fields
		private short m_headDataH_ID;
		private DateTime m_headDataMeasureTime;
		private float m_headDataPressure;
		private float m_headDataEnergy;
		private string m_headDataDescription;
		#endregion

		#region Properties
		public short headDataH_ID
		{
			get	{ return m_headDataH_ID; }
		}
		public DateTime headDataMeasureTime
		{
			get	{ return m_headDataMeasureTime; }
		}
		public float headDataPressure
		{
			get	{ return m_headDataPressure; }
		}
		public float headDataEnergy
		{
			get	{ return m_headDataEnergy; }
		}
		public string headDataDescription
		{
			get	{ return m_headDataDescription; }
		}
		#endregion

		#region construction
		public MeasuredHead(short headDataH_ID,DateTime headDataMeasureTime,
			float headDataPressure, float headDataEnergy, 
			string headDataDescription)
		{
			m_headDataH_ID = headDataH_ID;
			m_headDataMeasureTime = headDataMeasureTime;
			m_headDataPressure = headDataPressure;
			m_headDataEnergy = headDataEnergy;
			m_headDataDescription = headDataDescription;
		}
		#endregion

		#region public methods
		public MeasuredHead Clone()
		{
			return new MeasuredHead(m_headDataH_ID,m_headDataMeasureTime, headDataPressure, 
						m_headDataEnergy, m_headDataDescription);
		}
		#endregion
	}
	#endregion

	#region class for measured data
	public class MeasuredData
	{
		#region Fields
		private short m_dataMeasurepointIndex;
		private float m_dataTemperature;
		private float m_dataPressure;
		private float m_dataFlow;
		private float m_dataHumidity;
		private string m_dataDescription;
		#endregion

		#region Properties
		public short dataMeasurepointIndex
		{
			get	{ return m_dataMeasurepointIndex; }
		}
		public float dataTemperature
		{
			get	{ return m_dataTemperature; }
		}
		public float dataPressure
		{
			get	{ return m_dataPressure; }
		}
		public float dataFlow
		{
			get	{ return m_dataFlow; }
		}
		public float dataHumidity
		{
			get	{ return m_dataHumidity; }
		}
		public string dataDescription
		{
			get	{ return m_dataDescription; }
		}
		#endregion

		#region construction
		public MeasuredData(short dataMeasurepointIndex,float dataTemperature,
			float dataPressure, float dataFlow, 
			float dataHumidity, string dataDescription)
		{

			m_dataMeasurepointIndex = dataMeasurepointIndex;
			m_dataTemperature = dataTemperature;
			m_dataPressure = dataPressure;
			m_dataFlow = dataFlow;
			m_dataHumidity = dataHumidity;
			m_dataDescription = dataDescription;
		}
		#endregion

		#region public methods
		public MeasuredData Clone()
		{
			return new MeasuredData(m_dataMeasurepointIndex,m_dataTemperature, m_dataPressure,
									m_dataFlow,m_dataHumidity,m_dataDescription);
		}
		#endregion
	}
	#endregion
}
