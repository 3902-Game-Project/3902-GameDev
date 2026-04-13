using GameProject.GlobalInterfaces;

namespace GameProject.Commands;

internal class QuitCommand(Game1 game) : ICommand {
  public void Execute() {
    game.Exit();
  }
}
