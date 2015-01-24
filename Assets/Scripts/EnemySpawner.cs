using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public int amountToSpawn;
    int amountSpawnd;

    public GameObject enemyToSpawn;

    float randomTime = 0f;
    float time = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > randomTime && amountSpawnd < amountToSpawn)
        {
            amountSpawnd++;
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            randomTime = Random.Range(.1f, 2f);
            time = 0f;
        }
	}
}
