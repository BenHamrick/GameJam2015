using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SecondIsland : MonoBehaviour {

	public List<GameObject> spawners;

	private bool triggered = false;

	void Awake(){
		for(int i = 0; i < spawners.Count; i += 1){
			spawners[i].SetActive(false);
		}

	}

	void OnTriggerEnter2D(Collider2D collider){

		if (!triggered) {
			if (collider.gameObject.GetComponent<CharacterController> () != null) {
				for(int i = 0; i < spawners.Count; i += 1){
					spawners[i].SetActive(true);
				}

				triggered = true;
			}	
		}

	}

}
