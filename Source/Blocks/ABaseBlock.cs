using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal abstract class ABaseBlock : IBlock, ICollidable {
  public IShape Shape => Collider;
  public BoxCollider Collider { get; set; }
  public Vector2 Position { get; set; }

  public Layer Layer { get; } = Layer.Environment;
  public Layer Mask { get; } = Layer.Player;

  public Rectangle BoundingBox => throw new System.NotImplementedException();

  protected ABaseBlock(Vector2 position, float width = Constants.BASE_BLOCK_WIDTH, float height = Constants.BASE_BLOCK_HEIGHT) {
    Position = position;
    Vector2 centerOffset = new(width / 2.0f, height / 2.0f);

    Collider = new BoxCollider(width, height, position + centerOffset);
  }

  public virtual void OnCollision(CollisionInfo info) { }
  public abstract void Update(double deltaTime);
  public abstract void Draw(SpriteBatch spriteBatch);
}
