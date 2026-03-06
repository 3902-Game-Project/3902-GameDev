using System.Collections.Generic;
using GameProject.Controllers;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.Source.Collision;
using GameProject.Source.CollisionResponse;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateGameType(Game1 game) : IGameState {
  private IController keyboardController;
  private IController mouseController;

  private CollisionHandler collisionHandler;

  public Player Player { get; private set; } = new Player(game);
  public List<IBlock> Blocks; // temporary for sprint2
  public int BlockNumber { get; set; } // temporary for sprint2
  public List<IItem> Items; // temporary for sprint2
  public int ItemNumber { get; set; } // temporary for sprint2

  public int EnemyNumber { get; set; } = 0;  // temporary for sprint2

  public List<IEnemy> Enemies { get; set; } = new List<IEnemy>();

  public IBlock BlockSprite { get; set; }
  public ISprite CurrentSprite { get; set; }
  public IItem ItemSprite { get; set; }

  public ILevelManager LevelManager { get; private set; }

  public void Initialize() {
    keyboardController = new GameKeyboardController(game);
    mouseController = new GameMouseController(game);
    LevelManager = new LevelManager(game);
    Blocks = new List<IBlock>(); // temporary for sprint2
    BlockNumber = Blocks.Count; // temporary for sprint2
    Items = new List<IItem>(); // temporary for sprint2
    ItemNumber = Items.Count; // temporary for sprint2
    LevelManager.Initialize();
    collisionHandler = new CollisionHandler();
  }

  public void LoadContent() {
    Player.LoadContent();
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

    LevelManager.LoadContent(game.Content);
  }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    mouseController.Update(gameTime);
    LevelManager.Update(gameTime);
    Player.Update(gameTime);

    if (Enemies != null && Enemies.Count > 0) {
      Enemies[EnemyNumber].Update(gameTime);
    }

    if (Items != null && Items.Count > 0) {
      Items[ItemNumber].Update(gameTime);
    }

    game.ProjectileManager.Update(gameTime);

    if (Enemies != null && Enemies.Count > 0 && EnemyNumber < Enemies.Count) {
      var activeEnemy = Enemies[EnemyNumber];

      //player vs enemy
      CollisionSide playerEnemySide = CollisionDetector.GetCollisionSide(Player.BoundingBox, activeEnemy.BoundingBox);
      if (playerEnemySide != CollisionSide.None) {
        collisionHandler.HandleCollision(Player, activeEnemy, playerEnemySide);
      }

      //enemy vs block
      if (Blocks != null && Blocks.Count > 0 && BlockNumber < Blocks.Count) {
        var activeBlock = Blocks[BlockNumber];
        CollisionSide enemyBlockSide = CollisionDetector.GetCollisionSide(activeEnemy.BoundingBox, activeBlock.BoundingBox);
        if (enemyBlockSide != CollisionSide.None) {
          collisionHandler.HandleCollision(activeEnemy, activeBlock, enemyBlockSide);
        }
      }

      //Todo: bullet vs enemy


    }

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

    LevelManager.Draw(gameTime);

    game.SpriteBatch.End();
  }
}
