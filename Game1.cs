/**
 * Authors:
 * Q
 * Santosh Schaefer
 */

using GameProject.Controllers;
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

  public Game1() {

    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;

  }
  protected override void Initialize() {
    keyboardController = new KeyboardController(this);


    base.Initialize();
  }

  protected override void LoadContent() {
    _spriteBatch = new SpriteBatch(GraphicsDevice);

    myTexture = Content.Load<Texture2D>("Metro");
    currentSprite = new FixedSprite(myTexture, new Vector2(400, 200));

    myFont = Content.Load<SpriteFont>("CreditsFont");
  }


  protected override void Update(GameTime gameTime) {
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    keyboardController.Update(gameTime);
    currentSprite.Update(gameTime);

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime) {
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _spriteBatch.Begin();
    currentSprite.Draw(_spriteBatch);
    _spriteBatch.End();

    base.Draw(gameTime);
  }

}
