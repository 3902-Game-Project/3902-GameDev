using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.Items;

public class AutomaticFire(GunStats stats) : IFireMode {
  private float countdown = 0;

  public bool CanFire(UseType useType) {
    if (stats.CurrentAmmo > 0) {
      if (countdown > 0) return false;
      countdown = stats.FireRate;
    } else if (useType == UseType.Pressed) {
      countdown = stats.ReloadTime;
      stats.CurrentAmmo = stats.MaxAmmo;
      SoundManager.Instance.Play(stats.ReloadID);
      return false;
    }

    stats.CurrentAmmo--;

    return true;
  }

  public void Update(GameTime gameTime) {
    countdown -= (float) gameTime.ElapsedGameTime.TotalSeconds;
  }
}
