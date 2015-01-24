using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    float time = 0f;
    bool needsMoney = false;
    PolyNavAgent agent;
    CharacterController characterToHunt;

    float health;

	// Use this for initialization
	void Start () {
        agent = GetComponent<PolyNavAgent>();
	    if(Random.Range(0,100) > 50)
        {
            needsMoney = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > 1f)
        {
            time = 0f;
            characterToHunt = findCharacterTochase();
        }
        if(characterToHunt != null)
            agent.SetDestination(characterToHunt.transform.position);
	}

    public void Hit(int amount)
    {
        health -= amount;
    }

    CharacterController findCharacterTochase()
    {
        CharacterController playerOfInterest = null;
        int mostMoney = 0;
        float distance = float.MaxValue;
        foreach (CharacterController player in CharacterController.instance)
        {
            if (needsMoney)
            {
                if (player.money > mostMoney)
                {
                    playerOfInterest = player;
                    mostMoney = player.money;
                }
            }
            else
            {
                if(Vector2.Distance(player.transform.position, transform.position) < distance)
                {
                    distance = Vector2.Distance(player.transform.position, transform.position);
                    playerOfInterest = player;
                }
            }
        }
        return playerOfInterest;
    }
}
