using System.Collections.Generic;
using System.Diagnostics;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.Factories;
using GameProject.Globals;
using GameProject.Interfaces;
using GameProject.Items;
using GameProject.Managers;
using GameProject.Misc;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateGameType : IGameState {
  private readonly Game1 game;

  private IController keyboardController;
  private IController mouseController;
  private IController gamePadController;
  
  public Player Player { get; private set; }

  public ILevelManager LevelManager { get; private set; }

  public StateGameType(Game1 game) {
    this.game = game;
    LevelManager = new LevelManager(game);
    Player = new Player(LevelManager, game);
  }

  public void Initialize() {
    keyboardController = new KeyboardController(
      pressedMappings: new Dictionary<Keys, ICommand> {
        { Keys.R, new ReturnToMenuAndResetCommand(game) },
        { Keys.Q, new QuitCommand(game) },
        { Keys.P, new PauseCommand(game) },
        { Keys.I, new OpenItemScreenCommand(game) },
        { Keys.J, new PlayerUseItemCommand(Player, UseType.Pressed) },
        { Keys.E, new PlayerDieCommand(Player) },
        { Keys.T, new PreviousLevelCommand(LevelManager) },
        { Keys.Y, new NextLevelCommand(LevelManager) },
        { Keys.F, new PlayerInteractCommand(Player) },
        { Keys.Space, new SwapWeaponCommand(Player) },
        { Keys.Tab, new ToggleMusicCommand() },
      },
      downMappings: new Dictionary<Keys, ICommand> {
        { Keys.W, new PlayerMoveUpCommand(Player) },
        { Keys.S, new PlayerMoveDownCommand(Player) },
        { Keys.A, new PlayerMoveLeftCommand(Player) },
        { Keys.D, new PlayerMoveRightCommand(Player) },
        { Keys.Up, new PlayerMoveUpCommand(Player) },
        { Keys.Down, new PlayerMoveDownCommand(Player) },
        { Keys.Left, new PlayerMoveLeftCommand(Player) },
        { Keys.Right, new PlayerMoveRightCommand(Player) },
      }
    );

    mouseController = new MouseController(
      pressedMappings: new Dictionary<MouseButtons, ICommand> {
        { MouseButtons.Right, new PreviousLevelCommand(LevelManager) },
        { MouseButtons.Left, new NextLevelCommand(LevelManager) },
      }
    );

    // The gamepad bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.
    gamePadController = new GamePadController(
      pressedMappings: new Dictionary<Buttons, ICommand> {
        { Buttons.X, new QuitCommand(game) },
        { Buttons.B, new ReturnToMenuAndResetCommand(game) },
        { Buttons.A, new PlayerUseItemCommand(Player, UseType.Pressed) },
        { Buttons.Y, new PlayerDieCommand(Player) },
        { Buttons.LeftShoulder, new PreviousLevelCommand(LevelManager) },
        { Buttons.RightShoulder, new NextLevelCommand(LevelManager) },
      },
      downMappings: new Dictionary<Buttons, ICommand> {
        { Buttons.DPadUp, new PlayerMoveUpCommand(Player) },
        { Buttons.DPadDown, new PlayerMoveDownCommand(Player) },
        { Buttons.DPadLeft, new PlayerMoveLeftCommand(Player) },
        { Buttons.DPadRight, new PlayerMoveRightCommand(Player) },
      }
    );

    LevelManager.Initialize();
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

    Vector2 mapPosition = new Vector2(keysStartPosition.X + 100, keysStartPosition.Y);
    spriteBatch.Draw(
      texture: TextureStore.Instance.MainBlockItemAtlas,
      position: mapPosition,
      sourceRectangle: new Rectangle(128, 448, 111, 97),  // FIX
      color: Color.White,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 2f,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );


    spriteBatch.End();

    graphicsDevice.Viewport = game.DefaultViewport;
  }

  public void OnStateEnter(bool prevStateIsCurrentState) {
    if (!prevStateIsCurrentState) {
      SoundManager.Instance.PlayLoop(SoundID.Background);
    }

    if (LevelManager.CurrentLevel != null) {
      LevelManager.CurrentLevel.CollisionManager.Clear();
      LevelManager.CurrentLevel.CollisionManager.Add(Player);
      foreach (var block in LevelManager.CurrentLevel.CollidableBlocks) {
        LevelManager.CurrentLevel.CollisionManager.Add(block);
      }
      foreach (var doorBlock in LevelManager.CurrentLevel.Doors) {
        LevelManager.CurrentLevel.CollisionManager.Add(doorBlock);
      }
      foreach (var enemy in LevelManager.CurrentLevel.Enemies) {
        if (enemy.Health > 0) {
          LevelManager.CurrentLevel.CollisionManager.Add(enemy);
        }
      }
    }
  }

  public void OnStateLeave(bool nextStateIsCurrentState) {
    if (!nextStateIsCurrentState) {
      SoundManager.Instance.StopAll();
    }
  }

  public void OnStateStartFadeIn(bool prevStateIsCurrentState) {
    LevelManager.CompleteLevelSwitch();
  }

  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
