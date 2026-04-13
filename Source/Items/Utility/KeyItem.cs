using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items.Utility;

public class KeyItem(Texture2D keyTexture, Vector2 startPosition, ILevelManager levelManager) : IItem, IWorldPickup {
  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  private Rectangle sourceRectangle = new(0, 1344, 21, 39); // CHANGE
  private Vector2 origin;
  public Vector2 Position { get; set; } = startPosition;
  public bool IsCollected { get; set; } = false;
  public ItemCategory Category { get; } = ItemCategory.Consumable;

  public void Draw(SpriteBatch spriteBatch) {
    if (!IsCollected) {
      origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

      spriteBatch.Draw(
        keyTexture,
        Position,
        sourceRectangle,
        Color.White,
        0f,
        origin,
        1f,
        SpriteEffects.None,
        0f
      );
    }
  }

  public void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint) {
    spriteBatch.Draw(
      texture: keyTexture,
      position: position,
      sourceRectangle: sourceRectangle,
      color: tint,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: scale,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }

  public void Update(GameTime gameTime) {
    // if key is picked up, stop drawing key
  }

  public void Use(UseType useType) {
    // unlocks and changes door state, remove key from inventory if used correctly
  }
  public void OnPickup(Player player) {
    IsCollected = true;
    //Player.Inventory.PickUpItem(this);
    levelManager.CurrentLevel.Pickups.Remove(this);
  }
}
