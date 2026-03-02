using Microsoft.Xna.Framework.Content;

namespace GameProject.Interfaces;

public interface IInitable {
  void Initialize();

  void LoadContent(ContentManager content);
}
