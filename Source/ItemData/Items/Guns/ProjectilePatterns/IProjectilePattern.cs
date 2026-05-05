using GameProject.Items;
using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.ProjectilePatterns;

internal interface IProjectilePattern {
  void SpawnProjectiles(ProjectileManager projectileManager, Vector2 spawnPosition, Vector2 direction, GunStats stats);
}
