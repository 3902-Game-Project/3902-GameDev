using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

public static class MiscAssetStore {
  public static SpriteFont MainFont { get; private set; }

  public static void Initialize() { }

  public static void LoadContent(ContentManager contentManager) {
    MainFont = contentManager.Load<SpriteFont>("Misc/CreditsFont");
  }
}
