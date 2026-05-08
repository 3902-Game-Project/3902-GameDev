using GameProject.Items;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;

namespace GameProject.ProjectilePatterns;

internal class BFGPattern : IProjectilePattern {
  internal void SpawnProjectiles(ProjectileManager manager, Vector2 position, Vector2 direction, GunStats stats) {
    manager.Add(ProjectileFactory.CreateBFGShot(position, direction, stats));
  }
}
