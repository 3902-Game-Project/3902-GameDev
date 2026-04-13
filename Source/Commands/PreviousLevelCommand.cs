namespace GameProject.Commands;

internal class PreviousLevelCommand(ILevelManager levelManager) : ICommand {
  public void Execute() {
    levelManager.PreviousLevel();
  }
}
