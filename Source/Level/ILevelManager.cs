using GameProject.GlobalInterfaces;

namespace GameProject.Level;

internal interface ILevelManager : IInitable, ITemporalUpdatable, IGPDrawable {
  internal ILevel CurrentLevel { get; }
  internal string CurrentLevelName { get; }
  internal void SwitchLevel(string newLevelName);
  internal void SwitchLevelWithoutFading(string newLevelName);
  internal void PreviousLevel();
  internal void NextLevel();
  internal void InitializeLevel();
  internal void CheckBfgSpawnable();
}
