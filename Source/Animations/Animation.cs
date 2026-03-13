using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameProject.Animations;

public class Animation(List<Rectangle> frames, int fps) {
  private readonly float frameDuration = 1f / fps;
  private int currentFrame = 0;
  private float timer = 0f;

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

    timer += dt;
    if (timer >= frameDuration) {
      currentFrame = (currentFrame + 1) % frames.Count;
      timer -= frameDuration;
    }
  }

  public Rectangle CurrentFrame => frames[currentFrame];
}
