using UnityEngine;
using System.Collections;

public class ScoreZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		if(other.attachedRigidbody.gameObject.tag == "SpawnItem")
			Debug.Log("SCORED!");

    }
}
