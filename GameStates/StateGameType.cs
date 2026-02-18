using System.Collections.Generic;
using GameProject.Controllers;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateGameType(Game1 game) : IGameState {
  private IController keyboardController;
  public List<IBlock> Blocks;
  public int BlockNumber { get; set; } = 0;

  public List<ISprite> Enemies {  get; set; } = new List<ISprite>();

  public void Initialize() {
    keyboardController = new GameKeyboardController(game);
    Blocks = new List<IBlock>();
  }

  public void LoadContent() {
  }

  public void Update(GameTime gameTime) {
    foreach (var enemy in Enemies) {
      {
        enemy.Update(gameTime);
      }
    }

    keyboardController.Update(gameTime);
  }

  public void Draw(GameTime gameTime) {
    game.GraphicsDevice.Clear(Color.CornflowerBlue);
    game.SpriteBatch.Begin();

    if (Blocks != null && Blocks.Count > 0 && BlockNumber < Blocks.Count) {
      Blocks[BlockNumber].Draw(game.SpriteBatch);
    }

    game.Player.Draw(game.SpriteBatch);

    foreach (var enemy in Enemies) {
      {
        enemy.Draw(game.SpriteBatch);
      }
    }

    game.SpriteBatch.End();
  }
}
