using GameProject.GameStates;

namespace GameProject.Commands;

internal class OpenSavePromptCommand(Game1 game) : IGPCommand {
  internal void Execute() => game.StateMachine.ChangeStateWithoutFading(GameState.StateSavePrompt);
}
