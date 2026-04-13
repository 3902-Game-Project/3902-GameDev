using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

internal interface IProjectile : ISprite {
  bool IsExpired { get; }

  void Expire();
  Rectangle BoundingBox { get; }

}
