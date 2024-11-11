using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spherePrefab;    // The sphere prefab to spawn
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f); // Size of the spawn area
    public float spawnInterval = 1f;   // Time interval between spawns

    private void Start()
    {
        // Start spawning spheres at regular intervals
        InvokeRepeating("SpawnSphere", 0f, spawnInterval);
    }

    private void SpawnSphere()
    {
        // Generate a random position within the spawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0f,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        // Adjust the spawn height and position relative to the Spawner
        Vector3 spawnPosition = transform.position + randomPosition + Vector3.up * 5f;

        // Instantiate the sphere at the calculated position
        Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
    }
}