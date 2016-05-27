using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

namespace Data
{
	public class DBWriter
	{


		class DataVector
		{
			public System.TimeSpan time;
			public string[] values;
		}

		List<DataVector> _data = new System.Collections.Generic.List<DataVector> ();
		Dictionary<string,int> _keyMapping = new Dictionary<string,int> ();

		DataVector _temp;

		DateTime _startTime;


		public int SamplesCount
		{
			get{
				return _data.Count;
			}
		}

		public DBWriter()
		{
			_startTime = DateTime.Now;
		}
		public void ClearAll ()
		{
			_data.Clear ();
			_keyMapping.Clear ();
			_temp = null;

		}

		public void ClearData ()
		{
			_data.Clear ();
			_temp = null;
		}

		public void AddKey (string name)
		{
			if (_keyMapping.ContainsKey (name.ToLower ()))
				return;
			_keyMapping.Add (name.ToLower (), _keyMapping.Count);
		}

		public bool AddData (string Name, string Value)
		{
			if (_keyMapping.ContainsKey (Name.ToLower ()) == false)
				return false;
			int index = _keyMapping [Name.ToLower ()];

			if (index == -1)
				return false;
			if (_temp == null) {
				_temp = new DataVector ();
				_temp.values = new string[_keyMapping.Count];
			}

			_temp.values [index] = Value;
			return true;
		}

		public bool PushData ()
		{
			if (_temp == null)
				return false;

			_temp.time = DateTime.Now-_startTime;
			_data.Add (_temp);
			_temp = null;
			return true;
		}


		public bool WriteValues (string path)
		{
			StreamWriter w = File.CreateText (path);
			if (w == null)
				return false;

			//Write Header
			w.Write ("Time\t");
			foreach (var k in _keyMapping.Keys) {
				w.Write (string.Format ("{0}\t", k));
			}
			w.WriteLine ();

			//Write data
			foreach (var d in _data) {
				string s = string.Format ("{0}\t", string.Format("{0:0.000}",d.time.TotalMilliseconds/1000.0f));

				for(int i=0;i<d.values.Length;++i){
					s += string.Format ("{0}", d.values[i]);
					if (i != d.values.Length - 1)
						s += "\t";
				}
				w.WriteLine (s);
			}
			w.Close ();
			return true;
		}
	}

}