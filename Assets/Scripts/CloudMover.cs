using UnityEngine;
using System.Collections;

public class CloudMover : MonoBehaviour {

    float time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > 60f)
        {
            Destroy(gameObject);
        }
        transform.position = (Vector2)transform.position + new Vector2(-Time.deltaTime * .5f,0);
	}
}
