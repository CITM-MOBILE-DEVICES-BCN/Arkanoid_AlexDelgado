using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct PlayerSerializableData
{
    public int score;
    public int highscore;
    public uint lives;
    public uint level;
}

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public PlayerSerializableData data;

    public bool canContinue;
    public bool continueGame;
    public string filePath = "/settings.gamedata";

    PlayerData()
    {
        canContinue = File.Exists(filePath);
    }

    public void IncreaseScore(Brick.Type brickType)
    {
        switch(brickType)
        {
            case Brick.Type.grey:
                data.score += 3;
                break;
            default:
                data.score++;
                break;

        }
        if (data.score >= data.highscore)
        {
            data.highscore = data.score;
        }
        Debug.Log("High Score: " + data.highscore);
        Debug.Log("Score: " + data.score);
    }
    public void InitializeData()
    {
        data.score = 0;
        data.level = 0;
        data.lives = 3;
        Debug.Log("Lives: " + data.lives);
    }

    public void IncreaseLives()
    {
        data.lives++;
        Debug.Log("Lives: " + data.lives);
    }

    public void DecreaseLives()
    {
        data.lives--;

        if (data.lives < 1)
        {
            GameManager.Instance.StateManager.ChangeState(new GameOverState());
        }

        Debug.Log("Lives: " + data.lives);
    }

    private void Save()
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fileStream, data);
        fileStream.Close();
    }

    public void Load()
    {
        if(File.Exists(filePath))
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            data = (PlayerSerializableData)bf.Deserialize(fileStream);
            fileStream.Close();
        }
    }

    public string GetLevel()
    {
        if(data.level == 0)
        {
            Debug.Log("Returning level1");
            return "Level1";
        }
        else if(data.level == 1)
        {
            Debug.Log("Returning level2");
            return "Level2";
        }

        return "";
    }

    public void AdvanceLevel()
    {
        if(data.level == 1)
        {
            Debug.Log("Setting level to 0");
            data.level = 0;
        }
        else
        {
            Debug.Log("Setting level to 1");
            data.level = 1;
        }

        Save();
    }

}
