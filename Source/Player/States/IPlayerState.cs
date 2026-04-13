using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

internal interface IPlayerState {
  void Update(GameTime gameTime);
  void Draw(SpriteBatch spriteBatch);
  void UseItem(UseType useType);
  void MoveUp();
  void MoveDown();
  void MoveLeft();
  void MoveRight();
  void Die();
}
