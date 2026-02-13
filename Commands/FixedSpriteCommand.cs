using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.Commands;

public class FixedSpriteCommand(Game1 game) : ICommand {
  public void Execute() {
    game.StateGame.CurrentSprite = new FixedSprite(game.GlobalVars.Assets.Textures.MetroTexture, new Vector2(400, 200));
  }
}
