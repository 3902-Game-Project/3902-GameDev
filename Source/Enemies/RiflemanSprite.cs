using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

public class RiflemanSprite : IEnemy {
  // Data needed by states
  public Texture2D Texture { get; private set; }
  public ProjectileManager ProjectileManager { get; private set; }
  public Vector2 Position;
  public Vector2 Velocity { get; set; }
  public int FacingDirection { get; set; } = 1;

  public Rectangle BoundingBox {
    get {
      if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) return Rectangle.Empty;
      Rectangle source = CurrentSourceRectangles[CurrentFrame];
      float scale = 2f;
      int width = (int)(source.Width * scale);
      int height = (int)(source.Height * scale);
      return new Rectangle((int)Position.X - width / 2, (int)Position.Y - height, width, height);
    }
  }

  public List<Rectangle> CurrentSourceRectangles;
  public int CurrentFrame;

  private IRiflemanState state;

  public RiflemanSprite(Texture2D texture, Vector2 position, ProjectileManager projectileManager) {
    Texture = texture;
    Position = position;
    ProjectileManager = projectileManager;
    state = new RifleWanderState(this);
  }

  public void ChangeState(IRiflemanState newState) {
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
    SpriteEffects effect = FacingDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

    // Origin at feet
    Vector2 origin = new(source.Width / 2, source.Height);

    spriteBatch.Draw(Texture, Position, source, Color.White, 0f, origin, 2f, effect, 0f);
  }

  public void TakeDamage() { ChangeState(new RifleDeathState(this)); }
}
