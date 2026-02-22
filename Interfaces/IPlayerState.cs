using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

public interface IPlayerState {
  void Update(GameTime gameTime);
  void Draw(SpriteBatch spriteBatch);
  void UseItem();
  // TODO: Add MoveUp(), MoveDown(), MoveLeft(), MoveRight() here
}
