using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerStates;

public class PlayerStaticState : IPlayerState {
  private Player player;

  // Same rectangles as moving state
  private Rectangle SpriteRight = new(766, 62, 213, 316);
  private Rectangle SpriteLeft = new(1528, 425, 180, 319);

  public PlayerStaticState(Player player) {
    this.player = player;
  }

  public void Update(GameTime gameTime) {
    // If we start moving, switch state
    if (player.Velocity != Vector2.Zero) {
      player.State = new PlayerAnimatedMovingState(player);
    }
  }

  public void UseItem() { }

  public void Draw(SpriteBatch spriteBatch) {
    Texture2D texture = player.game.GlobalVars.Assets.Textures.PlayerTexture; //should make it global

    Rectangle sourceRect;
    Vector2 origin;

    // Check direction even when standing still
    if (player.Direction == FacingDirection.Right) {
      sourceRect = SpriteRight;
    } else {
      sourceRect = SpriteLeft;
    }

    origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
      texture,
      player.Position,
      sourceRect,
      Color.White,
      0f,
      origin,
      1f,
      SpriteEffects.None,
      0f
    );
  }
}
