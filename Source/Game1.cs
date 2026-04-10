using GameProject.Factories;
using GameProject.GameStates;
using GameProject.Globals;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject;

public class Game1 : Game {
  public static readonly int HUD_HEIGHT = 100;

  private readonly GraphicsDeviceManager graphics;
  public SpriteBatch SpriteBatch { get; private set; }
  public AssetStore Assets { get; private set; }
  private StateTransitionType StateTransition;
  public IGameState StateMenu { get; private set; }
  public IGameState StateLoss { get; private set; }
  public IGameState StateWin { get; private set; }
  public IGameState StatePause { get; private set; }
  public IGameState StateItem { get; private set; }
  public StateGameType StateGame { get; private set; }
  public Viewport DefaultViewport { get; private set; }
  public Viewport HudViewport { get; private set; }
  public Viewport GameViewport { get; private set; }

  private IGameState currentState;

  public Game1() {
    graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;

    Assets = new AssetStore(this);

  }

  public void ChangeState(IGameState state) {
    StateTransition.SetFadingStates(currentState, state);
    ChangeStateWithoutFading(StateTransition);
  }

  public void ChangeStateWithoutFading(IGameState state) {
    currentState = state;
  }

  public void ResetGameState() {
    StateGame = new StateGameType(this);
    StateGame.Initialize();
    StateGame.LoadContent();
  }

  protected override void Initialize() {
    graphics.PreferredBackBufferHeight = 576 + HUD_HEIGHT;
    graphics.PreferredBackBufferWidth = 960;
    graphics.ApplyChanges();

    DefaultViewport = GraphicsDevice.Viewport;
    HudViewport = new Viewport(0, 0, graphics.PreferredBackBufferWidth, HUD_HEIGHT);
    GameViewport = new Viewport(0, HUD_HEIGHT, graphics.PreferredBackBufferWidth, 576);

    base.Initialize();
  }

  protected override void LoadContent() {
    SpriteBatch = new SpriteBatch(GraphicsDevice);

    Assets.LoadContent();

    BlockSpriteFactory.Instance.LoadAllTextures(Content);
    EnemySpriteFactory.Instance.LoadAllTextures(Content);
    SoundManager.Instance.LoadAllContent(Content);
    ItemSpriteFactory.Instance.LoadAllTextures(Content);
    ProjectileFactory.Instance.LoadAllTextures(Content);

    StateMenu = new StateMenuType(this);
    StateLoss = new StateLossType(this);
    StateWin = new StateWinType(this);
    StateGame = new StateGameType(this);
    currentState = StateMenu;

    Assets.Initialize();
    StateMenu.Initialize();
    StateLoss.Initialize();
    StateWin.Initialize();
    StateGame.Initialize();

    StateMenu.LoadContent();
    StateLoss.LoadContent();
    StateWin.LoadContent();
    StateGame.LoadContent();
  }

  protected override void Update(GameTime gameTime) {
    currentState.Update(gameTime);

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime) {
    currentState.Draw(gameTime);

    base.Draw(gameTime);
  }
}
