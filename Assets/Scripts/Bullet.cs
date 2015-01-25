using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public GameObject bulletHit;
    public int damage;

    SpriteRenderer spriteRenderer;

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
        EnemyController controller = other.GetComponent<EnemyController>();
		BossController bossController = other.GetComponent<BossController> ();
        if (controller != null)
        {
            controller.Hit(damage);
        }

		if (bossController != null) {
			bossController.Hit(damage);
		}
        if (bulletHit != null)
        {
            ((GameObject)Instantiate(bulletHit, transform.position, transform.rotation)).GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        }
        Destroy(gameObject);




    }
}
