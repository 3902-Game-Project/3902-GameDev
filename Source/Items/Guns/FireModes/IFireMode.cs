using GameProject.Controllers;
using GameProject.GlobalInterfaces;

namespace GameProject.FireModes;

internal interface IFireMode : IInstantaneousUpdatable {
  bool CanFire(UseType useType);
  void OnEquip();
  void OnUnequip();
}
