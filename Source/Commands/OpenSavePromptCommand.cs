using GameProject.GameStates;
namespace GameProject.Commands;

internal class OpenSavePromptCommand(Game1 game) : IGPCommand {
  public void Execute() => game.ChangeStateWithoutFading(game.StateSavePrompt);
}
