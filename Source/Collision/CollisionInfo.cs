using GameProject.Interfaces;
using GameProject.Source.CollisionResponse;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;

public class CollisionInfo {
  public ICollidable Collider { get; }
  public CollisionSide Side { get; }
  public Vector2 Direction { get; }
}
