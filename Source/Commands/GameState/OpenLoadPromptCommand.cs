using GameProject.GameStates;

namespace GameProject.Commands;

internal class OpenLoadPromptCommand(Game1 game) : IGPCommand {
  internal void Execute() => game.StateMachine.ChangeStateWithoutFading(GameState.StateLoadPrompt);
}
