using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Globals;

namespace GameProject.Projectiles;

internal class BangProjectile : IProjectile {
  public bool IsExpired { get; private set; }
  private Vector2 position;
  private float lifetimeCounter = 0f;

  public Rectangle BoundingBox => new Rectangle((int) position.X, (int) position.Y, 1, 1);

  public BangProjectile(Vector2 startPosition) {
    this.position = startPosition;
  }

  public void Expire() => IsExpired = true;

  public void Update(double deltaTime) {
    lifetimeCounter += (float) deltaTime;
    if (lifetimeCounter >= 2f) Expire(); // Disappears after 2 seconds
  }

  public void Draw(SpriteBatch spriteBatch) {
    // Draw "BANG!" text
    spriteBatch.DrawString(MiscAssetStore.Instance.MainFont, "BANG!", position, Color.Red);
  }
}
