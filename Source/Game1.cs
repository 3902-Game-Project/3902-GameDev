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

  public StateMenuType StateMenu { get; private set; }
  public StateGameType StateGame { get; private set; }
  private IGameState currentState;
  public BlockSpriteFactory blockFactory;
  public ProjectileManager ProjectileManager { get; private set; }
  public ItemSpriteFactory ItemSpriteFactory { get; private set; }

  public Game1() {
    graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;

    Assets = new AssetStore(this);

    StateMenu = new StateMenuType(this);
    ResetGameState();
    currentState = StateMenu;

    blockFactory = new BlockSpriteFactory();
    ProjectileManager = new ProjectileManager();
    ItemSpriteFactory = new ItemSpriteFactory(ProjectileManager);
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
    Assets.Initialize();
    StateMenu.Initialize();
    StateGame.Initialize();

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

    ItemSpriteFactory.LoadAllTextures(Content);
    ProjectileFactory.Instance.LoadAllTextures(Content);

    var revolver = ItemSpriteFactory.CreateRevolver();
    StateGame.Items.Add(revolver);

    var rifle = ItemSpriteFactory.CreateRifle();
    StateGame.Items.Add(rifle);

    var shotgun = ItemSpriteFactory.CreateShotgun();
    StateGame.Items.Add(shotgun);
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
