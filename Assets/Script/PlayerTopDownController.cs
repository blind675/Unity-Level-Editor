using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerTopDownController : MonoBehaviour {

	public float roationSpeed = 180f;
	public float movementSpeed = 4f;

	private Vector3 movement;
	private Vector3 mousePosition;

	private CharacterController characterController;

	private void Start ()
	{
		characterController = GetComponent<CharacterController> ();
	}

	void Update ()
	{
		mousePosition = Input.mousePosition;
		LookWithMouse ();

		movement.x = Input.GetAxisRaw ("Horizontal");
		movement.y = 0;
		movement.z = Input.GetAxisRaw ("Vertical");
		Move ();

	}


	private void Move ()
	{
		// Movement
		Vector3 motion = movement;
		//motion -= Vector3.up * 9.1f; // Add gravity
		motion *= (Mathf.Abs (movement.x) == 1 && Mathf.Abs (movement.z) == 1) ? .7f : 1f;

		characterController.Move (motion * movementSpeed * Time.deltaTime);

	}


	private void LookWithMouse ()
	{
		// Looking
		Camera camera = Camera.main;
		mousePosition = camera.ScreenToWorldPoint (new Vector3 (mousePosition.x, mousePosition.y, camera.transform.position.y - transform.position.y));

		Quaternion targetRotation = Quaternion.LookRotation (mousePosition - new Vector3 (transform.position.x, 0, transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, roationSpeed * Time.deltaTime);
	}


}
