using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public interface IFireMode {
  bool CanFire(UseType useType);
  void Update(GameTime gameTime);
}
