using GameProject.Items;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class ProjectileFactory {
  private Texture2D projectileTexture;
  private Texture2D specialWeaponsTexture;
  private Texture2D bombFlyingTexture;
  private Texture2D bombBlinkingTexture;
  private static readonly ProjectileFactory instance = new();

  public static ProjectileFactory Instance => instance;

  private ProjectileFactory() { }

  public void LoadAllTextures(ContentManager content) {
    projectileTexture = content.Load<Texture2D>("Misc/projectile_spritesheet");
    specialWeaponsTexture = content.Load<Texture2D>("Items/new_guns");
    bombFlyingTexture = content.Load<Texture2D>("Enemies/bossSprite");
    bombBlinkingTexture = content.Load<Texture2D>("Enemies/bombSprite");
  }

  public IProjectile CreateBullet(Vector2 startPosition, Vector2 direction, float velocity, float lifetime, int damage) {
    return new BulletDefault(projectileTexture, startPosition, direction, velocity, lifetime, damage);
  }

  public IProjectile CreateBomb(Vector2 startPosition, Vector2 direction, float velocity, float lifetime) {
    return new BombProjectile(projectileTexture, startPosition, direction, velocity, lifetime);
  }

  public IProjectile CreateBang(ABaseGun sourceGun) {
    return new BangProjectile(specialWeaponsTexture, sourceGun);
  }

  public IProjectile CreateBFGShot(Vector2 position, Vector2 direction, GunStats stats) {
    return new BFGProjectile(specialWeaponsTexture, position, direction, stats.BulletVelocity, stats.BaseDamage);
  }

  public IProjectile CreateBossBomb(Vector2 position, Vector2 direction, int damage) {
    return new BossBomb(bombFlyingTexture, bombBlinkingTexture, position, direction, damage);
  }
}
