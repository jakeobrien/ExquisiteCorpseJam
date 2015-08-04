using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
    public int maxRings;
    private int _ring;
    private Rigidbody2D _rb;
    private Transform _playerTransform;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if ( _playerTransform == null ) return;
        MoveTowardPlayer();
    }

    /*
    void OnCollisionEnter2D( Collision2D col )
    {
        Ball ball = col.transform.GetComponent<Ball>();
        if ( ball == null || ball.IsStickingToPlayer() ) return;

        ball.StickToPlayer( this.transform );
    }
    */
    private void MoveTowardPlayer()
    {
        float playerRadius = _playerTransform.GetComponent<CircleCollider2D>().radius * 2.5f ;
        Vector3 attachmentPoint = _playerTransform.position + ( transform.position - _playerTransform.position ).normalized * playerRadius;

        Vector3 direction = ( attachmentPoint - transform.position );
        _rb.AddForce( direction, ForceMode2D.Impulse );
    }

    public void StickToPlayer( Transform playerTransform )
    {
        _playerTransform = playerTransform; 
    }

    public bool IsStickingToPlayer()
    {
        return _playerTransform != null;
    }
}
