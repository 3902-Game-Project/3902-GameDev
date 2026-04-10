using GameProject.Interfaces;

namespace GameProject.Commands;

public class PauseCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeState(game.StatePause);
  }
}
