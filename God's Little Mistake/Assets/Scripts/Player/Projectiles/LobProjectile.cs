using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveProjectile : MonoBehaviour
{
    public float targetDistance = 10f;   // The desired target distance
    public float rotationAngle = 45f;    // The rotation angle in degrees
    public float gravity = 9.81f;        // Acceleration due to gravity

    private float initialSpeed;
    private float launchAngle;

    private Vector3 initialPosition;
    private float elapsedTime = 0f;
    private bool isShooting = false;

    private void Start()
    {
        initialPosition = transform.position;

        // Calculate the initial speed and launch angle for the given target distance and rotation angle
        CalculateInitialSpeedAndAngle(targetDistance, rotationAngle);
    }

    private void Update()
    {
        if (isShooting)
        {
            // Calculate the projectile's position using the equations of motion
            float x = initialPosition.x + initialSpeed * Mathf.Cos(launchAngle) * elapsedTime;
            float y = initialPosition.y + initialSpeed * Mathf.Sin(launchAngle) * elapsedTime - 0.5f * gravity * elapsedTime * elapsedTime;
            float z = initialPosition.z;

            transform.position = new Vector3(x, y, z);

            elapsedTime += Time.deltaTime;

            if (Vector3.Distance(transform.position, initialPosition) > targetDistance)
            {
                // Projectile has reached the desired distance
                isShooting = false;
            }
        }
    }

    public void Shoot()
    {
        // Set the initial position and start shooting
        initialPosition = transform.position;
        elapsedTime = 0f;
        isShooting = true;

        // Calculate the initial velocity components
        float initialVelocityX = initialSpeed * Mathf.Cos(launchAngle);
        float initialVelocityY = initialSpeed * Mathf.Sin(launchAngle);

        // Apply an initial force to the Rigidbody to start the projectile's motion
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(initialVelocityX, initialVelocityY, 0);
    }

    private void CalculateInitialSpeedAndAngle(float distance, float angle)
    {
        // Convert the launch angle to radians
        launchAngle = angle * Mathf.Deg2Rad;

        // Calculate the initial speed required to hit the target distance
        initialSpeed = Mathf.Sqrt((distance * gravity) / Mathf.Sin(2 * launchAngle));
    }
}
