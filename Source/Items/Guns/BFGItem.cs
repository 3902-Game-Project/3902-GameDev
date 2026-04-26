using GameProject.FireModes;
using GameProject.Managers;
using GameProject.PlayerSpace;
using GameProject.ProjectilePatterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class BFGItem : ABaseGun {
  public BFGItem(Texture2D texture, Vector2 startPosition, Player player, ILevelManager levelManager, GunStats stats)
    : base(texture, startPosition, player, levelManager, stats) {

    Category = ItemCategory.Primary;

    sourceRectangle = new Rectangle(0, 0, 40, 20);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2f, 0) * scale;
    fireMode = new SemiAutoFire(this.stats);
    projectilePattern = new BFGPattern();
  }
}
