using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallHolder : MonoBehaviour 
{
    private List<Ball> __balls;
    private List<Ball> _balls
    {
        get
        {
            if ( __balls == null ) __balls = new List<Ball>();
            return __balls;
        }
    }

    void OnCollisionEnter2D( Collision2D col )
    {
        Ball ball = col.transform.GetComponent<Ball>();
        if ( ball == null || _balls.Contains( ball ) ) return;

        ball.StickToPlayer( this.transform );
    }
}
