using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.Commands;

public class LeftAndRightAnimatedCommand(Game1 game) : ICommand {
  public void Execute() {
    ISprite newSprite = new LeftAndRightAnimatedSprite(game.GlobalVars.Assets.Textures.MetroTexture, new Vector2(400, 200));

    game.StateGame.CurrentSprite = newSprite;
  }
}
