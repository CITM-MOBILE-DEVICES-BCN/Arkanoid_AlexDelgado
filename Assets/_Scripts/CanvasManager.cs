using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;


//[ExecuteInEditMode]
public class CanvasManager : MonoBehaviour
{
    private float baseWidth;
    private float baseHeight;
    private Vector3 initialScale;


    [SerializeField] PlayerData playerData;

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI highscore;
    [SerializeField] TextMeshProUGUI lives;

    [SerializeField] Canvas landscapeCanvas;
    [SerializeField] Canvas pauseCanvas;

    [SerializeField] GameObject map;

    void Start()
    {
        playerData.OnChangeScore += UpdateScore;
        playerData.OnChangeLives += UpdateLives;
        playerData.OnChangeHighscore += UpdateHighscore;

        UpdateScore(playerData.Score);
        UpdateLives(playerData.Lives);
        UpdateHighscore(playerData.Highscore);


        baseWidth = 1920f;
        baseHeight = 1080f;
        initialScale = map.transform.localScale;
        GameManager.Instance.StateManager.OnChangeState += OnPause;

        pauseCanvas.enabled = false;

    }

    private void OnDestroy()
    {
        playerData.OnChangeScore -= UpdateScore;
        playerData.OnChangeLives -= UpdateLives;
        playerData.OnChangeHighscore -= UpdateHighscore;
        GameManager.Instance.StateManager.OnChangeState -= OnPause;
    }

    private void Update()
    {
        Rescale();
    }

    private void UpdateLives(int l)
    {
        lives.text = l.ToString();
    }
    private void UpdateScore(int s)
    {
        score.text = s.ToString();
    }
    private void UpdateHighscore(int h)
    {
        highscore.text = h.ToString();
    }

    private void Rescale()
    {

        float scaleX = Screen.width / baseWidth;

        map.transform.localScale = initialScale * scaleX;
    }

    private void OnPause(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Pause)
        {
            pauseCanvas.enabled = true;
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
        GameManager.Instance.StateManager.ChangeState(new PlayState());
        AudioManager.Instance.PlayclickFX();
    }

    public void ExitGame()
    {
        AudioManager.Instance.PlayclickFX();
        GameManager.Instance.SaveGame();
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
