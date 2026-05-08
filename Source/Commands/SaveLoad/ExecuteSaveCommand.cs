using GameProject.GameStates;
using GameProject.SaveLoad;

namespace GameProject.Commands;

internal class ExecuteSaveCommand(Game1 game, StateSavePromptType promptState) : IGPCommand {
  public void Execute() {
    if (!promptState.IsShowingSuccess) {
      SaveLoadManager.SaveGame(game.StateMachine.StateGame);
      promptState.IsShowingSuccess = true;
    }
  }
}
