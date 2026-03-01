using GameProject.Items.Guns;
using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.Interfaces.ItemInterfaces;
public interface IProjectilePattern {
  void SpawnProjectiles(ProjectileManager projectileManager, Vector2 spawnPosition, Vector2 direction, GunStats stats);
}
