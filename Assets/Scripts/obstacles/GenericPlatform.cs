using UnityEngine;
using System.Collections;

public class GenericPlatform : MonoBehaviour 
{
    public void SetSize( Vector2 size )
    {
        transform.localScale = size;
    }
}
