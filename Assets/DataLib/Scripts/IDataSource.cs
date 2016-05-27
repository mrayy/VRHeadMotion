using UnityEngine;
using System.Collections;

namespace Data
{
public interface IDataSource {

	string GetName ();
	string GetValue();
}
}