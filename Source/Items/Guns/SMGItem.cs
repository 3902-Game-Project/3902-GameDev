using GameProject.FireModes;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class SMGItem : ABaseGun {
  public SMGItem(Texture2D texture, Vector2 startPosition, Player player, ILevelManager levelManager, GunStats stats)
    : base(texture, startPosition, player, levelManager, stats) {
    Category = ItemCategory.Primary;
    sourceRectangle = new Rectangle(0, 0, 25, 10);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2f, 0) * scale;
    fireMode = new AutomaticFire(this.stats);
  }
}
