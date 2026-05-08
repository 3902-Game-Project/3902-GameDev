using GameProject.FireModes;
using GameProject.PlayerSpace;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class RifleItem : ABaseGun {
  public RifleItem(Texture2D texture, Vector2 startPosition, Player player, ProjectileManagerGetter GetProjectileManager, GunStats stats)
    : base(texture, startPosition, player, GetProjectileManager, stats) {
    Category = ItemCategory.Primary;
    sourceRectangle = new Rectangle(0, 19, 37, 10);
    this.stats.FireRate /= 3f;
    bulletSpawnOffset = new Vector2(sourceRectangle.Width * 0.5f, -1.0f * (sourceRectangle.Height * 0.5f - 3.0f)) * SCALE;
    fireMode = new SemiAutoFire(this.stats);
  }
}
