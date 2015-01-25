using UnityEngine;
using System.Collections;

public class CloudMover : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (Vector2)transform.position + new Vector2(-Time.deltaTime,0);
	}
}
