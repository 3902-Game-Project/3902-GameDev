using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.Items;

public class SemiAutoFire(GunStats stats) : IFireMode {
  private float countdown = 0;

  public bool CanFire(UseType useType) {
    bool canFire = true;

    if (useType != UseType.Pressed) return false;

    if (countdown > 0) return false;

    countdown = stats.FireRate;
    stats.CurrentAmmo--;
    if (stats.CurrentAmmo <= 0) {
      countdown = stats.ReloadTime;
      stats.CurrentAmmo = stats.MaxAmmo;
      SoundManager.Instance.Play(stats.ReloadID);
    }

    return canFire;
  }

  public void Update(GameTime gameTime) {
    countdown -= (float) gameTime.ElapsedGameTime.TotalSeconds;
  }
}
