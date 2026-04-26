using GameProject.Items;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class WorldPickupFactory {
  private Texture2D ammoSpritesheet;

  public static WorldPickupFactory Instance { get; } = new();

  private WorldPickupFactory() { }

  public void LoadAllTextures(ContentManager contentManager) {
    ammoSpritesheet = contentManager.Load<Texture2D>("Items/ammo_drops");
  }

  public IWorldPickup CreateAmmo(Vector2 position, AmmoType type, int amount) {
    return new AmmoWorldPickup(ammoSpritesheet, position, type, amount);
  }
}
