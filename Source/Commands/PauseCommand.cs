using GameProject.GlobalInterfaces;

namespace GameProject.Commands;

internal class PauseCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeState(game.StatePause);
  }
}
