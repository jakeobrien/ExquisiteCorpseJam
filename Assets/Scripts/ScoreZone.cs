using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScoreZone : MonoBehaviour 
{
	
	public string scoreTag = "SpawnItem";
	
    public int colorValue;
    public int hitPoints = 5;
	public Transform scoreEffect;
	public Transform penaltyEffect;

    private int _currentHitPoints;
    private Vector3 _originalSize;

    void Awake()
    {
        _currentHitPoints = hitPoints;
        _originalSize = transform.localScale;
    }

	void OnCollisionEnter2D(Collision2D other) 
    {
        Ball ball = other.transform.GetComponent<Ball>();
        if ( ball == null ) return;
        if ( !ball.IsStickingToPlayer() ) return;

        if ( colorValue == ball.colorValue )
        {
			//Debug.Log("SCORED!");
			ScoreManager.Score();
			
			if ( scoreEffect )
				Instantiate( scoreEffect, other.transform.position, Quaternion.identity );
	
			Destroy( other.transform.gameObject );

            RemoveHitPoint();
        } else {
			//Debug.Log("PENALTY");
			ScoreManager.Penalty();
			
			if ( penaltyEffect )
				Instantiate( penaltyEffect, other.transform.position, Quaternion.identity );

			Destroy( other.transform.gameObject );
		}
    }

    private void CheckDeath()
    {
        if ( _currentHitPoints <= 0 ) 
        {
            Destroy( this.gameObject );
            ScoreManager.SetScore( 0 );
        }
    }

    private void RemoveHitPoint()
    {
        Vector3 punch = transform.localScale * .5f;
        float duration = .25f;
        int vibrato = 5;
        float elasticity = .25f;
        transform.DOPunchScale( punch, duration, vibrato, elasticity ).OnComplete( TweenToCurrentSize );

        _currentHitPoints--; 
    }

    private void TweenToCurrentSize()
    {
        Vector3 targetScale = _originalSize * ( (float)_currentHitPoints / (float)hitPoints );
        transform.DOScale( targetScale, .25f ).OnComplete( CheckDeath );
    }
}
