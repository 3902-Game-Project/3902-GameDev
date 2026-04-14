using GameProject.Collisions;
using GameProject.Items;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

internal class AmmoWorldPickup(Texture2D texture, Vector2 position, AmmoType type, int amount) : BaseWorldPickup(position), ICollidable {
  public AmmoType Type { get; } = type;
  public int Amount { get; } = amount;

  private Rectangle sourceRectangle = new(9, 15, 12, 11);
  private Vector2 origin;
  private readonly float scale = 2f;

  public override void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
    // Dynamic tinting using existing sprites
    Color tintColor = Type == AmmoType.Light ? Color.Yellow : (Type == AmmoType.Heavy ? Color.Gray : Color.Red);
    spriteBatch.Draw(texture, Position, sourceRectangle, tintColor, 0f, origin, scale, SpriteEffects.None, 0f);
  }

  public override void Update(GameTime gameTime) { }

  public void OnPickup(Player player) {
    player.Inventory.Ammo[Type] += Amount;
  }
}
