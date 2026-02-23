using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerStates;

public class PlayerAnimatedMovingState(Player player) : IPlayerState {
  private Rectangle SpriteRight = new(766, 62, 213, 316);
  private Rectangle SpriteLeft = new(1528, 425, 180, 319);

  public void Update(GameTime gameTime) {
    // If velocity is zero, go back to static/idle
    if (player.Velocity == Vector2.Zero) {
      player.State = new PlayerStaticState(player);
    }
  }

  public void UseItem() { }

  public void Draw(SpriteBatch spriteBatch) {
    Texture2D texture = player.game.Assets.Textures.PlayerTexture;

    Rectangle sourceRect;
    Vector2 origin;

    // Choose the sprite based on the Player's persistent Direction
    if (player.Direction == FacingDirection.Right) {
      sourceRect = SpriteRight;
    } else {
      sourceRect = SpriteLeft;
    }

    // Center the origin so the sprite doesn't jump around weirdly
    origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
      texture,
      player.Position,
      sourceRect,
      Color.White,
      0f,
      origin,
      0.2f,
      SpriteEffects.None,
      0f
    );
  }
}
