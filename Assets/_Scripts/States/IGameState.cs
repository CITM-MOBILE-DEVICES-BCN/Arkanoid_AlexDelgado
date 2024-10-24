public interface IGameState
{
	public GameManager.GameState CurrentState { get; set; }

	public void EnterState();
}
