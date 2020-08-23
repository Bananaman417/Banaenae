using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
	private Camera cam;

	public float debugDistance;
	public int ChangeThisValue = 10;

	private void Start()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		Vector3 dest = (transform.position + Controller.thePlayer.transform.position) / 2;
		dest.z = -10;
		transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime * 5);

		float magn = (transform.position - Controller.thePlayer.transform.position).sqrMagnitude;
		debugDistance = magn;
		if (magn > 100 + ChangeThisValue)
		{
			cam.orthographicSize = 2 + Mathf.Sqrt(magn - 100);
		}
		else
			cam.orthographicSize = 2;
		

	}
}