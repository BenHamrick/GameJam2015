using UnityEngine;
using System.Collections;

public class Island1 : MonoBehaviour {

	public RoomController roomController;

	public int island;

	public int deadEnemyNumber;

	void Update(){
	
		if (Stats.instance.GetEnemyKilled (island) >= deadEnemyNumber) {
			roomController.OpenExitDoor();
		}
	}
}
