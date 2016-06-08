using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour {

	public LPExperimentManager ExpManager;
	public Canvas RootCanvas;
	public Text TextBox;


	// Use this for initialization
	IEnumerator Start () {
		TextBox.text="Waiting for Subject";

		while (Input.GetKey (KeyCode.Space) == false || Input.touchCount>0) {
			yield return new WaitForEndOfFrame ();
		}

		ExpManager.IsPaused = true;
		TextBox.text="Ready..";
		yield return new WaitForSeconds (5);
		TextBox.text="Go!";
		yield return new WaitForSeconds (0.7f);
		ExpManager.IsPaused = false;
		RootCanvas.enabled = false;
		yield return new WaitForSeconds (30);
		ExpManager.ExportData ();
		ExpManager.IsPaused = true;
		TextBox.text="Done, thank you!";
		RootCanvas.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
