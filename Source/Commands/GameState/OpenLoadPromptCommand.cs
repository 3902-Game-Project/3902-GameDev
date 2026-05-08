using GameProject.GameStates;

namespace GameProject.Commands;

internal class OpenLoadPromptCommand(Game1 game) : IGPCommand {
  public void Execute() => game.StateMachine.ChangeStateWithoutFading(GameState.StateLoadPrompt);
}
