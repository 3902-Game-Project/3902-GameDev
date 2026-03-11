using GameProject.Enums;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class AmmoRefill(Texture2D texture, Vector2 startPosition) : IItem {
  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  private Rectangle sourceRectangle = new(0, 0, 8, 8);
  private Vector2 origin;
  public Vector2 Position { get; set; } = startPosition;
  public ItemCategory Category { get; } = ItemCategory.Consumable; // Just to prevent errors for now - want to eventually make ammo something you gather by walking over it


  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
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

  public void Update(GameTime gameTime) { }

  public void Use(UseType useType) {
    // Logic for using the ammo refill item
  }
}
