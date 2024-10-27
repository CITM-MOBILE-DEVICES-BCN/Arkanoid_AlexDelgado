using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2[] predefinedDirections;

    [SerializeField] private float speed = 1;
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float collisionIncrement = 0.1f;
    [SerializeField] private Transform paddleTarget;
    [SerializeField] private float paddleOffsetY = 1;
    private float paddleOffsetX;

    public float Speed { get { return speed; } private set { speed = value; } }

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
        paddleOffsetX = paddleTarget.gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
        transform.position = paddleTarget.position + new Vector3(paddleOffsetX, paddleOffsetY, 0);
        GameManager.Instance.StateManager.OnChangeState += OnLaunch;
    }

    private void OnEnable()
    {
    }

    private void OnDestroy()
    {
        GameManager.Instance.StateManager.OnChangeState -= OnLaunch;
    }

    private void Update()
    {
        if(GameManager.Instance.CurrentGameState == GameState.Init)
        {
            transform.position = paddleTarget.position + new Vector3(0, paddleOffsetY, 0);
        }
    }

    private void OnLaunch(GameState gameState)
    {
        if(gameState == GameState.Play)
        {
            Launch();
        }
    }

    private void Launch()
    {
        // TODO: hacer que la bola salga hacia donde se mueve el player
        rb.velocity = new Vector2(0f, speed);
    }

    private void Stay()
    {
        transform.position = paddleTarget.position + new Vector3(0, paddleOffsetY, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Instance.PlayHitFX();
        if(speed < maxSpeed)
        {
            speed += collisionIncrement;
        }
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
