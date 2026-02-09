using Microsoft.Xna.Framework.Graphics;

namespace sprint0;

public interface IEnemy {
  void Update();
  void Draw(SpriteBatch spriteBatch);
  void ChangeDirection();
  void TakeDamage();
}
