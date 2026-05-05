using GameProject.Level;

namespace GameProject.Commands;

internal class ChangeLevelCommand(ChangeLevelCallback changeLevelCallback, string levelName) : IGPCommand {
  public void Execute() {
    changeLevelCallback(levelName);
  }
}
