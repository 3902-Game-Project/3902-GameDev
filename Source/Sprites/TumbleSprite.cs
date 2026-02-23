using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

public class TumbleSprite : IEnemy {
  public Texture2D Texture { get; private set; }
  public Vector2 Position;
  public Vector2 Velocity;
  public int FacingDirection = 1;

  public List<Rectangle> CurrentSourceRectangles;
  public int CurrentFrame;

  private ITumbleState state;

  public TumbleSprite(Texture2D texture, Vector2 position) {
    Texture = texture;
    Position = position;
    state = new TumbleIdleState(this);
  }

  public void ChangeState(ITumbleState newState) {
    state = newState;
  }

  public void Update(GameTime gameTime) {
    state.Update(gameTime);
    if (Position.X < 0) {
      Position.X = 0;
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) {
      return;
    }

    Rectangle source = CurrentSourceRectangles[CurrentFrame];

    SpriteEffects effect = (FacingDirection > 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

    Vector2 origin = new(source.Width / 2, source.Height);

    float scale = 0.4f;
    spriteBatch.Draw(Texture, Position, source, Color.White, 0f, origin, scale, effect, 0f);
  }

  public void TakeDamage() { ChangeState(new TumbleDeathState(this)); }
}
