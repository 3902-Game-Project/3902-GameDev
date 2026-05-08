namespace GameProject.Commands;

internal class MetaCommand(IGPCommand[] commands) : IGPCommand {
  internal void Execute() {
    foreach (var command in commands) {
      command.Execute();
    }
  }
}
