using GameProject.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

internal class HeartWorldPickup(Texture2D texture, Vector2 position) : ABaseWorldPickup(position), ICollidable {
  private static readonly Rectangle SOURCE_RECT = new(9, 15, 12, 11);
  private static readonly float SCALE = 2f;
  
  private Vector2 origin;
  
  public override void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(SOURCE_RECT.Width / 2, SOURCE_RECT.Height / 2);

    spriteBatch.Draw(
      texture,
      Position,
      SOURCE_RECT,
      Color.White,
      0f,
      origin,
      SCALE,
      SpriteEffects.None,
      0f
    );
  }

  public override void Update(double deltaTime) { }
}
