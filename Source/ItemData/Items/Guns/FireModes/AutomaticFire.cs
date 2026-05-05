using GameProject.Controllers;
using GameProject.Items;

namespace GameProject.FireModes;

internal class AutomaticFire(GunStats stats) : IFireMode {
  private double countdown = 0.0;

  public bool CanFire(UseType useType) {
    if (useType != UseType.Pressed && useType != UseType.Held) {
      return false;
    }

    if (countdown > 0 || stats.CurrentAmmo <= 0) {
      return false;
    }

    countdown = stats.FireRate;
    return true;
  }

  public void OnEquip() { }

  public void OnUnequip() {
    countdown = 0;
  }

  public void Update(double deltaTime) {
    if (countdown > 0) {
      countdown -= deltaTime;
    }
  }
}
