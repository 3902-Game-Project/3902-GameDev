using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

public static class AssetStore {
  public static TextureStore Textures { get; private set; } = new TextureStore();
  public static SpriteFont MainFont { get; private set; }

  public static void Initialize() {
    Textures.Initialize();
  }

  public static void LoadContent(ContentManager contentManager) {
    Textures.LoadContent(contentManager);
    MainFont = contentManager.Load<SpriteFont>("Misc/CreditsFont");
  }
}
