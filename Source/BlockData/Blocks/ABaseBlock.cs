using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal abstract class ABaseBlock : IBlock, ICollidable {
  internal IShape Shape => Collider;
  internal BoxCollider Collider { get; set; }
  internal Vector2 Position { get; set; }

  internal Layer Layer { get; } = Layer.Environment;

  internal Rectangle BoundingBox => throw new System.NotImplementedException();

  private protected ABaseBlock(Vector2 position, float width = Constants.BASE_BLOCK_WIDTH, float height = Constants.BASE_BLOCK_HEIGHT) {
    Position = position;
    Vector2 centerOffset = new(width / 2.0f, height / 2.0f);

    Collider = new BoxCollider(width, height, position + centerOffset);
  }

  internal abstract void Update(double deltaTime);
  
  internal abstract void Draw(SpriteBatch spriteBatch);

  internal virtual void OnCollision(CollisionInfo info) { }
}
