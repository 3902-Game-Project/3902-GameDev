using GameProject.Globals;
using GameProject.Items;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Factories;

internal class WorldPickupFactory {
  internal static WorldPickupFactory Instance { get; } = new();

  private WorldPickupFactory() { }

  internal static IWorldPickup CreateAmmo(Vector2 position, AmmoType type, int amount) {
    return new AmmoWorldPickup(TextureStore.Instance.AmmoRefill, position, type, amount);
  }
}
