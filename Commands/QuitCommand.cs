using GameProject.Interfaces;

namespace GameProject.Commands;

public class QuitCommand(Game1 game) : ICommand {
  public void Execute() {
    game.Exit();
  }
}
