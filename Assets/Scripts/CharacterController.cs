using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

	public static int playerCounter;
    static CharacterController[] instancePrivate;
	public static CharacterController[] instance;

    public GameObject bullet;

	public int playerIndex;
	public int money;
	public int health;
	private int tempMoneyLoss;

	public float force;

	public float shakeDuration;

	public float shakeSpeed;

	public float shakeMagnitude;

	public float moneyPercentage;

	private Vector2 aimDirection = Vector2.zero;

	private Vector2 moveDirection;

	public Transform[] weapon;
    int weaponCount = 0;

	public int playerDamage;

	public float rateOfFire;

	private float time;

    public Animator animator;

    float xScale;

    int controllerAmount = 0;

    bool animatingUp;

    SpriteRenderer sprtieRenderer;

	void Awake(){
        sprtieRenderer = GetComponent<SpriteRenderer>();
        xScale = transform.localScale.x;

		if (instance == null) {
            instancePrivate = new CharacterController[4];
			instance = new CharacterController[4];
			playerCounter = 0;
		}
		else{
			playerCounter += 1;
		}

		playerIndex = playerCounter;

        instancePrivate[playerCounter] = this;
		instance [playerCounter] = this;

		aimDirection = Vector2.zero;

		ResetCharacter ();

        renderer.castShadows = true;
        renderer.receiveShadows = true;
	}

    void OnEnable()
    {
        if (instance[playerIndex] == null)
            transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        instance[playerIndex] = this;
    }

    void OnDisable()
    {
        instance[playerIndex] = null;
    }

	void Update(){
        if (controllerAmount != InputManager.Devices.Count)
        {
            controllerAmount = InputManager.Devices.Count;
            for (int i = 0; i < InputManager.Devices.Count; i++)
            {
                instancePrivate[i].gameObject.SetActive(true);
            }
            for (int i = InputManager.Devices.Count; i < 4; i++)
            {
                instancePrivate[i].gameObject.SetActive(false);
            }

        }

		time -= Time.deltaTime;
        if (InputManager.Devices.Count > playerIndex)
        {
            Move();

            Aim();


            Shoot();

            Animate();
        }
	}

    void Animate()
    {
        if (aimDirection.y < 0f)
        {
            if (animatingUp)
            {
                animatingUp = false;
                animator.SetTrigger("UpLeft");
            }

            if (aimDirection.x < 0f)
            {
                transform.localScale = new Vector3(xScale, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector3(-xScale, transform.localScale.y);
            }
        }
        else
        {
            if (!animatingUp)
            {
                animatingUp = true;
                animator.SetTrigger("DownLeft");
            }

            if (aimDirection.x < 0f)
            {
                transform.localScale = new Vector3(-xScale, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector3(xScale, transform.localScale.y);
            }
        }

        
    }

    void RunAnimation(string animationName)
    {
        if(!animationName.Equals(animator.name))
            animator.Play(animationName);
    }


	private void Aim(){
        if (InputManager.Devices[playerIndex].RightStick != Vector2.zero)
            aimDirection = InputManager.Devices[playerIndex].RightStick;
        else if(moveDirection != Vector2.zero)
            aimDirection = moveDirection;
	}

	private void Move(){

		moveDirection = InputManager.Devices [playerIndex].LeftStick;
        if(moveDirection != Vector2.zero)
        {
            animator.SetBool("Running",true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
		rigidbody2D.velocity = moveDirection * force;
	}

	private void Shoot(){
		if(InputManager.Devices [playerIndex].RightTrigger){
			if(time < 0){
				time = rateOfFire;
                
				if(aimDirection != Vector2.zero){
                    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                    GameObject bulletInstance = (GameObject)Instantiate(bullet, weapon[weaponCount].position, Quaternion.AngleAxis(angle, Vector3.forward));
                    bulletInstance.GetComponent<SpriteRenderer>().sortingOrder = sprtieRenderer.sortingOrder + 500;
                    weaponCount++;
                    if (weaponCount >= weapon.Length)
                        weaponCount = 0;
                    aimDirection.Normalize();
                    bulletInstance.rigidbody2D.AddForce(aimDirection * 500f);
				}

			}


		}
		else{

			if(time != rateOfFire){
				time = rateOfFire;
			}

		}
		
		
	}


	public void ResetCharacter(){
		money = 0;
	}

    IEnumerator vibrate()
    {
        InputManager.Devices[playerIndex].Vibrate(1f, 1f);
        yield return new WaitForSeconds(shakeDuration);
        InputManager.Devices[playerIndex].Vibrate(0f, 0f);
    }

	public int Hit(int damage){
        StartCoroutine(vibrate());
		PerlinShake.instance.PlayShake (shakeDuration, shakeSpeed, shakeMagnitude);

		health -= damage;
		if (health > 0) {
			tempMoneyLoss = (int)(money * moneyPercentage);
			
			money -= tempMoneyLoss;

			return tempMoneyLoss;
		}
		else{
			return money;
		}
	}

}
