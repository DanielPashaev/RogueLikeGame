using UnityEngine;
using System.Collections;

public class WizardSpawner : MonoBehaviour
{
    public GameObject wizardPrefab;
    public float initialSpawnInterval = 15f; // slower than bandits
    public float spawnAcceleration = 0.99f;
    public float minSpawnInterval = 5f;
    public float spawnRadius = 12f;

    private float currentSpawnInterval;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnWizards());
    }

    IEnumerator SpawnWizards()
    {
        while (true)
        {
            SpawnWizard();
            yield return new WaitForSeconds(currentSpawnInterval);
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval * spawnAcceleration);
        }
    }

    void SpawnWizard()
    {
        Vector2 spawnOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0f);
        Instantiate(wizardPrefab, spawnPosition, Quaternion.identity);
    }
}
