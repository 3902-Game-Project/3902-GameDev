using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Level;
using System.Collections.Generic;

namespace GameProject.Projectiles;

internal class BossBomb : IProjectile {
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }

  private Texture2D flyingTexture;
  private Texture2D blinkingTexture;
  private int currentFrame = 0;
  private double animationTimer = 0;

  private List<Rectangle> flyingRectangles;
  private List<Rectangle> blinkingRectangles;

  private bool isLanded = false;
  private bool isExploding = false;
  private double fuseTimer = 0;
  private int damage;

  // --- IProjectile Interface Requirements ---
  public bool IsExpired { get; private set; } = false;

  public void Expire() {
    IsExpired = true;
  }

  // Gives the bomb a roughly 40x40 hitbox centered on its position
  public Rectangle BoundingBox => new Rectangle(
    (int) Position.X - 20,
    (int) Position.Y - 20,
    40,
    40
  );
  // ------------------------------------------

  public BossBomb(Texture2D flyingTex, Texture2D blinkingTex, Vector2 startPos, Vector2 direction, int damage) {
    this.flyingTexture = flyingTex;
    this.blinkingTexture = blinkingTex;
    this.Position = startPos;
    this.damage = damage;
    this.Velocity = direction * 150f;

    this.flyingRectangles = [
        new(527, 324, 31, 19),
        new(566, 324, 50, 19)
    ];

    this.blinkingRectangles = [
        // Blinking Frames (0 - 11)
        new(7, 39, 40, 52), new(65, 43, 40, 48), new(119, 41, 41, 50),
        new(175, 43, 41, 48), new(231, 41, 41, 50), new(287, 38, 41, 53),
        new(344, 37, 41, 54), new(401, 37, 40, 55), new(456, 43, 41, 48),
        new(513, 38, 40, 55), new(568, 40, 41, 53), new(624, 44, 41, 49),
        
        // Explosion Frames (12 - 20)
        new(29, 166, 56, 60), new(131, 158, 76, 73), new(234, 158, 98, 81),
        new(351, 146, 95, 97), new(457, 147, 101, 96), new(567, 143, 103, 105),
        new(15, 267, 82, 92), new(119, 260, 101, 100), new(235, 264, 93, 93)
    ];
  }

  public void Update(double deltaTime) {
    if (IsExpired) return;

    animationTimer += deltaTime;

    if (!isLanded) {
      // --- PHASE 1: FLYING ---
      Position += Velocity * (float) deltaTime;
      Velocity *= 0.92f;

      if (animationTimer > 0.15) {
        currentFrame = (currentFrame + 1) % flyingRectangles.Count;
        animationTimer = 0;
      }

      if (Velocity.Length() < 10f) {
        Velocity = Vector2.Zero;
        isLanded = true;
        currentFrame = 0;
        animationTimer = 0;
      }
    } else if (!isExploding) {
      // --- PHASE 2: BLINKING ON THE GROUND ---
      fuseTimer += deltaTime;

      // Loop ONLY the first 12 frames so it doesn't accidentally show the explosion early!
      if (animationTimer > 0.15) {
        currentFrame = (currentFrame + 1) % 12;
        animationTimer = 0;
      }

      // After 2 seconds, trigger the explosion
      if (fuseTimer > 2.0) {
        isExploding = true;
        currentFrame = 12; // Jump immediately to the first explosion frame!
        animationTimer = 0;
      }
    } else {
      // --- PHASE 3: EXPLOSION ---
      // Play the explosion fast (0.05 seconds per frame)
      if (animationTimer > 0.05) {
        currentFrame++;
        animationTimer = 0;

        // When we run out of explosion frames, the bomb is done!
        if (currentFrame >= blinkingRectangles.Count) {
          Explode();
        }
      }
    }
  }

  private void Explode() {
    // TODO: Damage the player/spawn explosion hitbox here!
    Expire(); // Tells the LevelManager to delete this object
  }

  public void Draw(SpriteBatch spriteBatch) {
    if (IsExpired) return;

    Texture2D currentTexture = isLanded ? blinkingTexture : flyingTexture;
    Rectangle sourceRect = isLanded ? blinkingRectangles[currentFrame] : flyingRectangles[currentFrame];

    Vector2 origin = new Vector2(sourceRect.Width / 2f, sourceRect.Height / 2f);

    spriteBatch.Draw(currentTexture, Position, sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0f);
  }
}
