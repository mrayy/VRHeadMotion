using UnityEngine;
using System.Collections;
using System;

namespace Data
{
	public interface ISamplingCondition
	{

		void Reset ();

		bool CanSample ();

		void OnSampled ();
	}


	public class TimeOutSamplingCondition:ISamplingCondition
	{

		//sampling time
		public long SampleMS = 30;

		long _time = 0;

		long GetTimeMS ()
		{
			return (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
		}

		public TimeOutSamplingCondition (long time)
		{
			SampleMS = time;
			Reset ();
		}

		public void Reset ()
		{
			_time = GetTimeMS ();
		}

		public bool CanSample ()
		{
			long t = GetTimeMS ();
			if (t - _time < SampleMS)
				return false;
			return true;
		}

		public void OnSampled ()
		{
			_time = GetTimeMS ();
		}
	}

}