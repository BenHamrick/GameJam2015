using UnityEngine;
using System.Collections;
using InControl;

public class CharacterController : MonoBehaviour {

	public static int playerCounter;
	public static CharacterController[] instance;

	public int playerIndex;
	public int money;
	public int health;
	private int tempMoneyLoss;

	public float moneyPercentage;

	private Vector2 aimDirection = Vector2.zero;

	public Transform weapon;


	void Awake(){
		if (instance == null) {
			instance = new CharacterController[4];
			playerCounter = 0;
		}
		else{
			playerCounter += 1;
		}

		playerIndex = playerCounter;

		instance [playerCounter] = this;

		aimDirection = Vector2.zero;

		ResetCharacter ();
	}

	void Update(){
	
		aimDirection = InputManager.Devices [playerIndex].RightStick;

		RaycastHit2D hit = Physics2D.Raycast(weapon.position, aimDirection);
		Debug.DrawLine (weapon.position, ((Vector3)weapon.position + (Vector3)aimDirection * 10000.0F) , Color.red);

		print (aimDirection);



	}

	public void ResetCharacter(){
		money = 0;
	}

	public int Hit(int damage){

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
