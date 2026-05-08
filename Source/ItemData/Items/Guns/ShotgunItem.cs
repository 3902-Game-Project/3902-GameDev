using GameProject.FireModes;
using GameProject.PlayerSpace;
using GameProject.ProjectilePatterns;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class ShotgunItem : ABaseGun {
  public ShotgunItem(Texture2D texture, Vector2 position, Player player, ProjectileManagerGetter GetProjectileManager, GunStats stats)
    : base(texture, position, player, GetProjectileManager, stats) {
    Category = ItemCategory.Primary;
    sourceRectangle = new Rectangle(0, 10, 27, 9);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width * 0.5f, -1.0f * (sourceRectangle.Height * 0.5f - 3.0f)) * SCALE;
    fireMode = new SemiAutoFire(this.stats);

    // Override the default SingleShotPattern from DefaultGun
    projectilePattern = new SpreadPattern();
  }
}
