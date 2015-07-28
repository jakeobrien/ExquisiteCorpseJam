using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Spawner : MonoBehaviour
{

    [System.Serializable]
    public class SpawnItem
    {
        public GameObject prefab;
        [Range(0,1)]
        public float weight;
    }

    [Tooltip("Objects to spawn per second")]
    public float frequency;
    [Tooltip("Frequency will vary randomly by half this amount in either direction")]
    public float frequencyVariance;
    [Tooltip("Number of objects to create at Start")]
    public int startCount;
    public bool autoStart;
    [Tooltip("Spawned objects will be childed to this. If null, the spawner will be the parent.")]
    public Transform spawnParent;
    [HideInInspector]
    public bool spawningEnabled = true;
    public SpawnItem[] spawnItems;
	[Tooltip("How long the spawner will be active")]
	public float spawnTime = 60.9f;
	[Tooltip("X and Y range from spawner objects will appear")]
	public Vector2 spawnExtents = new Vector2(0, 0);
	[Tooltip("UI text object that is the timer")]
    public Text textTimer;

    protected bool _hasStarted;

    //=================================
    // 
    // Overide in subclassses
    // 
    // ================================

    protected virtual bool CanSpawn()
    {
        return true;
    }

    protected virtual Vector3? GetSpawnPosition() 
    {
		return transform.position + new Vector3(Random.Range(-spawnExtents.x, spawnExtents.x), Random.Range(-spawnExtents.y, spawnExtents.y), 0);
    }

    protected virtual void ConfigureSpawn(GameObject spawn) { }


    //=================================
    // 
    // Public
    // 
    // ================================

    public void Spawn(int number)
    {
        for (int i = 0; i < number; i++) {
            Spawn();
        }
    }

    public void Spawn()
    {
        var pos = GetSpawnPosition();
        if (pos == null) return;
        var item = GetRandomSpawnPrefab();
        if (item == null) return;
        var obj = SpawnItemAtPos(item, pos.Value);
        ConfigureSpawn(obj);
    }

    public virtual void StartSpawning()
    {
        if (_hasStarted) return;
        NormalizeSpawnItemWeights();
        Spawn(startCount);
        _hasStarted = true;
        StartCoroutine(SpawnPeriodically());
    }


    //=================================
    // 
    // Private
    // 
    // ================================

    private void Start()
    {
        if (autoStart) StartSpawning();
    }

    private void Update()
    {
		System.TimeSpan t = System.TimeSpan.FromSeconds( spawnTime );
		textTimer.text = string.Format("{0:D1}:{1:D2}", t.Minutes, t.Seconds);

		spawnTime -= Time.deltaTime;
    }

    private void NormalizeSpawnItemWeights()
    {
        if (spawnItems == null || spawnItems.Length == 0) return;
        var totalSpawnWeight = 0f;
        foreach (var item in spawnItems) {
            totalSpawnWeight += item.weight;
        }
        if (totalSpawnWeight == 0f) totalSpawnWeight = 1f;
        foreach (var item in spawnItems) {
            item.weight *= 1 / totalSpawnWeight;
        }
    }

    private IEnumerator SpawnPeriodically()
    {
		while (spawnTime > 0) {
            yield return new WaitForSeconds(1f / GetVariedFrequency());
            if(spawningEnabled && CanSpawn()) Spawn();
        }
    }

    private float GetVariedFrequency()
    {
        if (frequencyVariance == 0f) return frequency;
        return frequency + Random.Range(-0.5f, 0.5f) * frequencyVariance;
    }

    private GameObject GetRandomSpawnPrefab()
    {
        if (spawnItems.Length == 0) return null;
        if (spawnItems.Length == 1) return spawnItems[0].prefab;
        var r = Random.Range(0f, 1f);
        foreach (var item in spawnItems) {
            if (r < item.weight) return item.prefab;
            r -= item.weight;
        }
        return null;
    }

    private GameObject SpawnItemAtPos(GameObject prefab, Vector3 pos)
    {
        var item = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
        var tr = item.transform;
        if (spawnParent == null) {
            tr.parent = transform;
        } else {
            tr.parent = spawnParent;
        }
        tr.position = pos;
        return item.gameObject;
    }

}
