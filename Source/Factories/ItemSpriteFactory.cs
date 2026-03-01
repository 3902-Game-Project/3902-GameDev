using GameProject.Interfaces;
using GameProject.Items;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

public class ItemSpriteFactory (ProjectileManager projectileManager) {
  private Texture2D basicGunsTexture;

  public void LoadAllTextures(ContentManager content) {
    basicGunsTexture = content.Load<Texture2D>("basic_guns_spritesheet");
  }

  public IItem CreateRevolver() {
    var stats = new GunStats();
    stats.BulletVelocity = 200f;
    return new RevolverItem(basicGunsTexture, new Vector2(300, 300), projectileManager, stats);
  }

  public IItem CreateRifle() {
    var stats = new GunStats();
    stats.BulletVelocity = 500f;
    return new RifleItem(basicGunsTexture, new Vector2(300, 300), projectileManager, stats);
  }

  public IItem CreateShotgun() {
    var stats = new GunStats();
    stats.SpreadAngle = 30f;
    stats.PelletCount = 5;
    return new ShotgunItem(basicGunsTexture, new Vector2(300, 300), projectileManager, stats);
  }
}
