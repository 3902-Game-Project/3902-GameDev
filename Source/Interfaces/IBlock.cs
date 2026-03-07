using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

public enum BlockState { solid, broken,   // for Barrel, 
                        locked, closed, blocked, open,  // for Doors,
                        lit, extinguished,  // for FirePit
                        still, moving }   // for Crate,  
public interface IBlock {
  ICollider Collider { get; }
  public void Update(GameTime gameTime);
  public void Draw(SpriteBatch spriteBatch);

  Rectangle BoundingBox { get; }
}
