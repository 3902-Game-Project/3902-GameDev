using GameProject.GameStates;
using GameProject.SaveLoad;

namespace GameProject.Commands;

internal class ExecuteLoadCommand(Game1 game, StateLoadPromptType promptState) : IGPCommand {
  public void Execute() {
    if (!promptState.IsShowingSuccess) {
      SaveLoadManager.LoadGame(game.StateGame);
      promptState.IsShowingSuccess = true;
    }
  }
}
