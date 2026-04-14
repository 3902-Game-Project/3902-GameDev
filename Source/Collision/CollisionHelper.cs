using Microsoft.Xna.Framework;
using GameProject.Collisions;

namespace GameProject.Collisions;

internal static class CollisionHelper {
  internal static Vector2 GetNudgedPosition(CollisionInfo info, Vector2 currentPosition, float nudgeAmount) {
    return currentPosition + (info.Direction * nudgeAmount);
  }
}
