using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

public interface IBlock {
  ICollider Collider { get; }
  public void Update(GameTime gameTime);
  public void Draw(SpriteBatch spriteBatch);

  Rectangle BoundingBox { get; }
}
