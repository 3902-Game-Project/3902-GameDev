using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Interfaces;

namespace GameProject.PlayerStates {
  public class PlayerStaticState : IPlayerState {
    private Player player;

    public PlayerStaticState(Player player) {
      this.player = player;
    }

    public void Update(GameTime gameTime) {
    }

    public void UseItem() {
    }

    public void Draw(SpriteBatch spriteBatch) {
      Texture2D texture = player.game.GlobalVars.Assets.Textures.MetroTexture;
      Rectangle sourceRect = new Rectangle(65, 2, 26, 24);

      Vector2 origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

      spriteBatch.Draw(
          texture: texture,
          position: player.Position,
          sourceRectangle: sourceRect,
          color: Color.White,
          rotation: 0f,
          origin: origin,
          scale: 2f,
          effects: SpriteEffects.None,
          layerDepth: 0f
      );
    }
  }
}
