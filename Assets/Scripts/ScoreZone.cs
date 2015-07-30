using UnityEngine;
using System.Collections;

public class ScoreZone : MonoBehaviour {
	
	public string scoreTag = "SpawnItem";
	
	public Transform scoreEffect;
	public Transform penaltyEffect;

	void OnTriggerEnter2D(Collider2D other) {

		if(other.attachedRigidbody.gameObject.tag == scoreTag) {
			Debug.Log("SCORED!");
			ScoreManager.Score();
			
			if ( scoreEffect )
				Instantiate( scoreEffect, other.attachedRigidbody.GetComponent<Transform>().position, Quaternion.identity );
	
			Destroy( other.attachedRigidbody.gameObject );
		} else  if ( other.attachedRigidbody.gameObject.tag != "Player" ) {
			Debug.Log("PENALTY");
			ScoreManager.Penalty();
			
			if ( penaltyEffect )
				Instantiate( penaltyEffect, other.attachedRigidbody.GetComponent<Transform>().position, Quaternion.identity );

			Destroy( other.attachedRigidbody.gameObject );
		}

    }
}
