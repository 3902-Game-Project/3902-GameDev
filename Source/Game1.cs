using System;
using GameProject.Enemies;
using GameProject.GameStates;
using GameProject.Globals;
using GameProject.Items;
using GameProject.Misc;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject;

internal class Game1 : Game {
  private readonly GraphicsDeviceManager graphics;
  private RenderTarget2D renderTarget;
  private ValueTracker<RenderTarget2D> renderTargetTracker;
  private ValueTracker<Viewport> viewportTracker;
  private Rectangle renderScaleRectangle;

  public SpriteBatch SpriteBatch { get; private set; }
  public Viewport DefaultViewport { get; private set; }
  public Viewport HudViewport { get; private set; }
  public Viewport GameViewport { get; private set; }
  public GameStateMachine StateMachine { get; private set; }

  public Game1() {
    graphics = new GraphicsDeviceManager(this);
    IsMouseVisible = true;
    Content.RootDirectory = "Content";

    StateMachine = new(this);
  }

  protected override void Initialize() {
    graphics.PreferredBackBufferHeight = Constants.WINDOW_HEIGHT;
    graphics.PreferredBackBufferWidth = Constants.WINDOW_WIDTH;
    graphics.ApplyChanges();

    renderTargetTracker = ValueTrackerFactory.CreateRenderTargetTracker(GraphicsDevice);

    Window.AllowUserResizing = true;
    Window.ClientSizeChanged += OnResize;

    DefaultViewport = GraphicsDevice.Viewport;
    // Technically it might be best to get the width from graphics.PreferredBackBufferWidth in case it changes,
    // And similarly for graphics.PreferredBackBufferHeight (which could alter Constants.GAME_HEIGHT)
    // But its cleaner to use the value from Constants instead without altering the value from Constants to match
    HudViewport = new Viewport(0, 0, Constants.WINDOW_WIDTH, Constants.HUD_HEIGHT);
    GameViewport = new Viewport(0, Constants.HUD_HEIGHT, Constants.WINDOW_WIDTH, Constants.GAME_HEIGHT);

    viewportTracker = ValueTrackerFactory.CreateViewportTracker(GraphicsDevice, DefaultViewport);

    MiscAssetStore.Instance.Initialize();
    TextureStore.Instance.Initialize();

    StateMachine.Initialize();

    base.Initialize();
  }

  private void OnResize(object sender, EventArgs e) {
    UpdateRenderScaleRectangle();
  }

  private void UpdateRenderScaleRectangle() {
    int screenWidth = Window.ClientBounds.Width;
    int screenHeight = Window.ClientBounds.Height;

    float outputAspect = (float) screenWidth / screenHeight;
    float preferredAspect = (float) Constants.WINDOW_WIDTH / Constants.WINDOW_HEIGHT;

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
    renderTarget = new RenderTarget2D(GraphicsDevice, Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT);
    UpdateRenderScaleRectangle();

    MiscAssetStore.Instance.LoadContent(Content);
    TextureStore.Instance.LoadContent(GraphicsDevice, Content);

    EnemyFactory.Instance.LoadAllTextures(Content);
    SoundManager.Instance.LoadAllContent(Content);
    ItemFactory.Instance.LoadAllTextures(Content);
    ProjectileFactory.Instance.LoadAllTextures(Content);

    StateMachine.LoadContent(Content);
  }

  protected override void Update(GameTime gameTime) {
    StateMachine.Update(gameTime.ElapsedGameTime.TotalSeconds, IsActive);

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime) {
    // Render everything that should be on screen to a texture

    using (renderTargetTracker.TempSet(renderTarget)) {
      StateMachine.LowLevelDraw(new(
        GraphicsDevice.Clear,
        renderTargetTracker,
        viewportTracker,
        SpriteBatch
      ));
    }

    // Then render the texture to screen

    GraphicsDevice.Clear(Constants.LETTERBOX_COLOR);
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
