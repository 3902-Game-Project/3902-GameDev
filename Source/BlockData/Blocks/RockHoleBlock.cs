using GameProject.Collisions;
using GameProject.Commands;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class RockHoleBlock(Texture2D RockHoleTexture, Vector2 xyPos, IGPCommand changeLevelCommand) : ABaseBlock(xyPos) {
  private static readonly Rectangle SOURCE_RECT = new(384, 128, 64, 64);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(RockHoleTexture, Position, SOURCE_RECT, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (info.Collider is Player) {
      changeLevelCommand.Execute();
    }
  }
}
