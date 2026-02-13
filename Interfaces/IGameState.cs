using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

public interface IGameState {
  void Update(GameTime gameTime);
  void Draw(SpriteBatch spriteBatch, GameTime gameTime);
}
