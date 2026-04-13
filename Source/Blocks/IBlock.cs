using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

public enum BarrelBlockState {
  Solid,
  Broken,
}

public enum FirePitBlockState {
  Lit,
  Extinguished,
}

public enum CrateBlockState {
  Still,
  Moving,
}

public enum LockableDoorBlockState {
  Locked,
  Open,
}

public enum VaultDoorBlockState {
  Locked,
  Opening,
  Open,
}

public interface IBlock : ICollidable {
  public void Update(GameTime gameTime);
  public void Draw(SpriteBatch spriteBatch);

  Rectangle BoundingBox { get; }
}
