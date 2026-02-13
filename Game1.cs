using GameProject.Controllers;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject;

public class Game1 : Game {
  private GraphicsDeviceManager _graphics;
  private SpriteBatch _spriteBatch;

  private Texture2D myTexture;
  public Texture2D Texture {
    get { return myTexture; }
  }
  private SpriteFont myFont;
  private IController keyboardController;
  private ISprite currentSprite;
  public ISprite CurrentSprite {
    set { currentSprite = value; }
  }

  public IGameState stateMenu { get; private set; }
  public IGameState stateGame { get; private set; }
  private IGameState currentState;

  public Game1() {
    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
    stateMenu = GameStateFactory.Instance.CreateMenuState(this);
    stateGame = GameStateFactory.Instance.CreateGameState(this);
    currentState = stateMenu;
  }

  public void ChangeState(IGameState state) {
    currentState = state;
  }

  protected override void Initialize() {
    keyboardController = new KeyboardController(this);

    base.Initialize();
  }

  protected override void LoadContent() {
    _spriteBatch = new SpriteBatch(GraphicsDevice);

    myTexture = Content.Load<Texture2D>("Metro");
    currentSprite = new FixedSprite(myTexture, new Vector2(400, 200));
    EnemySpriteFactory.Instance.LoadAllTextures(Content);
    myFont = Content.Load<SpriteFont>("CreditsFont");
  }

  protected override void Update(GameTime gameTime) {
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
      Exit();
    }

    keyboardController.Update(gameTime);
    currentSprite.Update(gameTime);
    currentState.Update(gameTime);

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime) {
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _spriteBatch.Begin();
    currentSprite.Draw(_spriteBatch);
    currentState.Draw(_spriteBatch, gameTime);
    _spriteBatch.End();

    base.Draw(gameTime);
  }
}
