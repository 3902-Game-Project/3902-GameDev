using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

internal class TextureStore : IInitable {
  public static TextureStore Instance { get; } = new TextureStore();

  public Texture2D WhitePixel { get; private set; }
  public Texture2D MainBlockItemAtlas { get; private set; }
  public Texture2D HealthBar { get; private set; }

  public void Initialize() { }

  public void LoadContent(ContentManager contentManager) {
    WhitePixel = contentManager.Load<Texture2D>("Misc/WhitePixel");
    MainBlockItemAtlas = contentManager.Load<Texture2D>("Misc/desert-atlas-v8");
    HealthBar = contentManager.Load<Texture2D>("Misc/blood_red_bar");
  }
}
