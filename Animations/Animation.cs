using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameProject.Animations;

public class Animation {
  private readonly List<Rectangle> frames;
  private readonly float frameDuration;
  private int currentFrame;
  private float timer;

  public Animation(List<Rectangle> frames, int fps) {
    this.frames = frames;
    this.frameDuration = 1f / fps;
    currentFrame = 0;
    timer = 0f;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

    timer += dt;
    if (timer >= frameDuration) {
      currentFrame = (currentFrame + 1) % frames.Count;
      timer -= frameDuration;
    }
  }

  public Rectangle CurrentFrame => frames[currentFrame];
}
