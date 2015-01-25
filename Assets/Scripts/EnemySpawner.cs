using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public int amountToSpawn;
    int amountSpawnd;

    public RoomController roomController;
	public BossController bossController;

    public GameObject enemyToSpawn;

    float randomTime = 0f;
    float time = 0f;

	public int island;

	public bool bossIsland = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(roomController != null){
			if (roomController.onPlatform)
			{
				time += Time.deltaTime;
				if (time > randomTime && amountSpawnd < amountToSpawn)
				{
					amountSpawnd++;
					GameObject temp = (GameObject)Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
					temp.GetComponent<EnemyController>().island = island;
					randomTime = Random.Range(.1f, 2f);
					time = 0f;
				}
			}
		}

		else{
			if (bossIsland)
			{
				time += Time.deltaTime;
				if (time > randomTime && amountSpawnd < amountToSpawn)
				{
					amountSpawnd++;
					GameObject temp = (GameObject)Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
					temp.GetComponent<EnemyController>().island = island;
					randomTime = Random.Range(.1f, 2f);
					time = 0f;
				}
			}
		}

	}
}
