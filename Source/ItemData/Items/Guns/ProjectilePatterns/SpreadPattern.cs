using GameProject.Factories;
using GameProject.Items;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;

namespace GameProject.ProjectilePatterns;

internal class SpreadPattern : IProjectilePattern {
  public void SpawnProjectiles(ProjectileManager projectileManager, Vector2 spawnPosition, Vector2 direction, GunStats stats) {
    for (int i = 0; i < stats.PelletCount; i++) {
      float angle = -stats.SpreadAngle / 2 + stats.SpreadAngle / (stats.PelletCount - 1) * i;
      Vector2 rotatedDirection = Vector2.Transform(direction, Matrix.CreateRotationZ(MathHelper.ToRadians(angle)));
      projectileManager.Add(ProjectileFactory.Instance.CreateBullet(
        spawnPosition,
        rotatedDirection,
        stats.BulletVelocity,
        stats.BulletLifetime,
        stats.BaseDamage
      ));
    }
  }
}
