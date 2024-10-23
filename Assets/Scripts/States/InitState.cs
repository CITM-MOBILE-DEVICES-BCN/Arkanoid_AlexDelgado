using UnityEngine;
using static GameManager;

public class InitState : IGameState
{
	public GameManager.GameState CurrentState { get; set; }

	public InitState()
	{
		this.CurrentState = GameState.Init;
	}

	public void EnterState()
	{
	}
}
