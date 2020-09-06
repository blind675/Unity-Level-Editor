using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour {

	public LevelManager levelManager;

	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			levelManager.GoToNextLevel ();
		}
	}


}
