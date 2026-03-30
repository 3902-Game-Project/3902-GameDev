using GameProject.Factories;
using GameProject.GameStates;
using GameProject.Globals;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject;

public class Game1 : Game {
  private readonly GraphicsDeviceManager graphics;

  public SpriteBatch SpriteBatch { get; private set; }
  public AssetStore Assets { get; private set; }
  public BlockSpriteFactory BlockFactory { get; private set; }
  public ItemSpriteFactory ItemSpriteFactory { get; private set; }

  public IGameState StateMenu { get; private set; }
  public IGameState StateLoss { get; private set; }
  public IGameState StateWin { get; private set; }
  public StateGameType StateGame { get; private set; }
  public CollisionManager CollisionManager { get; private set; }

  private IGameState currentState;

  public Game1() {
    graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;

    Assets = new AssetStore(this);

    StateMenu = new StateMenuType(this);
    StateLoss = new StateLossType(this);
    StateWin = new StateWinType(this);
    StateGame = new StateGameType(this);
    currentState = StateMenu;

    BlockFactory = new BlockSpriteFactory(this);
    ItemSpriteFactory = new ItemSpriteFactory(this);
  }

  public void ChangeState(IGameState state) {
    currentState = state;
  }

  public void ResetGameState() {
    StateGame = new StateGameType(this);
    StateGame.Initialize();
    StateGame.LoadContent();
  }

  protected override void Initialize() {
    CollisionManager = new CollisionManager();
    graphics.PreferredBackBufferHeight = 576;
    graphics.PreferredBackBufferWidth = 960;
    graphics.ApplyChanges();

    Assets.Initialize();
    StateMenu.Initialize();
    StateLoss.Initialize();
    StateWin.Initialize();
    StateGame.Initialize();

    base.Initialize();
  }

  protected override void LoadContent() {
    SpriteBatch = new SpriteBatch(GraphicsDevice);

    Assets.LoadContent();

    EnemySpriteFactory.Instance.LoadAllTextures(Content);

    ItemSpriteFactory.LoadAllTextures(Content);
    ProjectileFactory.Instance.LoadAllTextures(Content);

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
