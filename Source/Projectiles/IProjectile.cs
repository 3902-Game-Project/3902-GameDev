using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Projectiles;

internal interface IProjectile : ISprite {
  internal bool IsExpired { get; }

  internal void Expire();

  internal Rectangle BoundingBox { get; }
}
