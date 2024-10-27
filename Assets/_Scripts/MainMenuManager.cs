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

    void Start()
    {
        if(!data.canContinue)
        {
            continueButton.interactable = false;
        }
    }

    public void NewGame()
    {
        data.Load();
        data.InitializeData();
        SceneManager.LoadScene("Level1");
    }

    public void LoadGame()
    {
        data.Load();
        SceneManager.LoadScene(data.GetLevel());
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
