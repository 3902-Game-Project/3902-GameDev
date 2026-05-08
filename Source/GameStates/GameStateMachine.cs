using System.Collections.Generic;
using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework.Content;

namespace GameProject.GameStates;

internal enum GameState {
  StateGame,
  StateMenu,
  StateLoss,
  StateWin,
  StatePause,
  StateItemScreen,
  StateSavePrompt,
  StateLoadPrompt,
}

internal class GameStateMachine(Game1 game) : IInitable, ITemporalActiveUpdatable, ILowLevelDrawable {
  private StateTransitionType StateTransition;
  public StateGameType StateGame { get; private set; }
  private IGameState StateMenu { get; set; }
  private IGameState StateLoss { get; set; }
  private IGameState StateWin { get; set; }
  private IGameState StatePause { get; set; }
  private IGameState StateItemScreen { get; set; }
  private IGameState StateSavePrompt { get; set; }
  private IGameState StateLoadPrompt { get; set; }
  private IGameState currentState;

  private IGameState GetGameState(GameState state) {
    return state switch {
      GameState.StateGame => StateGame,
      GameState.StateMenu => StateMenu,
      GameState.StateLoss => StateLoss,
      GameState.StateWin => StateWin,
      GameState.StatePause => StatePause,
      GameState.StateItemScreen => StateItemScreen,
      GameState.StateSavePrompt => StateSavePrompt,
      GameState.StateLoadPrompt => StateLoadPrompt,
      _ => throw new System.NotImplementedException(),
    };
  }

  private void ChangeIStateWithoutFading(IGameState newState) {
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

  public void ChangeState(GameState newStateEnum) {
    StateTransition.SetFadingStates(currentState, GetGameState(newStateEnum));
    ChangeStateWithoutFading(StateTransition);
  }

  public void ChangeStateWithoutFading(GameState newStateEnum) {
    ChangeIStateWithoutFading(GetGameState(newStateEnum));
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
