using UnityEngine;
using System.Collections;

public class GenericPlatform : MonoBehaviour 
{
    public Transform endOne;
    public Transform endTwo;

    public void SetSize( Vector2 size )
    {
        transform.localScale = size;

        endOne.localScale = endTwo.localScale = new Vector3( .05f, 3f, 0f );

        endOne.localPosition = new Vector3( .5f, 0f, 0f );
        endTwo.localPosition = -endOne.localPosition;
    }
}
