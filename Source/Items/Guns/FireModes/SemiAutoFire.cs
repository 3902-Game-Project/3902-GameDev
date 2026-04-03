using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Items;

public class SemiAutoFire(GunStats stats) : IFireMode {
  private float countdown = 0;

  public bool CanFire(UseType useType) {
    bool canFire = true;

    if (useType != UseType.Pressed) return false;

    if (stats.CurrentAmmo <= 0) {
      countdown = stats.ReloadTime;
      stats.CurrentAmmo = stats.MaxAmmo;
      stats.ReloadSFX.Play();
      return false;
    }

    if (countdown > 0) return false;

    countdown = stats.FireRate;
    stats.CurrentAmmo--;

    return canFire;
  }

  public void Update(GameTime gameTime) {
    countdown -= (float) gameTime.ElapsedGameTime.TotalSeconds;
  }
}
