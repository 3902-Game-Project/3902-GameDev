using GameProject.Controllers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class WhipItem(Texture2D texture, Vector2 startPosition) : IItem {
  private static readonly Rectangle SOURCE_RECT = new(0, 0, 8, 8);

  private Vector2 origin;

  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  public Vector2 Position { get; set; } = startPosition;
  public ItemCategory Category { get; } = ItemCategory.Melee;
  public void OnEquip() { }
  public void OnUnequip() { }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(SOURCE_RECT.Width / 2, SOURCE_RECT.Height / 2);

    spriteBatch.Draw(
      texture,
      Position,
      SOURCE_RECT,
      Color.White,
      0f,
      origin,
      1f,
      SpriteEffects.None,
      0f
    );
  }

  public void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint) {
    spriteBatch.Draw(
      texture: texture,
      position: position,
      sourceRectangle: SOURCE_RECT,
      color: tint,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: scale,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }

  public void Update(double deltaTime) { }

  public void OnPickup(Player player) { }

  public void Use(UseType useType) {
    // Logic for using the whip item
  }
}
