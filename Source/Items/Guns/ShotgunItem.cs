using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class ShotgunItem : DefaultGun {

  public ShotgunItem(Texture2D texture, Vector2 position, Game1 game, GunStats stats)
    : base(texture, position, game, stats) {

    this.sourceRectangle = new(0, 10, 27, 9);
    Category = ItemCategory.Primary;

    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 * (sourceRectangle.Height / 2 - 3)) * scale;

    fireMode = new SemiAutoFire(stats);
    projectilePattern = new SpreadPattern();
  }
}
