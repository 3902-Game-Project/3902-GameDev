using System;
using System.Collections.Generic;
using GameProject.Items;
using GameProject.Managers;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using GameProject.Items;

namespace GameProject.PlayerSpace;

internal class PlayerInventory(ILevelManager levelManager) {
  private readonly Random random = new();

  // 1. Weapons List
  public List<IItem> Weapons { get; private set; } = [];
  public int ActiveWeaponIndex { get; private set; } = 0;
  public IItem ActiveItem => Weapons.Count > 0 ? Weapons[ActiveWeaponIndex] : null;

  // 2. General Items List (Potions, Whiskey, etc.)
  public List<IItem> GeneralItems { get; private set; } = [];

  public List<IItem> Keys { get; private set; } = [];

  // Overarching Ammo Stores
  public Dictionary<AmmoType, int> Ammo { get; private set; } = new() {
    { AmmoType.Light, 30 },
    { AmmoType.Heavy, 10 },
    { AmmoType.Shells, 10 }
  };

  public void EquipWeapon(int index) {
    if (index >= 0 && index < Weapons.Count) {
      ActiveWeaponIndex = index;
      ActiveItem?.OnEquip();
    }
  }

  public void PickupItem(IItem newItem) {
    // Sort the item into the correct list based on its type or category

    if (newItem.Category == ItemCategory.Primary || newItem.Category == ItemCategory.Sidearm) {
      if (Weapons.Count < 2) {
        Weapons.Add(newItem);
        ActiveWeaponIndex = Weapons.Count - 1;
      } else {
        DropItem(ActiveItem);
        Weapons[ActiveWeaponIndex] = newItem;
      }
      ActiveItem?.OnEquip();
    } else if (newItem is KeyItem) {
      // If it's a key, put it in the new Keys list!
      Keys.Add(newItem);
    } else {
      // Everything else goes in the general backpack
      GeneralItems.Add(newItem);
    }
  }

  private void DropItem(IItem itemToDrop) {
    float tossX = random.Next(-100, 100);
    float tossY = random.Next(-150, -50);
    Vector2 dropVelocity = new(tossX, tossY);

    IWorldPickup droppedItem = new ItemWorldPickup(itemToDrop, dropVelocity);
    levelManager.CurrentLevel.AddPickup(droppedItem);
  }

  public void SwapActiveWeapon() {
    if (Weapons.Count > 1) {
      ActiveWeaponIndex = (ActiveWeaponIndex == 0) ? 1 : 0;
      ActiveItem?.OnEquip();
    }
  }
}
