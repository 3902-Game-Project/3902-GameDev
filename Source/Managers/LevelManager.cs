using System;
using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Managers;

internal class LevelManager : ILevelManager {
  string[] LEVEL_NAMES = [
    "00_test"
  ];

  List<ILevel> levels = new();
  int currentLevel = 0;

  public void Initialize() { }

  public void LoadContent(ContentManager content) {
    if (LEVEL_NAMES.Length == 0) {
      throw new ArgumentException("There must be at least one level to load");
    }

    foreach (var name in LEVEL_NAMES) {
      levels.Add(Level.FromString(content.Load<string>(name)));
    }
  }

  public void Update(GameTime gameTime) {
    levels[currentLevel].Update(gameTime);
  }

  public void Draw(GameTime gameTime) {
    levels[currentLevel].Draw(gameTime);
  }

  public void SwitchLevel(int levelIndex) {
    if (levelIndex < 0 || levelIndex >= LEVEL_NAMES.Length) {
      throw new ArgumentException("Attempt to set level index out of bounds");
    }

    currentLevel = levelIndex;
  }
}
