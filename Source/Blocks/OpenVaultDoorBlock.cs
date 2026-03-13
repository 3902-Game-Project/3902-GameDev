using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class OpenVaultDoorBlock : BaseBlock {
  private Texture2D texture;
  private List<Rectangle> sourceRects;
  private int currentFrame;
  private double animationTimer;
  private double timePerFrame = 0.3;
  private ILevelManager levelManager;
  public string PairedLevelName { get; private set; }
  public BlockState State { get; set; }

  public OpenVaultDoorBlock(Texture2D OpenVaultDoorTexture, Vector2 xyPos, string pairedLevelName, ILevelManager levelManager) : base(xyPos, 128f, 128f) {
    texture = OpenVaultDoorTexture;
    currentFrame = 0;
    PairedLevelName = pairedLevelName;
    State = BlockState.lit;
    sourceRects = new List<Rectangle> {
      new Rectangle(64, 192, 64, 64),
      new Rectangle(128, 192, 64, 64),
      new Rectangle(192, 192, 64, 64),
      new Rectangle(256, 192, 64, 64),
      new Rectangle(320, 192, 64, 64)
    };
    this.levelManager = levelManager;
  }

  public override void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    animationTimer += dt;
    if (animationTimer >= timePerFrame) {
      currentFrame++;
      if (currentFrame == sourceRects.Count) { currentFrame = 0; }  // delete post sprint3
      animationTimer = 0;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRects[currentFrame], Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (info.Collider is Player) {
      levelManager.SwitchLevel(PairedLevelName);
    }
  }
}
