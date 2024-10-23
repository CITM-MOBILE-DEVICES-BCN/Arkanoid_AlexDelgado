using UnityEngine;
using static GameManager;

public class PauseState : IGameState
{
	public GameManager.GameState CurrentState { get; set; }
	public PauseState()
	{
		this.CurrentState = GameState.Pause;
	}

	public void EnterState()
	{
	}
}
