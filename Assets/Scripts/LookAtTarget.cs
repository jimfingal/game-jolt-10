// C#
using System;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;
	
	void Update()
	{
		if(target != null)
		{
			transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z) + offset);
		}
	}
}