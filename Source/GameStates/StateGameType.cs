using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.Factories;
using GameProject.Globals;
using GameProject.Items;
using GameProject.Managers;
using GameProject.Misc;
using GameProject.PlayerSpace;
using GameProject.Source.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateGameType : IGameState {
  private static Color BACKGROUND_COLOR = new(20, 20, 120);
  private static Rectangle NON_HUD_RECTANGLE = new(0, 0, Game1.GAME_WIDTH, Game1.GAME_HEIGHT);

  private readonly Game1 game;

  private IController<Keys> keyboardController;
  private IController<MouseButtons> mouseController;
  private IController<Buttons> gamePadController;
  private Texture2D ammoTexture;
  private RenderTarget2D nonHUDTarget;

  public Player Player { get; private set; }

  public ILevelManager LevelManager { get; private set; }

  public StateGameType(Game1 game) {
    this.game = game;
    LevelManager = new LevelManager(game);
    Player = new Player(LevelManager, game);
  }

  public void Initialize() {
    keyboardController = new KeyboardController(
      pressedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.Q, new QuitCommand(game) },
        { Keys.Back, new ReturnToMenuAndResetCommand(game) },
        { Keys.P, new PauseCommand(game) },
        { Keys.I, new OpenItemScreenCommand(game) },
        { Keys.J, new PlayerUseItemCommand(Player, UseType.Pressed) },
        { Keys.K, new PlayerUseKeyCommand(Player, UseType.Pressed) },
        { Keys.F, new PlayerInteractCommand(Player) },
        { Keys.Space, new SwapWeaponCommand(Player) },
        { Keys.C, new PlayerDropItemCommand(game.StateGame.Player) },
        { Keys.R, new ReloadWeaponCommand(Player) },
        { Keys.L, new PlayerDieCommand(Player) },
        { Keys.Tab, new ToggleMusicCommand() },
      },
      downMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.W, new PlayerMoveUpCommand(Player) },
        { Keys.S, new PlayerMoveDownCommand(Player) },
        { Keys.A, new PlayerMoveLeftCommand(Player) },
        { Keys.D, new PlayerMoveRightCommand(Player) },
        { Keys.Up, new PlayerMoveUpCommand(Player) },
        { Keys.Down, new PlayerMoveDownCommand(Player) },
        { Keys.Left, new PlayerMoveLeftCommand(Player) },
        { Keys.Right, new PlayerMoveRightCommand(Player) },
        { Keys.J, new PlayerUseItemCommand(Player, UseType.Held) },
      },
      releasedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.J, new PlayerUseItemCommand(Player, UseType.Released) },
      }
    );

    // Debug button binds:
    if (Flags.DebugButtonBinds) {
      keyboardController.PressedMappings.Add(Keys.T, new PreviousLevelCommand(LevelManager));
      keyboardController.PressedMappings.Add(Keys.Y, new NextLevelCommand(LevelManager));
      keyboardController.PressedMappings.Add(Keys.H, new ToggleUpdatesCommand());
    }

    mouseController = new MouseController();

    // Debug button binds:
    if (Flags.DebugButtonBinds) {
      mouseController.PressedMappings.Add(MouseButtons.Right, new PreviousLevelCommand(LevelManager));
      mouseController.PressedMappings.Add(MouseButtons.Left, new NextLevelCommand(LevelManager));
    }

    // The gamepad bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.
    gamePadController = new GamePadController(
      pressedMappings: new Dictionary<Buttons, IGPCommand> {
        { Buttons.X, new QuitCommand(game) },
        { Buttons.B, new ReturnToMenuAndResetCommand(game) },
        { Buttons.Y, new PauseCommand(game) },
        { Buttons.RightTrigger, new OpenItemScreenCommand(game) },
        { Buttons.A, new PlayerUseItemCommand(Player, UseType.Pressed) },
        { Buttons.LeftTrigger, new PlayerUseKeyCommand(Player, UseType.Pressed) },
        { Buttons.LeftStick, new PlayerInteractCommand(Player) },
        { Buttons.RightThumbstickUp, new SwapWeaponCommand(Player) },
        { Buttons.RightThumbstickDown, new PlayerDropItemCommand(game.StateGame.Player) },
        { Buttons.Start, new ReloadWeaponCommand(Player) },
        { Buttons.RightStick, new PlayerDieCommand(Player) },
        { Buttons.Back, new ToggleMusicCommand() },
      },
      downMappings: new Dictionary<Buttons, IGPCommand> {
        { Buttons.DPadUp, new PlayerMoveUpCommand(Player) },
        { Buttons.LeftThumbstickUp, new PlayerMoveUpCommand(Player) },
        { Buttons.DPadDown, new PlayerMoveDownCommand(Player) },
        { Buttons.LeftThumbstickDown, new PlayerMoveDownCommand(Player) },
        { Buttons.DPadLeft, new PlayerMoveLeftCommand(Player) },
        { Buttons.LeftThumbstickLeft, new PlayerMoveLeftCommand(Player) },
        { Buttons.DPadRight, new PlayerMoveRightCommand(Player) },
        { Buttons.LeftThumbstickRight, new PlayerMoveRightCommand(Player) },
        { Buttons.A, new PlayerUseItemCommand(Player, UseType.Held) },
      },
      releasedMappings: new Dictionary<Buttons, IGPCommand> {
        { Buttons.A, new PlayerUseItemCommand(Player, UseType.Released) },
      }
    );

    // Debug button binds:
    if (Flags.DebugButtonBinds) {
      gamePadController.PressedMappings.Add(Buttons.LeftShoulder, new PreviousLevelCommand(LevelManager));
      gamePadController.PressedMappings.Add(Buttons.RightShoulder, new NextLevelCommand(LevelManager));
      gamePadController.PressedMappings.Add(Buttons.RightThumbstickRight, new ToggleUpdatesCommand());
    }

    LevelManager.Initialize();
    CheatCodes.Instance.Initialize(Player);
  }

  public void LoadContent(ContentManager contentManager) {
    nonHUDTarget = new RenderTarget2D(game.GraphicsDevice, Game1.GAME_WIDTH, Game1.GAME_HEIGHT);

    Player.Inventory.PickupItem(ItemFactory.Instance.CreateRevolver(0f, 0f, game.StateGame.Player, game.StateGame.LevelManager));
    Player.Inventory.PickupItem(ItemFactory.Instance.CreateRifle(0f, 0f, game.StateGame.Player, game.StateGame.LevelManager));
    CheatCodes.Instance.LevelManager = LevelManager;

    ammoTexture = contentManager.Load<Texture2D>("Items/ammo_drops");

    LevelManager.LoadContent(contentManager);
  }

  public void Update(double deltaTime) {
    if (Flags.SlowMode) {
      deltaTime *= 0.5;
    }

    keyboardController.Update();
    mouseController.Update();
    gamePadController.Update();

    CheatCodes.Instance.Update(deltaTime);

    if (!Flags.HaltAllUpdates) {
      Player.Update(deltaTime);
      LevelManager.Update(deltaTime);
    }
  }

  public void LowLevelDraw(GraphicsDevice graphicsDevice, RenderTargetTracker renderTargetTracker, SpriteBatch spriteBatch) {
    graphicsDevice.Clear(BACKGROUND_COLOR);

    using (renderTargetTracker.TempSetTarget(nonHUDTarget)) {
      spriteBatch.Begin(
        sortMode: SpriteSortMode.Deferred,
        blendState: BlendState.AlphaBlend,
        samplerState: SamplerState.PointClamp,
        depthStencilState: DepthStencilState.None,
        rasterizerState: RasterizerState.CullNone
      );

      LevelManager.Draw(spriteBatch);
      Player.Draw(spriteBatch);

      spriteBatch.End();
    }

    graphicsDevice.Viewport = game.GameViewport;

    Effect effect;
    if (Flags.EnableVignette) {
      MiscAssetStore.Instance.Vignette.Parameters["VignetteCenter"].SetValue(Player.Position / new Vector2(Game1.GAME_WIDTH, Game1.GAME_HEIGHT));
      effect = MiscAssetStore.Instance.Vignette;
    } else {
      effect = null;
    }

    spriteBatch.Begin(
      sortMode: SpriteSortMode.Deferred,
      blendState: BlendState.AlphaBlend,
      samplerState: SamplerState.PointClamp,
      depthStencilState: DepthStencilState.None,
      rasterizerState: RasterizerState.CullNone,
      effect: effect
    );

    spriteBatch.Draw(nonHUDTarget, NON_HUD_RECTANGLE, Color.White);

    spriteBatch.End();

    graphicsDevice.Viewport = game.HudViewport;

    spriteBatch.Begin(
      sortMode: SpriteSortMode.Deferred,
      blendState: BlendState.AlphaBlend,
      samplerState: SamplerState.PointClamp,
      depthStencilState: DepthStencilState.None,
      rasterizerState: RasterizerState.CullNone
    );

    spriteBatch.Draw(
      texture: TextureStore.Instance.HUDBackground,
      position: Vector2.Zero,
      sourceRectangle: null,
      color: Color.DarkSlateGray,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0f
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
    Vector2 ammoPosition = new(healthBarPosition.X + visualHealthBarWidth + 20, healthBarPosition.Y);
    var activeWeapon = Player.Inventory.ActiveItem;

    if (activeWeapon != null) {
      // Much cleaner! If it's any gun inheriting from DefaultGun, it will grab the stats.
      if (activeWeapon is ABaseGun activeGun) {
        GunStats activeStats = activeGun.PublicStats;

        if (activeStats != null) {
          // Draw Current Gun's Ammo
          string ammoText = $"Ammo: {activeStats.CurrentAmmo} / {activeStats.MaxAmmo}";
          spriteBatch.DrawString(MiscAssetStore.Instance.MainFont, ammoText, ammoPosition, Color.White);

          // Draw the 3 ammo types
          if (ammoTexture != null) {
            float iconScale = 2f;
            float textOffset = 35f;
            float spacing = 90f;
            Vector2 reserveStartPos = ammoPosition + new Vector2(0, 30);

            // Light Ammo
            spriteBatch.Draw(ammoTexture, reserveStartPos, new Rectangle(0, 0, 16, 16), Color.White, 0f, Vector2.Zero, iconScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(MiscAssetStore.Instance.MainFont, $"{Player.Inventory.Ammo[AmmoType.Light]}", reserveStartPos + new Vector2(textOffset, 5), Color.White);

            // Heavy Ammo
            Vector2 heavyPos = reserveStartPos + new Vector2(spacing, 0);
            spriteBatch.Draw(ammoTexture, heavyPos, new Rectangle(16, 0, 16, 16), Color.White, 0f, Vector2.Zero, iconScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(MiscAssetStore.Instance.MainFont, $"{Player.Inventory.Ammo[AmmoType.Heavy]}", heavyPos + new Vector2(textOffset, 5), Color.White);

            // Shells
            Vector2 shellsPos = reserveStartPos + new Vector2(spacing * 2, 0);
            spriteBatch.Draw(ammoTexture, shellsPos, new Rectangle(32, 0, 16, 16), Color.White, 0f, Vector2.Zero, iconScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(MiscAssetStore.Instance.MainFont, $"{Player.Inventory.Ammo[AmmoType.Shells]}", shellsPos + new Vector2(textOffset, 5), Color.White);
          }
        }
      }
    }

    Rectangle[] keySourceRects = [
      new Rectangle(0, 448, 8, 13),
      new Rectangle(9, 448, 8, 13),
      new Rectangle(17, 448, 8, 14)
    ];
    Vector2 keysStartPosition = new(ammoPosition.X + 200, healthBarPosition.Y);
    float keyScale = 3f;

    int keyCount = 0;
    foreach (var item in Player.Inventory.GeneralItems) {
      if (item is KeyItem) keyCount++;
    }
    int keysToDraw = MathHelper.Clamp(keyCount, 0, 3);

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

    Vector2 mapPosition = new(keysStartPosition.X + 100, keysStartPosition.Y);
    spriteBatch.Draw(
      texture: TextureStore.Instance.MainBlockItemAtlas,
      position: mapPosition,
      sourceRectangle: new Rectangle(128, 448, 97, 29), // FIX
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
  }

  public void OnStateLeave(bool nextStateIsCurrentState) {
    if (!nextStateIsCurrentState) {
      SoundManager.Instance.StopAll();
    }
  }

  public void OnStateStartFadeIn(bool prevStateIsCurrentState) {
    LevelManager.InitializeLevel();
  }

  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
