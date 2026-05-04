using GameProject.FireModes;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class RevolverItem : ABaseGun {
  public RevolverItem(Texture2D texture, Vector2 startPosition, Player player, ILevelManager levelManager, GunStats stats)
    : base(texture, startPosition, player, levelManager, stats) {

    Category = ItemCategory.Sidearm;
    sourceRectangle = new Rectangle(0, 0, 16, 9);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2f, -1f * (sourceRectangle.Height / 2f - 3f)) * SCALE;
    fireMode = new SemiAutoFire(this.stats);
  }
}
