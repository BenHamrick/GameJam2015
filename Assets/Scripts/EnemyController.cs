using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    float time = 0f;
    bool needsMoney = false;
    PolyNavAgent agent;
    CharacterController characterToHunt = null;

    public GameObject moneyObject;

    public GameObject dethObject;

    bool didGetHit;

    public float health;

	public int island;

	// Use this for initialization
	void Start () {
        float random = Random.Range(.1f, .4f);
        transform.localScale = new Vector2(random, random);
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
            time = Random.Range(.75f, 2f);
            characterToHunt = findCharacterTochase();
            if(characterToHunt.transform != null)
                agent.SetDestination(characterToHunt.transform.position);
        }
	}

    public void Hit(int amount)
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
        if (!didGetHit)
        {
            didGetHit = true;
            StartCoroutine(gotHit());
        }
        health -= amount;
        if(health <= 0f)
        {
            int length = Random.Range(5, 10);
            for (int i = 0; i < length; i++)
            {
                GameObject moneyGameObject = (GameObject)Instantiate(moneyObject, transform.position, Quaternion.identity);
                moneyGameObject.rigidbody2D.AddForce(Random.insideUnitCircle * 2000f);
            }
            GameObject dethGameObject = (GameObject)Instantiate(dethObject, transform.position, Quaternion.identity);
            dethGameObject.transform.localScale = transform.localScale;
			Stats.instance.EnemyKilled(island);
            Destroy(gameObject);
        }
    }

    IEnumerator gotHit()
    {
        yield return new WaitForSeconds(.1f);
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
            if (player != null)
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
                    if (Vector2.Distance(player.transform.position, transform.position) < distance)
                    {
                        distance = Vector2.Distance(player.transform.position, transform.position);
                        playerOfInterest = player;
                    }
                }
            }
        }
        return playerOfInterest;
    }
}
