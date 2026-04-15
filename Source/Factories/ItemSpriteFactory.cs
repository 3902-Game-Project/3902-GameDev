using GameProject.Globals;
using GameProject.Items;
using GameProject.Managers;
using GameProject.PlayerSpace;
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

  public IItem CreateRevolver(float xPos, float yPos, Player player, ILevelManager levelManager) {
    GunStats stats = new() {
      AmmoType = AmmoType.Light,
      BulletVelocity = 500f,
      FireRate = .2f,
      MaxAmmo = 6,
      CurrentAmmo = 6,
      ReloadTime = 0.2f,
      BaseDamage = 30
    };
    return new RevolverItem(basicGunsTexture, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public IItem CreateRifle(float xPos, float yPos, Player player, ILevelManager levelManager) {
    var stats = new GunStats {
      AmmoType = AmmoType.Heavy,
      BulletVelocity = 1000f,
      MaxAmmo = 10,
      CurrentAmmo = 10,
      ReloadTime = 0.4f,
      BaseDamage = 70
    };
    return new RifleItem(basicGunsTexture, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public IItem CreateShotgun(float xPos, float yPos, Player player, ILevelManager levelManager) {
    var stats = new GunStats {
      AmmoType = AmmoType.Shells,
      BulletVelocity = 400f,
      SpreadAngle = 15f,
      PelletCount = 5,
      MaxAmmo = 2,
      CurrentAmmo = 2,
      ReloadTime = 0.6f,
      BaseDamage = 20
    };
    return new ShotgunItem(basicGunsTexture, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public static IItem CreateKey(float xPos, float yPos, ILevelManager levelManager) {
    return new KeyItem(TextureStore.Instance.MainBlockItemAtlas, new Vector2(xPos, yPos), levelManager);
  }
}
