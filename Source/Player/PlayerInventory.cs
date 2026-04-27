using System;
using System.Collections.Generic;
using GameProject.Items;
using GameProject.Managers;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace;

internal class PlayerInventory(ILevelManager levelManager) {
  private readonly Random random = new();

  // 1. Weapons List
  public List<IItem> Weapons { get; private set; } = [];
  public int ActiveWeaponIndex { get; private set; } = 0;
  public IItem ActiveItem => Weapons.Count > 0 ? Weapons[ActiveWeaponIndex] : null;

  // 2. General Items List (Potions, Whiskey, etc.)
  public List<IItem> GeneralItems { get; set; } = [];

  public List<IItem> Keys { get; set; } = [];

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
    }
  }
  public void RemoveGeneralItem(IItem itemToRemove) {
    GeneralItems.Remove(itemToRemove);
  }

  private void DropItem(IItem itemToDrop) {
    float tossX = random.Next(-100, 100);
    float tossY = random.Next(-150, -50);
    Vector2 dropVelocity = new(tossX, tossY);

    IWorldPickup droppedItem = new ItemWorldPickup(itemToDrop, dropVelocity);
    levelManager.CurrentLevel.AddPickup(droppedItem);
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

  public void Update(double deltaTime) {
    if (ActiveItem != null) {
      ActiveItem.Update(deltaTime);

      if (ActiveItem is ABaseGun gun && gun.PublicStats.CurrentAmmo <= 0 && !gun.IsReloading) {
        gun.StartReload();
      }
    }
  }

  public void Draw(SpriteBatch spriteBatch, Vector2 Position, FacingDirection Direction, Texture2D whitePixel) {
    if (ActiveItem != null) {
      float unscaledWidth = 171f;
      float unscaledHeight = 323f;
      Vector2 spriteCenter = new(unscaledWidth / 2f, unscaledHeight / 2f);
      float playerScale = 0.15f;

      Vector2 rightHandUnscaled = new(100f, 195f);
      Vector2 leftHandUnscaled = new(18f, 188f);
      Vector2 upHandUnscaled = new(120f, 150f);
      Vector2 downHandUnscaled = new(40f, 190f);
      Vector2 currentOffset;

      if (Direction == FacingDirection.Right) {
        currentOffset = (rightHandUnscaled - spriteCenter) * playerScale;
      } else if (Direction == FacingDirection.Left) {
        currentOffset = (leftHandUnscaled - spriteCenter) * playerScale;
      } else if (Direction == FacingDirection.Up) {
        currentOffset = (upHandUnscaled - spriteCenter) * playerScale;
      } else {
        currentOffset = (downHandUnscaled - spriteCenter) * playerScale;
      }
      ActiveItem.Position = Position + currentOffset;
      ActiveItem.Direction = Direction;
    }

    ActiveItem?.Draw(spriteBatch);

    // Draw Overhead Reload Bar or BFG Bar
    if (ActiveItem is BFGItem bfg && whitePixel != null) {
      float segmentWidth = 20f;
      float gap = 4f;
      float totalWidth = (segmentWidth * 3) + (gap * 2);
      Vector2 barPos = Position + new Vector2(-totalWidth / 2f, -80f);

      for (int i = 0; i < 3; i++) {
        Color c = (i < bfg.PublicStats.CurrentAmmo) ? Color.LimeGreen : Color.DarkGray * 0.5f;
        spriteBatch.Draw(whitePixel, new Rectangle((int) (barPos.X + i * (segmentWidth + gap)), (int) barPos.Y, (int) segmentWidth, 8), c);
      }
    } else if (ActiveItem is ABaseGun gun && gun.IsReloading && whitePixel != null) {
      float barWidth = 60f;
      float barHeight = 8f;
      Vector2 barPos = Position + new Vector2(-barWidth / 2f, -80f);

      float progress = 1.0f - ((float) (gun.ReloadTimer / gun.PublicStats.ReloadTime));

      spriteBatch.Draw(whitePixel, new Rectangle((int) barPos.X, (int) barPos.Y, (int) barWidth, (int) barHeight), Color.DarkGray * 0.8f);
      spriteBatch.Draw(whitePixel, new Rectangle((int) barPos.X, (int) barPos.Y, (int) (barWidth * progress), (int) barHeight), Color.White);
    }
  }
}
