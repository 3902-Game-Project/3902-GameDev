using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Collisions;
using GameProject.Interfaces;

namespace GameProject.Blocks;

public abstract class BaseBlock : IBlock, ICollidable {
  public IShape Shape => Collider;
  public BoxCollider Collider { get; private set; }
  public Vector2 Position { get; set; }

  public Layer Layer { get; } = Layer.Environment;
  public Layer Mask { get; } = Layer.Player;

  public Rectangle BoundingBox => throw new System.NotImplementedException();

  protected BaseBlock(Vector2 position, float width = 64f, float height = 64f) {
    this.Position = position;
    Vector2 centerOffset = new Vector2(width / 2f, height / 2f);

    this.Collider = new BoxCollider(width, height, position + centerOffset);
  }

  public virtual void OnCollision(CollisionInfo info) {
  }
  public abstract void Update(GameTime gameTime);
  public abstract void Draw(SpriteBatch spriteBatch);
}
