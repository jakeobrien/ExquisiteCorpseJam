using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {
	
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = "0";
		ScoreManager.scoreChanged += ScoreChanged;
	}
	
	void ScoreChanged( int score ) {
		text.text = score.ToString();
	}
}
