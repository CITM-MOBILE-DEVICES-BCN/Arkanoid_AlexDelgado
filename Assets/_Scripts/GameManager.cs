using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameStateManager = new GameStateManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public GameStateManager gameStateManager;

    public GameStateManager StateManager => gameStateManager;
    public GameState CurrentGameState { get { return gameStateManager.CurrentState; } }

    public enum GameState
    {
        Init,
        Play,
        Pause,
        GameOver
    }
    
    private void Start()
    {
        gameStateManager.ChangeState(new InitState());
    }
}
