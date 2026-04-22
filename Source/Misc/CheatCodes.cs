using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GameProject.Factories;
using GameProject.Items;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Source.Misc;

internal class CheatCodes {
  public static CheatCodes Instance { get; } = new CheatCodes();
  public ILevelManager LevelManager { get; set; }

  private bool healthOn = false;
  private bool ammoOn = false;
  private bool itemsOn = false;
  private readonly int buffer = 8;
  public List<Keys> lastPressed = [];

  // Unlimited Health Mappings
  private readonly List<Keys> unlimitedHealthWASD = [
    Keys.W, Keys.W, Keys.S, Keys.S, Keys.A, Keys.D, Keys.A, Keys.D
  ];
  private readonly List<Keys> unlimitedHealthArrows = [
    Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right
  ];

  // Unlimited Ammo Mappings
  private readonly List<Keys> unlimitedAmmoWASD = [
    Keys.W, Keys.A, Keys.S, Keys.D, Keys.W, Keys.A, Keys.S, Keys.D
  ];
  private readonly List<Keys> unlimitedAmmoArrows = [
    Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Right
  ];

  // Unlimited Items Mappings
  private readonly List<Keys> unlimitedItemsWASD = [
    Keys.W, Keys.W, Keys.A, Keys.A, Keys.D, Keys.D, Keys.S, Keys.S
  ];
  private readonly List<Keys> unlimitedItemsArrows = [
    Keys.Up, Keys.Up, Keys.Left, Keys.Left, Keys.Right, Keys.Right, Keys.Down, Keys.Down
  ];

  public void UnliimitedHealth(Player player) {
    if (CodesMatch(unlimitedHealthWASD) || CodesMatch(unlimitedHealthArrows)) {
      player.Health = 999999;
      lastPressed.Clear();
      healthOn = true;
      Debug.WriteLine("unlimited health");
    }
  }

  public void UnlimitedAmmo(Player player) {
    if (CodesMatch(unlimitedAmmoWASD) || CodesMatch(unlimitedAmmoArrows)) {
      player.Inventory.Ammo[AmmoType.Heavy] += 9999;
      player.Inventory.Ammo[AmmoType.Shells] += 9999;
      player.Inventory.Ammo[AmmoType.Light] += 9999;
      lastPressed.Clear();
      ammoOn = true;
      Debug.WriteLine("unlimited ammo");
    }
  }

  public void UnliimitedItems(Player player) {
    if (CodesMatch(unlimitedItemsWASD) || CodesMatch(unlimitedItemsArrows) || itemsOn) {
      if (!player.Inventory.GeneralItems.OfType<KeyItem>().Any()) {
        IItem key = ItemFactory.CreateKey(-1f, -1f, LevelManager);
        player.Inventory.PickupItem(key);
      }
      if (player.Inventory.GeneralItems.OfType<HealthItem>().Any()) {
        IItem health = ItemFactory.Instance.CreateHealthItem(-1f, -1f);
        player.Inventory.PickupItem(health);
      }
      if (player.Inventory.GeneralItems.OfType<InfiniteAmmoItem>().Any()) {
        IItem ammo = ItemFactory.Instance.CreateInfiniteAmmoItem(-1f, -1f);
        player.Inventory.PickupItem(ammo);
      }
      if (player.Inventory.GeneralItems.OfType<InvincibilityItem>().Any()) {
        IItem invincible = ItemFactory.Instance.CreateInvincibilityItem(-1f, -1f);
        player.Inventory.PickupItem(invincible);
      }
      if (!itemsOn) {
        lastPressed.Clear();
        itemsOn = true;
        Debug.WriteLine("unlimited items");
      }
      
    }
  }

  public bool CodesMatch(List<Keys> code) {
    bool valid = true;

    if (lastPressed.Count != code.Count) {
      valid = false;
    } else {
      for (int i = 0; i < buffer; i++) {
        if (lastPressed[i] != code[i]) return false;
      }
    }

    return valid;
  }

  public void AddKey(Keys key) {
    if (lastPressed.Count >= buffer) {
      lastPressed.RemoveAt(0);
    }
    lastPressed.Add(key);
  }

  public void UpdateCheats(Player player) {
    if (!healthOn) Instance.UnliimitedHealth(player);
    if (!ammoOn) Instance.UnlimitedAmmo(player);  
    if (!itemsOn) Instance.UnliimitedItems(player);
  }
}
