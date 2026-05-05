using GameProject.Misc;

namespace GameProject.Items;

internal class GunStats {
  public float BulletVelocity { get; set; } = 200f;
  public float BulletLifetime { get; set; } = 5f;
  public float SpreadAngle { get; set; } = 0f;
  public int PelletCount { get; set; } = 1;
  public float FireRate { get; set; } = 1f;
  public float ReloadTime { get; set; } = 1f;
  public int MaxAmmo { get; set; } = 1;
  public int CurrentAmmo { get; set; } = 1;
  public int BaseDamage { get; set; } = 50;
  public SoundID GunshotID { get; set; } = SoundID.GunshotDefault;
  public SoundID ReloadID { get; set; } = SoundID.ReloadDefault;
  public AmmoType AmmoType { get; set; } = AmmoType.Light;
  public bool ReloadsOneByOne { get; set; } = true;
  public float EquipTime { get; set; } = 0.5f;

  public GunStats() { }
}
