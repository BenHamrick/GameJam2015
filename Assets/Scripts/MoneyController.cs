using UnityEngine;
using System.Collections;

public class MoneyController : MonoBehaviour {

    bool didGet;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!didGet)
            {
                didGet = true;
                StartCoroutine(goTo(other.transform));
            }
        }
    }

    IEnumerator goTo(Transform position)
    {
        float time = 0f;
        while (true)
        {
            time += Time.deltaTime;
            if (time > 1f)
                time = 1f;
            transform.position = Vector2.Lerp(transform.position, position.position, time);
            yield return null;
        }
    }
}
