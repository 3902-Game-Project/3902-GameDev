using System.Collections.Generic;
using GameProject.Controllers;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.GameStates;

public class StateGameType(Game1 game) : IGameState {
  private IController keyboardController;
  public List<IBlock> Blocks; // temporary for sprint2
  public int BlockNumber { get; set; } // temporary for sprint2
  
  
  public IBlock BlockSprite { get; set; }
  public ISprite CurrentSprite { get; set; }

  public void Initialize() {
    keyboardController = new GameKeyboardController(game);
    Blocks = new List<IBlock>(); // temporary for sprint2
    BlockNumber = Blocks.Count;
    
  }

  public void LoadContent() {
    CurrentSprite = new FixedSprite(game.GlobalVars.Assets.Textures.MetroTexture, new Vector2(400, 200));
  }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    CurrentSprite.Update(gameTime);

    //BlockSprite = Blocks[BlockNumber];
  }

  public void Draw(GameTime gameTime) {
    game.GraphicsDevice.Clear(Color.CornflowerBlue);

    game.SpriteBatch.Begin();
    CurrentSprite.Draw(game.SpriteBatch);
    Blocks[BlockNumber].Draw(game.SpriteBatch);
    game.SpriteBatch.End();
  }
}
