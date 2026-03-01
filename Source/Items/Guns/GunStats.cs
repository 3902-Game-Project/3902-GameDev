namespace GameProject.Items.Guns;

public class GunStats {
  public float BulletVelocity { get; set; }
  public float BulletLifetime { get; set; }
  public float SpreadAngle { get; set; } = 0f;
  public int PelletCount { get; set; } = 1;
  public float FireRate { get; set; }
  public float ReloadTime { get; set; }
  public int MaxAmmo { get; set; }
  public int CurrentAmmo { get; set; }
  public float BaseDamage { get; set; }

  public GunStats() { }
}
