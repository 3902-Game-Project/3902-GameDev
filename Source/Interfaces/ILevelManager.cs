namespace GameProject.Interfaces;

public interface ILevelManager : IInitable, IUpdatable, IGPDrawable {
  ILevel CurrentLevel { get; }

  public void SwitchLevel(int newLevelIndex);
  public void PreviousLevel();
  public void NextLevel();
}
