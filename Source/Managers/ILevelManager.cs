using GameProject.GlobalInterfaces;
using GameProject.Level;

namespace GameProject.Managers;

internal interface ILevelManager : IInitable, IInstantaneousUpdatable, IGPDrawable {
  ILevel CurrentLevel { get; }
  public void SwitchLevel(string newLevelName);
  public void PreviousLevel();
  public void NextLevel();
  public void InitializeLevel();
}
