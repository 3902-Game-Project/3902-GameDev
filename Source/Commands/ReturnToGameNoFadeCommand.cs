using GameProject.Interfaces;

namespace GameProject.Commands;

public class ReturnToGameNoFadeCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeStateWithoutFading(game.StateGame);
  }
}
