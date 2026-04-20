using GameProject.Collisions;
using GameProject.Items;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

internal class AmmoWorldPickup(Texture2D texture, Vector2 position, AmmoType type, int amount) : ABaseWorldPickup(position), ICollidable {
  public AmmoType Type { get; } = type;
  public int Amount { get; } = amount;

  private Vector2 origin;
  private readonly float scale = 2f;

  public override void Draw(SpriteBatch spriteBatch) {
    int xOffset = Type == AmmoType.Light ? 0 : (Type == AmmoType.Heavy ? 16 : 32);
    Rectangle trueSourceRect = new(xOffset, 0, 16, 16);

    origin = new Vector2(8, 8);

    spriteBatch.Draw(texture, Position, trueSourceRect, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
  }

  public override void Update(GameTime gameTime) { }

  public override void OnPickup(Player player) {
    player.Inventory.Ammo[Type] += Amount;
  }
}
