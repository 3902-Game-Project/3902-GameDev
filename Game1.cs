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

  public StateMenuType StateMenu { get; private set; }
  public StateGameType StateGame { get; private set; }
  private IGameState currentState;
  public BlockSpriteFactory blockFactory;
  public Player Player { get; private set; }

  public Game1() {
    graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;

    Assets = new AssetStore(this);

    StateMenu = new StateMenuType(this);
    StateGame = new StateGameType(this);
    currentState = StateMenu;

    blockFactory = new BlockSpriteFactory();
  }

  public void ChangeState(IGameState state) {
    currentState = state;
  }

  protected override void Initialize() {
    Assets.Initialize();
    StateMenu.Initialize();
    StateGame.Initialize();
    Player = new Player(this);

    base.Initialize();
  }

  protected override void LoadContent() {
    SpriteBatch = new SpriteBatch(GraphicsDevice);

    Assets.LoadContent();
    StateMenu.LoadContent();
    StateGame.LoadContent();

    blockFactory.LoadAllTextures(this);
    EnemySpriteFactory.Instance.LoadAllTextures(Content);

    var snake = EnemySpriteFactory.Instance.CreateSnakeSprite();
    StateGame.Enemies.Add(snake);

    var shotgunner = EnemySpriteFactory.Instance.CreateShotgunnerSprite();
    StateGame.Enemies.Add(shotgunner);

    var bat = EnemySpriteFactory.Instance.CreateBatSprite();
    StateGame.Enemies.Add(bat);
  }

  protected override void Update(GameTime gameTime) {
    currentState.Update(gameTime);

    Player.Update(gameTime);

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime) {
    currentState.Draw(gameTime);

    //Player.Draw(SpriteBatch);

    base.Draw(gameTime);
  }
}
