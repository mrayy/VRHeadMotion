using UnityEngine;
using System.Collections;

public class UserFace : MonoBehaviour {
	public TargetObject Target;
	MeshRenderer mr;

	public float TargetError=2;//error in degrees
	// Use this for initialization
	void Start () {

		mr=this.GetComponent<MeshRenderer> ();
	}


	// Update is called once per frame
	void Update () {
		this.transform.localPosition = new Vector3 (0, 0, Target.Radius-0.1f);

		float diff = (Target.transform.position - transform.position).magnitude;

		float alpha = Mathf.Min(1.0f,diff/TargetError);

		if (alpha < 0.5f)
			Target.SetRandomPosition ();

		mr.material.color = new Color (1, 1, 1, alpha);
	}
}
