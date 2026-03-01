using System.Diagnostics;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Items;

public class SemiAutoFire : IFireMode {
  private GunStats stats;
  private float timeSinceLastShot;

  public SemiAutoFire(GunStats stats) {
    this.stats = stats;
    timeSinceLastShot = stats.FireRate;
  }

  public bool CanFire(UseType useType) {
    bool canFire = true;
    if (useType != UseType.Pressed) canFire = false;
    if (timeSinceLastShot < stats.FireRate) canFire = false;
    if (stats.CurrentAmmo == 0) canFire = false;
    if (canFire) {
      timeSinceLastShot = 0f;
    }
    Debug.WriteLine($"TimeSinceLastShot: {timeSinceLastShot}");
    return canFire;
  }

  public void Update(GameTime gameTime) {
    timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
  }
}
