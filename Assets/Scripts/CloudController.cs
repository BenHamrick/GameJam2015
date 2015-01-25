using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {

    public GameObject[] clouds;

    float time;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        time-=Time.deltaTime;
	    if(time < 0f)
        {
            time = Random.Range(0f, 4f);
            Instantiate(clouds[Random.Range(0,clouds.Length - 1)], (Vector2)transform.position + new Vector2(20f, 0f) + (Random.insideUnitCircle *  10f), Quaternion.identity);
        }
	}
}
