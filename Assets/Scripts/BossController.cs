using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossController : MonoBehaviour {

	public float health;

	public float maxHealth;

	public float healthPercentage;

	bool didGetHit;


	// Use this for initialization
	void Awake () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		healthPercentage = health / maxHealth;

	}

	public void Hit(int damage){
		health -= damage;

		GetComponent<SpriteRenderer>().color = Color.gray;
		if (!didGetHit)
		{
			didGetHit = true;
			StartCoroutine(gotHit());
		}
	}

	IEnumerator gotHit()
	{

		yield return new WaitForSeconds(.1f);
		GetComponent<SpriteRenderer>().color = Color.white;

		didGetHit = false;
	}

}
