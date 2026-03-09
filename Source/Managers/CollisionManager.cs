using System;
using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
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
        if (CheckCollison(c1, c2));
      }
    }
  }

  
  private bool CheckCollison(ICollidable c1, ICollidable c2) {
    if (c1.Shape.Type == ShapeType.Box && c2.Shape.Type == ShapeType.Box)
      return BoxBoxCollision(c1.Shape as BoxCollider, c2.Shape as BoxCollider);

    if (c1.Shape.Type == ShapeType.Box && c2.Shape.Type == ShapeType.Circle)
      return BoxCircleCollision(c1.Shape as BoxCollider, c2.Shape as CircleCollider);

    if (c1.Shape.Type == ShapeType.Circle && c2.Shape.Type == ShapeType.Box)
      return BoxCircleCollision(c2.Shape as BoxCollider, c1.Shape as CircleCollider);

    if (c1.Shape.Type == ShapeType.Circle && c2.Shape.Type == ShapeType.Circle)
      return CircleCircleCollision(c1.Shape as CircleCollider, c2.Shape as CircleCollider);
      
    return false;
  }

  private bool BoxBoxCollision(BoxCollider b1, BoxCollider b2) {
    return !(b1.Right < b2.Left ||
      b1.Left > b2.Right ||
      b1.Top < b2.Bottom ||
      b1.Bottom > b2.Top);
  }

  private bool BoxCircleCollision(BoxCollider b, CircleCollider c) {
    float closestX = Math.Clamp(c.position.X, b.Left, b.Left + b.width);
    float closestY = Math.Clamp(c.position.Y, b.Top, b.Top + b.height);

    float dx = c.position.X - closestX;
    float dy = c.position.Y - closestY;

    return (dx * dx + dy * dy) <= c.radius * c.radius;
  }

  private bool CircleCircleCollision(CircleCollider c1, CircleCollider c2) {
    float distance = MathF.Sqrt(MathF.Pow(c2.position.X - c1.position.X, 2) + MathF.Pow(c2.position.Y - c1.position.Y, 2));
    return distance <= c1.radius + c2.radius;
  }
}
