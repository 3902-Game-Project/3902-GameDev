using GameProject.Controllers;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal class PlayerDeadState(Player player, Game1 game) : APlayerState(player) {
  private static readonly double LOSS_SCREEN_TIME = 3.0;
  private readonly GPTimer deadTimer = new();

  private Rectangle deadSprite = new(1470, 1060, 304, 97);
  public override void MoveUp() { }
  public override void MoveDown() { }
  public override void MoveLeft() { }
  public override void MoveRight() { }
  public override void UseItem(UseType useType) { }
  public override void UseKey(UseType useType) { }
  public override void Die() { }

  public override void Update(GameTime gameTime) {
    player.Velocity = Vector2.Zero;

    deadTimer.Update(gameTime);

    if (deadTimer.Time >= LOSS_SCREEN_TIME) {
      game.ChangeState(game.StateLoss);
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    Vector2 origin = new(deadSprite.Width / 2, deadSprite.Height / 2);

    spriteBatch.Draw(
      player.Texture,
      player.Position,
      deadSprite,
      Color.White,
      0f,
      origin,
      0.15f,
      SpriteEffects.None,
      0f
    );
  }
}
