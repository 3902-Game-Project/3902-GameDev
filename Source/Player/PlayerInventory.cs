using System;
using System.Collections.Generic;
using GameProject.GlobalInterfaces;
using GameProject.Globals;
using GameProject.Items;
using GameProject.Level;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace;

internal class PlayerInventory(Player player, CurrentLevelGetter GetCurrentLevel) : IInitable {
  private readonly Random random = new();
  private const int MAX_WEAPONS = 2;
  private const int MAX_GENERAL_ITEMS = 10;
  private const int MIN_DROP_VEL_X = -100;
  private const int MAX_DROP_VEL_X = 100;
  private const int MIN_DROP_VEL_Y = -150;
  private const int MAX_DROP_VEL_Y = -50;

  private const float BAR_Y_OFFSET = -80f;
  private const float BFG_SEGMENT_WIDTH = 20f;
  private const float BFG_SEGMENT_GAP = 4f;
  private const float RELOAD_BAR_WIDTH = 60f;
  private const float RELOAD_BAR_HEIGHT = 8f;

  // 1. Weapons List
  public List<IItem> Weapons { get; private set; } = [];
  public int ActiveWeaponIndex { get; private set; } = 0;
  public IItem ActiveItem => Weapons.Count > 0 ? Weapons[ActiveWeaponIndex] : null;

  // 2. General Items List (Potions, Whiskey, etc.)
  public List<IItem> GeneralItems { get; set; } = [];

  // Overarching Ammo Stores
  public Dictionary<AmmoType, int> Ammo { get; set; } = new() {
    { AmmoType.Light, 30 },
    { AmmoType.Heavy, 10 },
    { AmmoType.Shells, 10 },
    { AmmoType.BFG, 3 }
  };

  public void EquipWeapon(int index) {
    if (index >= 0 && index < Weapons.Count) {
      ActiveWeaponIndex = index;
      ActiveItem?.OnEquip();
    }
  }

  public void PickupItem(IItem newItem) {
    if (newItem.Category == ItemCategory.Primary || newItem.Category == ItemCategory.Sidearm) {
      if (Weapons.Count < 2) {
        Weapons.Add(newItem);
        ActiveWeaponIndex = Weapons.Count - 1;
      } else {
        DropItem(ActiveItem);
        Weapons[ActiveWeaponIndex] = newItem;
      }
      ActiveItem?.OnEquip();
    } else {
      GeneralItems.Add(newItem);
      if (GeneralItems.Count > MAX_GENERAL_ITEMS) {
        DropItem(GeneralItems[0]);
        GeneralItems.RemoveAt(0);
      }
    }
  }

  public void RemoveGeneralItem(IItem itemToRemove) {
    GeneralItems.Remove(itemToRemove);
  }

  private void DropItem(IItem itemToDrop) {
    float tossX = random.Next(MIN_DROP_VEL_X, MAX_DROP_VEL_X);
    float tossY = random.Next(MIN_DROP_VEL_Y, MAX_DROP_VEL_Y);
    Vector2 dropVelocity = new(tossX, tossY);

    IWorldPickup droppedItem = new ItemWorldPickup(itemToDrop, dropVelocity);
    GetCurrentLevel().AddPickup(droppedItem);
  }

  public void DropCurrentItem() {
    if (Weapons.Count <= 0) {
      return;
    }

    DropItem(ActiveItem);

    Weapons.Remove(ActiveItem);

    if (ActiveWeaponIndex >= Weapons.Count && ActiveWeaponIndex > 0) {
      ActiveWeaponIndex = Weapons.Count - 1;
    }
  }

  public void SwapActiveWeapon() {
    if (Weapons.Count > 1) {
      ActiveWeaponIndex = (ActiveWeaponIndex == 0) ? 1 : 0;
      ActiveItem?.OnEquip();
    }
  }

  public void Initialize() { }

  public void LoadContent(ContentManager content) {
    PickupItem(ItemFactory.Instance.CreateRevolver(0.0f, 0.0f, player, () => GetCurrentLevel().ProjectileManager));
    PickupItem(ItemFactory.Instance.CreateRifle(0.0f, 0.0f, player, () => GetCurrentLevel().ProjectileManager));
  }

  public void Update(double deltaTime) {
    if (ActiveItem != null) {
      ActiveItem.Update(deltaTime);

      if (ActiveItem is ABaseGun gun && gun.Stats.CurrentAmmo <= 0 && !gun.IsReloading) {
        gun.StartReload();
      }
    }
  }

  public void Draw(SpriteBatch spriteBatch, Vector2 Position, FacingDirection Direction, Texture2D whitePixel) {
    if (ActiveItem != null) {
      Vector2 spriteCenter = new(Constants.PLAYER_SPRITE_WIDTH / 2f, Constants.PLAYER_SPRITE_HEIGHT / 2f);

      Vector2 rightHandUnscaled = new(100f, 195f);
      Vector2 leftHandUnscaled = new(18f, 188f);
      Vector2 upHandUnscaled = new(120f, 150f);
      Vector2 downHandUnscaled = new(40f, 190f);
      Vector2 currentOffset;

      if (Direction == FacingDirection.Right) {
        currentOffset = (rightHandUnscaled - spriteCenter) * Constants.PLAYER_SPRITE_SCALE;
      } else if (Direction == FacingDirection.Left) {
        currentOffset = (leftHandUnscaled - spriteCenter) * Constants.PLAYER_SPRITE_SCALE;
      } else if (Direction == FacingDirection.Up) {
        currentOffset = (upHandUnscaled - spriteCenter) * Constants.PLAYER_SPRITE_SCALE;
      } else {
        currentOffset = (downHandUnscaled - spriteCenter) * Constants.PLAYER_SPRITE_SCALE;
      }
      ActiveItem.Position = Position + currentOffset;
      ActiveItem.Direction = Direction;
    }

    ActiveItem?.Draw(spriteBatch);

    // Draw Overhead Reload Bar or BFG Bar
    if (ActiveItem is BFGItem bfg && whitePixel != null) {
      float totalWidth = (BFG_SEGMENT_WIDTH * 3) + (BFG_SEGMENT_GAP * 2);
      Vector2 barPos = Position + new Vector2(-totalWidth / 2f, BAR_Y_OFFSET);

      for (int i = 0; i < 3; i++) {
        Color c = (i < bfg.Stats.CurrentAmmo) ? Color.LimeGreen : Color.DarkGray * 0.5f;
        spriteBatch.Draw(whitePixel, new Rectangle((int) (barPos.X + i * (BFG_SEGMENT_WIDTH + BFG_SEGMENT_GAP)), (int) barPos.Y, (int) BFG_SEGMENT_WIDTH, (int) RELOAD_BAR_HEIGHT), c);
      }
    } else if (ActiveItem is ABaseGun gun && gun.IsReloading && whitePixel != null) {
      Vector2 barPos = Position + new Vector2(-RELOAD_BAR_WIDTH / 2f, BAR_Y_OFFSET);

      float progress = 1.0f - ((float) (gun.ReloadTimer / gun.Stats.ReloadTime));

      spriteBatch.Draw(whitePixel, new Rectangle((int) barPos.X, (int) barPos.Y, (int) RELOAD_BAR_WIDTH, (int) RELOAD_BAR_HEIGHT), Color.DarkGray * 0.8f);
      spriteBatch.Draw(whitePixel, new Rectangle((int) barPos.X, (int) barPos.Y, (int) (RELOAD_BAR_WIDTH * progress), (int) RELOAD_BAR_HEIGHT), Color.White);
    }
  }
}
