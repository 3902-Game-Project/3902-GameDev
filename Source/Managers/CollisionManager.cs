using System;
using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.Source.CollisionResponse;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Managers;

public class CollisionManager {
  private List<ICollidable> colliders;

  public CollisionManager() {
    colliders = new List<ICollidable>();
  }
  public void AddCollider(ICollidable collider) {
    if (!colliders.Contains(collider)) {
      colliders.Add(collider);
    }
  }
  public void Clear() {
    colliders.Clear();
  }

  //private Texture2D debugTexture;

  /*
  public void DebugDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice) {
    // Create a 1x1 white pixel if we haven't already
    if (debugTexture == null) {
      debugTexture = new Texture2D(graphicsDevice, 1, 1);
      debugTexture.SetData(new[] { Color.White });
    }

    // Draw a semi-transparent red box over EVERY collider in the manager
    foreach (var c in colliders) {
      if (c.Shape is BoxCollider box) {
        Rectangle rect = new Rectangle((int)box.Left, (int)box.Top, (int)box.width, (int)box.height);
        spriteBatch.Draw(debugTexture, rect, Color.Red * 0.5f); // 50% transparent red
      }
    }
  }
  */

  public void Update(GameTime gameTime) {
    for (int i = 0; i < colliders.Count - 1; i++) {
      ICollidable c1 = colliders[i];
      for (int j = i + 1; j < colliders.Count; j++) {
        ICollidable c2 = colliders[j];
        CollisionInfo info1, info2;
        if (CheckCollison(c1, c2, out info1, out info2)) {
          info1.Collider = c2;
          info2.Collider = c1;
          c1.OnCollision(info1);
          c2.OnCollision(info2);
        }
      }
    }
  }


  private bool CheckCollison(ICollidable c1, ICollidable c2, out CollisionInfo info1, out CollisionInfo info2) {
    info1 = null;
    info2 = null;

    if (c1.Shape.Type == ShapeType.Box && c2.Shape.Type == ShapeType.Box)
      return BoxBoxCollision(c1.Shape as BoxCollider, c2.Shape as BoxCollider, out info1, out info2);

    if (c1.Shape.Type == ShapeType.Box && c2.Shape.Type == ShapeType.Circle)
      return BoxCircleCollision(c1.Shape as BoxCollider, c2.Shape as CircleCollider, out info1, out info2);

    if (c1.Shape.Type == ShapeType.Circle && c2.Shape.Type == ShapeType.Box)
      return BoxCircleCollision(c2.Shape as BoxCollider, c1.Shape as CircleCollider, out info2, out info1);

    if (c1.Shape.Type == ShapeType.Circle && c2.Shape.Type == ShapeType.Circle)
      return CircleCircleCollision(c1.Shape as CircleCollider, c2.Shape as CircleCollider, out info1, out info2);

    return false;
  }

  private bool BoxBoxCollision(BoxCollider b1, BoxCollider b2, out CollisionInfo info1, out CollisionInfo info2) {
    info1 = null;
    info2 = null;
    if (b1.Right < b2.Left || b1.Left > b2.Right ||
      b1.Top < b2.Bottom || b1.Bottom > b2.Top) {
      return false;
    }
    float overlapX = MathF.Min(b1.Right - b2.Left, b2.Right - b1.Left);
    float overlapY = MathF.Min(b1.Top - b2.Bottom, b2.Top - b1.Bottom);

    CollisionSide side1;
    CollisionSide side2;
    Vector2 direction1;
    Vector2 direction2;

    if (overlapX < overlapY) {
      // Horizontal collision
      if (b1.position.X < b2.position.X) {
        side1 = CollisionSide.Right;
        side2 = CollisionSide.Left;
        direction1 = new Vector2(1, 0);
        direction2 = new Vector2(-1, 0);
      } else {
        side1 = CollisionSide.Left;
        side2 = CollisionSide.Right;
        direction1 = new Vector2(-1, 0);
        direction2 = new Vector2(1, 0);
      }
    } else {
      // Vertical collision
      if (b1.position.Y < b2.position.Y) {
        side1 = CollisionSide.Bottom;
        side2 = CollisionSide.Top;
        direction1 = new Vector2(0, 1);
        direction2 = new Vector2(0, -1);
      } else {
        side1 = CollisionSide.Top;
        side2 = CollisionSide.Bottom;
        direction1 = new Vector2(0, -1);
        direction2 = new Vector2(0, 1);
      }
    }

    info1 = new CollisionInfo { Side = side1, Direction = direction1 };
    info2 = new CollisionInfo { Side = side2, Direction = direction2 };

    return true;
  }

  private bool BoxCircleCollision(BoxCollider b, CircleCollider c, out CollisionInfo info1, out CollisionInfo info2) {
    info1 = null;
    info2 = null;

    float closestX = Math.Clamp(c.position.X, b.Left, b.Right);
    float closestY = Math.Clamp(c.position.Y, b.Top, b.Bottom);

    float dx = c.position.X - closestX;
    float dy = c.position.Y - closestY;

    if ((dx * dx + dy * dy) > c.radius * c.radius) return false;

    Vector2 direction1 = new Vector2(c.position.X - closestX, c.position.Y - closestY);
    direction1 = Vector2.Normalize(direction1);
    Vector2 direction2 = Vector2.Negate(direction1);

    CollisionSide side1, side2;

    float magX = Math.Abs(direction1.X);
    float magY = Math.Abs(direction1.Y);

    if (magX > magY) {
      // Horizontal dominant collision
      if (direction1.X > 0) {
        side1 = CollisionSide.Right;
        side2 = CollisionSide.Left;
      } else {
        side1 = CollisionSide.Left;
        side2 = CollisionSide.Right;
      }
    } else {
      // Vertical dominant collision
      if (direction1.Y > 0) {
        side1 = CollisionSide.Top;
        side2 = CollisionSide.Bottom;
      } else {
        side1 = CollisionSide.Bottom;
        side2 = CollisionSide.Top;
      }
    }

    info1 = new CollisionInfo { Side = side1, Direction = direction1 };
    info2 = new CollisionInfo { Side = side2, Direction = direction2 };

    return true;
  }

  private bool CircleCircleCollision(CircleCollider c1, CircleCollider c2, out CollisionInfo info1, out CollisionInfo info2) {
    info1 = null;
    info2 = null;

    float dx = c2.position.X - c1.position.X;
    float dy = c2.position.Y - c1.position.Y;
    float r = c1.radius + c2.radius;
    float distSq = dx * dx + dy * dy;
    if (distSq > (r * r)) return false;

    Vector2 direction1 = c2.position - c1.position;
    direction1 = Vector2.Normalize(direction1);
    Vector2 direction2 = Vector2.Negate(direction1);

    CollisionSide side1, side2;

    float magX = Math.Abs(direction1.X);
    float magY = Math.Abs(direction1.Y);
    if (magX > magY) {
      // Horizontal dominant collision
      if (direction1.X > 0) {
        side1 = CollisionSide.Right;
        side2 = CollisionSide.Left;
      } else {
        side1 = CollisionSide.Left;
        side2 = CollisionSide.Right;
      }
    } else {
      // Vertical dominant collision
      if (direction1.Y > 0) {
        side1 = CollisionSide.Top;
        side2 = CollisionSide.Bottom;
      } else {
        side1 = CollisionSide.Bottom;
        side2 = CollisionSide.Top;
      }
    }

    info1 = new CollisionInfo { Side = side1, Direction = direction1 };
    info2 = new CollisionInfo { Side = side2, Direction = direction2 };

    return true;
  }
}
