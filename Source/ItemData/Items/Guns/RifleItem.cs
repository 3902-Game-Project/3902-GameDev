using GameProject.FireModes;
using GameProject.Level;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class RifleItem : ABaseGun {
  public RifleItem(Texture2D texture, Vector2 startPosition, Player player, ILevelManager levelManager, GunStats stats)
    : base(texture, startPosition, player, levelManager, stats) {
    Category = ItemCategory.Primary;
    sourceRectangle = new Rectangle(0, 19, 37, 10);
    this.stats.FireRate /= 3f;
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2f, -1f * (sourceRectangle.Height / 2f - 3f)) * SCALE;
    fireMode = new SemiAutoFire(this.stats);
  }
}
