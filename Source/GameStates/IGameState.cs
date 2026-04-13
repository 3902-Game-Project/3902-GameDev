namespace GameProject.Interfaces;

internal interface IGameState : IGPUpdatable, ILowLevelDrawable, IInitable {
  void OnStateEnter(bool prevStateIsCurrentState);
  void OnStateLeave(bool nextStateIsCurrentState);
  void OnStateStartFadeIn(bool prevStateIsCurrentState);
  void OnStateEndFadeOut(bool nextStateIsCurrentState);
}
