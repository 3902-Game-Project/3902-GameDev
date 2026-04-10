namespace GameProject.Interfaces;

public interface IGameState : IGPUpdatable, ILowLevelDrawable, IInitable {
  void OnStateEnter();
  void OnStateLeave();
  void OnStateStartFadeIn();
  void OnStateEndFadeOut();
}
