using GameProject.Globals;
using GameProject.Items;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class ItemFactory {
  private Texture2D basicGunsTexture;
  private Texture2D healthItemTexture;
  private Texture2D invincibilityItemTexture;
  private Texture2D infiniteAmmoItemTexture;
  private Texture2D BFGTexture;

  public static ItemFactory Instance { get; } = new();

  private ItemFactory() { }

  public void LoadAllTextures(ContentManager contentManager) {
    basicGunsTexture = contentManager.Load<Texture2D>("Items/basic_guns_spritesheet");
    invincibilityItemTexture = contentManager.Load<Texture2D>("Items/InvincibilityPotion");
    healthItemTexture = contentManager.Load<Texture2D>("Items/HealthPotion");
    infiniteAmmoItemTexture = contentManager.Load<Texture2D>("Items/AmmoPotion");
    BFGTexture = contentManager.Load<Texture2D>("Items/special_weapons_sheet");
  }

  public IItem CreateRevolver(float xPos, float yPos, Player player, ILevelManager levelManager) {
    GunStats stats = new() {
      AmmoType = AmmoType.Light,
      BulletVelocity = 500f,
      FireRate = .2f,
      MaxAmmo = 6,
      CurrentAmmo = 6,
      ReloadTime = 0.5f,
      BaseDamage = 30
    };

    return new RevolverItem(basicGunsTexture, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public IItem CreateRifle(float xPos, float yPos, Player player, ILevelManager levelManager) {
    var stats = new GunStats {
      AmmoType = AmmoType.Heavy,
      BulletVelocity = 1000f,
      MaxAmmo = 1,
      CurrentAmmo = 1,
      ReloadTime = 1f,
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
      ReloadTime = 1f,
      BaseDamage = 20
    };

    return new ShotgunItem(basicGunsTexture, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public IItem CreateSMG(float xPos, float yPos, Player player, ILevelManager levelManager) {
    var stats = new GunStats {
      AmmoType = AmmoType.Light,
      BulletVelocity = 600f,
      FireRate = 0.1f, // Fast fire
      MaxAmmo = 30,
      CurrentAmmo = 30,
      ReloadTime = 1.5f,
      BaseDamage = 15,
      ReloadsOneByOne = false // Reloads entire clip at once
    };
    return new SMGItem(BFGTexture, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public IItem CreateBFG(float xPos, float yPos, Player player, ILevelManager levelManager) {
    var stats = new GunStats {
      AmmoType = AmmoType.BFG,
      BulletVelocity = 800f,
      FireRate = 1.0f,
      MaxAmmo = 3,
      CurrentAmmo = 3,
      ReloadTime = 9999f, // Cannot reload
      BaseDamage = 2000,
      ReloadsOneByOne = false
    };
    return new BFGItem(BFGTexture, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public IItem CreateFakeBFG(float xPos, float yPos, Player player, ILevelManager levelManager) {
    var stats = new GunStats {
      AmmoType = AmmoType.BFG,
      FireRate = 1.0f,
      MaxAmmo = 999, // Doesn't matter
      CurrentAmmo = 999
    };
    return new FakeBFGItem(BFGTexture, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public static IItem CreateKey(float xPos, float yPos, ILevelManager levelManager) {
    return new KeyItem(TextureStore.Instance.MainBlockItemAtlas, new Vector2(xPos, yPos), levelManager);
  }

  public IItem CreateHealthItem(float xPos, float yPos, Player player) {
    return new HealthItem(healthItemTexture, new Vector2(xPos, yPos), player);
  }

  public IItem CreateInvincibilityItem(float xPos, float yPos, Player player) {
    return new InvincibilityItem(invincibilityItemTexture, new Vector2(xPos, yPos), player);
  }

  public IItem CreateInfiniteAmmoItem(float xPos, float yPos, Player player) {
    return new InfiniteAmmoItem(infiniteAmmoItemTexture, new Vector2(xPos, yPos), player);
  }
}
