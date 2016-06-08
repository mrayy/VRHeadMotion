using UnityEngine;
using System.Collections;
using System;

public class ExperimentManager : MonoBehaviour {

	public Data.DataSourceManager[] Experiments;

	public int Counter=0;
	public KeyCode SwitchKey=KeyCode.F1;
	public KeyCode ExportDataKey=KeyCode.F2;
	public KeyCode PauseKey=KeyCode.F5;
	public KeyCode NextSubjectKey=KeyCode.F8;

	public int ActiveExperimentID;

	public bool IsPaused;

	public DebugInterface DebugInterface;

	public string BaseExperimentsFolder="Experiments\\";
	public string ExpPrefix = "";

	 class ExperimentManagerDebugger:DebugInterface.IDebugElement
	{
		ExperimentManager owner;
		public ExperimentManagerDebugger(ExperimentManager owner)
		{
			this.owner=owner;
		}
		public string GetDebugString()
		{
			return owner.GetDebugString ();
		}
	}

	public Data.DataSourceManager ActiveExperiment
	{
		get{
			if (Experiments.Length == 0)
				return null;
			return Experiments [ActiveExperimentID];
		}
	}

	// Use this for initialization
	protected virtual void Start () {
		if (DebugInterface)
			DebugInterface.AddDebugElement (new ExperimentManagerDebugger (this));
		ActiveExperiment.Init ();
		string path = Application.dataPath + "\\" + BaseExperimentsFolder + ExpPrefix + "_meta.txt";
		try{
			string data=System.IO.File.ReadAllText (path);
			if (data != "") {
				Counter = int.Parse (data);
			}
		}catch(Exception e) {
		}
		OnNewSubject ();
	}

	protected virtual void OnNewSubject()
	{
		string path = Application.dataPath + "\\" + BaseExperimentsFolder + ExpPrefix + "_meta.txt";

		System.IO.File.WriteAllText (path, (Counter+1).ToString ());
	}

	// Update is called once per frame
	protected virtual void Update () {
	
		if (Input.GetKeyDown (SwitchKey))
			NextExperiment ();
		if (Input.GetKeyDown (ExportDataKey))
			ExportData ();
		if (Input.GetKeyDown (PauseKey))
			IsPaused=!IsPaused;
		if (Input.GetKeyDown (NextSubjectKey)) {
			ActiveExperiment.WriteData ();
			++Counter;
			OnNewSubject ();
		}

		if(!IsPaused)
			ActiveExperiment.Sample();

	}
	public virtual void ExportData()
	{
		ActiveExperiment.WriteData ();
	}

	public void NextExperiment()
	{
		ActiveExperimentID = (ActiveExperimentID + 1) % Experiments.Length;
		ActiveExperiment.Init ();
	}

	public virtual string GetDebugString()
	{
		string str = "Experiment Manager [" + (IsPaused ? "Paused" : "Running") + "]\n";
		if (!IsPaused) {
			str += "\n\tSubjects Count: " + Counter;
			str += "\n\tActive Experiment: " + ActiveExperiment.Name;
			str += "\n\tSamples Count: " + ActiveExperiment.SamplesCount;
		}

		return str;
	}
}
