namespace GameProject.Interfaces;

internal interface IGameState : IGPUpdatable, ILowLevelDrawable, IInitable {
  void OnStateEnter(bool nextStateIsCurrentState);
  void OnStateLeave(bool nextStateIsCurrentState);
  void OnStateStartFadeIn(bool nextStateIsCurrentState);
  void OnStateEndFadeOut(bool nextStateIsCurrentState);
}
