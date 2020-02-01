using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRoot : MonoBehaviour
{
	public Camera camera;

	public Vector3 anchor = new Vector3(1, 0, 1);

	[ContextMenu("SetRoot")]
	void Start()
	{
		var pos = camera.ViewportPointToRay(anchor);
		//var localPos = transform.InverseTransformPoint(pos.GetPoint(anchor.z));

		transform.position = pos.GetPoint(anchor.z);
	}
}
