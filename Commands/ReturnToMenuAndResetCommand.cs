using GameProject.Interfaces;

namespace GameProject.Commands;

public class ReturnToMenuAndResetCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeState(game.StateMenu);
    // TODO: Reset game state here
  }
}
