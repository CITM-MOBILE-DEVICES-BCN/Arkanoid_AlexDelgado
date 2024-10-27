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
    public int lives;
    public uint level;
    public bool died;
}

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public PlayerSerializableData data;

    public bool continueGame;
    public string filePath = "/settings.gamedata";

    public event Action<int> OnChangeLives;
    public event Action<int> OnChangeScore;
    public event Action<int> OnChangeHighscore;

    public int Score {  get { return data.score; } }
    public int Lives { get { return data.lives; } }
    public int Highscore { get { return data.highscore; } }

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
            OnChangeHighscore?.Invoke(data.highscore);
        }

        OnChangeScore?.Invoke(data.score);
    }
    public void InitializeData()
    {
        data.score = 0;
        data.level = 0;
        data.lives = 3;
        data.died = false;
    }

    public void IncreaseLives()
    {
        data.lives++;

        OnChangeLives?.Invoke(data.lives);
    }

    public void DecreaseLives()
    {
        data.lives--;

        if (data.lives < 1)
        {
            GameManager.Instance.StateManager.ChangeState(new GameOverState());
            data.died = true;
        }

        OnChangeLives?.Invoke(data.lives);
    }

    public void Save()
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fileStream, data);
        fileStream.Close();

        Debug.Log("Saving Game");
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
            return "Level1";
        }
        else if(data.level == 1)
        {
            return "Level2";
        }

        return "";
    }

    public void AdvanceLevel()
    {
        if(data.level == 1)
        {
            data.level = 0;
        }
        else
        {
            data.level = 1;
        }

        Save();
    }

    public bool CanContinue()
    {
        return File.Exists(filePath) && !data.died;
    }

}
