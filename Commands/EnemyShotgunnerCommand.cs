using GameProject.Factories;
using GameProject.Interfaces;

namespace GameProject.Commands;

public class EnemyShotgunnerCommand(Game1 game) : ICommand {
  public void Execute() {
    game.StateGame.CurrentSprite = EnemySpriteFactory.Instance.CreateShotgunnerSprite();
  }
}
