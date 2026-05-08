using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework.Content;

namespace GameProject.GameStates;

internal class GameStateMachine(Game1 game) : IInitable, ITemporalActiveUpdatable, ILowLevelDrawable {
  private StateTransitionType StateTransition;
  public StateGameType StateGame { get; private set; }
  public IGameState StateMenu { get; private set; }
  public IGameState StateLoss { get; private set; }
  public IGameState StateWin { get; private set; }
  public IGameState StatePause { get; private set; }
  public IGameState StateItemScreen { get; private set; }
  public IGameState StateSavePrompt { get; private set; }
  public IGameState StateLoadPrompt { get; private set; }
  private IGameState currentState;

  public void ChangeState(IGameState newState) {
    StateTransition.SetFadingStates(currentState, newState);
    ChangeStateWithoutFading(StateTransition);
  }

  public void ChangeStateWithoutFading(IGameState newState) {
    bool nextStateIsCurrentState;

    if (currentState == StateTransition || newState == StateTransition) {
      nextStateIsCurrentState = StateTransition.NextStateIsCurrentState();
    } else {
      nextStateIsCurrentState = currentState == newState;
    }

    currentState.OnStateLeave(nextStateIsCurrentState);
    currentState = newState;
    newState.OnStateEnter(nextStateIsCurrentState);
  }

  public void ResetGameState() {
    StateGame = new StateGameType(game);
    StateGame.Initialize();
    StateGame.LoadContent(game.Content);
  }

  public void Initialize() { }

  public void LoadContent(ContentManager contentManager) {
    StateTransition = new StateTransitionType(game);
    StateMenu = new StateMenuType(game);
    StateLoss = new StateLossType(game);
    StateWin = new StateWinType(game);
    StatePause = new StatePauseType(game);
    StateItemScreen = new StateItemScreenType(game);
    StateSavePrompt = new StateSavePromptType(game);
    StateLoadPrompt = new StateLoadPromptType(game);
    StateGame = new StateGameType(game);
    currentState = StateMenu;

    StateTransition.Initialize();
    StateMenu.Initialize();
    StateLoss.Initialize();
    StateWin.Initialize();
    StatePause.Initialize();
    StateItemScreen.Initialize();
    StateSavePrompt.Initialize();
    StateLoadPrompt.Initialize();
    StateGame.Initialize();

    StateTransition.LoadContent(contentManager);
    StateMenu.LoadContent(contentManager);
    StateLoss.LoadContent(contentManager);
    StateWin.LoadContent(contentManager);
    StatePause.LoadContent(contentManager);
    StateItemScreen.LoadContent(contentManager);
    StateSavePrompt.LoadContent(contentManager);
    StateLoadPrompt.LoadContent(contentManager);
    StateGame.LoadContent(contentManager);
  }

  public void Update(double deltaTime, bool isActive) {
    currentState.Update(deltaTime, isActive);
  }

  public void LowLevelDraw(LowLevelDrawParams drawData) {
    currentState.LowLevelDraw(drawData);
  }
}
