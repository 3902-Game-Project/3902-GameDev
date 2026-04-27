using GameProject.PlayerSpace;
using GameProject.Source.Commands;

namespace GameProject.Commands;

internal class GameplayTestModeCommand(Player player) :
  MetaCommand([
    new PlayerUnlimitedHealthCommand(player),
    new PlayerUnlimitedAmmoCommand(player),
  ]) { }
