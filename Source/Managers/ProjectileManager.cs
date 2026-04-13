using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Managers;

internal class ProjectileManager {
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

  public void Update(GameTime gameTime) {
    for (int i = projectiles.Count - 1; i >= 0; i--) {
      projectiles[i].Update(gameTime);
      if (projectiles[i].IsExpired) {
        projectiles.RemoveAt(i);
      }
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    foreach (var projectile in projectiles) {
      projectile.Draw(spriteBatch);
    }
  }
}
