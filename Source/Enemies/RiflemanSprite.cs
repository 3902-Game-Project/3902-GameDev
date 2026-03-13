using GameProject.Interfaces;
using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

public class RiflemanSprite : BaseEnemy {
  private IRiflemanState state;

  public RiflemanSprite(Texture2D texture, Vector2 position, Game1 game) : base(texture, position, 48f, 96f) {
    state = new RifleWanderState(this, game);
  }

  public void ChangeState(IRiflemanState newState) {
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
    SpriteEffects effect = FacingDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
    Vector2 origin = new(source.Width / 2f, source.Height);

    spriteBatch.Draw(Texture, Position, source, Color.White, 0f, origin, 2f, effect, 0f);
  }

  public override void TakeDamage() { ChangeState(new RifleDeathState(this)); }
}
