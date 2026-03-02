namespace GameProject.Interfaces;

public interface ILevelManager : IInitable, IUpdatable, IGPDrawable {
  public void SwitchLevel(int newLevelIndex);
  public void PreviousLevel();
  public void NextLevel();
}
