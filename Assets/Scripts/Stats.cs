using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	public static Stats instance;

	public int[] enemiesKilled;

	void Awake(){
		instance = this;
		enemiesKilled = new int[5];
	}

	public void EnemyKilled(int island){
		enemiesKilled [island] += 1;
	}

	public int GetEnemyKilled(int island){
		return enemiesKilled[island];
	}
}
