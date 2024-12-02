using UnityEngine;

public class PenguinCollision : MonoBehaviour
{
    public int scorePerHit = 100;
    private AudioSource audioSource;
    private void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void FixedUpdate()
    {
        // Perform a raycast along the penguin's trajectory
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 velocity = rb.velocity;
        if (velocity.magnitude < 0.1f) return; 

        RaycastHit hit;
        if (Physics.Raycast(transform.position, velocity.normalized, out hit, 1.5f))
        {
            if (hit.collider.CompareTag("Asteroid"))
            {
                HandleCollision(hit.collider.gameObject);
            }
        }
    }

    private void PlayCollisionSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void HandleCollision(GameObject asteroid)
    {
        PlayCollisionSound();
        Destroy(asteroid);
        Destroy(gameObject, 0.5f); // Delay penguin destruction to allow sound to play
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(scorePerHit);
        }
    }
}
