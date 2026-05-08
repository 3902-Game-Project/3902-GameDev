using GameProject.GlobalInterfaces;

namespace GameProject.GameStates;

internal interface IGameState : IInitable, ITemporalActiveUpdatable, ILowLevelDrawable {
  internal void OnStateEnter(bool prevStateIsCurrentState);
  internal void OnStateLeave(bool nextStateIsCurrentState);
  internal void OnStateStartFadeIn(bool prevStateIsCurrentState);
  internal void OnStateEndFadeOut(bool nextStateIsCurrentState);
}
