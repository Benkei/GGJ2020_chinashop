using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRoot : MonoBehaviour
{
	public Camera camera;

	public Vector3 anchor = new Vector3(1, 0, 1);


	void Start()
	{
		var pos = camera.ViewportToWorldPoint(anchor);
		var localPos = transform.InverseTransformPoint(pos);

		transform.localPosition = localPos;
	}
}
