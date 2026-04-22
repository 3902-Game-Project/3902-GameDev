using System.Collections.Generic;
using System.Diagnostics;
using GameProject.Items;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Source.Misc;

internal class CheatCodes {
  public static CheatCodes Instance { get; } = new CheatCodes();

  private bool healthOn = false;
  private bool ammoOn = false;
  private bool itemsOn = false;
  private readonly int buffer = 8;
  public List<Keys> lastPressed = new List<Keys>();

  // Unlimited Health Mappings
  private List<Keys> unlimitedHealthWASD = new List<Keys>{
    Keys.W, Keys.W, Keys.S, Keys.S, Keys.A, Keys.D, Keys.A, Keys.D
  };
  private List<Keys> unlimitedHealthArrows = new List<Keys>{
    Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right
  };

  // Unlimited Ammo Mappings
  private List<Keys> unlimitedAmmoWASD = new List<Keys>{
    Keys.W, Keys.A, Keys.S, Keys.D, Keys.W, Keys.A, Keys.S, Keys.D
  };
  private List<Keys> unlimitedAmmoArrows = new List<Keys>{
    Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Right
  };

  // Unlimited Items Mappings
  private List<Keys> unlimitedItemsWASD = new List<Keys>{
    Keys.W, Keys.W, Keys.A, Keys.A, Keys.D, Keys.D, Keys.S, Keys.S
  };
  private List<Keys> unlimitedItemsArrows = new List<Keys>{
    Keys.Up, Keys.Up, Keys.Left, Keys.Left, Keys.Right, Keys.Right, Keys.Down, Keys.Down
  };

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
    if (CodesMatch(unlimitedItemsWASD) || CodesMatch(unlimitedItemsArrows)) {
      // handle items

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

  public void UpdateCheats(Player player) {
    if (!healthOn) Instance.UnliimitedHealth(player);
    if (!ammoOn) Instance.UnlimitedAmmo(player);
    if (!itemsOn) Instance.UnliimitedItems(player);
  }
}
