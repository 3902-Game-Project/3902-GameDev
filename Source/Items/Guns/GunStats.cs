using GameProject.Factories;
using Microsoft.Xna.Framework.Audio;

namespace GameProject.Items;

public class GunStats {
  public float BulletVelocity { get; set; } = 200f;
  public float BulletLifetime { get; set; } = 5f;
  public float SpreadAngle { get; set; } = 0f;
  public int PelletCount { get; set; } = 1;
  public float FireRate { get; set; } = 1f;
  public float ReloadTime { get; set; } = 1f;
  public int MaxAmmo { get; set; } = 1;
  public int CurrentAmmo { get; set; } = 1;
  public float BaseDamage { get; set; } = 0f;
  public SoundEffect GunshotSFX { get; set; } = SoundFactory.Instance.CreateGunshotDefaultSFX();
  public SoundEffect ReloadSFX { get; set; } = SoundFactory.Instance.CreateReloadDefaultSFX();

  public GunStats() { }
}
