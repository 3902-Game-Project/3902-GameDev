using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

internal enum UseType { Pressed, Held, Released }
internal interface IController {
  void Update(GameTime gameTime);
}
