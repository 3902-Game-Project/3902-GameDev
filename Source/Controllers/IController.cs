using Microsoft.Xna.Framework;

namespace GameProject.Controllers;

internal enum UseType { Pressed, Held, Released }
internal interface IController {
  void Update(GameTime gameTime);
}
