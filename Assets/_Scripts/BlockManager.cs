using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockManager : MonoBehaviour
{
    private Brick[] bricks;
    private int deadBlocks;

    void Start()
    {
        bricks = FindObjectsOfType<Brick>();
        deadBlocks = bricks.Length;

        Brick.OnBrickDestroy += OnBlockDestroy;
    }

    private void OnBlockDestroy(Brick brick)
    {
        deadBlocks--;
        if (deadBlocks < 1)
        {
            GameManager.Instance.AdvanceLevel();
        }
        GameManager.Instance.IncreaseScore(brick.type);
    }
}
