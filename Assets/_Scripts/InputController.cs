using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class InputController : MonoBehaviour
{
    private InputActions controls;
    private bool modeIA = false;

    [SerializeField] private Transform ballTransform;

    private void Awake()
    {
        controls = new InputActions();
    }
    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Shoot.started += ctx => PaddleShoot();
        controls.Player.SwitchMode.started += ctx => SwitchMode();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        SetPlayerDirection();
    }

    private void PaddleShoot()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Init)
        {
            GameManager.Instance.StateManager.ChangeState(new PlayState());
        }
    }

    private void SwitchMode()
    {
        modeIA = !modeIA;
    }

    private void SetPlayerDirection()
    {
        if (modeIA)
        {
            PaddleShoot();

            Transform playerTransform = PlayerController.Instance.transform;

            float offset;
            if (playerTransform.position.x < ballTransform.position.x)
            {
                offset = 0.1f;
            }
            else
            {
                offset = -0.1f;
            }

            if (playerTransform.position.x + offset < ballTransform.position.x)
            {
                PlayerController.Instance.SetDir(1f, modeIA);
            }
            else
            {
                PlayerController.Instance.SetDir(-1f, modeIA);
            }
        }
        else
        {
            PlayerController.Instance.SetDir(controls.Player.Move.ReadValue<float>());
        }
    }
}
