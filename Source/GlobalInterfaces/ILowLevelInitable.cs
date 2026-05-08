using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GlobalInterfaces;

internal interface ILowLevelInitable {
  internal void Initialize();

  internal void LoadContent(GraphicsDevice graphicsDevice, ContentManager content);
}
