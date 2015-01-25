using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public AudioClip defaultMusic;

	public AudioClip bossMusic;

	public AudioClip bossSeenMusic;

	public static MusicController instance;

	private bool bossSeen = false;

	void Awake(){
		instance = this;
	}
	// Use this for initialization
	void Start () {
		//audio.PlayOneShot (defaultMusic);
	}
	
	// Update is called once per frame
	void Update () {
		if (!bossSeen) {
			if (!audio.isPlaying) {
				audio.clip = defaultMusic;
				audio.Play ();
			}	
		}

		else {
			if (!audio.isPlaying) {
				audio.clip = bossMusic;
				audio.Play ();
			}	
		}

	}

	public void BossSeen(){
		audio.Stop ();
		audio.clip = bossSeenMusic;
		audio.Play ();
		bossSeen = true;
	}
}

