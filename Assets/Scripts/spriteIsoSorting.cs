using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class spriteIsoSorting : MonoBehaviour {

    int layer = 0;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
	// Update is called once per frame
	void Update () {
        if (layer != -(int)((collider2D.bounds.center.y - collider2D.bounds.extents.y) * 100f))
        {
            layer = -(int)((collider2D.bounds.center.y - collider2D.bounds.extents.y) * 100f);
            spriteRenderer.sortingOrder = layer;
        }
	}
}
