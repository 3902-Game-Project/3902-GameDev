using GameProject.Misc;

namespace GameProject.Interfaces;

public interface ILevelManager : IInitable, IUpdatable, IGPDrawable {
  ILevel CurrentLevel { get; }
  public void SwitchLevel(string newLevelName);
  public void PreviousLevel();
  public void NextLevel();
}
