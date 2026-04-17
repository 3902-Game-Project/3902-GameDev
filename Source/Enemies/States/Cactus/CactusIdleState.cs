using Microsoft.Xna.Framework;

namespace GameProject.Enemies.CactusStates;

internal class CactusIdleState : ICactusState {
  private readonly Cactus cactus;

  public CactusIdleState(Cactus cactus) {
    this.cactus = cactus;

    this.cactus.Velocity = Vector2.Zero;

    this.cactus.CurrentSourceRectangles = [
      new(228,55,222,264)
    ];
    this.cactus.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) { }
}
