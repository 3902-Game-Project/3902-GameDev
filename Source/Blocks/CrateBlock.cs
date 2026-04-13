using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class CrateBlock(Texture2D CrateTexture, Vector2 xyPos) : BaseBlock(xyPos) {
  private Rectangle sourceRect = new(128, 0, 64, 64);
  private const float velocity = 2f;
  private Vector2 direction;
  public BlockState State { get; set; } = BlockState.still;

  public override void Update(GameTime gameTime) {
    if (State == BlockState.moving) {
      Position += velocity * direction;
      UpdateCollider();
      State = BlockState.still;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(CrateTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock || info.Collider is IEnemy) {
      State = BlockState.still;
    } else if (State == BlockState.still && info.Collider is Player) {
      State = BlockState.moving;

      switch (info.Side) {
        case CollisionResponse.CollisionSide.Top:
          direction = new Vector2(0, 1);
          break;

        case CollisionResponse.CollisionSide.Bottom:
          direction = new Vector2(0, -1);
          break;

        case CollisionResponse.CollisionSide.Left:
          direction = new Vector2(1, 0);
          break;

        case CollisionResponse.CollisionSide.Right:
          direction = new Vector2(-1, 0);
          break;

        default:
          State = BlockState.still;
          break;
      }
    }
  }

  protected void UpdateCollider() {
    if (Collider != null) {
      Collider.Position = Position + new Vector2(Collider.Width / 2f, Collider.Height / 2f);
    }
  }

  public void ChangeState(BlockState state) {
    State = state;
  }
}
