using GameProject.Controllers;
using GameProject.GlobalInterfaces;

namespace GameProject.PlayerSpace.States;

internal interface IPlayerState : IInstantaneousUpdatable, IGPDrawable {
  void MoveUp();
  void MoveDown();
  void MoveLeft();
  void MoveRight();
  void UseItem(UseType useType);
  void UseKey(UseType useType);
  void TakeDamage(int amount);
  void Die();
}
