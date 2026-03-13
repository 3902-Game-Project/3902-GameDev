using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Managers;

public class ProjectileManager {
  private List<IProjectile> projectiles;
  public List<IProjectile> Projectiles => projectiles;

  public ProjectileManager() {
    projectiles = new List<IProjectile>();
  }

  public void AddProjectile(IProjectile projectile) {
    projectiles.Add(projectile);
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
