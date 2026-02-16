using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Interfaces;

namespace GameProject.PlayerStates {
  public class PlayerAnimatedMovingState : IPlayerState {
    private Player player;
    private List<Rectangle> frames;
    private int currentFrame;
    private double timer;
    private double fps = 10.0; 

    public PlayerAnimatedMovingState(Player player) {
      this.player = player;
      this.currentFrame = 0;
      this.timer = 0;

      frames = new List<Rectangle> {
                new Rectangle(76, 0, 24, 24),
                new Rectangle(108, 0, 26, 24)
            };
    }

    public void Update(GameTime gameTime) {
      // 1. Handle Animation Timing
      timer += gameTime.ElapsedGameTime.TotalSeconds;
      if (timer >= 1.0 / fps) {
        timer -= 1.0 / fps;
        currentFrame++;

        if (currentFrame >= frames.Count) {
          currentFrame = 0;
        }
      }

      if (player.Velocity == Vector2.Zero) {
        player.State = new PlayerStaticState(player);
      }
    }

    public void UseItem() {
    }

    public void Draw(SpriteBatch spriteBatch) {
      Texture2D texture = player.game.GlobalVars.Assets.Textures.MetroTexture;
      Rectangle sourceRect = frames[currentFrame];

      // Center the origin
      Vector2 origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

      spriteBatch.Draw(
          texture,
          player.Position,
          sourceRect,
          Color.White,
          0f,
          origin,
          2f,
          SpriteEffects.None, // Keeping your "no facing direction" rule
          0f
      );
    }
  }
}
