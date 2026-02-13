using GameProject.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

public class TextureStore(Game1 game) : IGlobalData {
  public Texture2D MetroTexture { get; private set; }

  public void Initialize() { }

  public void LoadContent() {
    MetroTexture = game.Content.Load<Texture2D>("Metro");
  }
}
