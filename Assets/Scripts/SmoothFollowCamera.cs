using UnityEngine;
using System.Collections;

public class SmoothFollowCamera : MonoBehaviour {
	
	public Transform target;
	Transform t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tpos = target.position;
		tpos.z = t.position.z;
		t.position = Vector3.Lerp( t.position, tpos, Time.deltaTime );
	}
}
