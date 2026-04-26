using System.Collections.Generic;
using GameProject.GlobalInterfaces;
using GameProject.Misc;
using Microsoft.Xna.Framework;

namespace GameProject.Animations;

internal class Animation(List<Rectangle> frames, int fps) : ITemporalUpdatable {
  private readonly double frameDuration = 1f / fps;
  private int currentFrame = 0;
  private readonly GPTimer timer = new();

  public Rectangle CurrentFrame => frames[currentFrame];

  public void Update(double deltaTime) {
    timer.Update(deltaTime);

    if (timer.Time >= frameDuration) {
      currentFrame = (currentFrame + 1) % frames.Count;
      timer.OffsetTime(-frameDuration);
    }
  }
}
