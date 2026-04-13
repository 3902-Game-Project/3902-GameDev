using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

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

internal interface IBlock : ICollidable {
  public void Update(GameTime gameTime);
  public void Draw(SpriteBatch spriteBatch);

  Rectangle BoundingBox { get; }
}
