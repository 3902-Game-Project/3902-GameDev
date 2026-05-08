using System;
using GameProject.Collisions.Shapes;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;

[Flags]
internal enum Layer {
  None = 0,
  Environment = 1,
  Player = 2,
  Enemies = 4,
  Projectiles = 8,
  Pickups = 16,
}

internal interface ICollidable {
  internal IShape Shape { get; }
  internal Vector2 Position { get; }
  internal Layer Layer { get; }

  internal void OnCollision(CollisionInfo collisionInfo);
}
