using GameProject.GlobalInterfaces;

namespace GameProject.GameStates;

internal interface IGameState : IInitable, ILowLevelDrawable {
  void Update(double deltaTime, bool isActive);
  void OnStateEnter(bool prevStateIsCurrentState);
  void OnStateLeave(bool nextStateIsCurrentState);
  void OnStateStartFadeIn(bool prevStateIsCurrentState);
  void OnStateEndFadeOut(bool nextStateIsCurrentState);
}
