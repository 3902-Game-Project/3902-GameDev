using GameProject.Enemies.BatStates;
using GameProject.Enemies.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Bat : ABaseEnemy {
  public Bat(Texture2D texture, Vector2 position) : base(texture, position, 64f, 64f) {
    DrawScale = 2f;
    FlipOnRightDir = true;
    CurrentState = new BatIdleState(this);
  }

  protected override void TransitionToDeathState() {
    CurrentState = new GenericDeathState(this, [new(3, 20, 25, 11)]);
  }
}
