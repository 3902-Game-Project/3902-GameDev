using GameProject.GlobalInterfaces;
using GameProject.Globals;
using GameProject.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

internal class ProjectileFactory : IInitable {
  private Texture2D projectileTexture;
  private Texture2D bombBlinkingTexture;

  public static ProjectileFactory Instance { get; } = new();

  private ProjectileFactory() { }

  public void Initialize() { }

  public void LoadContent(ContentManager content) {
    projectileTexture = content.Load<Texture2D>("Projectiles/Projectile Spritesheet");
    bombBlinkingTexture = content.Load<Texture2D>("Projectiles/Bomb Spritesheet");
  }

  public IProjectile CreateBullet(Vector2 startPosition, Vector2 direction, float velocity, float lifetime, int damage) {
    return new BulletDefault(projectileTexture, startPosition, direction, velocity, lifetime, damage);
  }

  public IProjectile CreateBomb(Vector2 startPosition, float lifetime) {
    return new BombProjectile(projectileTexture, startPosition, lifetime);
  }

  public static IProjectile CreateBang(ABaseGun sourceGun) {
    return new BangProjectile(TextureStore.Instance.NewGuns, sourceGun);
  }

  public static IProjectile CreateBFGShot(Vector2 position, Vector2 direction, GunStats stats) {
    return new BFGProjectile(TextureStore.Instance.NewGuns, position, direction, stats.BulletVelocity, stats.BaseDamage);
  }

  public IProjectile CreateBossBomb(Vector2 position, Vector2 direction, int damage) {
    return new BossBomb(TextureStore.Instance.Boss, bombBlinkingTexture, position, direction, damage);
  }
}
