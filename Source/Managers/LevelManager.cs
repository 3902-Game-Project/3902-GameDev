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

  private readonly List<ILevel> levels = new();
  private int currentLevelIndex = 0;

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
    levels[currentLevelIndex].Update(gameTime);
  }

  public void Draw(GameTime gameTime) {
    levels[currentLevelIndex].Draw(gameTime);
  }

  public void SwitchLevel(int newLevelIndex) {
    if (newLevelIndex < 0 || newLevelIndex >= LEVEL_NAMES.Length) {
      throw new ArgumentException("Attempt to set level index out of bounds");
    }

    if (newLevelIndex != currentLevelIndex)
      currentLevelIndex = newLevelIndex;
  }

  public void PreviousLevel() {
    SwitchLevel(Math.Max(currentLevelIndex - 1, 0));
  }

  public void NextLevel() {
    SwitchLevel(Math.Min(currentLevelIndex + 1, LEVEL_NAMES.Length - 1));
  }
}
