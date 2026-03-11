using GameProject.Controllers;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.Source.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateGameType(Game1 game) : IGameState {
  private IController keyboardController;
  private IController mouseController;
  private IController gamePadController;

  private CollisionManager collisionManager;

  public Player Player { get; private set; } = new Player(game);

  public ILevelManager LevelManager { get; private set; }

  public void Initialize() {
    keyboardController = new GameKeyboardController(game);
    mouseController = new GameMouseController(game);
    gamePadController = new GameGamePadController(game);
    LevelManager = new LevelManager(game);
    LevelManager.Initialize();
    collisionManager = new CollisionManager();
    collisionManager.AddCollider(Player);
  }

  public void LoadContent() {
    Player.LoadContent();

    game.BlockFactory.LoadAllTextures(game.Content);

    LevelManager.LoadContent(game.Content);
  }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    mouseController.Update(gameTime);
    gamePadController.Update(gameTime);

    LevelManager.Update(gameTime);
    Player.Update(gameTime);
    game.ProjectileManager.Update(gameTime);

    collisionManager.Clear();
    collisionManager.AddCollider(Player);

    if (LevelManager.CurrentLevel != null) {
      foreach (var block in LevelManager.CurrentLevel.CollidableBlocks) {
        collisionManager.AddCollider(block);
      }
      foreach (var enemy in LevelManager.CurrentLevel.Enemies) {
        collisionManager.AddCollider(enemy);
      }
    }
    collisionManager.Update(gameTime);

    // whoever is responsible for blocks move this into Level.cs and fix this to use collidableBlocks:
    /*
    if (Blocks != null) {
      foreach (IBlock block in Blocks) {
        block.Update(gameTime);
        if (block.Collider is BoxCollider blockBox && Player.Collider is BoxCollider playerBox) {

          if (playerBox.CheckCollision(blockBox)) {
            float distanceX = playerBox.position.X - blockBox.position.X;
            float distanceY = playerBox.position.Y - blockBox.position.Y;
            float minDistanceX = (playerBox.dimensions.X / 2f) + (blockBox.dimensions.X / 2f);
            float minDistanceY = (playerBox.dimensions.Y / 2f) + (blockBox.dimensions.Y / 2f);

            float depthX = minDistanceX - System.Math.Abs(distanceX);
            float depthY = minDistanceY - System.Math.Abs(distanceY);

            if (depthX < depthY) {
              if (distanceX > 0) {
                Player.Position = new Vector2(Player.Position.X + depthX, Player.Position.Y);
              } else {
                Player.Position = new Vector2(Player.Position.X - depthX, Player.Position.Y);
              }
            } else {
              if (distanceY > 0) {
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y + depthY);
              } else {
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y - depthY);
              }
            }

            Player.Collider.position = Player.Position;
          }
        }
      }
    }*/

    // Move into Level.cs and fix:
    /*
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
      }*/

    //Todo: bullet vs enemy

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

    LevelManager.Draw(gameTime);

    // change to use Blocks from Level.cs
    /*if (Blocks != null && Blocks.Count > 0 && BlockNumber < Blocks.Count) {
      Blocks[BlockNumber].Draw(game.SpriteBatch);
    }*/

    Player.Draw(game.SpriteBatch);

    game.ProjectileManager.Draw(game.SpriteBatch);
    collisionManager.DebugDraw(game.SpriteBatch, game.GraphicsDevice);
    game.SpriteBatch.End();
  }
}
