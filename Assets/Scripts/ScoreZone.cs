using UnityEngine;
using System.Collections;

public class ScoreZone : MonoBehaviour {
	
	public string scoreTag = "SpawnItem";
	
	public Transform scoreEffect;
	public Transform penaltyEffect;

	void OnCollisionEnter2D(Collision2D other) {

		if(other.transform.gameObject.tag == scoreTag) {
			//Debug.Log("SCORED!");
			ScoreManager.Score();
			
			if ( scoreEffect )
				Instantiate( scoreEffect, other.transform.position, Quaternion.identity );
	
			Destroy( other.transform.gameObject );
		} else  if ( other.transform.gameObject.tag != "Player" ) {
			//Debug.Log("PENALTY");
			ScoreManager.Penalty();
			
			if ( penaltyEffect )
				Instantiate( penaltyEffect, other.transform.position, Quaternion.identity );

			Destroy( other.transform.gameObject );
		}

    }
}
