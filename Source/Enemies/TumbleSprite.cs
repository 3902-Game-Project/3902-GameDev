using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

public class TumbleSprite : BaseEnemy {
  private ITumbleState state;

  public TumbleSprite(Texture2D texture, Vector2 position) : base(texture, position, 48f, 48f) {
    state = new TumbleIdleState(this);
  }

  public void ChangeState(ITumbleState newState) {
    state = newState;
  }

  public override void Update(GameTime gameTime) {
    state.Update(gameTime);
    if (Position.X < 0) {
      Position = new Vector2(0, Position.Y);
    }

    UpdateCollider();
  }

  public override void Draw(SpriteBatch spriteBatch) {
    if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) return;

    Rectangle source = CurrentSourceRectangles[CurrentFrame];
    SpriteEffects effect = FacingDirection > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
    Vector2 origin = new(source.Width / 2f, source.Height);

    spriteBatch.Draw(Texture, Position, source, Color.White, 0f, origin, 0.4f, effect, 0f);
  }

  public override void TakeDamage() { ChangeState(new TumbleDeathState(this)); }
}
