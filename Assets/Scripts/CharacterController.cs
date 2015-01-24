using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

	public static int playerCounter;
	public static CharacterController[] instance;

    public Slider healthSlider;

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

	void Awake(){
        xScale = transform.localScale.x;

		if (instance == null) {
			instance = new CharacterController[4];
			playerCounter = 0;
		}
		else{
			playerCounter += 1;
		}

		playerIndex = playerCounter;

		instance [playerCounter] = this;

		aimDirection = Vector2.zero;

		ResetCharacter ();

        renderer.castShadows = true;
        renderer.receiveShadows = true;
	}

	void Update(){

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
            animator.SetTrigger("UpLeft");

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
            animator.SetTrigger("DownLeft");

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

		rigidbody2D.velocity = moveDirection * force;
	}

	private void Shoot(){
		if(InputManager.Devices [playerIndex].RightTrigger){
			if(time < 0){
				time = rateOfFire;
                
				if(aimDirection != Vector2.zero){
                    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                    GameObject bulletInstance = (GameObject)Instantiate(bullet, weapon[weaponCount].position, Quaternion.AngleAxis(angle, Vector3.forward));
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

	public int Hit(int damage){
		PerlinShake.instance.PlayShake (shakeDuration, shakeSpeed, shakeMagnitude);

		health -= damage;
        healthSlider.value = (float)health / 100f;
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
