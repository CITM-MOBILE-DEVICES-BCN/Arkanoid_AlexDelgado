using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputActions controls;
    private float dir;
    private float screenLimitLeft;
    private float screenLimitRight;
    private float playerWidth;

    [SerializeField] private float speed = 5;
    [SerializeField] private float wallWidth = 1;
    [SerializeField] private Transform transformLimitLeft;
    [SerializeField] private Transform transformLimitRight;

    private void Awake()
    {
        controls = new InputActions();
    }
    private void Start()
    {
        Camera mainCamera = Camera.main;
        screenLimitLeft = transformLimitRight.position.x;
        screenLimitRight = transformLimitLeft.position.x;
        playerWidth = GetComponentInChildren<SpriteRenderer>().bounds.extents.x;


    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        dir = controls.Player.Move.ReadValue<float>();
        Move();
    }

    void Move()
    {
        transform.Translate(new Vector3(dir * Time.deltaTime * speed, 0, 0));

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, screenLimitLeft + playerWidth + wallWidth, screenLimitRight - playerWidth - wallWidth);
        transform.position = clampedPosition;
    }
}
