using GameProject.Factories;
using GameProject.Items;
using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.ProjectilePatterns;

internal class SingleShotPattern : IProjectilePattern {
  public void SpawnProjectiles(ProjectileManager projectileManager, Vector2 spawnPosition, Vector2 direction, GunStats stats) {
    projectileManager.Add(ProjectileFactory.Instance.CreateBullet(spawnPosition, direction, stats.BulletVelocity, stats.BulletLifetime));
  }
}
