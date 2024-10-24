using System.Collections;
using UnityEngine;
using static GameManager;

public class GameOverState : IGameState
{
	public GameManager.GameState CurrentState { get; set; }

	public GameOverState()
	{
		this.CurrentState = GameState.GameOver;
	}

	public void EnterState()
	{
	}
}
