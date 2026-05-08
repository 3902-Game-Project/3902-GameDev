using GameProject.Misc;

namespace GameProject.Items;

internal class GunStats {
  internal float BulletVelocity { get; set; } = 200f;
  internal float BulletLifetime { get; set; } = 5f;
  internal float SpreadAngle { get; set; } = 0f;
  internal int PelletCount { get; set; } = 1;
  internal float FireRate { get; set; } = 1f;
  internal float ReloadTime { get; set; } = 1f;
  internal int MaxAmmo { get; set; } = 1;
  internal int CurrentAmmo { get; set; } = 1;
  internal int BaseDamage { get; set; } = 50;
  internal SoundID GunshotID { get; set; } = SoundID.GunshotDefault;
  internal SoundID ReloadID { get; set; } = SoundID.ReloadDefault;
  internal AmmoType AmmoType { get; set; } = AmmoType.Light;
  internal bool ReloadsOneByOne { get; set; } = true;
  internal float EquipTime { get; set; } = 0.5f;

  internal GunStats() { }
}
