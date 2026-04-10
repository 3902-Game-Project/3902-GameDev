namespace GameProject.Interfaces;

public interface IGameState : IGPUpdatable, IGPDrawable, IInitable {
  void OnStateEnter();
  void OnStateLeave();
  void OnStateStartFadeIn();
  void OnStateEndFadeOut();
}
