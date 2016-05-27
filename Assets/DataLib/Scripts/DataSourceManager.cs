using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Data
{
	[Serializable]
	public class DataSourceManager
	{
		public string Path;

		public string Name;

		public int SamplesCount
		{
			get{
				return _dbWriter.SamplesCount;
			}
		}

		List<IDataSource> _data = new List<IDataSource> ();
		DBWriter _dbWriter = new DBWriter ();
		List<ISamplingCondition> _conditions=new List<ISamplingCondition>();

		public DataSourceManager(string name)
		{
			Name = name;
		}
		public virtual void Init ()
		{
			foreach (var c in _conditions) {
				c.Reset ();
			}
			ClearData ();
		}

		public void AddDataSource (IDataSource ds)
		{
			_data.Add (ds);
			_dbWriter.ClearAll ();

			//update keys
			foreach (var d in _data) {
				_dbWriter.AddKey (d.GetName ());
			}
		}

		public void AddSamplingCondition(ISamplingCondition c)
		{
			_conditions.Add (c);
		}

		public virtual bool CanSample()
		{
			foreach (var c in _conditions) {
				if (!c.CanSample ())
					return false;
			}
			return true;
		}


		public virtual bool Sample ()
		{
			if (!CanSample ())
				return false;
			foreach (var d in _data) {
				_dbWriter.AddData (d.GetName (), d.GetValue ());
			}
			_dbWriter.PushData ();
			foreach (var c in _conditions) {
				c.OnSampled ();
			}
			return true;
		}

		public void WriteData ()
		{
			_dbWriter.WriteValues (Path);
			_dbWriter.ClearData ();
		}

		public void ClearAll()
		{
			ClearDataSource ();
			_conditions.Clear ();
		}

		public void ClearDataSource ()
		{
			_data.Clear ();
			_dbWriter.ClearAll ();
		}

		public void ClearData ()
		{
			_dbWriter.ClearData ();
		}
	}
}