using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Enemies;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class CactusIdleState : ICactusState {
  private CactusSprite cactus;

  public CactusIdleState(CactusSprite cactus) {
    this.cactus = cactus;

    this.cactus.Velocity = Vector2.Zero;

    this.cactus.CurrentSourceRectangles = new List<Rectangle> {
      new(228,55,221,267)
    };
    this.cactus.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) { }
}
