using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public GameObject bulletHit;
    public int damage;

    SpriteRenderer spriteRenderer;

    public bool isEnemy;

    float time;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > 10f)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnemy)
        {
            EnemyController controller = other.GetComponent<EnemyController>();
            BossController bossController = other.GetComponent<BossController>();
            TenticalController tenticalController = other.GetComponent<TenticalController>();
            if (tenticalController != null)
            {
                tenticalController.Hit(damage);
            }
            if (controller != null)
            {
                controller.Hit(damage);
            }

            if (bossController != null)
            {
                bossController.Hit(damage);
            }
        }
        else
        {
            CharacterController character = other.GetComponent<CharacterController>();
            if(character != null)
            {
                character.Hit(damage);
            }
        }
        if (bulletHit != null)
        {
            ((GameObject)Instantiate(bulletHit, transform.position, transform.rotation)).GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        }
        Destroy(gameObject);




    }
}
