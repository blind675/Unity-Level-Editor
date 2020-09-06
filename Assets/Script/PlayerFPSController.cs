using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerFPSController : MonoBehaviour {

	public float gravity = -9.81f;

	public float mouseSensitivity = 100f;
	public float movementSpeed = 12f;

	private float xRotation = 0f;
	private CharacterController characterController;

	private Vector3 velocity;
	// Start is called before the first frame update
	void Start ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		characterController = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update ()
	{
		// looking
		float mouseX = Input.GetAxis ("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis ("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp (xRotation, -90f, 90f);

		Camera.main.transform.localRotation = Quaternion.Euler (xRotation, 0f, 0f);

		transform.Rotate (Vector3.up * mouseX);


		// moving
		float x = Input.GetAxisRaw ("Horizontal");
		float z = Input.GetAxisRaw ("Vertical");

		Vector3 move = transform.right * x + transform.forward * z;

		characterController.Move (move * movementSpeed * Time.deltaTime);

		velocity.y += gravity * Time.deltaTime;

		characterController.Move (velocity * Time.deltaTime);

	}
}
