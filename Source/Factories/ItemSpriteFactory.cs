using System;
using GameProject.Interfaces;
using GameProject.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

public class ItemSpriteFactory {
  private Texture2D basicGunsTexture;

  public static ItemSpriteFactory Instance { get; } = new();

  private ItemSpriteFactory() { }

  public void LoadAllTextures(ContentManager content) {
    basicGunsTexture = content.Load<Texture2D>("Items/basic_guns_spritesheet");
  }

  public IItem CreateRevolver(float xPos, float yPos, Game1 game) {
    GunStats stats = new() {
      BulletVelocity = 200f,
      FireRate = .2f,
      MaxAmmo = 6,
      CurrentAmmo = 6,
      ReloadTime = 1.5f
    };
    return new RevolverItem(basicGunsTexture, new Vector2(xPos, yPos), game, stats);
  }

  public IItem CreateRifle(float xPos, float yPos, Game1 game) {
    var stats = new GunStats {
      BulletVelocity = 500f,
      MaxAmmo = 1,
      CurrentAmmo = 1,
      ReloadTime = 1f
    };
    return new RifleItem(basicGunsTexture, new Vector2(xPos, yPos), game, stats);
  }

  public IItem CreateShotgun(float xPos, float yPos, Game1 game) {
    var stats = new GunStats {
      SpreadAngle = 30f,
      PelletCount = 5,
      MaxAmmo = 2,
      CurrentAmmo = 2,
      ReloadTime = 2f
    };
    return new ShotgunItem(basicGunsTexture, new Vector2(xPos, yPos), game, stats);
  }

  public IItem CreateKey(float xPos, float yPos) {
    throw new NotImplementedException("CreateKey not implemented"); // add key constructor method later
  }
}
