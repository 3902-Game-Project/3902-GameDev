using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class BarrelBlock(Texture2D barrelTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static List<Rectangle> sourceRects = new List<Rectangle>{
    new(64, 0, 64, 64),
    new(64, 448, 64, 64)
  };
  private int currentFrame = 0;
  public BarrelBlockState State { get; set; } = BarrelBlockState.Solid;

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    // We use the inherited 'Position' variable here to draw the sprite
    // exactly where the physical collision box is!
    spriteBatch.Draw(
      texture: barrelTexture,
      position: Position,
      sourceRectangle: sourceRects[currentFrame],
      color: Color.White,
      rotation: 0.0f,
      origin: Vector2.Zero,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
  }

  public override void OnCollision(CollisionInfo info) {
    if (info.Collider is IProjectile && State == BarrelBlockState.Solid) {
      ChangeState(BarrelBlockState.Broken);
      currentFrame = 1;
      Collider = new BoxCollider(0, 0, new(0, 0));
    }
  }

  public void ChangeState(BarrelBlockState state) {
    State = state;
  }
}
