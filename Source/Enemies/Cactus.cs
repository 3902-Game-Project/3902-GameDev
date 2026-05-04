using GameProject.Enemies.CactusStates;
using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Cactus : ABaseEnemy {
  public Cactus(Texture2D texture, Vector2 position) :
    base(
      texture: texture,
      position: position,
      colliderWidth: Constants.BASE_ENEMY_WIDTH * 0.5f,
      invulnerable: true
    ) {
    DrawScale = 0.2f;
    FlipOnRightDir = false;
    CurrentState = new CactusIdleState(this);
  }
}
