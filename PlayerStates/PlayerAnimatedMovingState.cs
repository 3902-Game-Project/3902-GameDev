using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Interfaces;

namespace GameProject.PlayerStates {
  public class PlayerAnimatedMovingState : IPlayerState {
    private Player player;

    private Rectangle SpriteRight = new Rectangle(766, 62, 213, 316);
    private Rectangle SpriteLeft = new Rectangle(1528, 425, 180, 319);

    public PlayerAnimatedMovingState(Player player) {
      this.player = player;
    }

    public void Update(GameTime gameTime) {
      // If velocity is zero, go back to static/idle
      if (player.Velocity == Vector2.Zero) {
        player.State = new PlayerStaticState(player);
      }
    }

    public void UseItem() { }

    public void Draw(SpriteBatch spriteBatch) {
      Texture2D texture = player.game.GlobalVars.Assets.Textures.PlayerTexture;

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
          1f,
          SpriteEffects.None,
          0f
      );
    }
  }
}
