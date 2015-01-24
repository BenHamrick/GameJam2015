using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public static int playerCounter;
	public static CharacterController[] instance;

	public int money;
	public int health;
	private int tempMoneyLoss;

	public float moneyPercentage;



	void Awake(){
		if (instance != null) {
			instance = new CharacterController[4];
			playerCounter = 0;
		}
		else{
			playerCounter += 1;
		}

		instance [playerCounter] = this;

		ResetCharacter ();
	}

	public void ResetCharacter(){
		money = 0;
	}

	public int HitByEnemy(int damage){

		health -= damage;

		if (health > 0) {
			tempMoneyLoss = (int)(money * moneyPercentage);
			
			money -= tempMoneyLoss;

			return tempMoneyLoss;
		}
		else{
			return money;
		}
	}

}
