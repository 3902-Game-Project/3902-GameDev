using System;
using GameProject.Factories;
using GameProject.GameStates;
using GameProject.Globals;
using GameProject.Managers;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject;

internal class Game1 : Game {
  public static readonly int HUD_HEIGHT = 100;
  public static readonly int GAME_HEIGHT = 576;
  public static readonly int GAME_WIDTH = 960;

  private readonly GraphicsDeviceManager graphics;
  public SpriteBatch SpriteBatch { get; private set; }
  public Viewport DefaultViewport { get; private set; }
  public Viewport HudViewport { get; private set; }
  public Viewport GameViewport { get; private set; }
  private RenderTarget2D renderTarget;
  public RenderTargetTracker RenderTargetTracker { get; private set; }
  private Rectangle renderScaleRectangle;

  private StateTransitionType StateTransition;
  public IGameState StateMenu { get; private set; }
  public IGameState StateLoss { get; private set; }
  public IGameState StateWin { get; private set; }
  public IGameState StatePause { get; private set; }
  public IGameState StateItemScreen { get; private set; }
  public StateGameType StateGame { get; private set; }

  private IGameState currentState;

  public Game1() {
    graphics = new GraphicsDeviceManager(this);
    RenderTargetTracker = new(GraphicsDevice);
    IsMouseVisible = true;

    Content.RootDirectory = "Content";
  }

  public void ChangeState(IGameState newState) {
    StateTransition.SetFadingStates(currentState, newState);
    ChangeStateWithoutFading(StateTransition);
  }

  public void ChangeStateWithoutFading(IGameState newState) {
    bool nextStateIsCurrentState;

    if (currentState == StateTransition || newState == StateTransition) {
      nextStateIsCurrentState = StateTransition.NextStateIsCurrentState();
    } else {
      nextStateIsCurrentState = currentState == newState;
    }

    currentState.OnStateLeave(nextStateIsCurrentState);
    currentState = newState;
    newState.OnStateEnter(nextStateIsCurrentState);
  }

  public void ResetGameState() {
    StateGame = new StateGameType(this);
    StateGame.Initialize();
    StateGame.LoadContent(Content);
  }

  protected override void Initialize() {
    graphics.PreferredBackBufferHeight = GAME_HEIGHT + HUD_HEIGHT;
    graphics.PreferredBackBufferWidth = GAME_WIDTH;
    graphics.ApplyChanges();

    Window.AllowUserResizing = true;
    Window.ClientSizeChanged += OnResize;
    DefaultViewport = new Viewport(0, 0, GAME_WIDTH, GAME_HEIGHT + HUD_HEIGHT);
    HudViewport = new Viewport(0, 0, GAME_WIDTH, HUD_HEIGHT);
    GameViewport = new Viewport(0, HUD_HEIGHT, GAME_WIDTH, GAME_HEIGHT);

    DefaultViewport = GraphicsDevice.Viewport;
    HudViewport = new Viewport(0, 0, graphics.PreferredBackBufferWidth, HUD_HEIGHT);
    GameViewport = new Viewport(0, HUD_HEIGHT, graphics.PreferredBackBufferWidth, GAME_HEIGHT);

    MiscAssetStore.Instance.Initialize();
    TextureStore.Instance.Initialize();

    base.Initialize();
  }

  private void OnResize(Object sender, EventArgs e) {
    UpdateRenderScaleRectangle();
  }

  private void UpdateRenderScaleRectangle() {
    int screenWidth = Window.ClientBounds.Width;
    int screenHeight = Window.ClientBounds.Height;

    float outputAspect = (float) screenWidth / screenHeight;
    float preferredAspect = (float) GAME_WIDTH / (GAME_HEIGHT + HUD_HEIGHT);

    int width, height;
    if (outputAspect <= preferredAspect) {
      width = screenWidth;
      height = (int) (screenWidth / preferredAspect);
    } else {
      width = (int) (screenHeight * preferredAspect);
      height = screenHeight;
    }

    int x = (screenWidth - width) / 2;
    int y = (screenHeight - height) / 2;

    renderScaleRectangle = new Rectangle(x, y, width, height);
  }

  protected override void LoadContent() {
    SpriteBatch = new SpriteBatch(GraphicsDevice);
    renderTarget = new RenderTarget2D(GraphicsDevice, GAME_WIDTH, GAME_HEIGHT + HUD_HEIGHT);
    UpdateRenderScaleRectangle();

    StateTransition = new StateTransitionType(this);
    StateMenu = new StateMenuType(this);
    StateLoss = new StateLossType(this);
    StateWin = new StateWinType(this);
    StatePause = new StatePauseType(this);
    StateItemScreen = new StateItemScreenType(this);
    StateGame = new StateGameType(this);
    currentState = StateMenu;

    StateTransition.Initialize();
    StateMenu.Initialize();
    StateLoss.Initialize();
    StateWin.Initialize();
    StatePause.Initialize();
    StateItemScreen.Initialize();
    StateGame.Initialize();

    MiscAssetStore.Instance.LoadContent(Content);
    TextureStore.Instance.LoadContent(Content);

    EnemyFactory.Instance.LoadAllTextures(Content);
    SoundManager.Instance.LoadAllContent(Content);
    ItemFactory.Instance.LoadAllTextures(Content);
    ProjectileFactory.Instance.LoadAllTextures(Content);
    WorldPickupFactory.Instance.LoadAllTextures(Content);

    StateTransition.LoadContent(Content);
    StateMenu.LoadContent(Content);
    StateLoss.LoadContent(Content);
    StateWin.LoadContent(Content);
    StatePause.LoadContent(Content);
    StateItemScreen.LoadContent(Content);
    StateGame.LoadContent(Content);
  }

  protected override void Update(GameTime gameTime) {
    currentState.Update(gameTime);

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime) {
    // Render everything that should be on screen to a texture

    RenderTargetTracker.Push(renderTarget);
    GraphicsDevice.SetRenderTarget(renderTarget);
    GraphicsDevice.Clear(Color.Black);
    currentState.LowLevelDraw(GraphicsDevice, SpriteBatch);
    RenderTargetTracker.Pop();

    // Then render the texture to screen

    GraphicsDevice.Clear(Color.Black);
    SpriteBatch.Begin(
      sortMode: SpriteSortMode.Deferred,
      blendState: BlendState.Opaque,
      samplerState: SamplerState.PointClamp,
      depthStencilState: DepthStencilState.None,
      rasterizerState: RasterizerState.CullNone
    );
    SpriteBatch.Draw(renderTarget, renderScaleRectangle, Color.White);
    SpriteBatch.End();

    base.Draw(gameTime);
  }
}
