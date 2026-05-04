using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class FireBlock(Texture2D FireTexture, Vector2 xyPos, Player player) : ABaseBlock(xyPos) {
  private static readonly List<Rectangle> SOURCE_RECTS = [
    new(384, 64, 64, 64),
    new(448, 64, 64, 64),
  ];

  private static readonly double TIME_PER_FRAME = 0.15;
  private static readonly int DAMAGE = 25;

  private int currentFrame = 0;
  private double animationTimer = 0.0f;

  private readonly Player player = player;

  public override void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer >= TIME_PER_FRAME) {
      currentFrame = 1 - currentFrame;
      animationTimer -= TIME_PER_FRAME;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(FireTexture, Position, SOURCE_RECTS[currentFrame], Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (info.Collider is Player) {
      player.TakeDamage(DAMAGE);
    }
  }
}
