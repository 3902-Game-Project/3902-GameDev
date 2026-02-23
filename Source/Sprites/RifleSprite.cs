using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

public class RifleSprite : IEnemy {
  // Data needed by states
  public Texture2D Texture { get; private set; }
  public Vector2 Position;
  public Vector2 Velocity;
  public int FacingDirection = 1;

  public List<Rectangle> CurrentSourceRectangles;
  public int CurrentFrame;

  private IRifleState state;

  public RifleSprite(Texture2D texture, Vector2 position) {
    Texture = texture;
    Position = position;
    state = new RifleDeathState(this);
  }

  public void ChangeState(IRifleState newState) {
    state = newState;
  }

  public void Update(GameTime gameTime) {
    state.Update(gameTime);

    // Keep inside bounds
    if (Position.X < 0) {
      Position.X = 0;
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) {
      return;
    }

    Rectangle source = CurrentSourceRectangles[CurrentFrame];

    // Flip logic
    SpriteEffects effect = (FacingDirection > 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

    // Origin at feet
    Vector2 origin = new(source.Width / 2, source.Height);

    spriteBatch.Draw(Texture, Position, source, Color.White, 0f, origin, 2f, effect, 0f);
  }

  public void TakeDamage() { ChangeState(new RifleDeathState(this)); }
}
