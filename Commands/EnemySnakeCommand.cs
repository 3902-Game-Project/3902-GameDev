using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Commands;

public class EnemySnakeCommand : ICommand {
  private Game1 myGame;

  public EnemySnakeCommand(Game1 game) {
    myGame = game;
  }

  public void Execute() {
    IEnemy snake = EnemySpriteFactory.Instance.CreateSnakeSprite();
    myGame.CurrentSprite = (ISprite)snake;
  }
}
