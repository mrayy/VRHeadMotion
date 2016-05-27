using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class TargetObject : MonoBehaviour {

	public float MinSpeed=10;
	public float MaxSpeed=180;

	public Vector3 MinAngles;//minimum angles (pitch,yaw,roll) 
	public Vector3 MaxAngles;//maximum angles (pitch,yaw,roll) 


	public float Radius=2;

	public Transform TargetObj;


	public Vector3 _targetAngle;
	Vector3 _currentAngle;
	float _targetSpeed;

	// Use this for initialization
	void Start () {
		if (VRDevice.isPresent) {
			InputTracking.Recenter ();
		}
	}

	public void SetRandomPosition()
	{
		_targetSpeed = Mathf.Deg2Rad*Random.Range (MinSpeed, MaxSpeed);
		_targetAngle = new Vector3 (
			Mathf.Deg2Rad* Random.Range (MinAngles.x, MaxAngles.x),
			Mathf.Deg2Rad*Random.Range (MinAngles.y, MaxAngles.y),
			Mathf.Deg2Rad*Random.Range (MinAngles.z, MaxAngles.z));
	}

	void _UpdatePosition()
	{
		_currentAngle=Vector3.Lerp(_currentAngle,_targetAngle,Time.deltaTime*_targetSpeed);
		Vector3 targetPos = Radius * new Vector3 (
			Mathf.Cos (_currentAngle.x) * Mathf.Sin (_currentAngle.y),
			Mathf.Sin (_currentAngle.x) ,
			Mathf.Cos (_currentAngle.x) * Mathf.Cos (_currentAngle.y));

		transform.localPosition = TargetObj.position+ targetPos;
		Vector3 forward = targetPos - TargetObj.position;

		Vector3 upVec = new Vector3 (Mathf.Sin (_targetAngle.z), Mathf.Cos (_targetAngle.z), 0);
		transform.localRotation = Quaternion.LookRotation (forward,upVec); //Quaternion.Euler(0,0, Mathf.Rad2Deg*_currentAngle.z);

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			SetRandomPosition ();

		_UpdatePosition ();
	}
}
