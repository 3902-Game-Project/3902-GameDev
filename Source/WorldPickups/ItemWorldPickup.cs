using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

public class ItemWorldPickup(IItem item, Vector2 dropVelocity) : BaseWorldPickup(item.Position), ICollidable, IWorldPickup {
  public Vector2 Velocity { get; set; } = dropVelocity;

  public override void Draw(SpriteBatch spriteBatch) {
    item.Draw(spriteBatch);
  }

  public override void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

    Position += Velocity * dt;
    item.Position = Position;

    Velocity *= 0.85f;
    if (Velocity.LengthSquared() < 1f) Velocity = Vector2.Zero;
  }
  public void OnPickup(Player player) {
    player.Inventory.PickupItem(item);
  }
}
