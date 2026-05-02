using GameProject.SaveLoad;

namespace GameProject.Commands;

internal class LoadGameCommand(Game1 game) : IGPCommand {
  public void Execute() {
    SaveLoadManager.LoadGame(game.StateGame);
  }
}
