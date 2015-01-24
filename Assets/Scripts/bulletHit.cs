using UnityEngine;
using System.Collections;

public class bulletHit : MonoBehaviour {

    float time = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > .3f)
        {
            Destroy(gameObject);
        }
	}
}
