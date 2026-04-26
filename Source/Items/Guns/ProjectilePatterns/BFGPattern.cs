using GameProject.Factories;
using GameProject.Items;
using GameProject.Managers;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;

namespace GameProject.ProjectilePatterns;

internal class BFGPattern : IProjectilePattern {
  public void SpawnProjectiles(ProjectileManager manager, Vector2 position, Vector2 direction, GunStats stats) {
    manager.Add(ProjectileFactory.Instance.CreateBFGShot(position, direction, stats));
  }
}
