using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class RifleItem : DefaultGun {
  public RifleItem(Texture2D texture, Vector2 startPosition, Game1 game, GunStats stats)
    : base(texture, startPosition, game, stats) {

    Category = ItemCategory.Primary;
    sourceRectangle = new Rectangle(0, 19, 37, 10);
    this.stats.FireRate /= 3f;
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2f, -1f * (sourceRectangle.Height / 2f - 3f)) * scale;
    fireMode = new SemiAutoFire(this.stats);
  }
}
