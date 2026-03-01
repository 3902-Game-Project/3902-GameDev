using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Managers;

internal class LevelManager : ILevelManager {
  string[] LEVEL_NAMES = [
    "00_test"
  ];

  List<ILevel> levels = new();

  public void Initialize() {
    throw new System.NotImplementedException();
  }

  public void LoadContent(ContentManager content) {
    throw new System.NotImplementedException();
  }

  public void Update(GameTime gameTime) {
    throw new System.NotImplementedException();
  }

  public void Draw(GameTime gameTime) {
    throw new System.NotImplementedException();
  }
}
