using GameProject.Items;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class ProjectileFactory {
  private Texture2D projectileTexture;
  private Texture2D specialWeaponsTexture;
  private static readonly ProjectileFactory instance = new();

  public static ProjectileFactory Instance => instance;

  private ProjectileFactory() { }

  public void LoadAllTextures(ContentManager content) {
    projectileTexture = content.Load<Texture2D>("Misc/projectile_spritesheet");
    specialWeaponsTexture = content.Load<Texture2D>("Items/special_weapons_sheet");
  }

  public IProjectile CreateBullet(Vector2 startPosition, Vector2 direction, float velocity, float lifetime, int damage) {
    return new BulletDefault(projectileTexture, startPosition, direction, velocity, lifetime, damage);
  }

  public IProjectile CreateBomb(Vector2 startPosition, Vector2 direction, float velocity, float lifetime) {
    return new BombProjectile(projectileTexture, startPosition, direction, velocity, lifetime);
  }

  public static IProjectile CreateBang(Vector2 startPosition) {
    return new BangProjectile(startPosition);
  }

  public IProjectile CreateBFGShot(Vector2 position, Vector2 direction, GunStats stats) {
    return new BFGProjectile(specialWeaponsTexture, position, direction, stats.BulletVelocity, stats.BaseDamage);
  }
}
