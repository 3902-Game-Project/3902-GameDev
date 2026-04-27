using GameProject.SaveLoad;

namespace GameProject.Commands;

internal class SaveGameCommand(Game1 game) : IGPCommand {
  public void Execute() {
    SaveLoadManager.SaveGame(game.StateGame);
  }
}
