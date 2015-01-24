using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorCollider : MonoBehaviour {

	private int characterCounter; 

	private List<CharacterController> charactersCrossed;

	void Awake(){
		characterCounter = 0;
		charactersCrossed = new List<CharacterController> ();

		collider2D.isTrigger = true;
	}

	void OnTiggerExit(Collider2D collider){
		if (collider.gameObject.GetComponent<CharacterController> () != null) {
			if(!charactersCrossed.Contains(collider.gameObject.GetComponent<CharacterController> ())){
				charactersCrossed.Add(collider.gameObject.GetComponent<CharacterController> ());

				characterCounter += 1;
			}
		}

		if (characterCounter < CharacterController.instance.Length) {
			CloseDoor();
		}
	}

	private void CloseDoor(){

		collider2D.isTrigger = false;
	}
}
