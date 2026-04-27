using GameProject.GlobalInterfaces;
using GameProject.Level;

namespace GameProject.Managers;

internal interface ILevelManager : IInitable, ITemporalUpdatable, IGPDrawable {
  ILevel CurrentLevel { get; }
  bool BfgSpawned { get; set; }
  ILevel BfgLevel { get; }
  void SwitchLevel(string newLevelName);
  void PreviousLevel();
  void NextLevel();
  void InitializeLevel();
  bool AllPreBfgLevelEnemiesKilled();
}
