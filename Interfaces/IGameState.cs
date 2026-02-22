using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public interface IGameState {
  void Initialize();
  void LoadContent();
  void Update(GameTime gameTime);
  void Draw(GameTime gameTime);
}
