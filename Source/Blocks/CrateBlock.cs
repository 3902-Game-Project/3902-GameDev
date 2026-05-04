using GameProject.Collisions;
using GameProject.Enemies;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class CrateBlock(Texture2D CrateTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static readonly Rectangle SOURCE_RECT = new(128, 0, 64, 64);
  
  private const float velocity = 120.0f;
  private Vector2 direction;

  protected void UpdateCollider() {
    if (Collider != null) {
      Collider.Position = Position + new Vector2(Collider.Width / 2f, Collider.Height / 2f);
    }
  }
  
  public CrateBlockState State { get; set; } = CrateBlockState.Still;

  public override void Update(double deltaTime) {
    if (State == CrateBlockState.Moving) {
      Position += velocity * direction * ((float) deltaTime);
      UpdateCollider();
      State = CrateBlockState.Still;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(CrateTexture, Position, SOURCE_RECT, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock || info.Collider is IEnemy) {
      State = CrateBlockState.Still;
      Position = CollisionHelper.GetNudgedPosition(info, Position, info.Overlap);
      UpdateCollider();
    } else if (State == CrateBlockState.Still && info.Collider is Player) {
      State = CrateBlockState.Moving;

      switch (info.Side) {
        case CollisionSide.Top:
          direction = new Vector2(0, 1);
          break;

        case CollisionSide.Bottom:
          direction = new Vector2(0, -1);
          break;

        case CollisionSide.Left:
          direction = new Vector2(1, 0);
          break;

        case CollisionSide.Right:
          direction = new Vector2(-1, 0);
          break;

        default:
          State = CrateBlockState.Still;
          break;
      }
    }
  }

  public void ChangeState(CrateBlockState state) {
    State = state;
  }
}
