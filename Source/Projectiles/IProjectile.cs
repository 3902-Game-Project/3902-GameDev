using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public interface IProjectile : ISprite {
  bool IsExpired { get; }

  void Expire();
  Rectangle BoundingBox { get; }

}
