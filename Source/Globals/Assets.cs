using GameProject.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

public class AssetStore(Game1 game) : IGlobalData {
  public TextureStore Textures { get; private set; } = new TextureStore(game);
  public SpriteFont MainFont { get; private set; }

  public void Initialize() {
    Textures.Initialize();
  }

  public void LoadContent() {
    Textures.LoadContent();
    MainFont = game.Content.Load<SpriteFont>("Misc/CreditsFont");
  }
}
