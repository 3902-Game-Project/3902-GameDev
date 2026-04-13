using GameProject.Managers;

namespace GameProject.Managers;

internal interface ILevelManager : IInitable, IGPUpdatable, IGPDrawable {
  ILevel CurrentLevel { get; }
  CollisionManager CollisionManager { get; }
  public void SwitchLevel(string newLevelName);
  public void PreviousLevel();
  public void NextLevel();
  public void CompleteLevelSwitch();
}
