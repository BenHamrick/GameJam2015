using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour {

	private int characterCounter; 

	private List<CharacterController> charactersCrossed;
	
	public GameObject doorEnter;
	public GameObject doorExit;

	public bool onPlatform = false;

	private int tempCount = 0;

	void Awake(){
		characterCounter = 0;
		charactersCrossed = new List<CharacterController> ();

		doorEnter.SetActive (false);

		doorExit.SetActive (true);
	}

	void OnTriggerEnter2D(Collider2D collider){

		if (!onPlatform) {
			if (collider.gameObject.GetComponent<CharacterController> () != null) {

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

				CloseEnterDoor();
			}		
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if (onPlatform) {
			if (collider.gameObject.GetComponent<CharacterController> () != null) {
				
				if(!charactersCrossed.Contains(collider.gameObject.GetComponent<CharacterController> ())){
					
					
					charactersCrossed.Remove(collider.gameObject.GetComponent<CharacterController> ());
					
					characterCounter -= 1;
				}
			}

		}

		if (characterCounter == 0) {
			CloseExitDoor();
		}
	}

	private void CloseEnterDoor(){
		doorEnter.SetActive (true);

		onPlatform = true;
	}

	private void CloseExitDoor(){
		doorExit.SetActive (true);
		
		onPlatform = false;
	}

	public void OpenExitDoor(){
		doorExit.SetActive (false);

	}
}
