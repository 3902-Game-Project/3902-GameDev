using GameProject.Items;
using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

internal interface IProjectilePattern {
  void SpawnProjectiles(ProjectileManager projectileManager, Vector2 spawnPosition, Vector2 direction, GunStats stats);
}
