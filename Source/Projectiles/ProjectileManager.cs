using System.Collections.Generic;
using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

internal class ProjectileManager : ITemporalUpdatable, IGPDrawable {
  private readonly List<IProjectile> projectiles;
  internal List<IProjectile> Projectiles => projectiles;

  internal ProjectileManager() {
    projectiles = [];
  }

  internal void Add(IProjectile projectile) {
    projectiles.Add(projectile);
  }

  internal void Remove(IProjectile projectile) {
    projectiles.Remove(projectile);
  }

  internal void ClearProjectiles() {
    projectiles.Clear();
  }

  internal void Update(double deltaTime) {
    for (int i = projectiles.Count - 1; i >= 0; i--) {
      projectiles[i].Update(deltaTime);
    }
  }

  internal void Draw(SpriteBatch spriteBatch) {
    foreach (var projectile in projectiles) {
      projectile.Draw(spriteBatch);
    }
  }
}
