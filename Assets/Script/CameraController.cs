using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float cammeraSpeed = 7.5f;

	public Transform player;

	// Update is called once per frame
	void Update ()
	{
		Vector3 cameraTarget = new Vector3 (player.position.x, transform.position.y, player.position.z);
		transform.position = Vector3.Lerp (transform.position, cameraTarget, Time.deltaTime * cammeraSpeed);
	}
}
