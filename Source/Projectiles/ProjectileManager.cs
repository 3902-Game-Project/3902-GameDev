using System.Collections.Generic;
using GameProject.GlobalInterfaces;
using GameProject.Projectiles;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Managers;

internal class ProjectileManager : ITemporalUpdatable, IGPDrawable {
  private readonly List<IProjectile> projectiles;
  public List<IProjectile> Projectiles => projectiles;

  public ProjectileManager() {
    projectiles = [];
  }

  public void Add(IProjectile projectile) {
    projectiles.Add(projectile);
  }

  public void Remove(IProjectile projectile) {
    projectiles.Remove(projectile);
  }

  public void ClearProjectiles() {
    projectiles.Clear();
  }

  public void Update(double deltaTime) {
    for (int i = projectiles.Count - 1; i >= 0; i--) {
      projectiles[i].Update(deltaTime);
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    foreach (var projectile in projectiles) {
      projectile.Draw(spriteBatch);
    }
  }
}
