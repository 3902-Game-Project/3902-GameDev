using GameProject.Enemies.CactusStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Cactus : ABaseEnemy {
  public Cactus(Texture2D texture, Vector2 position) : base(texture, position, 32f, 64f) {
    DrawScale = 0.2f;
    FlipOnRightDir = false;
    CurrentState = new CactusIdleState(this);
  }

  public override void TakeDamage(int damage) { return; }
}
