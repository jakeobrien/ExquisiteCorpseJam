using UnityEngine;
using System.Collections;

public class AmBehaviour : MonoBehaviour 
{
    // Controllers

    // Settings
    protected GameplaySettings _gameplaySettings { get { return GameplaySettings.Instance; } }

    // Components
    Transform _t;
    public Transform t 
    {
        get 
        { 
            if ( _t == null )
            {
                _t = this.transform;
            }

            return _t;
        }
    }

    Rigidbody _rb;
    public Rigidbody rb 
    {
        get 
        { 
            if ( _rb == null )
            {
                _rb = this.GetComponent<Rigidbody>();
            }

            return _rb;
        }
    }

    Rigidbody2D _rb2D;
    public Rigidbody2D rb2D 
    {
        get 
        { 
            if ( _rb2D == null )
            {
                _rb2D = this.GetComponent<Rigidbody2D>();
            }

            return _rb2D;
        }
    }
}
