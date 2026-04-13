using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class ShotgunItem : DefaultGun {
  public ShotgunItem(Texture2D texture, Vector2 position, Game1 game, GunStats stats)
    : base(texture, position, game, stats) {

    Category = ItemCategory.Primary;
    sourceRectangle = new Rectangle(0, 10, 27, 9);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2f, -1f * (sourceRectangle.Height / 2f - 3f)) * scale;
    fireMode = new SemiAutoFire(this.stats);

    // Override the default SingleShotPattern from DefaultGun
    projectilePattern = new SpreadPattern();
  }
}
