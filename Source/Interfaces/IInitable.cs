using Microsoft.Xna.Framework.Content;

namespace GameProject.Interfaces;

internal interface IInitable {
  void Initialize();

  void LoadContent(ContentManager content);
}
