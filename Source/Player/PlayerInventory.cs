using System;
using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Items;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Misc;

public class PlayerInventory(ILevelManager levelManager) {
  private readonly Random random = new();
  public List<IItem> Weapons { get; private set; } = [];
  public int ActiveWeaponIndex { get; private set; } = 0;
  public IItem ActiveItem => Weapons.Count > 0 ? Weapons[ActiveWeaponIndex] : null;

  public List<IItem> GeneralItems { get; private set; } = [];

  public void PickupItem(IItem newItem) {
    if (newItem is RevolverItem || newItem is RifleItem || newItem is ShotgunItem) {
      if (Weapons.Count < 2) {
        Weapons.Add(newItem);
        ActiveWeaponIndex = Weapons.Count - 1;
      } else {
        DropItem(ActiveItem);
        Weapons[ActiveWeaponIndex] = newItem;
      }
    } else {
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
    }
  }
}
