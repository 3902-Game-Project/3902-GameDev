using Microsoft.Xna.Framework.Content;

namespace GameProject.GlobalInterfaces;

internal interface IInitable {
  void Initialize();

  void LoadContent(ContentManager content);
}
