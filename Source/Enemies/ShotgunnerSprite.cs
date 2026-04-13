using GameProject.Interfaces;
using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class ShotgunnerSprite : BaseEnemy {
  private IShotgunnerState state;

  public ShotgunnerSprite(Texture2D texture, Vector2 position, ILevelManager levelManager) : base(texture, position, 48f, 96f) {
    state = new ShotgunnerWanderState(this, levelManager);
  }

  public void ChangeState(IShotgunnerState newState) {
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
    SpriteEffects effect = FacingDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
    Vector2 origin = new(source.Width / 2f, source.Height);

    Color tintColor = DamageFlashTimer > 0 ? Color.Red : Color.White;

    spriteBatch.Draw(Texture, Position, source, tintColor, 0f, origin, 1.6f, effect, 0f);
  }

  public override void TakeDamage(int damage) {
    bool wasAlive = Health > 0;

    base.TakeDamage(damage);

    if (wasAlive && Health <= 0) {
      ChangeState(new ShotgunnerDeathState(this));
    }
  }
}
