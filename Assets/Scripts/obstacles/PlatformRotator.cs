using UnityEngine;
using System.Collections;

public class PlatformRotator : MonoBehaviour 
{
    public GenericPlatform platformPrefab;

    public int platformCount;
    public float platformRadius;
    public Vector2 platformSizeMin;
    public Vector2 platformSizeMax;

    void Awake()
    {
        SpawnPlatforms();
    }

    private void SpawnPlatforms()
    {
        float angleIncrement = 360f / platformCount;
        Vector2 platformSize = platformSizeMin + ( platformSizeMax - platformSizeMin ) * Random.value;

        for( int i = 0; i < platformCount; i++ )
        {
            Vector3 spawnPos = Quaternion.AngleAxis( i * angleIncrement, Vector3.back ) * Vector3.up * platformRadius;
            spawnPos += transform.position;
            SpawnPlatform( spawnPos, platformSize );
        }
    }

    private GenericPlatform SpawnPlatform( Vector3 spawnPos, Vector2 platformSize )
    {
        GenericPlatform newPlatform = transform.InstantiateChild( platformPrefab, spawnPos );
        newPlatform.SetSize( platformSize );

        return newPlatform;
    }
}
