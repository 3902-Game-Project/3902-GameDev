using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Items;

public class SemiAutoFire : IFireMode {
  private GunStats stats;
  private float timeSinceLastShot;

  public SemiAutoFire(GunStats stats) {
    this.stats = stats;
    timeSinceLastShot = 0f;
  }

  public bool CanFire(UseType useType) {
    bool canFire = true;
    if (useType != UseType.Pressed) canFire = false;
    if (timeSinceLastShot >= stats.FireRate) canFire = false;
    if (stats.CurrentAmmo == 0) canFire = false;
    if (canFire) {
      timeSinceLastShot = 0f;
    }
    return canFire;
  }

  public void Update(GameTime gameTime) {
    timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
  }
}
