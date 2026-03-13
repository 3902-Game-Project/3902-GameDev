using GameProject.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

public class TextureStore(Game1 game) : IGlobalData {
  public Texture2D BlockTextures { get; private set; }
  public Texture2D PlayerTexture { get; private set; }
  public Texture2D WhitePixel { get; private set; }

  public void Initialize() { }

  public void LoadContent() {
    BlockTextures = game.Content.Load<Texture2D>("desert-atlas-v6");
    PlayerTexture = game.Content.Load<Texture2D>("playerSpritesheet");
    WhitePixel = game.Content.Load<Texture2D>("WhitePixel");
  }
}
