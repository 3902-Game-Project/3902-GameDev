using System;
using GameProject.Collisions;
using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

[Flags]
public enum Layer 
{
Environment = 0,
Player = 1,
Enemies = 2,
Projectiles = 4,
Pickups = 8 
}

public interface ICollidable {
  IShape Shape { get; }
  Vector2 Position { get; }
  Layer Layer { get; }
  Layer Mask { get; }

  void OnCollision(CollisionInfo collisionInfo);
}
