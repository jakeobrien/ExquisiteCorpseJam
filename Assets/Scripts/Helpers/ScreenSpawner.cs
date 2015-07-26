using UnityEngine;
using System.Collections;

public class ScreenSpawner : Spawner 
{

    protected override Vector3? GetSpawnPosition() 
    {
        var rect = ScreenHelper.Instance.Rect;
        return new Vector3(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax), 0f);
    }


}
