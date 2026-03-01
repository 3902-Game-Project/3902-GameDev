using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using GameProject.Factories;

namespace GameProject.Items;

public class SingleShotPattern : IProjectilePattern {
  public void SpawnProjectiles(ProjectileManager projectileManager, Vector2 spawnPosition, Vector2 direction, GunStats stats) {
    projectileManager.AddProjectile(ProjectileFactory.Instance.CreateBullet(spawnPosition, direction, stats.BulletVelocity, stats.BulletLifetime));
  }
}
