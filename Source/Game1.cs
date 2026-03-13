using GameProject.Factories;
using GameProject.GameStates;
using GameProject.Globals;
using GameProject.Interfaces;
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
  // StateGame must have explicit type as it is directly referenced as a global variable:
  public StateGameType StateGame { get; private set; }
  private IGameState currentState;

  public Game1() {
    graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;

    Assets = new AssetStore(this);

    StateMenu = new StateMenuType(this);
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
    graphics.PreferredBackBufferHeight = 576;
    graphics.PreferredBackBufferWidth = 960;
    graphics.ApplyChanges();

    Assets.Initialize();
    StateMenu.Initialize();
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
