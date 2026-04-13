using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.Items;

internal class AutomaticFire(GunStats stats) : IFireMode {
  private float countdown = 0;

  public bool CanFire(UseType useType) {
    if (countdown > 0 || stats.CurrentAmmo <= 0) {
      return false;
    }
    countdown = stats.FireRate;

    stats.CurrentAmmo--;
    if (stats.CurrentAmmo <= 0) {
      countdown = stats.ReloadTime;
      SoundManager.Instance.Play(stats.ReloadID);
    }

    return true;
  }

  public void Update(GameTime gameTime) {
    if (countdown > 0) {
      countdown -= (float) gameTime.ElapsedGameTime.TotalSeconds;
      if (countdown <= 0 && stats.CurrentAmmo <= 0) {
        stats.CurrentAmmo = stats.MaxAmmo;
      }
    }
  }
}
