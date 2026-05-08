using GameProject.Controllers;
using GameProject.GlobalInterfaces;

namespace GameProject.FireModes;

internal interface IFireMode : ITemporalUpdatable {
  internal bool CanFire(UseType useType);
  internal void OnEquip();
  internal void OnUnequip();
}
