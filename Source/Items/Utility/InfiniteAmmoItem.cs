using GameProject.Controllers;
using GameProject.PlayerSpace;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class InfiniteAmmoItem(Texture2D texture, Vector2 startPosition, Player player) : IItem, IWorldPickup {
  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  public Vector2 Position { get; set; } = startPosition;
  public bool IsCollected { get; set; } = false;
  public bool IsAutoCollect { get; } = true;

  public ItemCategory Category { get; } = ItemCategory.Consumable;

  public void OnEquip() { }
  public void OnUnequip() { }

  public void Draw(SpriteBatch spriteBatch) {
    if (!IsCollected) {
      spriteBatch.Draw(
          texture,
          Position,
          null,
          Color.White,
          0f,
          Vector2.Zero,
          .15f,
          SpriteEffects.None,
          0f
      );
    }
  }

  public void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint) {
    spriteBatch.Draw(
        texture: texture,
        position: position,
        sourceRectangle: null,
        color: tint,
        rotation: 0f,
        origin: Vector2.Zero,
        scale: scale,
        effects: SpriteEffects.None,
        layerDepth: 0f
    );
  }

  public void Update(double deltaTime) { }

  public void OnPickup(Player pickupPlayer) {
    if (!IsCollected) {
      IsCollected = true;
      // This actually puts it in the backpack!
      pickupPlayer.Inventory.PickupItem(this);
    }
  }

  public void Use(UseType useType) {
    // The primary constructor 'player' is used here!
    player.InfiniteAmmoTimer += 10f;
  }
}
