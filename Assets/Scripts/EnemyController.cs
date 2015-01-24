using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    float time = 0f;
    bool needsMoney = false;
    PolyNavAgent agent;
    CharacterController characterToHunt = null;

    bool didGetHit;

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
        time -= Time.deltaTime;
        if(time < 0f)
        {
            time = .5f;
            characterToHunt = findCharacterTochase();
            agent.SetDestination(characterToHunt.transform.position);
        }
	}

    public void Hit(int amount)
    {
        GetComponent<SpriteRenderer>().color = Color.grey;
        if (!didGetHit)
        {
            didGetHit = true;
            StartCoroutine(gotHit());
        }
        health -= amount;
    }

    IEnumerator gotHit()
    {
        yield return new WaitForSeconds(.01f);
        GetComponent<SpriteRenderer>().color = Color.white;
        didGetHit = false;
    }

    CharacterController findCharacterTochase()
    {
        CharacterController playerOfInterest = CharacterController.instance[0];
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
