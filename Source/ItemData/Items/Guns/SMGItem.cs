using GameProject.FireModes;
using GameProject.PlayerSpace;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class SMGItem : ABaseGun {
  public SMGItem(Texture2D texture, Vector2 startPosition, Player player, ProjectileManagerGetter GetProjectileManager, GunStats stats) :
    base(texture, startPosition, player, GetProjectileManager, stats) {
    Category = ItemCategory.Primary;

    sourceRectangle = new Rectangle(0, 20, 25, 25);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width * 0.5f, 0.0f) * SCALE;
    fireMode = new AutomaticFire(this.stats);
  }
}
