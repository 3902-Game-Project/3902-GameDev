using System;
using Microsoft.Xna.Framework;

namespace GameProject.Misc;

public static class VectorFuncs {
  public static float Angle(Vector2 vector) {
    return MathF.Atan2(vector.Y, vector.X);
  }
}
