using DefaultNamespace;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject penguinPrefab; // The penguin projectile prefab
    public GameObject catPrefab; // The cat projectile prefab
    public Transform player; // Reference to the player's position
    public UnknownComponent trajectoryVisualizer; // Reference to UnknownComponent for trajectory visualization

    public float maxLaunchPower = 50f;
    public float minLaunchPower = 10f;
    public int trajectoryPoints = 50; // Number of points for trajectory visualization

    private GameObject currentProjectilePrefab; // The currently selected projectile
    private float launchPower;
    private bool isDragging = false;
    private Vector3 initialMousePosition;

    void Start()
    {
        // Set the default projectile to the penguin
        currentProjectilePrefab = penguinPrefab;
    }

    void Update()
    {
        HandleProjectileSwitching(); // Check for switching projectiles
        HandleMouseInput(); // Handle aiming and firing
    }

    private void HandleProjectileSwitching()
    {
        // Switch to penguin with key '1'
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentProjectilePrefab = penguinPrefab;
            Debug.Log("Switched to Penguin projectile");
        }
        // Switch to cat with key '2'
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentProjectilePrefab = catPrefab;
            Debug.Log("Switched to Cat projectile");
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // On mouse button press
        {
            StartAiming();
        }
        else if (Input.GetMouseButton(0)) // While dragging
        {
            AdjustLaunchPower();
            CalculateAndDisplayTrajectory(); // Recalculate trajectory on drag
        }
        else if (Input.GetMouseButtonUp(0)) // On mouse button release
        {
            FireProjectile();
            ClearTrajectory();
        }
    }

    private void StartAiming()
    {
        isDragging = true;
        initialMousePosition = Input.mousePosition;
        launchPower = minLaunchPower;
    }

    private void AdjustLaunchPower()
    {
        if (!isDragging) return;

        // Calculate power based on vertical mouse drag distance
        Vector3 currentMousePosition = Input.mousePosition;
        float dragDistance = initialMousePosition.y - currentMousePosition.y;

        // Normalize drag distance to maxLaunchPower range
        float maxDragDistance = Screen.height * 0.5f;
        float normalizedDrag = Mathf.Clamp(dragDistance / maxDragDistance, 0f, 1f);

        // Map normalized drag to launch power range
        launchPower = Mathf.Lerp(minLaunchPower, maxLaunchPower, normalizedDrag);
    }

    private Vector3 CalculateLaunchVelocity()
    {
        // Velocity calculation
        return player.forward * launchPower * 0.7f + player.up * launchPower * 1.2f;
    }

    private void CalculateAndDisplayTrajectory()
    {
        if (trajectoryVisualizer == null) return;

        List<Vector3> points = new List<Vector3>();

        // Start trajectory from player's current position
        Vector3 velocity = CalculateLaunchVelocity();
        Vector3 position = player.position;
        Vector3 gravity = Physics.gravity;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            points.Add(position);
            position += velocity * Time.fixedDeltaTime;
            velocity += gravity * Time.fixedDeltaTime;
        }

        // Move the visualizer to the player's position
        trajectoryVisualizer.transform.position = player.position;

        // Convert trajectory points to local space relative to the visualizer
        for (int i = 0; i < points.Count; i++)
        {
            points[i] = trajectoryVisualizer.transform.InverseTransformPoint(points[i]);
        }

        // Render the trajectory ribbon
        trajectoryVisualizer.UnknownMethod(points);
    }

    private void ClearTrajectory()
    {
        if (trajectoryVisualizer != null)
        {
            trajectoryVisualizer.UnknownMethod(new List<Vector3>()); // Pass an empty list

            // Reset the position of the visualizer
            trajectoryVisualizer.transform.position = player.position;
        }
    }

    private void FireProjectile()
    {
        if (!isDragging) return;

        isDragging = false;

        // Calculate the spawn position slightly ahead of the player
        Vector3 spawnPosition = player.position + player.forward * 1.5f;

        // Instantiate the currently selected projectile
        GameObject projectile = Instantiate(currentProjectilePrefab, spawnPosition, Quaternion.identity);

        // Ignore collision between the projectile and the player
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), player.GetComponent<Collider>());

        // Apply velocity to the Rigidbody
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = CalculateLaunchVelocity();
        }

        // Clear the trajectory ribbon immediately after firing
        ClearTrajectory();
    }
}
