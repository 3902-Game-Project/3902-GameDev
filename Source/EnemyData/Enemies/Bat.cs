using GameProject.Enemies.BatStates;
using GameProject.Enemies.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Bat : ABaseEnemy {
  internal Bat(Texture2D texture, Vector2 position) : base(texture, position) {
    DrawScale = 2f;
    FlipWhenFacingRightUpDown = true;
    CurrentState = new BatIdleState(this);
  }

  private protected override void TransitionToDeathState() {
    CurrentState = new GenericDeathState(
      this,
      [
        new(3, 20, 25, 11),
      ]
    );
  }
}
