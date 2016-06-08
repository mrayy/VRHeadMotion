using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugInterface : MonoBehaviour {

	public interface IDebugElement
	{
		string GetDebugString();
	}
	public Text DebugText;

	List<IDebugElement> _debugElements=new List<IDebugElement>();

	public void AddDebugElement(IDebugElement e)
	{
		_debugElements.Add (e);
	}
	public void RemoveDebugElement(IDebugElement e)
	{
		_debugElements.Remove (e);
	}

	// Use this for initialization
	void Start () {
		DebugText.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (DebugText != null) {
			DebugText.text=GenerateDebugString();
		}

		if (Input.GetKeyDown(KeyCode.F9)) {
			DebugText.enabled=!DebugText.enabled;
		}
	}

	string GenerateDebugString()
	{
		string str = "";
		foreach (var e in _debugElements) {
			str+=e.GetDebugString()+"\n";
		}
		return str;
	}
	void OnGUI()
	{
	}
}
