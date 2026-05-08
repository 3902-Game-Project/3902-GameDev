using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Items;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

internal class ItemWorldPickup(IItem item, Vector2 dropVelocity = default) : ABaseWorldPickup(item.Position), ICollidable {
  internal Vector2 Velocity { get; set; } = dropVelocity;

  internal override void Draw(SpriteBatch spriteBatch) {
    item.Draw(spriteBatch);
  }

  internal override void Update(double deltaTime) {
    Position += Velocity * ((float) deltaTime);
    item.Position = Position;

    if (Shape is CircleCollider circle) {
      circle.Position = Position;
    }

    Velocity *= 0.85f;
    if (Velocity.LengthSquared() < 1f) Velocity = Vector2.Zero;
  }

  internal override void OnPickup(Player player) {
    player.Inventory.PickupItem(item);
  }
}
