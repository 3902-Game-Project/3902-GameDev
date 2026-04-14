using GameProject.Enemies.SnakeStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class SnakeSprite : BaseEnemy {
  private ISnakeState state;

  public SnakeSprite(Texture2D texture, Vector2 position) : base(texture, position, 64f, 32f) {
    state = new SnakeWanderState(this);
  }

  public void ChangeState(ISnakeState newState) {
    state = newState;
  }

  public override void Update(GameTime gameTime) {
    base.Update(gameTime);

    state.Update(gameTime);
    if (Position.X < 0) {
      Position = new Vector2(0, Position.Y);
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) return;

    Rectangle source = CurrentSourceRectangles[CurrentFrame];
    SpriteEffects effect = FacingDirection > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
    Vector2 origin = new(source.Width / 2f, source.Height);

    Color tintColor = DamageFlashTimer > 0 ? Color.Red : Color.White;

    spriteBatch.Draw(Texture, Position, source, tintColor, 0f, origin, 2f, effect, 0f);
  }

  public override void TakeDamage(int damage) {
    bool wasAlive = Health > 0;

    base.TakeDamage(damage);

    if (wasAlive && Health <= 0) {
      ChangeState(new SnakeDeathState(this));
    }
  }
}
