using Microsoft.Xna.Framework;

namespace GameProject.Enemies.CactusStates;

internal class CactusIdleState : IEnemyState {
  internal CactusIdleState(Cactus cactus) {
    cactus.Velocity = Vector2.Zero;
    cactus.CurrentSourceRectangles = [
      new(228, 55, 222, 264),
    ];
    cactus.CurrentFrame = 0;
  }
  internal void Update(double deltaTime) { }
}
