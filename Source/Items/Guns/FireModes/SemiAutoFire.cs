using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Items;

public class SemiAutoFire(GunStats stats) : IFireMode {
  private float timeSinceLastShot = stats.FireRate;

  public bool CanFire(UseType useType) {
    bool canFire = true;
    if (useType != UseType.Pressed) canFire = false;
    if (timeSinceLastShot < stats.FireRate) canFire = false;
    if (stats.CurrentAmmo == 0) canFire = false;
    if (canFire) {
      timeSinceLastShot = 0f;
    }
    return canFire;
  }

  public void Update(GameTime gameTime) {
    timeSinceLastShot += (float) gameTime.ElapsedGameTime.TotalSeconds;
  }
}
