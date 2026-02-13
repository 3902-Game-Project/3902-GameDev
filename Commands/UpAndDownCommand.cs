using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Commands;

public class UpAndDownCommand : ICommand {
  private Game1 myGame;

  public UpAndDownCommand(Game1 game) {
    myGame = game;
  }

  public void Execute() {
    Texture2D texture = myGame.Texture;

    ISprite newSprite = new UpAndDownSprite(texture, new Vector2(400, 200));

    myGame.CurrentSprite = newSprite;
  }
}
