using UnityEngine;
using System.Collections;

public class CameraCollider : MonoBehaviour {

    public BoxCollider2D[] colliders;

	// Use this for initialization
	void Start () {
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
        colliders[0].center = new Vector2(bottomLeft.x - (colliders[0].size.x * .5f), 0f);
        colliders[1].center = new Vector2(topRight.x + (colliders[0].size.x * .5f), 0f);
        colliders[2].center = new Vector2(0f, bottomLeft.y - (colliders[0].size.y * .5f));
        colliders[3].center = new Vector2(0f, topRight.y + (colliders[0].size.y * .5f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
