using UnityEngine;
using System.Collections;

public class LPDataManagerExp1 : Data.DataSourceManager {

	TargetObject _obj;
	UserFace _user;
	int SamplingRate=30;
	public LPDataManagerExp1(int samplingRate,TargetObject obj,UserFace usr):base("Experiment 1")
	{
		_obj = obj;
		_user = usr;
		this.SamplingRate = samplingRate;
	}
	public override void Init ()
	{
		base.Init ();

		AddDataSource (new ObjAngleX(_obj.transform));
		AddDataSource (new ObjAngleY(_obj.transform));
		AddDataSource (new ObjAngleZ(_obj.transform));

		AddDataSource (new ObjAngleX(_user.transform));
		AddDataSource (new ObjAngleY(_user.transform));
		AddDataSource (new ObjAngleZ(_user.transform));

		AddSamplingCondition (new Data.TimeOutSamplingCondition (1000/SamplingRate));
	}

	public override bool Sample ()
	{
		return base.Sample ();
	}

}
