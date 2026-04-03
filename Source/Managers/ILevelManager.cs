namespace GameProject.Interfaces;

public interface ILevelManager : IInitable, IGPUpdatable, IGPDrawable {
  ILevel CurrentLevel { get; }
  public void SwitchLevel(string newLevelName);
  public void PreviousLevel();
  public void NextLevel();
  public void CompleteLevelSwitch();
}
