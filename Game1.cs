using GameProject.Factories;
using GameProject.Globals;
using GameProject.Interfaces;
using GameProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameProject;

public class Game1 : Game {
  private readonly GraphicsDeviceManager graphics;

  public SpriteBatch SpriteBatch { get; private set; }
  public GlobalVarStore GlobalVars { get; private set; }

  public StateMenuType StateMenu { get; private set; }
  public StateGameType StateGame { get; private set; }
  private IGameState currentState;
  public List<IBlock> Blocks; // temporary for sprint2
  public int BlockNumber { get; set; } // temporary for sprint2

  public Game1() {
    graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;

    GlobalVars = new GlobalVarStore(this);

    StateMenu = new StateMenuType(this);
    StateGame = new StateGameType(this);
    currentState = StateMenu;

    Blocks = new List<IBlock>(); // temporary for sprint2
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
    SpriteBatch = new SpriteBatch(GraphicsDevice);

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
    currentState.Draw(gameTime);

    base.Draw(gameTime);
  }
}
