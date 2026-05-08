using GameProject.Controllers;
using GameProject.PlayerSpace;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class InvincibilityPotionItem(Texture2D texture, Vector2 startPosition, Player player) : IItem, IWorldPickup {
  internal FacingDirection Direction { get; set; } = FacingDirection.Right;
  internal Vector2 Position { get; set; } = startPosition;
  internal bool IsCollected { get; set; } = false;
  internal bool IsAutoCollect { get; } = true;

  internal ItemCategory Category { get; } = ItemCategory.Consumable;

  internal void OnEquip() { }
  internal void OnUnequip() { }

  internal void Draw(SpriteBatch spriteBatch) {
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

  internal void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint) {
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

  internal void Update(double deltaTime) { }

  internal void OnPickup(Player player) {
    IsCollected = true;
    // Logic to add to player's inventory goes here
  }

  internal void Use(UseType useType) {
    player.AddInvincibility(10f);
  }
}
