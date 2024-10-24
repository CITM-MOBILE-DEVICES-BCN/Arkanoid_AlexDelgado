using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class InputAdapter : MonoBehaviour
{
    private InputActions controls;

    private void Awake()
    {
        controls = new InputActions();
    }
    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Shoot.started += ctx => PaddleShoot();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void PaddleShoot()
    {
        Debug.Log("Paddle Shot");
        if (GameManager.Instance.CurrentGameState == GameState.Init)
        {
            GameManager.Instance.StateManager.ChangeState(new PlayState());
        }
    }
}
