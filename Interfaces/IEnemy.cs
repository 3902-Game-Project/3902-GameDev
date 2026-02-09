using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

public interface IEnemy {
  void Update();
  void Draw(SpriteBatch spriteBatch);
  void ChangeDirection();
  void TakeDamage();
}
