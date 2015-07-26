using UnityEngine;
using System.Collections;

public class Grabber : MonoBehavclops
{

    public LayerMask layersToGrab;

    private Vector3 _grabPosition;

    private bool _isGrabbing;
    private bool IsGrabbing
    {
        get { return _isGrabbing; }
        set
        {
            if (_isGrabbing == value) return;
            _isGrabbing = value;
            Cached<Rigidbody2D>().mass = (_isGrabbing ? 10f : 1f);
            if (_isGrabbing) _grabPosition = Position;
        }
    }

    private bool _shouldGrab;
    public bool ShouldGrab
    {
        get { return _shouldGrab; }
        set 
        {
            if (_shouldGrab == value) return;
            _shouldGrab = value;
            if (_isGrabbing && !_shouldGrab) IsGrabbing = false;
        }
    }

    public void FixedUpdate()
    {
        if (_isGrabbing) {
            Position = _grabPosition;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        DoCollision(collision);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        DoCollision(collision);
    }

    private void DoCollision(Collision2D collision)
    {
        if (!_shouldGrab) return;
        if (!layersToGrab.ContainsLayer(collision.gameObject.layer)) return;
        IsGrabbing = true;
    }
}
