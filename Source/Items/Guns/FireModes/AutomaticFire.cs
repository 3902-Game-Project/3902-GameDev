using GameProject.Controllers;
using GameProject.Items;
using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.FireModes;

internal class AutomaticFire(GunStats stats) : IFireMode {
  private double countdown = 0.0;

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

  public void OnEquip() {
    if (stats.CurrentAmmo <= 0) {
      countdown = stats.ReloadTime;
      SoundManager.Instance.Play(stats.ReloadID);
    }
  }

  public void OnUnequip() {
    countdown = 0f;
  }

  public void Update(double deltaTime) {
    if (countdown > 0) {
      countdown -= deltaTime;
      if (countdown <= 0 && stats.CurrentAmmo <= 0) {
        stats.CurrentAmmo = stats.MaxAmmo;
      }
    }
  }
}
