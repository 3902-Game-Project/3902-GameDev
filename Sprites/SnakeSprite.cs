using System;
using GameProject.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

internal class SnakeSprite(Texture2D snakeSpritesheet, int len, int width) : IEnemy {
  public void ChangeDirection() {
    throw new NotImplementedException();
  }

  public void Draw(SpriteBatch spriteBatch) {
    throw new NotImplementedException();
  }

  public void TakeDamage() {
    throw new NotImplementedException();
  }

  public void Update() {
    throw new NotImplementedException();
  }
}
