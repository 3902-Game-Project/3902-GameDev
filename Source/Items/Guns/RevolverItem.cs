using GameProject.FireModes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class RevolverItem : DefaultGun {
  public RevolverItem(Texture2D texture, Vector2 startPosition, Game1 game, GunStats stats)
    : base(texture, startPosition, game, stats) {

    Category = ItemCategory.Sidearm;
    sourceRectangle = new Rectangle(0, 0, 16, 9);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2f, -1f * (sourceRectangle.Height / 2f - 3f)) * scale;
    fireMode = new SemiAutoFire(this.stats);
  }
}
