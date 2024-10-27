using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            AudioManager.Instance.PlayloseLifeFX();
            playerData.DecreaseLives();
            GameManager.Instance.StateManager.ChangeState(new InitState());
        }
    }
}
