using GameProject.GlobalInterfaces;

namespace GameProject.GameStates;

internal interface IGameState : IInitable, IGPUpdatable, ILowLevelDrawable {
  void OnStateEnter(bool prevStateIsCurrentState);
  void OnStateLeave(bool nextStateIsCurrentState);
  void OnStateStartFadeIn(bool prevStateIsCurrentState);
  void OnStateEndFadeOut(bool nextStateIsCurrentState);
}
