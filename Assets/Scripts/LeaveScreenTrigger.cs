using UnityEngine;
using System.Collections;

public class LeaveScreenTrigger : MonoBehaviour {

    public Spawner secondAreaSpawner;
	bool triggered = false;

	void OnTriggerEnter2D( Collider2D other ) {
		if ( !triggered && other.attachedRigidbody.tag == "Player" ) {
			Camera.main.GetComponent<SmoothFollowCamera>().enabled = true;
			triggered = true;
            secondAreaSpawner.forceOn = true;
            secondAreaSpawner.StartSpawning();
		}
	}
}
