using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour {

	public void Destroy()
    {
        Destroy(gameObject);
    }
}
