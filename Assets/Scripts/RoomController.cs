using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour {

	private int characterCounter; 

	private List<CharacterController> charactersCrossed;
	
	public GameObject doorEnter;
	public GameObject doorExit;

	public GameObject doorEnterBeam;
	public GameObject doorExitBeam;

	public bool onPlatform = false;

	private int tempCount = 0;

	private bool challengeComplete = false;

	public GameObject polyNav;

	void Awake(){
		characterCounter = 0;
		charactersCrossed = new List<CharacterController> ();

		doorEnter.SetActive (false);
		doorEnterBeam.SetActive (false);

		doorExit.SetActive (true);
		doorExitBeam.SetActive (true);
	}

	void OnTriggerEnter2D(Collider2D collider){

		if (!onPlatform && !challengeComplete) {
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

		if(onPlatform && challengeComplete){

			if (collider.gameObject.GetComponent<CharacterController> () != null) {
				
				if(!charactersCrossed.Contains(collider.gameObject.GetComponent<CharacterController> ())){
					
					
					charactersCrossed.Add(collider.gameObject.GetComponent<CharacterController> ());
					
					characterCounter += 1;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider){

		if (onPlatform && challengeComplete) {
			if (collider.gameObject.GetComponent<CharacterController> () != null) {
				
				if(charactersCrossed.Contains(collider.gameObject.GetComponent<CharacterController> ())){
					
					
					charactersCrossed.Remove(collider.gameObject.GetComponent<CharacterController> ());
					
					characterCounter -= 1;
				}
			}

		}

		if (!onPlatform && !challengeComplete) {
			if (collider.gameObject.GetComponent<CharacterController> () != null) {
				
				if(charactersCrossed.Contains(collider.gameObject.GetComponent<CharacterController> ())){
					
					
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
		doorEnterBeam.SetActive (true);


		onPlatform = true;
	}

	private void CloseExitDoor(){
		print ("Close Exit Door");
		doorExit.SetActive (true);
		doorExitBeam.SetActive (true);

		onPlatform = false;
	}

	public void OpenExitDoor(){
		if (!challengeComplete) {
			print ("Open Exit Door");
			challengeComplete = true;
			doorExit.SetActive (false);
			doorExitBeam.SetActive (false);		
		}


	}
}
