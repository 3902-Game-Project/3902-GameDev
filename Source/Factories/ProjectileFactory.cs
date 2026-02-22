using GameProject.Interfaces;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class ProjectileFactory {
  private Texture2D placeholderGunTexture;
  private static ProjectileFactory instance = new();

  public static ProjectileFactory Instance {
    get { return instance; }
  }

  private ProjectileFactory() {
  }

  public void LoadAllTextures(ContentManager content) {
    placeholderGunTexture = content.Load<Texture2D>("placeholderGuns");
  }

  public IProjectile CreateBullet(Vector2 startPosition, Vector2 direction, float velocity, float lifetime) {
    return new BulletDefault(placeholderGunTexture, startPosition, direction, velocity, lifetime);
  }
}
