using GameProject.Controllers;
using GameProject.Factories;
using GameProject.Globals;
using GameProject.Interfaces;
using GameProject.Items;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateGameType : IGameState {
  private readonly Game1 game;

  private IController keyboardController;
  private IController mouseController;
  private IController gamePadController;

  private readonly CollisionManager collisionManager = new();

  public Player Player { get; private set; }

  public ILevelManager LevelManager { get; private set; }

  public StateGameType(Game1 game) {
    this.game = game;
    LevelManager = new LevelManager(game);
    Player = new Player(collisionManager, LevelManager, game);
  }

  public void Initialize() {
    keyboardController = new GameKeyboardController(game);
    mouseController = new GameMouseController(game);
    gamePadController = new GameGamePadController(game);
    LevelManager.Initialize();
    collisionManager.Add(Player);
  }

  public void LoadContent(ContentManager contentManager) {
    Player.LoadContent(contentManager);

    Player.Inventory.PickupItem(ItemSpriteFactory.Instance.CreateShotgun(0f, 0f, game));
    Player.Inventory.PickupItem(ItemSpriteFactory.Instance.CreateRifle(0f, 0f, game));

    LevelManager.LoadContent(contentManager);
  }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    mouseController.Update(gameTime);
    gamePadController.Update(gameTime);

    LevelManager.Update(gameTime);
    Player.Update(gameTime);

    foreach (var projectile in LevelManager.CurrentLevel.ProjectileManager.Projectiles) {
      if (projectile is ICollidable collidableProj) {
        collisionManager.Add(collidableProj);
      }
    }

    collisionManager.Update(gameTime);
  }

  public void LowLevelDraw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
    graphicsDevice.Clear(Color.CornflowerBlue);

    graphicsDevice.Viewport = game.GameViewport;

    spriteBatch.Begin(
      SpriteSortMode.Deferred,
      BlendState.AlphaBlend,
      SamplerState.PointClamp,
      DepthStencilState.None,
      RasterizerState.CullNone
    );

    LevelManager.Draw(spriteBatch);
    Player.Draw(spriteBatch);

    spriteBatch.End();

    graphicsDevice.Viewport = game.HudViewport;

    spriteBatch.Begin(
      SpriteSortMode.Deferred,
      BlendState.AlphaBlend,
      SamplerState.PointClamp,
      DepthStencilState.None,
      RasterizerState.CullNone
    );

    var healthBarPosition = new Vector2(20, 20);
    float healthPercent = MathHelper.Clamp(Player.Health / 100f, 0f, 1f);
    spriteBatch.Draw(
      texture: TextureStore.Instance.HealthBar,
      position: healthBarPosition,
      sourceRectangle: null,
      color: Color.DarkSlateGray,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 0.5f,  //scale of blood bar
      effects: SpriteEffects.None,
      layerDepth: 0f
    );


    int visibleWidth = (int) (TextureStore.Instance.HealthBar.Width * healthPercent);
    Rectangle sourceRectangle = new(0, 0, visibleWidth, TextureStore.Instance.HealthBar.Height);
    spriteBatch.Draw(
      texture: TextureStore.Instance.HealthBar,
      position: healthBarPosition,
      sourceRectangle: sourceRectangle,
      color: Color.White,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 0.5f, //scale of the blood bar
      effects: SpriteEffects.None,
      layerDepth: 0f
    );

    //draw player's items, keys...
    float visualHealthBarWidth = TextureStore.Instance.HealthBar.Width * 0.5f;
    Vector2 ammoPosition = new Vector2(healthBarPosition.X + visualHealthBarWidth + 20, healthBarPosition.Y);
    var activeWeapon = Player.Inventory.ActiveItem;

    if (activeWeapon != null) {
      // Much cleaner! If it's any gun inheriting from DefaultGun, it will grab the stats.
      if (activeWeapon is DefaultGun activeGun) {
        GunStats activeStats = activeGun.PublicStats;

        if (activeStats != null) {
          string ammoText;
          Color textColor;
          if (activeStats.CurrentAmmo <= 0) {
            ammoText = "RELOADING...";
            textColor = Color.Red;
          } else {
            ammoText = $"Ammo: {activeStats.CurrentAmmo} / {activeStats.MaxAmmo}";
            textColor = Color.White;
          }

          spriteBatch.DrawString(
              spriteFont: MiscAssetStore.Instance.MainFont,
              text: ammoText,
              position: ammoPosition,
              color: textColor
          );
        }
      }
    }


    Rectangle[] keySourceRects = [
        new Rectangle(0, 448, 8, 13),
        new Rectangle(9, 448, 8, 13),
        new Rectangle(17, 448, 8, 14)
    ];
    Vector2 keysStartPosition = new Vector2(ammoPosition.X + 200, healthBarPosition.Y);
    float keyScale = 3f;

    // FIX: Changed .Keys to .Keys.Count
    int keysToDraw = MathHelper.Clamp(Player.Inventory.Keys.Count, 0, 3);

    for (int i = 0; i < keysToDraw; i++) {
      Vector2 keyPos = keysStartPosition + new Vector2(i * 35, 0);

      spriteBatch.Draw(
          texture: TextureStore.Instance.MainBlockItemAtlas,
          position: keyPos,
          sourceRectangle: keySourceRects[i],
          color: Color.White,
          rotation: 0f,
          origin: Vector2.Zero,
          scale: keyScale,
          effects: SpriteEffects.None,
          layerDepth: 0f
      );
    }

    spriteBatch.End();

    graphicsDevice.Viewport = game.DefaultViewport;
  }

  public void OnStateEnter() {
    SoundManager.Instance.PlayLoop(SoundID.Background);
        collisionManager.Clear();
    collisionManager.Add(Player);

    if (LevelManager.CurrentLevel != null) {
      foreach (var block in LevelManager.CurrentLevel.CollidableBlocks) {
        collisionManager.Add(block);
      }
      foreach (var doorBlock in LevelManager.CurrentLevel.Doors) {
        collisionManager.Add(doorBlock);
      }
      foreach (var enemy in LevelManager.CurrentLevel.Enemies) {
        if (enemy.Health > 0) {
          collisionManager.Add(enemy);
        }
      }
    }
  }

  public void OnStateLeave() {
    SoundManager.Instance.StopAll();
  }

  public void OnStateStartFadeIn() {
    LevelManager.CompleteLevelSwitch();
  }

  public void OnStateEndFadeOut() { }
}
