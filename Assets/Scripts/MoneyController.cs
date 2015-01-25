using UnityEngine;
using System.Collections;

public class MoneyController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector2 direction = other.transform.position - transform.position;
            direction.Normalize();
            rigidbody2D.AddForce(direction * 100f);
        }
    }
}
