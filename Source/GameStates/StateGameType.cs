using GameProject.Controllers;
using GameProject.Interfaces;
using GameProject.Managers;
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

    Player.Inventory.PickupItem(game.ItemSpriteFactory.CreateRevolver(0f, 0f));
    Player.Inventory.PickupItem(game.ItemSpriteFactory.CreateRifle(0f, 0f));

    LevelManager.LoadContent(game.Content);
  }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    mouseController.Update(gameTime);
    gamePadController.Update(gameTime);

    LevelManager.Update(gameTime);
    Player.Update(gameTime);

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

    foreach (var projectile in LevelManager.CurrentLevel.ProjectileManager.Projectiles) {
      if (projectile is ICollidable collidableProj) {
        collisionManager.AddCollider(collidableProj);
      }
    }

    collisionManager.Update(gameTime);
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

    Player.Draw(game.SpriteBatch);

    // collisionManager.DebugDraw(game.SpriteBatch, game.GraphicsDevice);
    game.SpriteBatch.End();
  }
}
