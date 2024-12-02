using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Array of asteroid prefabs
    public float spawnInterval = 2f; // Time between spawns
    public Vector2 spawnArea = new Vector2(10f, 5f); // Width and height of the spawn area
    public float asteroidSpeed = 2f; // Base speed of the asteroids

    private void Start()
    {
        // Start spawning asteroids
        InvokeRepeating(nameof(SpawnAsteroid), 0f, spawnInterval);
    }

    private void SpawnAsteroid()
    {
        if (asteroidPrefabs.Length == 0)
        {
            Debug.LogError("No asteroid prefabs assigned to the spawner!");
            return;
        }

        // Randomly select an asteroid type
        int randomIndex = Random.Range(0, asteroidPrefabs.Length);
        GameObject selectedAsteroid = asteroidPrefabs[randomIndex];

        // Randomize the spawn position within the spawn area
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(5f, spawnArea.x), // Spawn farther to the right
            transform.position.y + Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f),
            transform.position.z
        );

        // Instantiate the selected asteroid
        GameObject asteroid = Instantiate(selectedAsteroid, spawnPosition, Quaternion.identity);

        // Set the asteroid's speed
        ComplexAsteroidMovement movement = asteroid.GetComponent<ComplexAsteroidMovement>();
        if (movement != null)
        {
            movement.speed = asteroidSpeed;
        }
    }
}
