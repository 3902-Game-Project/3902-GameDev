using GameProject.Controllers;
using GameProject.GlobalInterfaces;

namespace GameProject.PlayerSpace.States;

internal interface IPlayerState : ITemporalUpdatable, IGPDrawable {
  internal void MoveUp();
  internal void MoveDown();
  internal void MoveLeft();
  internal void MoveRight();
  internal void UseItem(UseType useType);
  internal void UseKey(UseType useType);
  internal void TakeDamage(int amount);
  internal void Die();
  internal void Interact();
}
