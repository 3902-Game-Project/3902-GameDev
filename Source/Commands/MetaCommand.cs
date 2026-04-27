namespace GameProject.Commands;

internal class MetaCommand(IGPCommand[] commands) : IGPCommand {
  public void Execute() {
    foreach (var command in commands) {
      command.Execute();
    }
  }
}
