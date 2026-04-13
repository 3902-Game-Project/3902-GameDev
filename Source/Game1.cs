using GameProject.Factories;
using GameProject.GameStates;
using GameProject.Globals;
using GameProject.Interfaces;
using GameProject.Managers;
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
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  public void ChangeState(IGameState newState) {
    StateTransition.SetFadingStates(currentState, newState);
    ChangeStateWithoutFading(StateTransition);
  }

  public void ChangeStateWithoutFading(IGameState newState) {
    bool nextStateIsCurrentState = currentState == newState;

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

    DefaultViewport = GraphicsDevice.Viewport;
    HudViewport = new Viewport(0, 0, graphics.PreferredBackBufferWidth, HUD_HEIGHT);
    GameViewport = new Viewport(0, HUD_HEIGHT, graphics.PreferredBackBufferWidth, GAME_HEIGHT);

    MiscAssetStore.Instance.Initialize();
    TextureStore.Instance.Initialize();

    base.Initialize();
  }

  protected override void LoadContent() {
    SpriteBatch = new SpriteBatch(GraphicsDevice);

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

    EnemySpriteFactory.Instance.LoadAllTextures(Content);
    SoundManager.Instance.LoadAllContent(Content);
    ItemSpriteFactory.Instance.LoadAllTextures(Content);
    ProjectileFactory.Instance.LoadAllTextures(Content);

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
    currentState.LowLevelDraw(GraphicsDevice, SpriteBatch);

    base.Draw(gameTime);
  }
}
