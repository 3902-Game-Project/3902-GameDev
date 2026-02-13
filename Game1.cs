using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject;

public class Game1 : Game {
  private readonly GraphicsDeviceManager graphics;
  private SpriteBatch spriteBatch;

  public GlobalVarStore GlobalVars { get; private set; }

  public IGameState StateMenu { get; private set; }
  public IGameState StateGame { get; private set; }
  private IGameState currentState;

  public Game1() {
    graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;

    GlobalVars = new GlobalVarStore(this);

    StateMenu = GameStateFactory.Instance.CreateMenuState(this);
    StateGame = GameStateFactory.Instance.CreateGameState(this);
    currentState = StateMenu;
  }

  public void ChangeState(IGameState state) {
    currentState = state;
  }

  protected override void Initialize() {
    GlobalVars.Initialize();
    StateMenu.Initialize();
    StateGame.Initialize();

    base.Initialize();
  }

  protected override void LoadContent() {
    spriteBatch = new SpriteBatch(GraphicsDevice);

    GlobalVars.LoadContent();
    StateMenu.LoadContent();
    StateGame.LoadContent();

    EnemySpriteFactory.Instance.LoadAllTextures(Content);
  }

  protected override void Update(GameTime gameTime) {
    currentState.Update(gameTime);

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime) {
    GraphicsDevice.Clear(Color.CornflowerBlue);

    spriteBatch.Begin();
    currentState.Draw(spriteBatch, gameTime);
    spriteBatch.End();

    base.Draw(gameTime);
  }
}
