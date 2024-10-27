using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton

    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private float dir;
    private float screenLimitLeft;
    private float screenLimitRight;
    private float playerWidth;

    [SerializeField] private float normalSpeed = 5;
    [SerializeField] private float IASpeed = 30;
    [SerializeField] private float wallWidth = 1;
    [SerializeField] private Transform transformLimitLeft;
    [SerializeField] private Transform transformLimitRight;

    private void Start()
    {
        Camera mainCamera = Camera.main;
        screenLimitLeft = transformLimitRight.position.x;
        screenLimitRight = transformLimitLeft.position.x;
        playerWidth = GetComponentInChildren<SpriteRenderer>().bounds.extents.x;


    }

    void Update()
    {
        Move();
    }

    public void SetDir(float directionX, bool modeIA = false)
    {
        if(modeIA)
        {
            dir = directionX * IASpeed;
        }
        else
        {
            dir = directionX * normalSpeed;
        }
    }

    void Move()
    {
        transform.Translate(new Vector3(dir * Time.deltaTime, 0, 0));

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, screenLimitLeft + playerWidth + wallWidth, screenLimitRight - playerWidth - wallWidth);
        transform.position = clampedPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 paddleCenter = transform.position;

            ballRB.velocity = Vector3.zero;

            Vector2 direction = hitPoint - paddleCenter;
            float ballSpeed = collision.gameObject.GetComponent<BallController>().Speed;

            ballRB.velocity = new Vector2(direction.normalized.x * ballSpeed, direction.normalized.y * ballSpeed);
        }
    }
}
