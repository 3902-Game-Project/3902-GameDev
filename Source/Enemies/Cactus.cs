using GameProject.Enemies.CactusStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Cactus : ABaseEnemy {
  private ICactusState state;

  public Cactus(Texture2D texture, Vector2 position) : base(texture, position, 32f, 64f) {
    state = new CactusIdleState(this);
  }

  public void ChangeState(ICactusState newState) {
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

    spriteBatch.Draw(Texture, Position, source, tintColor, 0f, origin, 0.2f, effect, 0f);
  }

  public override void TakeDamage(int damage) {
    if (Health <= 0) {
      return;
    }
    //Now cactus is unkillable
    //Health -= damage;
  }
}
