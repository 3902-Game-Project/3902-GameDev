using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GameProject.Commands;
using GameProject.Factories;
using GameProject.GlobalInterfaces;
using GameProject.Items;
using GameProject.Managers;
using GameProject.PlayerSpace;
using GameProject.Source.Commands;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Source.Misc;

internal class CheatCodes : ITemporalUpdatable {
  public static CheatCodes Instance { get; } = new CheatCodes();
  public ILevelManager LevelManager { get; set; }

  private readonly double maxWaitTime = 5f;
  private double pressedDeltaTime = 0f;

  private bool itemsOn = false;
  private readonly int buffer = 8;
  public List<Keys> lastPressed = [];

  private Dictionary<List<Keys>, IGPCommand> cheatCodes = [];

  public void Initialize(Player player) {
    Instance.cheatCodes = new Dictionary<List<Keys>, IGPCommand> {
      // Unlimited Health Mappings
      { [Keys.W, Keys.W, Keys.S, Keys.S, Keys.A, Keys.D, Keys.A, Keys.D], new PlayerUnlimitedHealthCommand(player) },
      { [Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right], new PlayerUnlimitedHealthCommand(player) },

      // Unlimited Ammo Mappings
      { [Keys.W, Keys.A, Keys.S, Keys.D, Keys.W, Keys.A, Keys.S, Keys.D], new PlayerUnlimitedAmmoCommand(player) },
      { [Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Right], new PlayerUnlimitedAmmoCommand(player) },

      // Unlimited Items Mappings
      { [Keys.W, Keys.W, Keys.A, Keys.A, Keys.D, Keys.D, Keys.S, Keys.S], new PlayerUnlimitedItemsCommand(player) },
      { [Keys.Up, Keys.Up, Keys.Left, Keys.Left, Keys.Right, Keys.Right, Keys.Down, Keys.Down], new PlayerUnlimitedItemsCommand(player) },

      // Toggle halt all enemies
      { [Keys.D6, Keys.D7], new ToggleHaltEnemyCommand() },

      // Don't input this one (kill player)
      { [Keys.D6, Keys.D9], new PlayerDieCommand(player) },
    };
  }

  public void UnlimitedHealth(Player player) {
    player.Health = 999999;
    lastPressed.Clear();
    Debug.WriteLine("unlimited health");
  }

  public void UnlimitedAmmo(Player player) {
    player.Inventory.Ammo[AmmoType.Heavy] += 9999;
    player.Inventory.Ammo[AmmoType.Shells] += 9999;
    player.Inventory.Ammo[AmmoType.Light] += 9999;
    lastPressed.Clear();
    Debug.WriteLine("unlimited ammo");
  }

  public void UnlimitedItems(Player player) {
    if (!player.Inventory.GeneralItems.OfType<KeyItem>().Any()) {
      IItem key = ItemFactory.CreateKey(-1f, -1f, LevelManager);
      player.Inventory.PickupItem(key);
    }

    if (!player.Inventory.GeneralItems.OfType<HealthItem>().Any()) {
      IItem health = ItemFactory.Instance.CreateHealthItem(-1f, -1f, player);
      player.Inventory.PickupItem(health);
    }

    if (!player.Inventory.GeneralItems.OfType<InfiniteAmmoItem>().Any()) {
      IItem ammo = ItemFactory.Instance.CreateInfiniteAmmoItem(-1f, -1f, player);
      player.Inventory.PickupItem(ammo);
    }

    if (!player.Inventory.GeneralItems.OfType<InvincibilityItem>().Any()) {
      IItem invincible = ItemFactory.Instance.CreateInvincibilityItem(-1f, -1f, player);
      player.Inventory.PickupItem(invincible);
    }
    // add additional cases for other items

    if (!itemsOn) {
      lastPressed.Clear();
      itemsOn = true;
      Debug.WriteLine("unlimited items");
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

  public void Update(double deltaTime) {
    if (pressedDeltaTime <= maxWaitTime) {
      foreach (var code in Instance.cheatCodes) {
        if (CodesMatch(code.Key)) code.Value.Execute();
      }
      pressedDeltaTime += deltaTime;
    } else {
      lastPressed.Clear();
      pressedDeltaTime = 0;
    }
  }
}
