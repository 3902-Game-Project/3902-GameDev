using System;
using Microsoft.Xna.Framework;

namespace GameProject.HelperFuncs;

internal static class VectorFuncs {
  internal static float Angle(Vector2 vector) {
    return MathF.Atan2(vector.Y, vector.X);
  }
}
