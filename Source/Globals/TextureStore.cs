using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

public static class TextureStore {
  public static Texture2D WhitePixel { get; private set; }
  public static Texture2D HealthBarTexture { get; private set; }

  public static void Initialize() { }

  public static void LoadContent(ContentManager contentManager) {
    WhitePixel = contentManager.Load<Texture2D>("Misc/WhitePixel");
    HealthBarTexture = contentManager.Load<Texture2D>("Misc/blood_red_bar");
  }
}
