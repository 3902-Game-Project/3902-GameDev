using GameProject.Controllers;
using GameProject.GlobalInterfaces;

namespace GameProject.PlayerSpace.States;

internal interface IPlayerState : IGPUpdatable, IGPDrawable {
  void UseItem(UseType useType);
  void UseKey(UseType useType);
  void MoveUp();
  void MoveDown();
  void MoveLeft();
  void MoveRight();
  void Die();
}
