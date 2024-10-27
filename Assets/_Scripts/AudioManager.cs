using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

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

    [SerializeField] private AudioSource hitFX;
    [SerializeField] private AudioSource blockDestroyFX;
    [SerializeField] private AudioSource clickFX;
    [SerializeField] private AudioSource loseLifeFX;
    [SerializeField] private AudioSource extraLifeFX;
    [SerializeField] private AudioSource newLevelFX;
    [SerializeField] private AudioSource startGameFX;
    [SerializeField] private AudioSource gameOverFX;

    public void PlayHitFX()
    {
        hitFX.Play();
    }
    public void PlayblockDestroyFX()
    {
        blockDestroyFX.Play();
    }
    public void PlayclickFX()
    {
        clickFX.Play();
    }
    public void PlayloseLifeFX()
    {
        loseLifeFX.Play();
    }
    public void PlayextraLifeFX()
    {
        extraLifeFX.Play();
    }
    public void PlaynewLevelFX()
    {
        newLevelFX.Play();
    }
    public void PlaystartGameFX()
    {
        startGameFX.Play();
    }
    public void PlaygameOverFX()
    {
        gameOverFX.Play();
    }
}
