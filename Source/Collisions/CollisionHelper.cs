using Microsoft.Xna.Framework;

namespace GameProject.Collisions;

internal static class CollisionHelper {
  public static Vector2 GetNudgedPosition(CollisionInfo info, Vector2 currentPosition, float nudgeAmount) {
    return currentPosition + (info.Direction * nudgeAmount);
  }
}

// TODO: POSSBILY: Add more logic depending on rectangles or circles
