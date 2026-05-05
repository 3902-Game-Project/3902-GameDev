using GameProject.GlobalInterfaces;

namespace GameProject.Level;

internal interface ILevelManager : IInitable, ITemporalUpdatable, IGPDrawable {
  ILevel CurrentLevel { get; }
  string CurrentLevelName { get; }
  void ChangeLevel(string levelName);
  void SwitchLevel(string newLevelName);
  void PreviousLevel();
  void NextLevel();
  void InitializeLevel();
  void CheckBfgSpawnable();
}
