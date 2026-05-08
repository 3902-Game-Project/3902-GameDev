using Microsoft.Xna.Framework.Content;

namespace GameProject.GlobalInterfaces;

internal interface IInitable {
  internal void Initialize();

  internal void LoadContent(ContentManager contentManager);
}
