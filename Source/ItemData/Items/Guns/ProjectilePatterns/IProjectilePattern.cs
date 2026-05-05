using GameProject.Items;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;

namespace GameProject.ProjectilePatterns;

internal interface IProjectilePattern {
  void SpawnProjectiles(ProjectileManager projectileManager, Vector2 spawnPosition, Vector2 direction, GunStats stats);
}
