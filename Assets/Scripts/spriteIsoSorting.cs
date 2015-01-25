using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class spriteIsoSorting : MonoBehaviour {

    int layer = 0;
    float time;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > .05f)
        {
            if (layer != -(int)((collider2D.bounds.center.y - collider2D.bounds.extents.y) * 100f))
            {
                layer = -(int)((collider2D.bounds.center.y - collider2D.bounds.extents.y) * 100f);
                spriteRenderer.sortingOrder = layer;
            }
        }
	}
}
