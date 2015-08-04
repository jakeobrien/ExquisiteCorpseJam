using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using DG.Tweening;

public class InsanityEffect : AmBehaviour 
{
    public Twirl twirlEffect;
    public float spinSpeed = 50f;
    public float penaltyStrength = .01f;
    public DOTData moveCenterData;
    
    void OnEnable()
    {
		ScoreManager.scoreChanged += ScoreChanged;
    }

    void OnDisable()
    {
		ScoreManager.scoreChanged -= ScoreChanged;
    }

    void Start()
    {
        MoveCenter();
    }

    void Update()
    {
        twirlEffect.angle = -180f + Mathf.PingPong( Time.time * spinSpeed, 360f );
    }

    public void MoveCenter()
    {
        Vector3 newRandomCenter = new Vector2( Random.value, Random.value );
        JuicyDOT.To( () => twirlEffect.center, x => twirlEffect.center = x, newRandomCenter, moveCenterData ).OnComplete( MoveCenter );
    }

    public void ScoreChanged( int score )
    {
        float scoreMod = Mathf.Min( 0f, (float)score );
        twirlEffect.radius = Vector2.one * Mathf.Abs(scoreMod) * penaltyStrength;
    }
}
