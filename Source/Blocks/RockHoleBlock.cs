using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class RockHoleBlock(Texture2D RockHoleTexture, Vector2 xyPos, string pairedLevelName, ILevelManager levelManager) : BaseBlock(xyPos, 64f, 64f) {
  private Rectangle sourceRect = new(384, 128, 64, 64);
  public string PairedLevelName { get; private set; } = pairedLevelName;

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(RockHoleTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
  public override void OnCollision(CollisionInfo info) {
    if (info.Collider is Player) {
      levelManager.SwitchLevel(PairedLevelName);
    }
  }
}
