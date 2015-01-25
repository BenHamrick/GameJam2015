using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour {

	private int characterCounter; 

	private List<CharacterController> charactersCrossed;
	
	public GameObject door;

	public bool onPlatform = false;

	private int tempCount = 0;

	void Awake(){
		characterCounter = 0;
		charactersCrossed = new List<CharacterController> ();

		door.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D collider){

		if (!onPlatform) {
			if (collider.gameObject.GetComponent<CharacterController> () != null) {

				print (collider.gameObject.GetComponent<CharacterController> ().playerIndex + " Crossed"); 

				if(!charactersCrossed.Contains(collider.gameObject.GetComponent<CharacterController> ())){


					charactersCrossed.Add(collider.gameObject.GetComponent<CharacterController> ());
					
					characterCounter += 1;
				}
			}

			tempCount = 0;

			for(int i = 0; i < CharacterController.instance.Length; i += 1){
				if(CharacterController.instance[i] != null){
					tempCount += 1;
				}
			}

			if (characterCounter >= tempCount) {

				CloseDoor();
			}		
		}
	}

	private void CloseDoor(){
		door.SetActive (true);

		onPlatform = true;
	}
}
