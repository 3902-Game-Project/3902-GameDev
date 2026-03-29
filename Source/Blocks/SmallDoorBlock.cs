using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class SmallDoorBlock : BaseBlock {
  private Texture2D texture;
  private int currentFrame;
  private List<Rectangle> sourceRects;
  private ILevelManager levelManager;
  public float Rotation { get; private set; }
  public string PairedLevelName { get; private set; }
  public BlockState State { get; private set; }

  public SmallDoorBlock(Texture2D SmallDoorTexture, Vector2 xyPos, string pairedLevelName, ILevelManager levelManager) : base(xyPos) {
    texture = SmallDoorTexture;
    Rotation = 0.0f;
    currentFrame = 1; // CHANGE LATER TO 0 = LOCKED
    State = BlockState.locked;
    sourceRects = new List<Rectangle> {
      new Rectangle(448, 256, 64, 64),
      new Rectangle(448, 448, 64, 64)
    };
    PairedLevelName = pairedLevelName;
    this.levelManager = levelManager;
  }

  public void Rotate() {
    if (Position.X == 0 && Position.Y == 0) {
      Rotation = MathHelper.ToRadians(270);
    } else if (Position.X > 0 && Position.Y > 0) {
      Rotation = MathHelper.ToRadians(90);
    } else if (Position.Y > 0 && Position.X == 0) {
      Rotation = MathHelper.ToRadians(180);
    }
  }

  public override void Update(GameTime gameTime) { 
    // if all enemies defeated, change state to open
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRects[currentFrame], Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (State == BlockState.open && info.Collider is Player) {
      levelManager.SwitchLevel(PairedLevelName);
    }
  }
}
