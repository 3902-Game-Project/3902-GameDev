using GameProject.Enemies.SnakeStates;
using GameProject.Enemies.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Snake : ABaseEnemy {
  public Snake(Texture2D texture, Vector2 position) : base(texture, position, 64f, 32f) {
    DrawScale = 2f;
    FlipOnRightDir = true;
    CurrentState = new SnakeWanderState(this);
  }

  protected override void TransitionToDeathState() {
    CurrentState = new GenericDeathState(
      this,
      [
        new(76, 143, 8, 17),
        new(108, 143, 8, 17),
        new(140, 143, 8, 17),
        new(43, 146, 10, 14),
        new(171, 146, 9, 14),
        new(10, 147, 12, 13),
        new(203, 147, 11, 13),
        new(235, 151, 12, 9),
        new(267, 153, 13, 7),
        new(299, 154, 15, 6),
      ]
    );
  }
}
