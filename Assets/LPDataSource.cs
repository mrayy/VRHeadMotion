using UnityEngine;
using System.Collections;

public class ObjAngleX : Data.IDataSource {
	public Transform Object;
	public ObjAngleX(Transform g)
	{
		Object = g;
	}
	public string GetName ()
	{
		return Object.name+"RX";
	}
	public string GetValue()
	{
		float v = Object.rotation.eulerAngles.x;
		if (v > 180)
			v = 360 - v;
		return v.ToString();
	}
}
public class ObjAngleY : Data.IDataSource {
	public Transform Object;
	public ObjAngleY(Transform g)
	{
		Object = g;
	}
	public string GetName ()
	{
		return Object.name+"RY";
	}
	public string GetValue()
	{
		float v = Object.rotation.eulerAngles.y;
		if (v > 180)
			v = 360 - v;
		return v.ToString();
	}
}
public class ObjAngleZ : Data.IDataSource {
	public Transform Object;
	public ObjAngleZ(Transform g)
	{
		Object = g;
	}
	public string GetName ()
	{
		return Object.name+"RZ";
	}
	public string GetValue()
	{
		float v = Object.rotation.eulerAngles.z;
		if (v > 180)
			v = 360 - v;
		return v.ToString();
	}
}

public class ButtonPressDataSource:Data.IDataSource
{
	string _name;
	KeyCode _trigger;
	string _on,_off;
	public ButtonPressDataSource(string name,KeyCode trigger,string on,string off)
	{
		_name = name;
		_trigger = trigger;
		_on = on;
		_off = off;
	}

	public string GetName ()
	{
		return _name;
	}
	public string GetValue()
	{
		return Input.GetKey (_trigger) ? _on:_off;
	}

}