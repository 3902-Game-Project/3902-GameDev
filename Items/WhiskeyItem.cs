using GameProject.Animations;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameProject.Items;

public class WhiskeyItem : IItem {
  private Texture2D texture;
  private Vector2 position;
  private Rectangle sourceRectangle;
  private Vector2 origin;

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      position,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      1f,
      SpriteEffects.None,
      0f
    );
  }

  public void Update(GameTime gameTime) {

  }

  public void OnPickup() {
    // Logic for using the whiskey item
  }

  public void Use() {
    // Logic for using the whiskey item
  }

  public WhiskeyItem(Texture2D texture, Vector2 startPosition) {
    this.texture = texture;
    this.position = startPosition;
    sourceRectangle = new Rectangle(0, 0, 8, 8);
  }
}