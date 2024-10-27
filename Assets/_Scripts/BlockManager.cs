using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockManager : MonoBehaviour
{
    private int deadBlocks;

    void Start()
    {
        deadBlocks = FindObjectsOfType<Brick>().Length;
        Brick.OnBrickDestroy += OnBlockDestroy;
    }
    private void OnDestroy()
    {
        Brick.OnBrickDestroy -= OnBlockDestroy;
    }

    private void OnBlockDestroy(Brick brick)
    {
        AudioManager.Instance.PlayblockDestroyFX();
        deadBlocks--;
        if (deadBlocks < 1)
        {
            GameManager.Instance.AdvanceLevel();
        }
        GameManager.Instance.IncreaseScore(brick.type);
        if(1 == UnityEngine.Random.Range(1, 50))
        {
            AudioManager.Instance.PlayextraLifeFX();
            GameManager.Instance.IncreaseLives();
        }
    }
}
