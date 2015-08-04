using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallHolder : AmBehaviour 
{
    public Player player;
    public float basePush;

    private List<Ball> __balls;
    private List<Ball> _balls
    {
        get
        {
            if ( __balls == null ) __balls = new List<Ball>();
            return __balls;
        }
    }

    void OnEnable()
    {
        _gameplaySettings.OnArmExtended += OnArmExtended;
        _gameplaySettings.OnBallDestroyed += OnBallDestroyed;
    }

    void OnDisable()
    {
        _gameplaySettings.OnArmExtended -= OnArmExtended;
        _gameplaySettings.OnBallDestroyed -= OnBallDestroyed;
    }

    void OnCollisionEnter2D( Collision2D col )
    {
        Ball ball = col.transform.GetComponent<Ball>();
        if ( ball == null || _balls.Contains( ball ) ) return;

        ball.StickToPlayer( this.transform );
        _balls.Add( ball );
    }

    // Events ==================================================
    public void OnArmExtended()
    {
        Vector3 direction = player.GetDirectionAwayFromArm();
        rb2D.AddForce( direction * basePush * (1+_balls.Count), ForceMode2D.Impulse );
        Debug.DrawRay( transform.position, direction * basePush * (1+_balls.Count), Color.red, 1f );
    }

    public void OnBallDestroyed( Ball ball )
    {
        if ( _balls.Contains( ball ) ) _balls.Remove( ball );
    }
}
