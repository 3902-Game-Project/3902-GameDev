using GameProject.FireModes;
using GameProject.PlayerSpace;
using GameProject.ProjectilePatterns;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class BFGItem : ABaseGun {
  internal BFGItem(Texture2D texture, Vector2 startPosition, Player player, ProjectileManagerGetter GetProjectileManager, GunStats stats)
    : base(texture, startPosition, player, GetProjectileManager, stats) {
    Category = ItemCategory.Primary;

    sourceRectangle = new Rectangle(0, 0, 40, 20);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2f, 0) * SCALE;
    fireMode = new SemiAutoFire(this.stats);
    projectilePattern = new BFGPattern();
  }
}
