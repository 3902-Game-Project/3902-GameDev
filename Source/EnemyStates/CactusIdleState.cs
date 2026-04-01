using System.Collections.Generic;
using GameProject.Enemies;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class CactusIdleState : ICactusState {
  private readonly CactusSprite cactus;

  public CactusIdleState(CactusSprite cactus) {
    this.cactus = cactus;

    this.cactus.Velocity = Vector2.Zero;

    this.cactus.CurrentSourceRectangles = [
      new(228,55,222,264)
    ];
    this.cactus.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) { }
}
