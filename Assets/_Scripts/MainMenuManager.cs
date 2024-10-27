using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    public Button continueButton;
    private bool changeScene;

    void Start()
    {
        AudioManager.Instance.PlaystartGameFX();
        if (!data.CanContinue())
        {
            continueButton.interactable = false;
        }
    }

    private void Update()
    {
        if (changeScene)
        {
            data.Load();
            SceneManager.LoadScene(data.GetLevel());
            changeScene = false;
        }
    }

    public void NewGame()
    {
        AudioManager.Instance.PlayclickFX();
        data.Load();
        data.InitializeData();
        SceneManager.LoadScene("Level1");
    }

    public void LoadGame()
    {
        AudioManager.Instance.PlayclickFX();
        changeScene = true;
    }

    public void ExitGame()
    {
        AudioManager.Instance.PlayclickFX();
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
