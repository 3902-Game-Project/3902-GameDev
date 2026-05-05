using GameProject.ButtonDiffTrackers;
using GameProject.Controllers;
using GameProject.Factories.Controller;
using GameProject.Globals;
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
  private static Rectangle NON_HUD_RECTANGLE = new(0, 0, Constants.WINDOW_WIDTH, Constants.GAME_HEIGHT);

  private RenderTarget2D nonHUDTarget;

  private readonly Game1 game;

  private IController<Keys> keyboardController;
  private IController<MouseButtons> mouseController;
  private IController<GPGamePadButtons> gamePadController;

  private readonly HUDManager hudManager;

  private void DrawGameWithoutVignette(GraphicsDevice graphicsDevice, ValueTracker<RenderTarget2D> renderTargetTracker, SpriteBatch spriteBatch) {
    graphicsDevice.Clear(BACKGROUND_COLOR);

    using (renderTargetTracker.TempSet(nonHUDTarget)) {
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
  }

  private void DrawGameVignette(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
    graphicsDevice.Viewport = game.GameViewport;

    try {
      Effect effect;
      if (Flags.Vignette && LevelManager.CurrentLevel.LevelFlags.Cave) {
        MiscAssetStore.Instance.Vignette.Parameters["VignetteCenter"].SetValue(Player.Position / new Vector2(Constants.WINDOW_WIDTH, Constants.GAME_HEIGHT));
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
    } finally {
      graphicsDevice.Viewport = game.DefaultViewport;
    }
  }

  private void DrawHUD(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
    graphicsDevice.Viewport = game.HudViewport;

    try {
      spriteBatch.Begin(
        sortMode: SpriteSortMode.Deferred,
        blendState: BlendState.AlphaBlend,
        samplerState: SamplerState.PointClamp,
        depthStencilState: DepthStencilState.None,
        rasterizerState: RasterizerState.CullNone
      );

      hudManager.Draw(spriteBatch);

      spriteBatch.End();
    } finally {
      graphicsDevice.Viewport = game.DefaultViewport;
    }
  }

  public Player Player { get; private set; }

  public ILevelManager LevelManager { get; private set; }

  public StateGameType(Game1 game) {
    this.game = game;
    LevelManager = new LevelManager(game);
    Player = new Player(LevelManager, game);
    hudManager = new(Player);
  }

  public void Initialize() {
    keyboardController = GameControllerFactory.CreateKeyboardController(game, Player, LevelManager);
    mouseController = GameControllerFactory.CreateMouseController(LevelManager);
    gamePadController = GameControllerFactory.CreateGamePadController(game, Player, LevelManager);

    LevelManager.Initialize();
    Player.Initialize();
    CheatCodes.Instance.LevelManager = LevelManager;
    CheatCodes.Instance.Initialize(Player);
  }

  public void LoadContent(ContentManager contentManager) {
    nonHUDTarget = new RenderTarget2D(game.GraphicsDevice, Constants.WINDOW_WIDTH, Constants.GAME_HEIGHT);

    LevelManager.LoadContent(contentManager);
    Player.LoadContent(contentManager);
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

  public void LowLevelDraw(GraphicsDevice graphicsDevice, ValueTracker<RenderTarget2D> renderTargetTracker, SpriteBatch spriteBatch) {
    DrawGameWithoutVignette(graphicsDevice, renderTargetTracker, spriteBatch);
    DrawGameVignette(graphicsDevice, spriteBatch);
    DrawHUD(graphicsDevice, spriteBatch);
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
