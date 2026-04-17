using GameProject.FireModes;
using GameProject.Managers;
using GameProject.PlayerSpace;
using GameProject.ProjectilePatterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class ShotgunItem : ABaseGun {
  public ShotgunItem(Texture2D texture, Vector2 position, Player player, ILevelManager levelManager, GunStats stats)
    : base(texture, position, player, levelManager, stats) {

    Category = ItemCategory.Primary;
    sourceRectangle = new Rectangle(0, 10, 27, 9);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2f, -1f * (sourceRectangle.Height / 2f - 3f)) * scale;
    fireMode = new SemiAutoFire(this.stats);

    // Override the default SingleShotPattern from DefaultGun
    projectilePattern = new SpreadPattern();
  }
}
