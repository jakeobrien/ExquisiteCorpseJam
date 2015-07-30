using UnityEngine;
using System.Collections;

public class ScoreManager {
	
	public delegate void scoreEvent( int s );
	public static event scoreEvent scoreChanged;
	
	static int _score;
	
	public static int Score() {
		_score++;
		
		if ( scoreChanged != null ) {
			scoreChanged( _score );
		}
		
		return _score;
	}
	
	public static int Penalty() {
		_score--;
		
		if ( _score < 0 ) {
			_score = 0;
		}
		
		if ( scoreChanged != null ) {
			scoreChanged( _score );
		}
		
		return _score;
	}
	
	public static int GetScore() {
		return _score;
	}
}
