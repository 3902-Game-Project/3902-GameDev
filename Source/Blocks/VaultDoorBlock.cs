using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class VaultDoorBlock : BaseBlock {
  private Texture2D texture;
  private List<Rectangle> sourceRects;
  private int currentFrame;
  private double animationTimer;
  private double timePerFrame = 0.3;
  private ILevelManager levelManager;
  public string PairedLevelName { get; private set; }
  public BlockState State { get; private set; }

  public VaultDoorBlock(Texture2D VaultDoorTexture, Vector2 xyPos, BlockState state, string pairedLevelName, ILevelManager levelManager) : base(xyPos, 128f, 128f) {
    texture = VaultDoorTexture;
    PairedLevelName = pairedLevelName;
    State = state;
    switch (state) {
      case BlockState.locked:
      default:
        currentFrame = 0;
        break;

      case BlockState.opening:
        currentFrame = 1;
        break;

      case BlockState.open:
        currentFrame = sourceRects.Count - 1;
        break;
    }
    sourceRects = new List<Rectangle> {
      new Rectangle(64, 128, 64, 64),
      new Rectangle(64, 192, 64, 64),
      new Rectangle(128, 192, 64, 64),
      new Rectangle(192, 192, 64, 64),
      new Rectangle(256, 192, 64, 64),
      new Rectangle(320, 192, 64, 64)
    };
    this.levelManager = levelManager;
  }

  public override void Update(GameTime gameTime) {
    if (State == BlockState.locked) {
      // check if player has key
    } else if (State == BlockState.opening) {
      float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
      animationTimer += dt;
      if (animationTimer >= timePerFrame) {
        currentFrame++;
        animationTimer = 0;
      }
      if (currentFrame >= sourceRects.Count) {
        State = BlockState.open;
        currentFrame = sourceRects.Count - 1;
      }
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRects[currentFrame], Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (State == BlockState.open && info.Collider is Player) {
      levelManager.SwitchLevel(PairedLevelName);
    }
  }

  // change state function?
}
