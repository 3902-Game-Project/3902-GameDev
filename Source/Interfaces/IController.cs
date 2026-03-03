using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public enum UseType { Pressed, Held, Released }
public interface IController {
  void Update(GameTime gameTime);
}
