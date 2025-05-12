using UnityEngine;
using System.Collections;

public class BanditSpawner : MonoBehaviour
{
    public GameObject banditPrefab;
    public float initialSpawnInterval = 8f;
    //how fast the interval of spawning becomes.
    public float spawnAcceleration = .98f;
    public float minSpawnInterval = 1f;
    public float spawnRadius = 10f;
    private float currentSpawnInterval;
    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnBandits());
    }

    IEnumerator SpawnBandits() {
        while(true) {
            SpawnBandit();
            yield return new WaitForSeconds(currentSpawnInterval);
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval * spawnAcceleration);
        }
    }

    void SpawnBandit() {
        Vector2 spawnOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0f);
        Instantiate(banditPrefab, spawnPosition, Quaternion.identity);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
