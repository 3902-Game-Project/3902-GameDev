using GameProject.Controllers;
using GameProject.Items;
using Microsoft.Xna.Framework;

namespace GameProject.FireModes;

internal class SemiAutoFire(GunStats stats) : IFireMode {
  private float timer = 0f;
  private bool triggerReleased = true;

  public bool CanFire(UseType useType) {
    if (useType == UseType.Released) {
      triggerReleased = true;
      return false;
    }

    // Only fire if the trigger was reset, the cooldown is done, we have bullets
    if (useType == UseType.Pressed && triggerReleased && timer <= 0 && stats.CurrentAmmo > 0) {
      triggerReleased = false;
      timer = stats.FireRate;
      return true;
    }
    return false;
  }

  public void Update(GameTime gameTime) {
    if (timer > 0) timer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
  }

  public void OnEquip() {
    triggerReleased = true;
  }

  public void OnUnequip() { }
}
