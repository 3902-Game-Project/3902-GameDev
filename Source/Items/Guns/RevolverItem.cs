using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class RevolverItem : DefaultGun {

  public RevolverItem(Texture2D texture, Vector2 startPosition, Game1 game, GunStats stats)
    : base(texture, startPosition, game, stats) {

    this.sourceRectangle = new(0, 0, 16, 9);
    Category = ItemCategory.Sidearm;

    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 * (sourceRectangle.Height / 2 - 3)) * scale;

    fireMode = new SemiAutoFire(stats);
    projectilePattern = new SingleShotPattern();
  }
}
