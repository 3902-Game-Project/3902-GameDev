using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Items;

public class AutomaticFire : IFireMode {
  GunStats stats;
  private float timeSinceLastShot;

  public AutomaticFire(GunStats stats) {
    this.stats = stats;
    timeSinceLastShot = 0f;
  }

  public bool CanFire(UseType useType) {
    bool canFire = true;
    if (timeSinceLastShot < stats.FireRate) canFire = false;
    if (canFire) timeSinceLastShot = 0f;
    return canFire;
  }

  public void Update(GameTime gameTime) {
    timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
  }
}
