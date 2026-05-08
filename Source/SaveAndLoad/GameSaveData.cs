using System.Collections.Generic;
using GameProject.Enemies;
using GameProject.Items;

namespace GameProject.SaveLoad;

internal class GameSaveData {
  internal PlayerSaveData Player { get; set; } = new();
  internal LevelSaveData Level { get; set; } = new();
}

internal class PlayerSaveData {
  internal float X { get; set; }
  internal float Y { get; set; }
  internal int Health { get; set; }
  internal Dictionary<AmmoType, int> Ammo { get; set; } = [];
  internal List<string> WeaponTypes { get; set; } = [];
  internal int ActiveWeaponIndex { get; set; }
}

internal class LevelSaveData {
  internal string LevelName { get; set; }
  internal List<EnemySaveData> Enemies { get; set; } = [];
}

internal class EnemySaveData {
  internal string TypeName { get; set; }
  internal float X { get; set; }
  internal float Y { get; set; }
  internal int Health { get; set; }
  internal FacingDirection Direction { get; set; }
}
