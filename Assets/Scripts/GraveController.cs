using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class GraveController : MonoBehaviour {

    CharacterController playerToRevive;
    CharacterController playerDoingReviving;
    int reviveState;

    public Sprite graveNormal;
    public Sprite graveRevive;

    public Slider slider;

    float time;

    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
	    if(InputManager.Devices[playerDoingReviving.playerIndex].Action1)
        {
            if (time > .1f)
            {
                spriteRenderer.sprite = graveRevive;
                time = 0f;
                slider.value = reviveState;
                reviveState+=5;
            }
        }
        else
        {
            spriteRenderer.sprite = graveNormal;
            slider.value = reviveState;
            reviveState = 0;
        }
	}

    public void SetPlayer(CharacterController controller)
    {
        playerToRevive = controller;
    }

    public void IncrementRevive(int amount)
    {
        reviveState += amount;
        if(reviveState >= 100)
        {
            playerToRevive.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharacterController>() != null && playerDoingReviving == null)
        {
            playerDoingReviving = other.GetComponent<CharacterController>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null && playerDoingReviving != null)
        {
            playerDoingReviving = null;
        }
    }
}
