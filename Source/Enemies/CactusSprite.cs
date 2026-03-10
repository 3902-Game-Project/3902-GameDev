using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

public class CactusSprite : IEnemy {
  public Texture2D Texture { get; private set; }
  public Vector2 Position;
  public Vector2 Velocity { get; set; }
  public int FacingDirection { get; set; } = 1;

  public Rectangle BoundingBox {
    get {
      if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) return Rectangle.Empty;
      Rectangle source = CurrentSourceRectangles[CurrentFrame];
      float scale = 0.2f;
      int width = (int)(source.Width * scale);
      int height = (int)(source.Height * scale);
      return new Rectangle((int)Position.X - width / 2, (int)Position.Y - height, width, height);
    }
  }

  public List<Rectangle> CurrentSourceRectangles;
  public int CurrentFrame;

  private ICactusState state;

  public CactusSprite(Texture2D texture, Vector2 position) {
    Texture = texture;
    Position = position;
    state = new CactusIdleState(this);
  }

  public void ChangeState(ICactusState newState) {
    state = newState;
  }

  public void Update(GameTime gameTime) {
    state.Update(gameTime);
  }

  public void Draw(SpriteBatch spriteBatch) {
    if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) {
      return;
    }

    Rectangle source = CurrentSourceRectangles[CurrentFrame];

    SpriteEffects effect = FacingDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

    Vector2 origin = new(source.Width / 2, source.Height);

    float scale = 0.2f;
    spriteBatch.Draw(Texture, Position, source, Color.White, 0f, origin, scale, effect, 0f);
  }

  public void TakeDamage() { }
}
