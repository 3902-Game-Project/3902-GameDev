using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Projectiles;

internal interface IProjectile : ISprite {
  bool IsExpired { get; }

  void Expire();
  Rectangle BoundingBox { get; }
}
