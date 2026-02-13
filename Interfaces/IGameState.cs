using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

public interface IGameState {
  void Initialize();
  void LoadContent();
  void Update(GameTime gameTime);
  void Draw(GameTime gameTime);
}
