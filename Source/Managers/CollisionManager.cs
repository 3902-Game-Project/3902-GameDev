using System;
using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.Source.CollisionResponse;
using Microsoft.Xna.Framework;

namespace GameProject.Managers;

public class CollisionManager {
  private List<ICollidable> colliders;

  public CollisionManager() {
    colliders = new List<ICollidable>();
  }

  public void Update(GameTime gameTime) {
    for (int i = 0; i < colliders.Count - 1; i++) {
      ICollidable c1 = colliders[i];
      for (int j = i + 1; j < colliders.Count; j++) {
        ICollidable c2 = colliders[j];
        CollisionInfo info1 = null, info2 = null;
        CheckCollison(c1, c2, out info1, out info2);
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
      return BoxCircleCollision(c2.Shape as BoxCollider, c1.Shape as CircleCollider, out info1, out info2);

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

    if (overlapX < overlapY)
    {
        // Horizontal collision
        if (b1.position.X < b2.position.X)
        {
            side1 = CollisionSide.Right;
            side2 = CollisionSide.Left;
        }
        else
        {
            side1 = CollisionSide.Left;
            side2 = CollisionSide.Right;
        }
    }
    else
    {
        // Vertical collision
        if (b1.position.Y < b2.position.Y)
        {
            side1 = CollisionSide.Bottom;
            side2 = CollisionSide.Top;
        }
        else
        {
            side1 = CollisionSide.Top;
            side2 = CollisionSide.Bottom;
        }
    }
    
    Vector2 direction1 = new Vector2(b2.position.X - b1.position.X, b2.position.Y - b1.position.Y);
    Vector2 direction2 = new Vector2(b1.position.X - b2.position.X, b1.position.Y - b2.position.Y);

    info1 = new CollisionInfo { Side = side1, Direction = direction1 };
    info2 = new CollisionInfo { Side = side2, Direction = direction2 };

    return true;
  }

  private bool BoxCircleCollision(BoxCollider b, CircleCollider c, out CollisionInfo info1, out CollisionInfo info2) {
    float closestX = Math.Clamp(c.position.X, b.Left, b.Left + b.width);
    float closestY = Math.Clamp(c.position.Y, b.Top, b.Top + b.height);

    float dx = c.position.X - closestX;
    float dy = c.position.Y - closestY;

    return (dx * dx + dy * dy) <= c.radius * c.radius;
  }

  private bool CircleCircleCollision(CircleCollider c1, CircleCollider c2, out CollisionInfo info1, out CollisionInfo info2) {
    float dx = c2.position.X - c1.position.X;
    float dy = c2.position.Y - c1.position.Y;
    float r = c1.radius + c2.radius;
    float distSq = dx * dx + dy * dy;
    return distSq <= (r * r);
  }
}
