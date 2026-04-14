using GameProject.Controllers;
using Microsoft.Xna.Framework;

namespace GameProject.FireModes;

internal interface IFireMode {
  bool CanFire(UseType useType);
  void Update(GameTime gameTime);
  void OnEquip();
  void OnUnequip();
}
