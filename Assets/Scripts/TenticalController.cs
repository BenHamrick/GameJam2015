using UnityEngine;
using System.Collections;
using InControl;

public class TenticalController : MonoBehaviour
{
    public Transform gunPosition;

    public GameObject bullet;

    public GameObject moneyObject;

    public float health;

    Vector2 playerPosition;
    float time = -3f;

    SpriteRenderer sprtieRenderer;

    bool didGetHit;

	// Use this for initialization
	void Start () {
        sprtieRenderer = GetComponent<SpriteRenderer>();
	}

    float randomNumber;

	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;
        if (time > randomNumber)
        {
            randomNumber = Random.Range(.1f,.5f);
            time = 0f;
            Vector2 direction = playerPosition - (Vector2)gunPosition.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject bulletInstance = (GameObject)Instantiate(bullet, gunPosition.position, Quaternion.AngleAxis(angle, Vector3.forward));
            bulletInstance.GetComponent<SpriteRenderer>().color = Color.red;
            bulletInstance.GetComponent<Bullet>().isEnemy = true;
            bulletInstance.GetComponent<SpriteRenderer>().sortingOrder = sprtieRenderer.sortingOrder + 500;
            direction.Normalize();
            bulletInstance.rigidbody2D.AddForce(direction * 500f);
            for (int i = 0; i < InputManager.Devices.Count; i++)
            {
                playerPosition = CharacterController.instance[i].transform.position;
            }
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
        if (health <= 0f)
        {
            int length = Random.Range(5, 10);
            for (int i = 0; i < length; i++)
            {
                GameObject moneyGameObject = (GameObject)Instantiate(moneyObject, transform.position, Quaternion.identity);
                moneyGameObject.rigidbody2D.AddForce(Random.insideUnitCircle * 2000f);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator gotHit()
    {
        yield return new WaitForSeconds(.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        didGetHit = false;
    }
}
