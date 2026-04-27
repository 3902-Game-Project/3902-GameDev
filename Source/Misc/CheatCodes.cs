using System;
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
  private static readonly double MAX_WAIT_TIME = 5f;

  public static CheatCodes Instance { get; } = new CheatCodes();

  private double pressedDeltaTime = 0f;
  private bool itemsOn = false;
  private Dictionary<List<Keys>, IGPCommand> cheatCodes = [];
  private int maxBufferSize = 0;

  int GetMaxCheatCodeLength() {
    int maxLength = 0;

    foreach (var code in Instance.cheatCodes) {
      if (code.Key.Count > maxLength) {
        maxLength = code.Key.Count;
      }
    }

    return maxLength;
  }

  public ILevelManager LevelManager { get; set; }
  public List<Keys> lastPressed = [];

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

      // Toggle ignette
      { [Keys.D1, Keys.D2, Keys.D1, Keys.D2], new ToggleVignetteCommand() },

      // Toggle halt all enemies
      { [Keys.D6, Keys.D7], new ToggleHaltEnemyCommand() },

      // Don't input this one (kill player)
      { [Keys.D6, Keys.D9], new PlayerDieCommand(player) },

      // Gameplay testing mode (Unlimited health, unlimited ammo)
      { [Keys.D3, Keys.D9, Keys.D0, Keys.D2], new GameplayTestModeCommand(player) },
    };

    maxBufferSize = GetMaxCheatCodeLength();
  }

  public void UnlimitedHealth(Player player) {
    player.Health = 999999;
    Debug.WriteLine("unlimited health");
  }

  public void UnlimitedAmmo(Player player) {
    player.Inventory.Ammo[AmmoType.Heavy] += 9999;
    player.Inventory.Ammo[AmmoType.Shells] += 9999;
    player.Inventory.Ammo[AmmoType.Light] += 9999;
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
      itemsOn = true;
      Debug.WriteLine("unlimited items");
    }
  }

  public bool CodesMatch(List<Keys> code) {
    if (lastPressed.Count < code.Count) {
      return false;
    }

    for (int i = Math.Max(lastPressed.Count - code.Count, 0); i < lastPressed.Count; i++) {
      if (lastPressed[i] != code[i - lastPressed.Count + code.Count]) return false;
    }

    return true;
  }

  public void AddKey(Keys key) {
    if (lastPressed.Count >= maxBufferSize) {
      lastPressed.RemoveAt(0);
    }
    lastPressed.Add(key);
  }

  public void Update(double deltaTime) {
    if (pressedDeltaTime <= MAX_WAIT_TIME) {
      var cheatCodeExecuted = false;

      foreach (var code in Instance.cheatCodes) {
        if (CodesMatch(code.Key)) {
          code.Value.Execute();

          cheatCodeExecuted = true;
        }
      }

      if (cheatCodeExecuted) {
        lastPressed.Clear();
      }

      pressedDeltaTime += deltaTime;
    } else {
      lastPressed.Clear();
      pressedDeltaTime = 0;
    }
  }
}
