using GameProject.Controllers;
using GameProject.GlobalInterfaces;

namespace GameProject.FireModes;

internal interface IFireMode : ITemporalUpdatable {
  bool CanFire(UseType useType);
  void OnEquip();
  void OnUnequip();
}
