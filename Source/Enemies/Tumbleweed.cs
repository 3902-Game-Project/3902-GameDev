using GameProject.Collisions;
using GameProject.Enemies.States;
using GameProject.Enemies.TumbleweedStates;
using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Tumbleweed : ABaseEnemy {
  public Tumbleweed(Texture2D texture, Vector2 position) :
    base(texture, position, Constants.BASE_ENEMY_WIDTH * 0.75f, Constants.BASE_ENEMY_HEIGHT * 0.75f) {
    DrawScale = 0.4f;
    FlipOnRightDir = true;
    CurrentState = new TumbleweedIdleState(this);
  }

  protected override void TransitionToDeathState() {
    CurrentState = new GenericDeathState(
      this,
      [
        new(383, 227, 137, 106),
      ]
    );
  }

  public override void OnCollision(CollisionInfo info) {
    base.OnCollision(info);
    if (info.Collider.Layer == Layer.Environment) {
      Velocity *= -1;
    }
  }
}
