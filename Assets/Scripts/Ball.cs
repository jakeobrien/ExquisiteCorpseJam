using UnityEngine;
using System.Collections;

public class Ball : AmBehaviour 
{
    public int colorValue;
    public CircleCollider2D collider;
    public float pushStrength = 5f;

    public int maxRings;
    private int _ring;
    private Player _player;
    private Transform _playerTransform;

    void OnEnable()
    {
        _gameplaySettings.OnArmExtended += OnArmExtended;
    }

    void OnDisable()
    {
        _gameplaySettings.OnArmExtended -= OnArmExtended;
    }

    void FixedUpdate()
    {
        if ( _playerTransform == null ) return;
        MoveTowardPlayer();

    }

    // Main Functions ==================================================
    public bool IsStickingToPlayer()
    {
        return _playerTransform != null;
    }

    private void MoveTowardPlayer()
    {
        float playerRadius = _playerTransform.GetComponent<CircleCollider2D>().radius * 3f ;
        Vector3 attachmentPoint = _playerTransform.position + ( transform.position - _playerTransform.position ).normalized * playerRadius;

        Vector3 direction = ( attachmentPoint - transform.position );
        rb2D.AddForce( direction, ForceMode2D.Impulse );
    }

    private void PushAwayFromArm()
    {
        if ( _player == null ) return;
        Vector3 directionOppositeArm = _player.GetDirectionAwayFromArm();
        rb2D.AddForce( directionOppositeArm * pushStrength, ForceMode2D.Impulse );
        Debug.Log("Hello?");
    }

    public void StickToPlayer( Transform playerTransform )
    {
        _player = playerTransform.GetComponent<Player>();
        _playerTransform = playerTransform; 
    }


    // Events ==================================================
    public void OnArmExtended()
    {
        PushAwayFromArm();    
    }
}
