using GameProject.Interfaces;
using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameProject.Sprites {
  public class SnakeSprite : IEnemy {
    // Public properties so states can change them
    public Texture2D Texture { get; private set; }
    public Vector2 Position; // This needs to be a field or public property
    public Vector2 Velocity;
    public int FacingDirection = 1;

    public List<Rectangle> CurrentSourceRectangles;
    public int CurrentFrame;

    private ISnakeState state;

    public SnakeSprite(Texture2D texture, Vector2 position) {
      Texture = texture;
      Position = position;
      // Start in Idle or Wander
      state = new SnakeWanderState(this);
    }

    public void ChangeState(ISnakeState newState) {
      state = newState;
    }

    public void Update(GameTime gameTime) {
      state.Update(gameTime);

      // Optional: Keep inside bounds here or inside state
      if (Position.X < 0) Position.X = 0;
    }

    public void Draw(SpriteBatch spriteBatch) {
      if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) return;

      Rectangle source = CurrentSourceRectangles[CurrentFrame];

      // Flip logic
      SpriteEffects effect = (FacingDirection > 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

      // Origin at feet
      Vector2 origin = new Vector2(source.Width / 2, source.Height);

      spriteBatch.Draw(Texture, Position, source, Color.White, 0f, origin, 2f, effect, 0f);
    }

    public void TakeDamage() { /* ... */ }
    public void ChangeDirection() { /* Helper if needed */ }
  }
}
