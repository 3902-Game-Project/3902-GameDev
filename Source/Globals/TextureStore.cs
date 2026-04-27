using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

internal class TextureStore : IInitable {
  public static TextureStore Instance { get; } = new TextureStore();

  public Texture2D WhitePixel { get; private set; }
  public Texture2D Player { get; private set; }
  public Texture2D MainBlockItemAtlas { get; private set; }
  public Texture2D HealthBar { get; private set; }
  public Texture2D HUDBackground { get; private set; }

  public void Initialize() { }

  public void LoadContent(ContentManager contentManager) {
    WhitePixel = contentManager.Load<Texture2D>("Misc/WhitePixel");
    Player = contentManager.Load<Texture2D>("Misc/playerSpritesheet");
    MainBlockItemAtlas = contentManager.Load<Texture2D>("Misc/desert-atlas-v10");
    HealthBar = contentManager.Load<Texture2D>("Misc/blood_red_bar");
    HUDBackground = contentManager.Load<Texture2D>("Misc/HUD Background");
  }
}
