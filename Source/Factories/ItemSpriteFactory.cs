using GameProject.Globals;
using GameProject.Items;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class ItemSpriteFactory {
  private Texture2D basicGunsTexture;

  public static ItemSpriteFactory Instance { get; } = new();

  private ItemSpriteFactory() { }

  public void LoadAllTextures(ContentManager content) {
    basicGunsTexture = content.Load<Texture2D>("Items/basic_guns_spritesheet");
  }

  public IItem CreateRevolver(float xPos, float yPos, Game1 game) {
    GunStats stats = new() {
      BulletVelocity = 500f,
      FireRate = .2f,
      MaxAmmo = 6,
      CurrentAmmo = 6,
      ReloadTime = 1.5f,
      BaseDamage = 30
    };
    return new RevolverItem(basicGunsTexture, new Vector2(xPos, yPos), game, stats);
  }

  public IItem CreateRifle(float xPos, float yPos, Game1 game) {
    var stats = new GunStats {
      BulletVelocity = 1000f,
      MaxAmmo = 10,
      CurrentAmmo = 10,
      ReloadTime = 3f,
      BaseDamage = 70
    };
    return new RifleItem(basicGunsTexture, new Vector2(xPos, yPos), game, stats);
  }

  public IItem CreateShotgun(float xPos, float yPos, Game1 game) {
    var stats = new GunStats {
      BulletVelocity = 200f,
      SpreadAngle = 10f,
      PelletCount = 5,
      MaxAmmo = 2,
      CurrentAmmo = 2,
      ReloadTime = 2f,
      BaseDamage = 15
    };
    return new ShotgunItem(basicGunsTexture, new Vector2(xPos, yPos), game, stats);
  }

  public static IItem CreateKey(float xPos, float yPos, ILevelManager levelManager) {
    return new KeyItem(TextureStore.Instance.MainBlockItemAtlas, new Vector2(xPos, yPos), levelManager);
  }
}
