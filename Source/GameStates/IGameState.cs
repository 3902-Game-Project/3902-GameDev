namespace GameProject.Interfaces;

internal interface IGameState : IGPUpdatable, ILowLevelDrawable, IInitable {
  void OnStateEnter();
  void OnStateLeave();
  void OnStateStartFadeIn();
  void OnStateEndFadeOut();
}
