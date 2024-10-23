using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2[] predefinedDirections;
    private Vector2 velocity;

    [SerializeField] private float speed = 1;
    [SerializeField] private Transform paddleTarget;
    [SerializeField] private float paddleOffsetY = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        predefinedDirections = new Vector2[]
        {
            new Vector2(0, 1),         // Up
            new Vector2(0, -1),        // Down
            new Vector2(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2),   // 45 degrees
            new Vector2(-Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2),  // 45 degrees
            new Vector2(Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2),  // 45 degrees
            new Vector2(-Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2), // 45 degrees
            new Vector2(Mathf.Cos(Mathf.PI / 8), Mathf.Sin(Mathf.PI / 8)),  // 22.5 degrees
            new Vector2(-Mathf.Cos(Mathf.PI / 8), Mathf.Sin(Mathf.PI / 8)), // 22.5 degrees
            new Vector2(Mathf.Cos(Mathf.PI / 8), -Mathf.Sin(Mathf.PI / 8)), // 22.5 degrees
            new Vector2(-Mathf.Cos(Mathf.PI / 8), -Mathf.Sin(Mathf.PI / 8)),// 22.5 degrees
            new Vector2(Mathf.Cos(3 * Mathf.PI / 8), Mathf.Sin(3 * Mathf.PI / 8)),  // 67.5 degrees
            new Vector2(-Mathf.Cos(3 * Mathf.PI / 8), Mathf.Sin(3 * Mathf.PI / 8)), // 67.5 degrees
            new Vector2(Mathf.Cos(3 * Mathf.PI / 8), -Mathf.Sin(3 * Mathf.PI / 8)), // 67.5 degrees
            new Vector2(-Mathf.Cos(3 * Mathf.PI / 8), -Mathf.Sin(3 * Mathf.PI / 8)),// 67.5 degrees
        };

        transform.position = paddleTarget.position + new Vector3(0, paddleOffsetY, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed += 0.01f;
        Vector2 currentDirection = rb.velocity.normalized;
        Vector2 closestDirection = FindClosestDirection(currentDirection);
        rb.velocity = closestDirection * speed;
    }

    private Vector2 FindClosestDirection(Vector2 currentDirection)
    {
        Vector2 closestDirection = predefinedDirections[0];
        float closestDot = Vector2.Dot(currentDirection, closestDirection);

        foreach (Vector2 dir in predefinedDirections)
        {
            float dot = Vector2.Dot(currentDirection, dir);
            if (dot > closestDot)
            {
                closestDot = dot;
                closestDirection = dir.normalized;
            }
        }

        return closestDirection;
    }
}
