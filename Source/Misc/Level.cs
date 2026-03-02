using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Misc;

internal class Level : ILevel {
  private Level() { }

  public static Level FromString(string levelData) {
    return new Level();
  }

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(GameTime gameTime) { }

  public void Draw(GameTime gameTime) { }
}
