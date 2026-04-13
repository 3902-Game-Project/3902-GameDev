using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class RifleItem : DefaultGun {

  public RifleItem(Texture2D texture, Vector2 startPosition, Game1 game, GunStats stats)
    : base(texture, startPosition, game, stats) {
    
    this.sourceRectangle = new Rectangle(0, 19, 37, 10);
    this.stats.FireRate /= 3f; //this line is to change the frequency of the bullets in rifle
    Category = ItemCategory.Primary;

    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 * (sourceRectangle.Height / 2 - 3)) * scale;

    this.fireMode = new SemiAutoFire(this.stats);
    this.projectilePattern = new SingleShotPattern();
  }
}
