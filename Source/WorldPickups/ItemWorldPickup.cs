using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Items;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

internal class ItemWorldPickup(IItem item, Vector2 dropVelocity = default) : ABaseWorldPickup(item.Position), ICollidable {
  public Vector2 Velocity { get; set; } = dropVelocity;

  public override void Draw(SpriteBatch spriteBatch) {
    item.Draw(spriteBatch);
  }

  public override void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

    Position += Velocity * dt;
    item.Position = Position;

    if (Shape is CircleCollider circle) {
      circle.Position = Position;
    }

    Velocity *= 0.85f;
    if (Velocity.LengthSquared() < 1f) Velocity = Vector2.Zero;
  }

  public override void OnPickup(Player player) {
    player.Inventory.PickupItem(item);
  }
}
