using UnityEngine;
using System.Collections;

public class TenticalController : MonoBehaviour
{
    enum State
    {
        crack, tentical
    }
    GameObject tentical;
    float time = 0f;

    SpriteRenderer spriteRenderer;

    public Sprite[] crack;

    int i = 0;

    State state;

	// Use this for initialization
	void Start () {
        state = State.crack;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        switch (state)
        {
            case State.crack:
                if (time > .5f)
                {
                    if (i < crack.Length)
                    {
                        i++;
                        spriteRenderer.sprite = crack[i];
                        time = 0f;
                    }
                    else
                    {
                        state = State.tentical;
                    }
                }
                break;
            case State.tentical:

                break;
            default:
                break;
        }
        
	}
}
