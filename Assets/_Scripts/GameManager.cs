using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class GameManager : MonoBehaviour
{
    #region Singleton
    
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] private PlayerData playerData;

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

    private bool changeScene;

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
        StateManager.OnChangeState += OnDeath;

        AudioManager.Instance.PlaynewLevelFX();
    }

    private void OnDestroy()
    {
        StateManager.OnChangeState -= OnDeath;
    }

    private void Update()
    {
        if(changeScene)
        {
            playerData.AdvanceLevel();
            SceneManager.LoadScene(playerData.GetLevel());
            changeScene = false;
        }
    }

    private void OnDeath(GameState state)
    {
        if(state == GameState.GameOver)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void AdvanceLevel()
    {
        changeScene = true;
    }

    public void IncreaseScore(Brick.Type brickType)
    {
        playerData.IncreaseScore(brickType);
    }
    public void IncreaseLives()
    {
        playerData.IncreaseLives();
    }
    public void SaveGame()
    {
        playerData.Save();
    }
}
