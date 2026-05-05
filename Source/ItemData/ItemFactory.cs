using GameProject.Globals;
using GameProject.Level;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class ItemFactory {
  private Texture2D basicGunsTexture;
  private Texture2D healthPotionTexture;
  private Texture2D invincibilityPotionTexture;
  private Texture2D infiniteAmmoPotionTexture;

  public static ItemFactory Instance { get; } = new();

  private ItemFactory() { }

  public void LoadAllTextures(ContentManager contentManager) {
    basicGunsTexture = contentManager.Load<Texture2D>("World Pickups/Items/Basic Guns Spritesheet");
    invincibilityPotionTexture = contentManager.Load<Texture2D>("World Pickups/Items/Invincibility Potion");
    healthPotionTexture = contentManager.Load<Texture2D>("World Pickups/Items/Health Potion");
    infiniteAmmoPotionTexture = contentManager.Load<Texture2D>("World Pickups/Items/Ammo Potion");
  }

  public IItem CreateRevolver(float xPos, float yPos, Player player, ILevelManager levelManager) {
    GunStats stats = new() {
      AmmoType = AmmoType.Light,
      BulletVelocity = 500f,
      FireRate = .2f,
      MaxAmmo = 6,
      CurrentAmmo = 6,
      ReloadTime = 0.5f,
      BaseDamage = 30,
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
      BaseDamage = 70,
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
      BaseDamage = 20,
    };

    return new ShotgunItem(basicGunsTexture, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public IItem CreateSMG(float xPos, float yPos, Player player, ILevelManager levelManager) {
    GunStats smgStats = new() {
      MaxAmmo = 30,
      CurrentAmmo = 30,
      BaseDamage = 15,
      BulletVelocity = 700f,
      FireRate = 0.2f,
      ReloadTime = 1.2f,
      AmmoType = AmmoType.Light,
      ReloadsOneByOne = false,
    };
    return new SMGItem(TextureStore.Instance.NewGuns, new Vector2(xPos, yPos), player, levelManager, smgStats);
  }

  public IItem CreateBFG(float xPos, float yPos, Player player, ILevelManager levelManager) {
    var stats = new GunStats {
      AmmoType = AmmoType.BFG,
      BulletVelocity = 800f,
      FireRate = 1.0f,
      MaxAmmo = 3,
      CurrentAmmo = 3,
      ReloadTime = 9999f,
      BaseDamage = 340,
      ReloadsOneByOne = false,
    };
    return new BFGItem(TextureStore.Instance.NewGuns, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public IItem CreateFakeBFG(float xPos, float yPos, Player player, ILevelManager levelManager) {
    var stats = new GunStats {
      AmmoType = AmmoType.BFG,
      FireRate = 1.0f,
      MaxAmmo = 999, // Doesn't matter
      CurrentAmmo = 999,
    };
    return new FakeBFGItem(TextureStore.Instance.NewGuns, new Vector2(xPos, yPos), player, levelManager, stats);
  }

  public static IItem CreateKey(float xPos, float yPos, ILevelManager levelManager) {
    return new KeyItem(TextureStore.Instance.MainBlockItemAtlas, new Vector2(xPos, yPos), levelManager);
  }

  public IItem CreateHealthPotion(float xPos, float yPos, Player player) {
    return new HealthPotionItem(healthPotionTexture, new Vector2(xPos, yPos), player);
  }

  public IItem CreateInvincibilityPotion(float xPos, float yPos, Player player) {
    return new InvincibilityPotionItem(invincibilityPotionTexture, new Vector2(xPos, yPos), player);
  }

  public IItem CreateInfiniteAmmo(float xPos, float yPos, Player player) {
    return new InfiniteAmmoPotionItem(infiniteAmmoPotionTexture, new Vector2(xPos, yPos), player);
  }
}
