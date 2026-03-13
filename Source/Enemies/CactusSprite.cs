using GameProject.Interfaces;
using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

public class CactusSprite : BaseEnemy {
  private ICactusState state;

  public CactusSprite(Texture2D texture, Vector2 position) : base(texture, position, 32f, 64f) {
    state = new CactusIdleState(this);
  }

  public void ChangeState(ICactusState newState) {
    state = newState;
  }

  public override void Update(GameTime gameTime) {
    state.Update(gameTime);

    UpdateCollider();
  }

  public override void Draw(SpriteBatch spriteBatch) {
    if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) return;

    Rectangle source = CurrentSourceRectangles[CurrentFrame];
    SpriteEffects effect = FacingDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
    Vector2 origin = new(source.Width / 2f, source.Height);

    spriteBatch.Draw(Texture, Position, source, Color.White, 0f, origin, 0.2f, effect, 0f);
  }

  public override void TakeDamage(int damage) {
    if (Health <= 0) {
      return;
    }
    Health -= damage;
  }
}
