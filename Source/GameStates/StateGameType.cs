using System;
using System.Collections.Generic;
using GameProject.Controllers;
using GameProject.Factories;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateGameType(Game1 game) : IGameState {
  private IController keyboardController;
  public Player Player { get; private set; } = new Player(game);
  public List<IBlock> Blocks; // temporary for sprint2
  public int BlockNumber { get; set; } // temporary for sprint2
  public List<IItem> Items; // temporary for sprint2
  public int ItemNumber { get; set; } // temporary for sprint2

  public int EnemyNumber { get; set; } = 0;  // temporary for sprint2

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
    game.BlockFactory.LoadAllTextures(game);

    var snake = EnemySpriteFactory.Instance.CreateSnakeSprite();
    Enemies.Add(snake);

    var shotgunner = EnemySpriteFactory.Instance.CreateShotgunnerSprite(game.ProjectileManager);
    Enemies.Add(shotgunner);

    var bat = EnemySpriteFactory.Instance.CreateBatSprite();
    Enemies.Add(bat);

    var rifleman = EnemySpriteFactory.Instance.CreateRifleSprite(game.ProjectileManager);
    Enemies.Add(rifleman);

    var tumbleweed = EnemySpriteFactory.Instance.CreateTumbleweedSprite();
    Enemies.Add(tumbleweed);

    var cactus = EnemySpriteFactory.Instance.CreateCactusSprite();
    Enemies.Add(cactus);

    var revolver = game.ItemSpriteFactory.CreateRevolver();
    Items.Add(revolver);

    var rifle = game.ItemSpriteFactory.CreateRifle();
    Items.Add(rifle);

    var shotgun = game.ItemSpriteFactory.CreateShotgun();
    Items.Add(shotgun);
  }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    Player.Update(gameTime);

    if (Enemies != null && Enemies.Count > 0) {
      Enemies[EnemyNumber].Update(gameTime);
    }

    game.ProjectileManager.Update(gameTime);
  }

  public void Draw(GameTime gameTime) {
    game.GraphicsDevice.Clear(Color.CornflowerBlue);
    game.SpriteBatch.Begin(
      SpriteSortMode.Deferred,
      BlendState.AlphaBlend,
      SamplerState.PointClamp,
      DepthStencilState.None,
      RasterizerState.CullNone
    );

    if (Blocks != null && Blocks.Count > 0 && BlockNumber < Blocks.Count) {
      Blocks[BlockNumber].Draw(game.SpriteBatch);
    }

    if (Items != null && Items.Count > 0 && ItemNumber < Items.Count) {
      Items[ItemNumber].Draw(game.SpriteBatch);
    }

    Player.Draw(game.SpriteBatch);

    if (Enemies != null && Enemies.Count > 0) {
      Enemies[EnemyNumber].Draw(game.SpriteBatch);
    }

    game.ProjectileManager.Draw(game.SpriteBatch);
    game.SpriteBatch.End();
  }
}
