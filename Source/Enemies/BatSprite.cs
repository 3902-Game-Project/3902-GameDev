using GameProject.Interfaces;
using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

public class BatSprite : BaseEnemy {
  private IBatState state;

  public BatSprite(Texture2D texture, Vector2 position) : base(texture, position, 64f, 64f) {
    state = new BatIdleState(this);
  }

  public void ChangeState(IBatState newState) {
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

    spriteBatch.Draw(Texture, Position, source, Color.White, 0f, origin, 2f, effect, 0f);
  }

  public override void TakeDamage(int damage) {
    if(Health <= 0) {
      return;
    }
    Health -= damage;
    if(Health <= 0) {
      ChangeState(new BatDeathState(this));
    }
  }
}
