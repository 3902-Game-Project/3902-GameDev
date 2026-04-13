using Microsoft.Xna.Framework;

namespace GameProject.GlobalInterfaces;

internal interface IFireMode {
  bool CanFire(UseType useType);
  void Update(GameTime gameTime);
  void OnEquip();
  void OnUnequip();
}
