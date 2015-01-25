using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossTriggerController : MonoBehaviour {

	public List<GameObject> tenList;
	
	public List<GameObject> hoardSpawners;
	
	private int hoardsSpawned = 0;

	public BossController bossController;

	private int characterCounter; 
	
	private List<CharacterController> charactersCrossed;
	
	
	public bool onPlatform = false;
	
	private int tempCount = 0;
	
	private bool beatBoss = false;
	
	private bool bossFight = false;


	// Use this for initialization
	void Awake () {
	
		for(int i = 0; i < hoardSpawners.Count; i += 1){
			hoardSpawners[i].SetActive(false);
		}

		characterCounter = 0;
		charactersCrossed = new List<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (bossFight) {
			if (bossController.healthPercentage < 0.75F && hoardsSpawned == 0) {
				hoardSpawners[0].SetActive(true);
				hoardsSpawned = 1;
			}
			
			if (bossController.healthPercentage < 0.5F  && hoardsSpawned == 1) {
				hoardSpawners[1].SetActive(true);
				
				hoardsSpawned = 2;
			}
			
			if (bossController.healthPercentage < 0.25F && hoardsSpawned == 2) {
				hoardSpawners[2].SetActive(true);
				
				hoardsSpawned = 3;
			}

			if (!bossController.animation.isPlaying){
				bossController.animation.Play();
			}
		}


	}

	void OnTriggerEnter2D(Collider2D collider){
		
		if (!onPlatform && !beatBoss) {
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
				BossFight(true);
				onPlatform = true;
			}		
		}
		
		if(onPlatform && beatBoss){
			
			if (collider.gameObject.GetComponent<CharacterController> () != null) {
				
				if(!charactersCrossed.Contains(collider.gameObject.GetComponent<CharacterController> ())){
					
					
					charactersCrossed.Add(collider.gameObject.GetComponent<CharacterController> ());
					
					characterCounter += 1;
				}
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D collider){
		
		if (!onPlatform && !beatBoss) {
			if (collider.gameObject.GetComponent<CharacterController> () != null) {
				
				if(charactersCrossed.Contains(collider.gameObject.GetComponent<CharacterController> ())){
					
					
					charactersCrossed.Remove(collider.gameObject.GetComponent<CharacterController> ());
					
					characterCounter -= 1;
					
					BossFight(false);
				}
			}
			
		}
		
		if (onPlatform && !beatBoss) {
			if (collider.gameObject.GetComponent<CharacterController> () != null) {
				
				if(charactersCrossed.Contains(collider.gameObject.GetComponent<CharacterController> ())){
					
					
					charactersCrossed.Remove(collider.gameObject.GetComponent<CharacterController> ());
					
					characterCounter -= 1;
					
					BossFight(false);
				}
			}
			
		}
		
		if (characterCounter == 0) {
			
			BossFight(false);

			onPlatform = false;
		}
	}
	
	private void BossFight(bool fight){
		bossFight = fight;

        StartCoroutine(activate(fight));
		
		
	}

    IEnumerator activate(bool fight)
    {
        for (int i = 0; i < tenList.Count; i += 1)
        {
            yield return new WaitForSeconds(Random.Range(0f, 2f));
            tenList[i].SetActive(fight);
        }
    }
}
