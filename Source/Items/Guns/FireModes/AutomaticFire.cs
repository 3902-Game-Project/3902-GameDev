using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Items;

public class AutomaticFire : IFireMode {
  private float fireRate;
  private float timeSinceLastShot;

  public AutomaticFire(float fireRate) {
    this.fireRate = fireRate;
    timeSinceLastShot = 0f;
  }

  public bool CanFire(UseType useType) {
    return timeSinceLastShot >= fireRate;
  }

  public void Update(GameTime gameTime) {
    timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
  }
}
