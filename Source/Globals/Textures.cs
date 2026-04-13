using GameProject.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

public class TextureStore : IGlobalData {
  public Texture2D WhitePixel { get; private set; }
  public Texture2D HealthBarTexture { get; private set; }

  public void Initialize() { }

  public void LoadContent(ContentManager contentManager) {
    WhitePixel = contentManager.Load<Texture2D>("Misc/WhitePixel");
    HealthBarTexture = contentManager.Load<Texture2D>("Misc/blood_red_bar");
  }
}
