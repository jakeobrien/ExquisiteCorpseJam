using UnityEngine;
using System.Collections;

public class ScoreZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {

		if(other.attachedRigidbody.gameObject.tag == "SpawnItem")
			Debug.Log("SCORED!");

    }
}
