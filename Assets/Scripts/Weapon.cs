using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public int damage;

    float time = 0f;

    bool canDoDamage = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > .1f)
        {
            collider2D.enabled = true;
            canDoDamage = true;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        CharacterController player = other.GetComponent<CharacterController>();
        if(player != null)
        {
            if (canDoDamage)
            {
                collider2D.enabled = false;
                canDoDamage = false;
                player.Hit(damage);
            }
        }
    }
}
