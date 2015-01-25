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

    public static int amount;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        amount++;
        if(amount == InputManager.Devices.Count)
        {
            for (int i = 0; i < CharacterController.instancePrivate.Length; i++)
            {
                CharacterController.instancePrivate[i].gameObject.SetActive(true);
            }
            Application.LoadLevel("test"); 
        }
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (playerDoingReviving)
        {
            if (InputManager.Devices[playerDoingReviving.playerIndex].Action1)
            {
                if (time > .1f)
                {
                    spriteRenderer.sprite = graveRevive;
                    time = 0f;
                    slider.value = (float)reviveState / 100f;
                    IncrementRevive(5);
                }
            }
            else
            {
                spriteRenderer.sprite = graveNormal;
                slider.value = (float)reviveState / 100f;
                reviveState = 0;
            }
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
            playerToRevive.health = 100;
            playerToRevive.gameObject.SetActive(true);
            playerToRevive.gameObject.transform.position = transform.position;
            amount--;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CharacterController>() != null && playerDoingReviving == null && other.GetComponent<CharacterController>() != playerToRevive)
        {
            playerDoingReviving = other.GetComponent<CharacterController>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<CharacterController>() != null && playerDoingReviving != null && other.GetComponent<CharacterController>() != playerToRevive)
        {
            playerDoingReviving = null;
        }
    }
}
