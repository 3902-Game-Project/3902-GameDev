using System.Collections.Generic;
using GameProject.Items;
using GameProject.Enemies;

namespace GameProject.SaveLoad;

internal class GameSaveData {
  public PlayerSaveData Player { get; set; } = new();
  public LevelSaveData Level { get; set; } = new();
}

internal class PlayerSaveData {
  public float X { get; set; }
  public float Y { get; set; }
  public int Health { get; set; }
  public Dictionary<AmmoType, int> Ammo { get; set; } = [];
  public List<string> WeaponTypes { get; set; } = [];
  public int ActiveWeaponIndex { get; set; }
}

internal class LevelSaveData {
  public List<EnemySaveData> Enemies { get; set; } = [];
}

internal class EnemySaveData {
  public string TypeName { get; set; }
  public float X { get; set; }
  public float Y { get; set; }
  public int Health { get; set; }
  public FacingDirection Direction { get; set; }
}
