namespace GameProject.Interfaces;

internal interface IGameState : IGPUpdatable, ILowLevelDrawable, IInitable {
  void OnStateEnter(bool nextStateIsSameState);
  void OnStateLeave(bool nextStateIsSameState);
  void OnStateStartFadeIn(bool nextStateIsSameState);
  void OnStateEndFadeOut(bool nextStateIsSameState);
}
