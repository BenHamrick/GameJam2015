using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	private Vector2 negativeXPlayer;
	private Vector2 positiveXPlayer;
	private Vector2 negativeYPlayer;
	private Vector2 positiveYPlayer;

	private CharacterController negativeXPlayerC;
	private CharacterController positiveXPlayerC;
	private CharacterController negativeYPlayerC;
	private CharacterController positiveYPlayerC;

	private Vector3 newPosition;


	public float journeyTime = 1.0F;
	private float startTime;

	private float newX;
	private float newY;


	// Use this for initialization
	void Start () {
		negativeXPlayer = Vector2.zero;
		positiveXPlayer = Vector2.zero;
		negativeYPlayer = Vector2.zero;
		positiveYPlayer = Vector2.zero;

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
		negativeXPlayer = CharacterController.instance[0].transform.position;
        positiveXPlayer = CharacterController.instance[0].transform.position;
        negativeYPlayer = CharacterController.instance[0].transform.position;
        positiveYPlayer = CharacterController.instance[0].transform.position;

		for(int i = 0; i < CharacterController.instance.Length; i += 1){
            if (CharacterController.instance[i] != null)
            {
                if (CharacterController.instance[i].transform.position.x < negativeXPlayer.x)
                {
                    negativeXPlayer = CharacterController.instance[i].transform.position;

                    negativeXPlayerC = CharacterController.instance[i];
                }

                else if (CharacterController.instance[i].transform.position.x > positiveXPlayer.x)
                {
                    positiveXPlayer = CharacterController.instance[i].transform.position;

                    positiveXPlayerC = CharacterController.instance[i];
                }

                if (CharacterController.instance[i].transform.position.y < negativeYPlayer.y)
                {
                    negativeYPlayer = CharacterController.instance[i].transform.position;

                    negativeYPlayerC = CharacterController.instance[i];
                }

                else if (CharacterController.instance[i].transform.position.y > positiveYPlayer.y)
                {
                    positiveYPlayer = CharacterController.instance[i].transform.position;

                    positiveYPlayerC = CharacterController.instance[i];
                }
            }
		}

		if (positiveXPlayer.x > 0 && negativeXPlayer.x < 0) {
			newX = negativeXPlayer.x + ((float)(positiveXPlayer.x + Mathf.Abs(negativeXPlayer.x)) / 2.0f);
		}
		else{
			newX = negativeXPlayer.x + Mathf.Abs(((float)(Mathf.Abs(positiveXPlayer.x) - Mathf.Abs(negativeXPlayer.x)) / 2.0f));
		}

		if (positiveYPlayer.y > 0 && negativeYPlayer.y < 0) {
			newY = negativeYPlayer.y + ((float)(positiveYPlayer.y + Mathf.Abs(negativeYPlayer.y)) / 2.0f);
		}
		else {

			newY = negativeYPlayer.y + Mathf.Abs(((float)(Mathf.Abs(positiveYPlayer.y) - Mathf.Abs(negativeYPlayer.y)) / 2.0f));
		}



		newPosition = new Vector3 (newX, newY, transform.position.z);

		float fracComplete = (Time.time - startTime) / journeyTime;

		transform.position = Vector3.Slerp(transform.position, newPosition, fracComplete);
	}
}
