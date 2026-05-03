using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

internal class TextureStore : IInitable {
  public static TextureStore Instance { get; } = new TextureStore();

  public Texture2D WhitePixel { get; private set; }
  public Texture2D TitleScreen { get; private set; }
  public Texture2D Player { get; private set; }
  public Texture2D MainBlockItemAtlas { get; private set; }
  public Texture2D NewGuns { get; private set; }
  public Texture2D AmmoRefill { get; private set; }
  public Texture2D Boss { get; private set; }
  public Texture2D HealthBar { get; private set; }
  public Texture2D HUDBackground { get; private set; }

  public void Initialize() { }

  public void LoadContent(ContentManager contentManager) {
    WhitePixel = contentManager.Load<Texture2D>("Misc/White Pixel");
    TitleScreen = contentManager.Load<Texture2D>("Misc/Title Screen");
    Player = contentManager.Load<Texture2D>("Misc/Player Spritesheet");
    MainBlockItemAtlas = contentManager.Load<Texture2D>("Misc/Main Block Item Atlas");
    NewGuns = contentManager.Load<Texture2D>("World Pickups/Items/New Guns Spritesheet");
    AmmoRefill = contentManager.Load<Texture2D>("World Pickups/Ammo Refill Spritesheet");
    Boss = contentManager.Load<Texture2D>("Enemies/Boss Spritesheet");
    HealthBar = contentManager.Load<Texture2D>("Misc/Health Bar");
    HUDBackground = contentManager.Load<Texture2D>("Misc/HUD Background");
  }
}
