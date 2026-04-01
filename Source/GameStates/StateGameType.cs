using GameProject.Controllers;
using GameProject.Enemies;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateGameType : IGameState {
  private Game1 game;

  private IController keyboardController;
  private IController mouseController;
  private IController gamePadController;

  private CollisionManager collisionManager = new();
  private Texture2D healthBarTexture;
  private Vector2 healthBarPosition = new(0, 0);

  public Player Player { get; private set; }

  public ILevelManager LevelManager { get; private set; }

  public StateGameType(Game1 game) {
    this.game = game;
    LevelManager = new LevelManager(game);
    Player = new Player(game.Content, collisionManager, LevelManager);
  }

  public void Initialize() {
    keyboardController = new GameKeyboardController(game);
    mouseController = new GameMouseController(game);
    gamePadController = new GameGamePadController(game);
    LevelManager.Initialize();
    collisionManager.AddCollider(Player);
  }

  public void LoadContent() {
    Player.LoadContent();

    Player.Inventory.PickupItem(ItemSpriteFactory.Instance.CreateRevolver(0f, 0f, game));
    Player.Inventory.PickupItem(ItemSpriteFactory.Instance.CreateRifle(0f, 0f, game));

    LevelManager.LoadContent(game.Content);
    healthBarTexture = game.Content.Load<Texture2D>("blood_red_bar");
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
        if (enemy.Health > 0) {
          collisionManager.AddCollider(enemy);
        }
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

    float healthPercent = MathHelper.Clamp(Player.Health / 100f, 0f, 1f);
    game.SpriteBatch.Draw(
      texture: healthBarTexture,
      position: healthBarPosition,
      sourceRectangle: null,
      color: Color.DarkSlateGray,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 0.5f,  //scale of blood bar
      effects: SpriteEffects.None,
      layerDepth: 0f
      );

    if (LevelManager.CurrentLevel != null) {
      foreach (var enemy in LevelManager.CurrentLevel.Enemies) {
        if (enemy is BaseEnemy baseEnemy && baseEnemy.Health > 0) {
          float enemyHealthPercent = MathHelper.Clamp((float) baseEnemy.Health / baseEnemy.MaxHealth, 0f, 1f);
          float scaleWidth = healthBarTexture.Width * 0.15f;
          Vector2 enemyHealthPositions = new(
            baseEnemy.Position.X - (scaleWidth / 2f),
            baseEnemy.Position.Y - baseEnemy.Collider.height);
          game.SpriteBatch.Draw(texture: healthBarTexture,
            position: enemyHealthPositions,
            sourceRectangle: null,
            color: Color.DarkSlateGray,
            rotation: 0f,
            origin: Vector2.Zero,
            scale: 0.15f,
            effects: SpriteEffects.None,
            layerDepth: 0f
            );
          int enemyHealthVisible = (int) (healthBarTexture.Width * enemyHealthPercent);
          Rectangle enemyHpSource = new(0, 0, enemyHealthVisible, healthBarTexture.Height);

          game.SpriteBatch.Draw(
            texture: healthBarTexture,
            position: enemyHealthPositions,
            sourceRectangle: enemyHpSource,
            color: Color.White,
            rotation: 0f,
            origin: Vector2.Zero,
            scale: 0.15f,
            effects: SpriteEffects.None,
            layerDepth: 0f
          );
        }
      }
    }

    int visibleWidth = (int) (healthBarTexture.Width * healthPercent);
    Rectangle sourceRectangle = new(0, 0, visibleWidth, healthBarTexture.Height);
    game.SpriteBatch.Draw(
      texture: healthBarTexture,
      position: healthBarPosition,
      sourceRectangle: sourceRectangle,
      color: Color.White,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 0.5f, //scale of the blood bar
      effects: SpriteEffects.None,
      layerDepth: 0f
    );

    // collisionManager.DebugDraw(game.SpriteBatch, game.GraphicsDevice);
    game.SpriteBatch.End();
  }
}
