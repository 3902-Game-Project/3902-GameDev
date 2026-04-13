using GameProject.Factories;
using GameProject.GlobalInterfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.Items;

internal class SingleShotPattern : IProjectilePattern {
  public void SpawnProjectiles(ProjectileManager projectileManager, Vector2 spawnPosition, Vector2 direction, GunStats stats) {
    projectileManager.Add(ProjectileFactory.Instance.CreateBullet(spawnPosition, direction, stats.BulletVelocity, stats.BulletLifetime));
  }
}
