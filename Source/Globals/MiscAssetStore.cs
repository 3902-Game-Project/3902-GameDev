using GameProject.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

public class MiscAssetStore : IInitable {
  public static MiscAssetStore Instance { get; } = new MiscAssetStore();

  public SpriteFont MainFont { get; private set; }

  public void Initialize() { }

  public void LoadContent(ContentManager contentManager) {
    MainFont = contentManager.Load<SpriteFont>("Misc/CreditsFont");
  }
}
