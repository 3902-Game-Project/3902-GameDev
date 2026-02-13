using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.Commands;

public class AnimatedSpriteCommand(Game1 game) : ICommand {
  public void Execute() {
    game.StateGame.CurrentSprite = new AnimatedSprite(game.GlobalVars.Assets.Textures.MetroTexture, new Vector2(400, 200));
  }
}
