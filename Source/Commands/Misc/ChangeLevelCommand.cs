using GameProject.Level;

namespace GameProject.Commands;

internal class ChangeLevelCommand(ChangeLevelCallback changeLevelCallback, string levelName) : IGPCommand {
  internal void Execute() {
    changeLevelCallback(levelName);
  }
}
