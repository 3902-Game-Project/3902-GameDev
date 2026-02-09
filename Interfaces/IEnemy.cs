using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces
{
  public interface IEnemy {
    void Update(GameTime gameTime); 
    void Draw(SpriteBatch spriteBatch);
    void ChangeDirection();
    void TakeDamage();
  }
}
