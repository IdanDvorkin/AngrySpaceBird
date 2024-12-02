using UnityEngine;

public class ComplexAsteroidMovement : MonoBehaviour
{
    public float speed = 2f; // Reduced speed for more manageable movement
    public float amplitudeY = 2f; // Vertical amplitude
    public float frequencyY = 1f; // Vertical frequency
    public float amplitudeX = 1f; // Horizontal amplitude
    public float frequencyX = 0.5f; // Horizontal frequency
    public Vector3 baseDirection = Vector3.left; // General direction of movement

    private float timeOffset;

    void Start()
    {
        // Randomize the initial phase for variety
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        // Calculate vertical oscillation
        float offsetY = Mathf.Sin(Time.time * frequencyY + timeOffset) * amplitudeY;

        // Calculate horizontal oscillation
        float offsetX = Mathf.Cos(Time.time * frequencyX + timeOffset) * amplitudeX;

        // Calculate the new position
        Vector3 offset = new Vector3(offsetX, offsetY, 0f);
        Vector3 movement = baseDirection * speed * Time.deltaTime; // Continuous leftward movement

        // Apply the position to the asteroid
        transform.position += movement + offset * Time.deltaTime;

        // Rotate the asteroid for visual effect
        transform.Rotate(new Vector3(0f, 0f, speed * Time.deltaTime * 10f));
    }
}
