using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Source.CollisionResponse;
using Microsoft.Xna.Framework;

namespace GameProject.Source.Collision;

public static class CollisionDetector {
  public static CollisionSide GetCollisionSide(Rectangle rectA, Rectangle rectB) {
    if (!rectA.Intersects(rectB)) {
      return CollisionSide.None;
    }

    // Get the overlapping area
    Rectangle overlap = Rectangle.Intersect(rectA, rectB);

    // If width >= height, it's a Top/Bottom collision
    if (overlap.Width >= overlap.Height) {
      if (rectA.Center.Y < rectB.Center.Y) return CollisionSide.Bottom;
      else return CollisionSide.Top;
    }
    // If height > width, it's a Left/Right collision
    else {
      if (rectA.Center.X < rectB.Center.X) return CollisionSide.Right;
      else return CollisionSide.Left;
    }
  }
}
