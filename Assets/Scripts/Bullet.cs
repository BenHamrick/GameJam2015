using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage;

    float time;

	// Use this for initialization
	void Start () {
	
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
        if (controller != null)
        {
            controller.Hit(damage);
        }
        Destroy(gameObject);
    }
}
