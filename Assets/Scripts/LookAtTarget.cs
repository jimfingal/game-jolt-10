// C#
using System;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
	public Transform target;
	
	void Update()
	{
		if(target != null)
		{
			transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
		}
	}
}