using GameProject.Collisions;
using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Blocks;

internal enum BarrelBlockState {
  Solid,
  Broken,
}

internal enum FirePitBlockState {
  Lit,
  Extinguished,
}

internal enum CrateBlockState {
  Still,
  Moving,
}

internal enum LockableDoorBlockState {
  Locked,
  Open,
}

internal enum VaultDoorBlockState {
  Locked,
  Opening,
  Open,
}

internal interface IBlock : ITemporalUpdatable, IGPDrawable, ICollidable {
  Rectangle BoundingBox { get; }
}
