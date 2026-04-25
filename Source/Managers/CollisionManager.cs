using System;
using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Managers;

// 1. Add an enum to force the resolution axis
internal enum CollisionAxis {
  Both,
  X,
  Y,
}

internal class CollisionManager : IInstantaneousUpdatable {
  private readonly List<ICollidable> colliders;
  private Texture2D debugTexture;

  public CollisionManager() {
    colliders = [];
  }

  public void Add(ICollidable collider) {
    if (!colliders.Contains(collider)) {
      colliders.Add(collider);
    }
  }

  public void Remove(ICollidable collider) {
    colliders.Remove(collider);
  }

  public void Clear() {
    colliders.Clear();
  }

  public void DebugDraw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) {
    if (debugTexture == null) {
      debugTexture = new Texture2D(graphicsDevice, 1, 1);
      debugTexture.SetData([Color.White]);
    }

    foreach (var c in colliders) {
      if (c.Shape is BoxCollider box) {
        Rectangle rect = new((int) box.Left, (int) box.Top, (int) box.Width, (int) box.Height);
        spriteBatch.Draw(debugTexture, rect, Color.Red * 0.5f);
      }
    }
  }

  public void Update() {
    for (int i = 0; i < colliders.Count - 1; i++) {
      ICollidable c1 = colliders[i];
      for (int j = i + 1; j < colliders.Count; j++) {
        ICollidable c2 = colliders[j];
        // Global updates use "Both"
        if (CheckCollison(c1, c2, CollisionAxis.Both, out CollisionInfo info1, out CollisionInfo info2)) {
          info1.Collider = c2;
          info2.Collider = c1;

          c1.OnCollision(info1);
          c2.OnCollision(info2);
        }
      }
    }
  }

  public void ResolveCollisionsFor(ICollidable movingEntity, CollisionAxis axis = CollisionAxis.Both, float cornerTolerance = 3.0f) {
    foreach (var otherEntity in colliders) {
      if (movingEntity == otherEntity) continue;

      if (CheckCollison(movingEntity, otherEntity, axis, out CollisionInfo info1, out CollisionInfo info2, cornerTolerance)) {
        info1.Collider = otherEntity;
        info2.Collider = movingEntity;
        movingEntity.OnCollision(info1);
        otherEntity.OnCollision(info2);
      }
    }
  }

  private static bool CheckCollison(ICollidable c1, ICollidable c2, CollisionAxis axis, out CollisionInfo info1, out CollisionInfo info2, float cornerTolerance = 3.0f) {
    info1 = null;
    info2 = null;

    if (c1.Shape.Type == ShapeType.Box && c2.Shape.Type == ShapeType.Box)
      return BoxBoxCollision(c1.Shape as BoxCollider, c2.Shape as BoxCollider, axis, cornerTolerance, out info1, out info2);

    // Circle collisions don't need the tolerance � they resolve radially
    if (c1.Shape.Type == ShapeType.Box && c2.Shape.Type == ShapeType.Circle)
      return BoxCircleCollision(c1.Shape as BoxCollider, c2.Shape as CircleCollider, out info1, out info2);
    if (c1.Shape.Type == ShapeType.Circle && c2.Shape.Type == ShapeType.Box)
      return BoxCircleCollision(c2.Shape as BoxCollider, c1.Shape as CircleCollider, out info2, out info1);
    if (c1.Shape.Type == ShapeType.Circle && c2.Shape.Type == ShapeType.Circle)
      return CircleCircleCollision(c1.Shape as CircleCollider, c2.Shape as CircleCollider, out info1, out info2);

    return false;
  }

  private static bool BoxBoxCollision(BoxCollider b1, BoxCollider b2, CollisionAxis axis, float cornerTolerance, out CollisionInfo info1, out CollisionInfo info2) {
    info1 = null;
    info2 = null;

    if (b1.Right <= b2.Left || b1.Left >= b2.Right ||
        b1.Top >= b2.Bottom || b1.Bottom <= b2.Top) {
      return false;
    }

    float overlapX = MathF.Min(b1.Right - b2.Left, b2.Right - b1.Left);
    float overlapY = MathF.Min(b1.Bottom - b2.Top, b2.Bottom - b1.Top);

    if (axis == CollisionAxis.X && overlapY <= cornerTolerance) return false;
    if (axis == CollisionAxis.Y && overlapX <= cornerTolerance) return false;
    CollisionSide side1;
    CollisionSide side2;
    Vector2 direction1;
    Vector2 direction2;
    float finalOverlap;

    float b1CenterX = b1.Left + (b1.Width / 2f);
    float b2CenterX = b2.Left + (b2.Width / 2f);
    float b1CenterY = b1.Top + (b1.Height / 2f);
    float b2CenterY = b2.Top + (b2.Height / 2f);

    bool resolveHorizontally;
    if (axis == CollisionAxis.X) resolveHorizontally = true;
    else if (axis == CollisionAxis.Y) resolveHorizontally = false;
    else resolveHorizontally = overlapX < overlapY;

    if (resolveHorizontally) {
      finalOverlap = overlapX;
      if (b1CenterX < b2CenterX) {
        side1 = CollisionSide.Right; side2 = CollisionSide.Left;
        direction1 = new Vector2(-1, 0); direction2 = new Vector2(1, 0);
      } else {
        side1 = CollisionSide.Left; side2 = CollisionSide.Right;
        direction1 = new Vector2(1, 0); direction2 = new Vector2(-1, 0);
      }
    } else {
      finalOverlap = overlapY;
      if (b1CenterY < b2CenterY) {
        side1 = CollisionSide.Bottom; side2 = CollisionSide.Top;
        direction1 = new Vector2(0, -1); direction2 = new Vector2(0, 1);
      } else {
        side1 = CollisionSide.Top; side2 = CollisionSide.Bottom;
        direction1 = new Vector2(0, 1); direction2 = new Vector2(0, -1);
      }
    }

    info1 = new CollisionInfo { Side = side1, Direction = direction1, Overlap = finalOverlap };
    info2 = new CollisionInfo { Side = side2, Direction = direction2, Overlap = finalOverlap };

    return true;
  }

  private static bool BoxCircleCollision(BoxCollider b, CircleCollider c, out CollisionInfo info1, out CollisionInfo info2) {
    info1 = null; info2 = null;

    float closestX = Math.Clamp(c.Position.X, b.Left, b.Right);
    float closestY = Math.Clamp(c.Position.Y, b.Top, b.Bottom);

    float dx = c.Position.X - closestX;
    float dy = c.Position.Y - closestY;

    if ((dx * dx + dy * dy) > c.Radius * c.Radius) return false;

    Vector2 direction1 = new(c.Position.X - closestX, c.Position.Y - closestY);
    direction1 = Vector2.Normalize(direction1);
    Vector2 direction2 = Vector2.Negate(direction1);

    CollisionSide side1, side2;
    float magX = Math.Abs(direction1.X);
    float magY = Math.Abs(direction1.Y);

    if (magX > magY) {
      if (direction1.X > 0) { side1 = CollisionSide.Right; side2 = CollisionSide.Left; } else { side1 = CollisionSide.Left; side2 = CollisionSide.Right; }
    } else {
      if (direction1.Y > 0) { side1 = CollisionSide.Top; side2 = CollisionSide.Bottom; } else { side1 = CollisionSide.Bottom; side2 = CollisionSide.Top; }
    }

    info1 = new CollisionInfo { Side = side1, Direction = direction1 };
    info2 = new CollisionInfo { Side = side2, Direction = direction2 };
    return true;
  }

  private static bool CircleCircleCollision(CircleCollider c1, CircleCollider c2, out CollisionInfo info1, out CollisionInfo info2) {
    info1 = null; info2 = null;

    float dx = c2.Position.X - c1.Position.X;
    float dy = c2.Position.Y - c1.Position.Y;
    float r = c1.Radius + c2.Radius;
    float distSq = dx * dx + dy * dy;
    if (distSq > (r * r)) return false;

    Vector2 direction1 = c2.Position - c1.Position;
    direction1 = Vector2.Normalize(direction1);
    Vector2 direction2 = Vector2.Negate(direction1);

    CollisionSide side1, side2;
    float magX = Math.Abs(direction1.X);
    float magY = Math.Abs(direction1.Y);
    if (magX > magY) {
      if (direction1.X > 0) { side1 = CollisionSide.Right; side2 = CollisionSide.Left; } else { side1 = CollisionSide.Left; side2 = CollisionSide.Right; }
    } else {
      if (direction1.Y > 0) { side1 = CollisionSide.Top; side2 = CollisionSide.Bottom; } else { side1 = CollisionSide.Bottom; side2 = CollisionSide.Top; }
    }

    info1 = new CollisionInfo { Side = side1, Direction = direction1 };
    info2 = new CollisionInfo { Side = side2, Direction = direction2 };
    return true;
  }
}
