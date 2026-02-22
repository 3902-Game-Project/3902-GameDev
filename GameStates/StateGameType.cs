using System.Collections.Generic;
using GameProject.Controllers;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.GameStates;

public class StateGameType(Game1 game) : IGameState {
  private IController keyboardController;
  public List<IBlock> Blocks; // temporary for sprint2
  public int BlockNumber { get; set; } // temporary for sprint2
  public List<IItem> Items; // temporary for sprint2
  public int ItemNumber { get; set; } // temporary for sprint2

  public List<ISprite> Enemies { get; set; } = new List<ISprite>();

  public IBlock BlockSprite { get; set; }
  public ISprite CurrentSprite { get; set; }
  public IItem ItemSprite { get; set; }

  public void Initialize() {
    keyboardController = new GameKeyboardController(game);
    Blocks = new List<IBlock>(); // temporary for sprint2
    BlockNumber = Blocks.Count; // temporary for sprint2
    Items = new List<IItem>(); // temporary for sprint2
    ItemNumber = Items.Count; // temporary for sprint2
  }

  public void LoadContent() {
  }

  public void Update(GameTime gameTime) {
    foreach (var enemy in Enemies) {
      enemy.Update(gameTime);
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
      enemy.Draw(game.SpriteBatch);
    }

    game.SpriteBatch.End();
  }
}
